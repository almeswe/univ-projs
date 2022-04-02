using FTPClient.ServerApi;
using FTPLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPClient
{
    public partial class FTPClientForm : Form
    {
        private readonly string _root = "/";

        public FTPClientForm() =>
            this.InitializeComponent();

        private void FTPClientFormLoad(object sender, EventArgs e)
        {
            //this.Reload();
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
        
        private void ReloadNode(TreeNode node, bool expand = false)
        {
            var response = FTPServerApi.GetDirectory(
                this.GetWrapper(node)?.Entity.AbsolutePath);
            if (response.IsErrored)
                this.ShowError(response.ErrorMessage);
            else
            {
                node.Nodes.Clear();
                node.Nodes.AddRange(this.MakeNodesFromEntities(response.ResponseData as 
                    IEnumerable<FileSystemEntity>).ToArray());
                if (node.Tag != null)
                    this.SetLoaded(node);
                if (expand)
                    node.Expand();
            }
            this.TreeView.EndUpdate();
        }

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
    }
}
