
namespace FTPClient
{
    partial class FTPClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPClientForm));
            this.TreeView = new System.Windows.Forms.TreeView();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.DomainTextBox = new System.Windows.Forms.TextBox();
            this.DomainLabel = new System.Windows.Forms.Label();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.ReloadNodeButton = new System.Windows.Forms.Button();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.DomainPanel = new System.Windows.Forms.Panel();
            this.PasteButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.MoveButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.PutToFileButton = new System.Windows.Forms.Button();
            this.AppendToFileButton = new System.Windows.Forms.Button();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReloadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppendMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.DomainPanel.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeView
            // 
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.ImageIndex = 0;
            this.TreeView.ImageList = this.ImageList;
            this.TreeView.Indent = 10;
            this.TreeView.ItemHeight = 18;
            this.TreeView.Location = new System.Drawing.Point(0, 0);
            this.TreeView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TreeView.Name = "TreeView";
            this.TreeView.SelectedImageIndex = 0;
            this.TreeView.ShowLines = false;
            this.TreeView.ShowPlusMinus = false;
            this.TreeView.Size = new System.Drawing.Size(642, 686);
            this.TreeView.TabIndex = 0;
            this.TreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewNodeMouseClick);
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "file16.ico");
            this.ImageList.Images.SetKeyName(1, "folder16.ico");
            this.ImageList.Images.SetKeyName(2, "site16.ico");
            // 
            // DomainTextBox
            // 
            this.DomainTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DomainTextBox.Location = new System.Drawing.Point(71, 4);
            this.DomainTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DomainTextBox.Name = "DomainTextBox";
            this.DomainTextBox.Size = new System.Drawing.Size(261, 22);
            this.DomainTextBox.TabIndex = 1;
            this.DomainTextBox.Text = "http://localhost:5000/api/entities";
            // 
            // DomainLabel
            // 
            this.DomainLabel.AutoSize = true;
            this.DomainLabel.Location = new System.Drawing.Point(4, 9);
            this.DomainLabel.Name = "DomainLabel";
            this.DomainLabel.Size = new System.Drawing.Size(60, 17);
            this.DomainLabel.TabIndex = 2;
            this.DomainLabel.Text = "Domain:";
            // 
            // ReloadButton
            // 
            this.ReloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReloadButton.Location = new System.Drawing.Point(-1, 28);
            this.ReloadButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(336, 26);
            this.ReloadButton.TabIndex = 3;
            this.ReloadButton.Text = "connect";
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButtonClick);
            // 
            // ReloadNodeButton
            // 
            this.ReloadNodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReloadNodeButton.Location = new System.Drawing.Point(-1, 53);
            this.ReloadNodeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ReloadNodeButton.Name = "ReloadNodeButton";
            this.ReloadNodeButton.Size = new System.Drawing.Size(336, 26);
            this.ReloadNodeButton.TabIndex = 4;
            this.ReloadNodeButton.Text = "reload node";
            this.ReloadNodeButton.UseVisualStyleBackColor = true;
            this.ReloadNodeButton.Click += new System.EventHandler(this.ReloadNodeButtonClick);
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.TreeView);
            this.SplitContainer.Panel1MinSize = 250;
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.DomainPanel);
            this.SplitContainer.Panel2MinSize = 250;
            this.SplitContainer.Size = new System.Drawing.Size(983, 686);
            this.SplitContainer.SplitterDistance = 642;
            this.SplitContainer.SplitterWidth = 7;
            this.SplitContainer.TabIndex = 5;
            // 
            // DomainPanel
            // 
            this.DomainPanel.Controls.Add(this.PasteButton);
            this.DomainPanel.Controls.Add(this.DeleteButton);
            this.DomainPanel.Controls.Add(this.MoveButton);
            this.DomainPanel.Controls.Add(this.CopyButton);
            this.DomainPanel.Controls.Add(this.PutToFileButton);
            this.DomainPanel.Controls.Add(this.AppendToFileButton);
            this.DomainPanel.Controls.Add(this.OpenFileButton);
            this.DomainPanel.Controls.Add(this.DomainTextBox);
            this.DomainPanel.Controls.Add(this.ReloadButton);
            this.DomainPanel.Controls.Add(this.ReloadNodeButton);
            this.DomainPanel.Controls.Add(this.DomainLabel);
            this.DomainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DomainPanel.Location = new System.Drawing.Point(0, 0);
            this.DomainPanel.Margin = new System.Windows.Forms.Padding(4);
            this.DomainPanel.Name = "DomainPanel";
            this.DomainPanel.Size = new System.Drawing.Size(334, 686);
            this.DomainPanel.TabIndex = 5;
            // 
            // PasteButton
            // 
            this.PasteButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasteButton.Location = new System.Drawing.Point(0, 239);
            this.PasteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PasteButton.Name = "PasteButton";
            this.PasteButton.Size = new System.Drawing.Size(336, 26);
            this.PasteButton.TabIndex = 11;
            this.PasteButton.Text = "paste";
            this.PasteButton.UseVisualStyleBackColor = true;
            this.PasteButton.Click += new System.EventHandler(this.PasteButtonClick);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton.Location = new System.Drawing.Point(0, 263);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(336, 26);
            this.DeleteButton.TabIndex = 10;
            this.DeleteButton.Text = "delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButtonClick);
            // 
            // MoveButton
            // 
            this.MoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveButton.Location = new System.Drawing.Point(0, 190);
            this.MoveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(336, 26);
            this.MoveButton.TabIndex = 9;
            this.MoveButton.Text = "cut";
            this.MoveButton.UseVisualStyleBackColor = true;
            this.MoveButton.Click += new System.EventHandler(this.MoveButtonClick);
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyButton.Location = new System.Drawing.Point(0, 214);
            this.CopyButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(336, 26);
            this.CopyButton.TabIndex = 8;
            this.CopyButton.Text = "copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButtonClick);
            // 
            // PutToFileButton
            // 
            this.PutToFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PutToFileButton.Location = new System.Drawing.Point(-1, 96);
            this.PutToFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PutToFileButton.Name = "PutToFileButton";
            this.PutToFileButton.Size = new System.Drawing.Size(336, 26);
            this.PutToFileButton.TabIndex = 7;
            this.PutToFileButton.Text = "put";
            this.PutToFileButton.UseVisualStyleBackColor = true;
            this.PutToFileButton.Click += new System.EventHandler(this.PutToFileButtonClick);
            // 
            // AppendToFileButton
            // 
            this.AppendToFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AppendToFileButton.Location = new System.Drawing.Point(-1, 145);
            this.AppendToFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AppendToFileButton.Name = "AppendToFileButton";
            this.AppendToFileButton.Size = new System.Drawing.Size(336, 26);
            this.AppendToFileButton.TabIndex = 6;
            this.AppendToFileButton.Text = "append";
            this.AppendToFileButton.UseVisualStyleBackColor = true;
            this.AppendToFileButton.Click += new System.EventHandler(this.AppendToFileButtonClick);
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenFileButton.Location = new System.Drawing.Point(-1, 121);
            this.OpenFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(336, 26);
            this.OpenFileButton.TabIndex = 5;
            this.OpenFileButton.Text = "open";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButtonClick);
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.Size = new System.Drawing.Size(211, 24);
            this.OpenMenuItem.Text = "Open";
            // 
            // ReloadMenuItem
            // 
            this.ReloadMenuItem.Name = "ReloadMenuItem";
            this.ReloadMenuItem.Size = new System.Drawing.Size(211, 24);
            this.ReloadMenuItem.Text = "Reload";
            // 
            // PutMenuItem
            // 
            this.PutMenuItem.Name = "PutMenuItem";
            this.PutMenuItem.Size = new System.Drawing.Size(211, 24);
            this.PutMenuItem.Text = "Put Text";
            // 
            // AppendMenuItem
            // 
            this.AppendMenuItem.Name = "AppendMenuItem";
            this.AppendMenuItem.Size = new System.Drawing.Size(211, 24);
            this.AppendMenuItem.Text = "Append Text";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(211, 24);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.ReloadMenuItem,
            this.PutMenuItem,
            this.AppendMenuItem,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "ContextMenuStrip";
            this.contextMenuStrip1.Size = new System.Drawing.Size(212, 124);
            // 
            // FTPClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 686);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.SplitContainer);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FTPClientForm";
            this.Text = "FTPClient";
            this.Load += new System.EventHandler(this.FTPClientFormLoad);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.DomainPanel.ResumeLayout(false);
            this.DomainPanel.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.TextBox DomainTextBox;
        private System.Windows.Forms.Label DomainLabel;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.Button ReloadNodeButton;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.Panel DomainPanel;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.Button AppendToFileButton;
        private System.Windows.Forms.Button PutToFileButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button MoveButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReloadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AppendMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button PasteButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

