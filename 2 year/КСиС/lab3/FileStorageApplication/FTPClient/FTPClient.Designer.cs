
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
            this.SuspendLayout();
            // 
            // TreeView
            // 
            this.TreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TreeView.ImageIndex = 0;
            this.TreeView.ImageList = this.ImageList;
            this.TreeView.Location = new System.Drawing.Point(2, 2);
            this.TreeView.Name = "TreeView";
            this.TreeView.SelectedImageIndex = 0;
            this.TreeView.ShowPlusMinus = false;
            this.TreeView.Size = new System.Drawing.Size(621, 695);
            this.TreeView.TabIndex = 0;
            this.TreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewNodeMouseDoubleClick);
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "file.ico");
            this.ImageList.Images.SetKeyName(1, "ico5382.ico");
            // 
            // DomainTextBox
            // 
            this.DomainTextBox.Location = new System.Drawing.Point(690, 2);
            this.DomainTextBox.Name = "DomainTextBox";
            this.DomainTextBox.Size = new System.Drawing.Size(227, 22);
            this.DomainTextBox.TabIndex = 1;
            this.DomainTextBox.Text = "http://localhost:5000/api/entities";
            // 
            // DomainLabel
            // 
            this.DomainLabel.AutoSize = true;
            this.DomainLabel.Location = new System.Drawing.Point(629, 4);
            this.DomainLabel.Name = "DomainLabel";
            this.DomainLabel.Size = new System.Drawing.Size(60, 17);
            this.DomainLabel.TabIndex = 2;
            this.DomainLabel.Text = "Domain:";
            // 
            // ReloadButton
            // 
            this.ReloadButton.Location = new System.Drawing.Point(632, 30);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(285, 26);
            this.ReloadButton.TabIndex = 3;
            this.ReloadButton.Text = "reload";
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButtonClick);
            // 
            // ReloadNodeButton
            // 
            this.ReloadNodeButton.Location = new System.Drawing.Point(632, 62);
            this.ReloadNodeButton.Name = "ReloadNodeButton";
            this.ReloadNodeButton.Size = new System.Drawing.Size(285, 26);
            this.ReloadNodeButton.TabIndex = 4;
            this.ReloadNodeButton.Text = "reload node";
            this.ReloadNodeButton.UseVisualStyleBackColor = true;
            this.ReloadNodeButton.Click += new System.EventHandler(this.ReloadNodeButtonClick);
            // 
            // FTPClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 699);
            this.Controls.Add(this.ReloadNodeButton);
            this.Controls.Add(this.ReloadButton);
            this.Controls.Add(this.DomainLabel);
            this.Controls.Add(this.DomainTextBox);
            this.Controls.Add(this.TreeView);
            this.Name = "FTPClientForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FTPClientFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.TextBox DomainTextBox;
        private System.Windows.Forms.Label DomainLabel;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.Button ReloadNodeButton;
    }
}

