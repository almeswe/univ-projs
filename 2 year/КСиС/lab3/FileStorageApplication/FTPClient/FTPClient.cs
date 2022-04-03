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

        public FTPClientForm() =>
            this.InitializeComponent();

        private void FTPClientFormLoad(object sender, EventArgs e)
        {
            this.TreeView.Nodes.Add(this.MakeRootNode());
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
            this.ReloadNode(this.TreeView.Nodes[0]);
        }

        private async void ReloadNode(TreeNode node, bool expand = false)
        {
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
            }
            this.ResetLoadingIcon(node);
        }

        private async Task<string> LoadFileNode(TreeNode node)
        {
            var wrapper = this.GetWrapper(node);
            if (wrapper?.Entity.Type != FileSystemEntityType.File)
                return null;
            var response = await FTPServerApi.GetFile(
                wrapper.Entity.AbsolutePath);
            return this.ProcessResponseError(response) ? 
                response.ResponseData as string : null;
        }

        private async void PutToFileNode(TreeNode node)
        {
            if (this.TreeNodeIsFile(node))
            {
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
        }

        private async void AppendToFileNode(TreeNode node)
        {
            if (this.TreeNodeIsFile(node))
            {
                var result = new FTPClientTextInput("").ShowDialog();
                if (result == DialogResult.OK)
                {
                    var response = await FTPServerApi.AppendTextToFile(
                        this.GetWrapper(node).Entity.AbsolutePath,
                            FTPClientTextInput.ReturnedText);
                    this.ProcessResponseError(response);
                }
            }
        }

        private bool TreeNodeIsFile(TreeNode node) =>
            this.GetWrapper(node)?.Entity.Type == 
                FileSystemEntityType.File;

        private bool TreeNodeIsDirectory(TreeNode node) =>
            this.GetWrapper(node)?.Entity.Type ==
                FileSystemEntityType.Directory;

        private void ReloadButtonClick(object sender, EventArgs e) =>
            this.ReloadRoot();

        private string GetFileName(string path) =>
            System.IO.Path.GetFileName(path);

        private string GetDirectoryName(string path) =>
            new System.IO.DirectoryInfo(path).Name;

        private void ReloadNodeButtonClick(object sender, EventArgs e)
        {
            if (this.TreeView.SelectedNode != null)
                this.ReloadNode(this.TreeView.SelectedNode);
        }

        private bool ProcessResponseError(FTPServerApiResponse response)
        {
            if (response.IsErrored)
                this.ShowError(response.ErrorMessage);
            return !response.IsErrored;
        }

        private void TreeViewNodeMouseDoubleClick(object sender, 
            TreeNodeMouseClickEventArgs e)
        {
            var wrapper = this.GetWrapper(e.Node);
            if (wrapper == null)
                return;
            if (e.Button == MouseButtons.Left && !wrapper.IsLoaded)
                if (wrapper.Entity.Type == FileSystemEntityType.Directory)
                    this.ReloadNode(e.Node, true);
        }

        private async void OpenFileButtonClick(object sender, EventArgs e)
        {
            if (this.TreeView.SelectedNode == null)
                return;
            string contents = await this.LoadFileNode(
                this.TreeView.SelectedNode);
            if (contents != null)
                new FTPClientTextViewer(contents).Show();
        }

        private void AppendToFileButtonClick(object sender, EventArgs e)
        {
            if (this.TreeView.SelectedNode != null)
                this.AppendToFileNode(this.TreeView.SelectedNode);
        }

        private void PutToFileButtonClick(object sender, EventArgs e)
        {
            if (this.TreeView.SelectedNode != null)
                this.PutToFileNode(this.TreeView.SelectedNode);
        }
    }
}
