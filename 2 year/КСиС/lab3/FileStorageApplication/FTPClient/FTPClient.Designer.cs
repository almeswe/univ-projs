
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
            this.PutToFileButton = new System.Windows.Forms.Button();
            this.AppendToFileButton = new System.Windows.Forms.Button();
            this.OpenFileButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.DomainPanel.SuspendLayout();
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
            this.TreeView.Margin = new System.Windows.Forms.Padding(2);
            this.TreeView.Name = "TreeView";
            this.TreeView.SelectedImageIndex = 0;
            this.TreeView.ShowLines = false;
            this.TreeView.ShowPlusMinus = false;
            this.TreeView.Size = new System.Drawing.Size(482, 557);
            this.TreeView.TabIndex = 0;
            this.TreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewNodeMouseDoubleClick);
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
            this.DomainTextBox.Location = new System.Drawing.Point(53, 3);
            this.DomainTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DomainTextBox.Name = "DomainTextBox";
            this.DomainTextBox.Size = new System.Drawing.Size(196, 20);
            this.DomainTextBox.TabIndex = 1;
            this.DomainTextBox.Text = "http://localhost:5000/api/entities";
            // 
            // DomainLabel
            // 
            this.DomainLabel.AutoSize = true;
            this.DomainLabel.Location = new System.Drawing.Point(3, 7);
            this.DomainLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DomainLabel.Name = "DomainLabel";
            this.DomainLabel.Size = new System.Drawing.Size(46, 13);
            this.DomainLabel.TabIndex = 2;
            this.DomainLabel.Text = "Domain:";
            // 
            // ReloadButton
            // 
            this.ReloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReloadButton.Location = new System.Drawing.Point(-1, 23);
            this.ReloadButton.Margin = new System.Windows.Forms.Padding(2);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(251, 21);
            this.ReloadButton.TabIndex = 3;
            this.ReloadButton.Text = "reload root";
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButtonClick);
            // 
            // ReloadNodeButton
            // 
            this.ReloadNodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReloadNodeButton.Location = new System.Drawing.Point(-1, 43);
            this.ReloadNodeButton.Margin = new System.Windows.Forms.Padding(2);
            this.ReloadNodeButton.Name = "ReloadNodeButton";
            this.ReloadNodeButton.Size = new System.Drawing.Size(251, 21);
            this.ReloadNodeButton.TabIndex = 4;
            this.ReloadNodeButton.Text = "reload node";
            this.ReloadNodeButton.UseVisualStyleBackColor = true;
            this.ReloadNodeButton.Click += new System.EventHandler(this.ReloadNodeButtonClick);
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
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
            this.SplitContainer.Size = new System.Drawing.Size(737, 557);
            this.SplitContainer.SplitterDistance = 482;
            this.SplitContainer.SplitterWidth = 5;
            this.SplitContainer.TabIndex = 5;
            // 
            // DomainPanel
            // 
            this.DomainPanel.Controls.Add(this.PutToFileButton);
            this.DomainPanel.Controls.Add(this.AppendToFileButton);
            this.DomainPanel.Controls.Add(this.OpenFileButton);
            this.DomainPanel.Controls.Add(this.DomainTextBox);
            this.DomainPanel.Controls.Add(this.ReloadButton);
            this.DomainPanel.Controls.Add(this.ReloadNodeButton);
            this.DomainPanel.Controls.Add(this.DomainLabel);
            this.DomainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DomainPanel.Location = new System.Drawing.Point(0, 0);
            this.DomainPanel.Name = "DomainPanel";
            this.DomainPanel.Size = new System.Drawing.Size(250, 557);
            this.DomainPanel.TabIndex = 5;
            // 
            // PutToFileButton
            // 
            this.PutToFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PutToFileButton.Location = new System.Drawing.Point(-1, 78);
            this.PutToFileButton.Margin = new System.Windows.Forms.Padding(2);
            this.PutToFileButton.Name = "PutToFileButton";
            this.PutToFileButton.Size = new System.Drawing.Size(251, 21);
            this.PutToFileButton.TabIndex = 7;
            this.PutToFileButton.Text = "put";
            this.PutToFileButton.UseVisualStyleBackColor = true;
            this.PutToFileButton.Click += new System.EventHandler(this.PutToFileButtonClick);
            // 
            // AppendToFileButton
            // 
            this.AppendToFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AppendToFileButton.Location = new System.Drawing.Point(-1, 118);
            this.AppendToFileButton.Margin = new System.Windows.Forms.Padding(2);
            this.AppendToFileButton.Name = "AppendToFileButton";
            this.AppendToFileButton.Size = new System.Drawing.Size(251, 21);
            this.AppendToFileButton.TabIndex = 6;
            this.AppendToFileButton.Text = "append";
            this.AppendToFileButton.UseVisualStyleBackColor = true;
            this.AppendToFileButton.Click += new System.EventHandler(this.AppendToFileButtonClick);
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenFileButton.Location = new System.Drawing.Point(-1, 98);
            this.OpenFileButton.Margin = new System.Windows.Forms.Padding(2);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(251, 21);
            this.OpenFileButton.TabIndex = 5;
            this.OpenFileButton.Text = "open";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButtonClick);
            // 
            // FTPClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 557);
            this.Controls.Add(this.SplitContainer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FTPClientForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FTPClientFormLoad);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.DomainPanel.ResumeLayout(false);
            this.DomainPanel.PerformLayout();
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
    }
}

