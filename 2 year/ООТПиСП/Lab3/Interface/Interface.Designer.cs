
namespace Inferface
{
    partial class InterfaceForm
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
            this.TransportsListBox = new System.Windows.Forms.ListBox();
            this.TransportDumpTextBox = new System.Windows.Forms.TextBox();
            this.RewriteButton = new System.Windows.Forms.Button();
            this.WriteToFileButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ReadFromFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TransportsListBox
            // 
            this.TransportsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TransportsListBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TransportsListBox.FormattingEnabled = true;
            this.TransportsListBox.ItemHeight = 18;
            this.TransportsListBox.Location = new System.Drawing.Point(12, 12);
            this.TransportsListBox.Name = "TransportsListBox";
            this.TransportsListBox.Size = new System.Drawing.Size(223, 454);
            this.TransportsListBox.TabIndex = 0;
            this.TransportsListBox.SelectedIndexChanged += new System.EventHandler(this.TransportsSelectedIndexChanged);
            // 
            // TransportDumpTextBox
            // 
            this.TransportDumpTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TransportDumpTextBox.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransportDumpTextBox.Location = new System.Drawing.Point(241, 12);
            this.TransportDumpTextBox.Multiline = true;
            this.TransportDumpTextBox.Name = "TransportDumpTextBox";
            this.TransportDumpTextBox.Size = new System.Drawing.Size(588, 412);
            this.TransportDumpTextBox.TabIndex = 1;
            // 
            // RewriteButton
            // 
            this.RewriteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RewriteButton.Location = new System.Drawing.Point(241, 430);
            this.RewriteButton.Name = "RewriteButton";
            this.RewriteButton.Size = new System.Drawing.Size(147, 34);
            this.RewriteButton.TabIndex = 2;
            this.RewriteButton.Text = "REWRITE";
            this.RewriteButton.UseVisualStyleBackColor = true;
            this.RewriteButton.Click += new System.EventHandler(this.RewriteButtonClick);
            // 
            // WriteToFileButton
            // 
            this.WriteToFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.WriteToFileButton.Location = new System.Drawing.Point(392, 430);
            this.WriteToFileButton.Name = "WriteToFileButton";
            this.WriteToFileButton.Size = new System.Drawing.Size(210, 34);
            this.WriteToFileButton.TabIndex = 3;
            this.WriteToFileButton.Text = "WRITE TO FILE";
            this.WriteToFileButton.UseVisualStyleBackColor = true;
            this.WriteToFileButton.Click += new System.EventHandler(this.WriteToFileButtonClick);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // ReadFromFileButton
            // 
            this.ReadFromFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReadFromFileButton.Location = new System.Drawing.Point(608, 430);
            this.ReadFromFileButton.Name = "ReadFromFileButton";
            this.ReadFromFileButton.Size = new System.Drawing.Size(221, 34);
            this.ReadFromFileButton.TabIndex = 4;
            this.ReadFromFileButton.Text = "READ FROM FILE";
            this.ReadFromFileButton.UseVisualStyleBackColor = true;
            this.ReadFromFileButton.Click += new System.EventHandler(this.ReadFromFileButtonClick);
            // 
            // InterfaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 475);
            this.Controls.Add(this.ReadFromFileButton);
            this.Controls.Add(this.WriteToFileButton);
            this.Controls.Add(this.RewriteButton);
            this.Controls.Add(this.TransportDumpTextBox);
            this.Controls.Add(this.TransportsListBox);
            this.Name = "InterfaceForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.InterfaceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox TransportsListBox;
        private System.Windows.Forms.TextBox TransportDumpTextBox;
        private System.Windows.Forms.Button RewriteButton;
        private System.Windows.Forms.Button WriteToFileButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Button ReadFromFileButton;
    }
}

