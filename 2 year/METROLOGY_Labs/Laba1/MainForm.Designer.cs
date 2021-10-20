
namespace Laba1
{
    partial class MainForm
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
            this.BrowseFileButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.AnalyzeButton = new System.Windows.Forms.Button();
            this.IncreaseFontButton = new System.Windows.Forms.Button();
            this.DecreaseFontButton = new System.Windows.Forms.Button();
            this.SwitchThemeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BrowseFileButton
            // 
            this.BrowseFileButton.Location = new System.Drawing.Point(1, 3);
            this.BrowseFileButton.Name = "BrowseFileButton";
            this.BrowseFileButton.Size = new System.Drawing.Size(102, 23);
            this.BrowseFileButton.TabIndex = 1;
            this.BrowseFileButton.Text = "open file";
            this.BrowseFileButton.UseVisualStyleBackColor = true;
            this.BrowseFileButton.Click += new System.EventHandler(this.BrowseFileButtonClick);
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CodeTextBox.BackColor = System.Drawing.Color.White;
            this.CodeTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CodeTextBox.ForeColor = System.Drawing.Color.Black;
            this.CodeTextBox.Location = new System.Drawing.Point(0, 27);
            this.CodeTextBox.Multiline = true;
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.CodeTextBox.Size = new System.Drawing.Size(512, 494);
            this.CodeTextBox.TabIndex = 0;
            this.CodeTextBox.WordWrap = false;
            // 
            // AnalyzeButton
            // 
            this.AnalyzeButton.Location = new System.Drawing.Point(102, 3);
            this.AnalyzeButton.Name = "AnalyzeButton";
            this.AnalyzeButton.Size = new System.Drawing.Size(102, 23);
            this.AnalyzeButton.TabIndex = 2;
            this.AnalyzeButton.Text = "analyze";
            this.AnalyzeButton.UseVisualStyleBackColor = true;
            this.AnalyzeButton.Click += new System.EventHandler(this.AnalyzeButtonClick);
            // 
            // IncreaseFontButton
            // 
            this.IncreaseFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IncreaseFontButton.Location = new System.Drawing.Point(472, 2);
            this.IncreaseFontButton.Name = "IncreaseFontButton";
            this.IncreaseFontButton.Size = new System.Drawing.Size(20, 23);
            this.IncreaseFontButton.TabIndex = 4;
            this.IncreaseFontButton.Text = "+";
            this.IncreaseFontButton.UseVisualStyleBackColor = true;
            this.IncreaseFontButton.Click += new System.EventHandler(this.IncreaseFontButtonClick);
            // 
            // DecreaseFontButton
            // 
            this.DecreaseFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DecreaseFontButton.Location = new System.Drawing.Point(491, 2);
            this.DecreaseFontButton.Name = "DecreaseFontButton";
            this.DecreaseFontButton.Size = new System.Drawing.Size(20, 23);
            this.DecreaseFontButton.TabIndex = 5;
            this.DecreaseFontButton.Text = "-";
            this.DecreaseFontButton.UseVisualStyleBackColor = true;
            this.DecreaseFontButton.Click += new System.EventHandler(this.DecreaseFontButtonClick);
            // 
            // SwitchThemeButton
            // 
            this.SwitchThemeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SwitchThemeButton.Location = new System.Drawing.Point(453, 2);
            this.SwitchThemeButton.Name = "SwitchThemeButton";
            this.SwitchThemeButton.Size = new System.Drawing.Size(20, 23);
            this.SwitchThemeButton.TabIndex = 6;
            this.SwitchThemeButton.Text = "t";
            this.SwitchThemeButton.UseVisualStyleBackColor = true;
            this.SwitchThemeButton.Click += new System.EventHandler(this.SwitchThemeButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 521);
            this.Controls.Add(this.SwitchThemeButton);
            this.Controls.Add(this.DecreaseFontButton);
            this.Controls.Add(this.IncreaseFontButton);
            this.Controls.Add(this.AnalyzeButton);
            this.Controls.Add(this.BrowseFileButton);
            this.Controls.Add(this.CodeTextBox);
            this.Name = "MainForm";
            this.Text = "Analyzer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BrowseFileButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.TextBox CodeTextBox;
        private System.Windows.Forms.Button AnalyzeButton;
        private System.Windows.Forms.Button IncreaseFontButton;
        private System.Windows.Forms.Button DecreaseFontButton;
        private System.Windows.Forms.Button SwitchThemeButton;
    }
}

