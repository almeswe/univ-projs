using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

using FTPLayer.Entity;
using FTPClient.ServerApi;

namespace FTPClient
{
    public partial class FTPClientForm : Form
    {
        private readonly string _root = "/";
        private Tuple<TreeNode, BufferActionKind> _buffered = null;

        public FTPClientForm() =>
            this.InitializeComponent();

        private void FTPClientFormLoad(object sender, EventArgs e)
        {
            this.TreeView.Nodes.Add(this.MakeRootNode());
            this.CheckPasteButtonEnabled();
        }

        private async void CheckPasteButtonEnabled()
        {
            while (!this.IsDisposed)
            {
                this.PasteButton.Enabled = this._buffered != null && 
                    this.TreeNodeIsDirectory(this.TreeView.SelectedNode);
                await Task.Delay(200);
            }
        }

        private TreeNode MakeRootNode() =>
            new TreeNode(this._root, 1, 1);

        private TreeNode MakeNodeFromEntity(FileSystemEntity entity)
        {
            var nodeText = entity.Type == FileSystemEntityType.File ?
                this.GetFileName(entity.AbsolutePath) :
                    this.GetDirectoryName(entity.AbsolutePath);
            var nodeImageIndex = (int)entity.Type;
            return new TreeNode(nodeText, nodeImageIndex, nodeImageIndex) {
                Tag = new TreeNodeWrapper(entity)
            };
        }

        private IEnumerable<TreeNode> MakeNodesFromEntities(
            IEnumerable<FileSystemEntity> entities)
        {
            foreach (var entity in entities)
                yield return this.MakeNodeFromEntity(entity);
        }

        private void SetLoaded(TreeNode node, bool isLoaded = true) =>
            ((TreeNodeWrapper)node.Tag).IsLoaded = isLoaded;

        private void SetLoadingIcon(TreeNode node) =>
            node.SelectedImageIndex = 2;

        private void ResetLoadingIcon(TreeNode node) =>
            node.SelectedImageIndex = 1;

        private TreeNodeWrapper GetWrapper(TreeNode node) =>
            ((TreeNodeWrapper)node.Tag);

        private void ShowError(string message) =>
            MessageBox.Show(message, "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void ReloadRoot()
        {
            FTPServerApi.ApiBaseUrl = this.DomainTextBox.Text;
            this.TreeView.Nodes.Clear();
            this.TreeView.Nodes.Add(this.MakeRootNode());
            this.ReloadNode(this.TreeView.Nodes[0], true);
        }

        private async void ReloadNode(TreeNode node, 
            bool expandAfter = false)
        {
            if (node == null)
                return;
            this.SetLoadingIcon(node);
            var response = await FTPServerApi.GetDirectory(
                this.GetWrapper(node)?.Entity.AbsolutePath);
            if (this.ProcessResponseError(response))
            {
                node.Nodes.Clear();
                this.TreeView.BeginUpdate();
                node.Nodes.AddRange(this.MakeNodesFromEntities(response.ResponseData as
                    IEnumerable<FileSystemEntity>).ToArray());
                if (node.Tag != null)
                    this.SetLoaded(node);
                this.TreeView.EndUpdate();
                if (expandAfter)
                    node.Expand();
            }
            this.ResetLoadingIcon(node);
        }

        private async Task<string> LoadFileNode(TreeNode node)
        {
            var wrapper = this.GetWrapper(node);
            if (!this.TreeNodeIsFile(node))
                return null;
            var response = await FTPServerApi.GetFile(
                wrapper.Entity.AbsolutePath);
            return this.ProcessResponseError(response) ? 
                response.ResponseData as string : null;
        }

        private async void PutToFileNode(TreeNode node)
        {
            if (!this.TreeNodeIsFile(node))
                return;
            var contents = await this.LoadFileNode(node);
            if (contents == null)
                return;
            var result = new FTPClientTextInput(
                contents).ShowDialog();
            if (result == DialogResult.OK)
            {
                var response = await FTPServerApi.PutTextToFile(
                    this.GetWrapper(node).Entity.AbsolutePath,
                        FTPClientTextInput.ReturnedText);
                this.ProcessResponseError(response);
            }
        }

        private async void AppendToFileNode(TreeNode node)
        {
            if (!this.TreeNodeIsFile(node))
                return;
            var result = new FTPClientTextInput("").ShowDialog();
            if (result == DialogResult.OK)
            {
                var response = await FTPServerApi.AppendTextToFile(
                    this.GetWrapper(node).Entity.AbsolutePath,
                        FTPClientTextInput.ReturnedText);
                this.ProcessResponseError(response);
            }
        }

        private async void DeleteFileNode(TreeNode node)
        {
            if (!this.TreeNodeIsFile(node))
                return;
            var response = await FTPServerApi.DeleteFile(
                this.GetWrapper(node).Entity.AbsolutePath);
            if (this.ProcessResponseError(response))
                this.ReloadNode(node.Parent, true);
        }

        private delegate Task<FTPServerApiResponse> 
            BufferAction(string source, string destination);

        private void BufferFileNode(
            TreeNode node, BufferActionKind kind)
        {
            if (node == null)
                return;
            var wrapper = this.GetWrapper(node);
            if (wrapper == null || wrapper.Entity.Type
                    != FileSystemEntityType.File)
                return;
            this._buffered = Tuple.Create(node, kind);
        }

        private async void BufferActionPerform(
            TreeNode toNode, BufferAction action)
        {
            var toNodeWrapper = this.GetWrapper(toNode);
            var bufferedNodeWrapper = this.GetWrapper(
                this._buffered.Item1);
            var sourceParent = this._buffered.Item1.Parent;
            var destinationParent = this.TreeView.SelectedNode?.Parent;
            string source = bufferedNodeWrapper.Entity.AbsolutePath;
            string destination = toNodeWrapper.Entity.AbsolutePath;
            var response = await action(source, System.IO.
                Path.Combine(destination, this.GetFileName(source)));
            if (this.ProcessResponseError(response))
            {
                this.ReloadNode(sourceParent, true);
                this.ReloadNode(destinationParent, true);
            }
        }

        private void PasteBufferedFileNode()
        {
            if (this.TreeView.SelectedNode == null ||
                    this._buffered == null)
                return;
            switch (this._buffered.Item2)
            {
                case BufferActionKind.Cut:
                    this.BufferActionPerform(this.TreeView.SelectedNode,
                        FTPServerApi.MoveFileTo);
                    break;
                case BufferActionKind.Copy:
                    this.BufferActionPerform(this.TreeView.SelectedNode,
                        FTPServerApi.CopyFileTo);
                    break;
            }
            this._buffered = null;
        }

        private bool TreeNodeIsFile(TreeNode node) =>
            this.GetWrapper(node)?.Entity.Type == 
                FileSystemEntityType.File;

        private bool TreeNodeIsDirectory(TreeNode node) =>
            this.GetWrapper(node)?.Entity.Type ==
                FileSystemEntityType.Directory;

        private string GetFileName(string path) =>
            System.IO.Path.GetFileName(path);

        private string GetDirectoryName(string path) =>
            new System.IO.DirectoryInfo(path).Name;

        private bool ProcessResponseError(FTPServerApiResponse response)
        {
            if (response.IsErrored)
                this.ShowError(response.ErrorMessage);
            return !response.IsErrored;
        }

        private void ReloadButtonClick(object sender, EventArgs e) =>
            this.ReloadRoot();

        private void ReloadNodeButtonClick(object sender, EventArgs e) =>
            this.ReloadNode(this.TreeView.SelectedNode);

        private async void OpenFileButtonClick(object sender, EventArgs e)
        {
            if (this.TreeView.SelectedNode == null)
                return;
            string contents = await this.LoadFileNode(
                this.TreeView.SelectedNode);
            if (contents != null)
                new FTPClientTextViewer(contents).Show();
        }

        private void AppendToFileButtonClick(object sender, EventArgs e) =>
            this.AppendToFileNode(this.TreeView.SelectedNode);

        private void PutToFileButtonClick(object sender, EventArgs e) =>
            this.PutToFileNode(this.TreeView.SelectedNode);

        private void CopyButtonClick(object sender, EventArgs e) =>
            this.BufferFileNode(this.TreeView.SelectedNode,
                BufferActionKind.Copy);

        private void MoveButtonClick(object sender, EventArgs e) =>
            this.BufferFileNode(this.TreeView.SelectedNode,
                BufferActionKind.Cut);

        private void DeleteButtonClick(object sender, EventArgs e) =>
            this.DeleteFileNode(this.TreeView.SelectedNode);

        private void PasteButtonClick(object sender, EventArgs e) =>
            this.PasteBufferedFileNode();

        private void TreeViewNodeMouseClick(object sender, 
            TreeNodeMouseClickEventArgs e)
        {
            var wrapper = this.GetWrapper(e.Node);
            if (wrapper == null)
                return;
            if (e.Button == MouseButtons.Left && !wrapper.IsLoaded)
                if (wrapper.Entity.Type == FileSystemEntityType.Directory)
                    this.ReloadNode(e.Node);
        }
    }
}