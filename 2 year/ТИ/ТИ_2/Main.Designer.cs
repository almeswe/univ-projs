
namespace ТИ_2
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
            this.SeedTextBox = new System.Windows.Forms.TextBox();
            this.AllOnesSeedButton = new System.Windows.Forms.Button();
            this.RandomSeedButton = new System.Windows.Forms.Button();
            this.SeedLabel = new System.Windows.Forms.Label();
            this.ViewSeedButton = new System.Windows.Forms.Button();
            this.ViewKeyButton = new System.Windows.Forms.Button();
            this.KetLabel = new System.Windows.Forms.Label();
            this.GenerateKeyButton = new System.Windows.Forms.Button();
            this.KeyTextBox = new System.Windows.Forms.TextBox();
            this.ViewInputFileButton = new System.Windows.Forms.Button();
            this.InputFileLabel = new System.Windows.Forms.Label();
            this.InputFileTextBox = new System.Windows.Forms.TextBox();
            this.SearchFileButton = new System.Windows.Forms.Button();
            this.ViewOutputFileButton = new System.Windows.Forms.Button();
            this.OutputFileLabel = new System.Windows.Forms.Label();
            this.OutputFileTextBox = new System.Windows.Forms.TextBox();
            this.EncryptButton = new System.Windows.Forms.Button();
            this.DecryptButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // SeedTextBox
            // 
            this.SeedTextBox.Location = new System.Drawing.Point(56, 13);
            this.SeedTextBox.Name = "SeedTextBox";
            this.SeedTextBox.Size = new System.Drawing.Size(292, 22);
            this.SeedTextBox.TabIndex = 0;
            // 
            // AllOnesSeedButton
            // 
            this.AllOnesSeedButton.Location = new System.Drawing.Point(13, 40);
            this.AllOnesSeedButton.Name = "AllOnesSeedButton";
            this.AllOnesSeedButton.Size = new System.Drawing.Size(166, 27);
            this.AllOnesSeedButton.TabIndex = 1;
            this.AllOnesSeedButton.Text = "All 1\'s";
            this.AllOnesSeedButton.UseVisualStyleBackColor = true;
            this.AllOnesSeedButton.Click += new System.EventHandler(this.AllOnesSeedButtonClick);
            // 
            // RandomSeedButton
            // 
            this.RandomSeedButton.Location = new System.Drawing.Point(182, 40);
            this.RandomSeedButton.Name = "RandomSeedButton";
            this.RandomSeedButton.Size = new System.Drawing.Size(204, 27);
            this.RandomSeedButton.TabIndex = 2;
            this.RandomSeedButton.Text = "Random";
            this.RandomSeedButton.UseVisualStyleBackColor = true;
            this.RandomSeedButton.Click += new System.EventHandler(this.RandomSeedButtonClick);
            // 
            // SeedLabel
            // 
            this.SeedLabel.AutoSize = true;
            this.SeedLabel.Location = new System.Drawing.Point(11, 16);
            this.SeedLabel.Name = "SeedLabel";
            this.SeedLabel.Size = new System.Drawing.Size(45, 17);
            this.SeedLabel.TabIndex = 3;
            this.SeedLabel.Text = "Seed:";
            // 
            // ViewSeedButton
            // 
            this.ViewSeedButton.Location = new System.Drawing.Point(351, 11);
            this.ViewSeedButton.Name = "ViewSeedButton";
            this.ViewSeedButton.Size = new System.Drawing.Size(35, 28);
            this.ViewSeedButton.TabIndex = 5;
            this.ViewSeedButton.Text = "...";
            this.ViewSeedButton.UseVisualStyleBackColor = true;
            this.ViewSeedButton.Click += new System.EventHandler(this.ViewSeedButton_Click);
            // 
            // ViewKeyButton
            // 
            this.ViewKeyButton.Location = new System.Drawing.Point(351, 221);
            this.ViewKeyButton.Name = "ViewKeyButton";
            this.ViewKeyButton.Size = new System.Drawing.Size(35, 28);
            this.ViewKeyButton.TabIndex = 10;
            this.ViewKeyButton.Text = "...";
            this.ViewKeyButton.UseVisualStyleBackColor = true;
            this.ViewKeyButton.Click += new System.EventHandler(this.ViewKeyButton_Click);
            // 
            // KetLabel
            // 
            this.KetLabel.AutoSize = true;
            this.KetLabel.Location = new System.Drawing.Point(11, 226);
            this.KetLabel.Name = "KetLabel";
            this.KetLabel.Size = new System.Drawing.Size(36, 17);
            this.KetLabel.TabIndex = 9;
            this.KetLabel.Text = "Key:";
            // 
            // GenerateKeyButton
            // 
            this.GenerateKeyButton.Location = new System.Drawing.Point(13, 250);
            this.GenerateKeyButton.Name = "GenerateKeyButton";
            this.GenerateKeyButton.Size = new System.Drawing.Size(373, 27);
            this.GenerateKeyButton.TabIndex = 7;
            this.GenerateKeyButton.Text = "Generate Key";
            this.GenerateKeyButton.UseVisualStyleBackColor = true;
            this.GenerateKeyButton.Click += new System.EventHandler(this.GenerateKeyButton_Click);
            // 
            // KeyTextBox
            // 
            this.KeyTextBox.Location = new System.Drawing.Point(56, 223);
            this.KeyTextBox.Name = "KeyTextBox";
            this.KeyTextBox.ReadOnly = true;
            this.KeyTextBox.Size = new System.Drawing.Size(292, 22);
            this.KeyTextBox.TabIndex = 6;
            // 
            // ViewInputFileButton
            // 
            this.ViewInputFileButton.Location = new System.Drawing.Point(351, 98);
            this.ViewInputFileButton.Name = "ViewInputFileButton";
            this.ViewInputFileButton.Size = new System.Drawing.Size(35, 28);
            this.ViewInputFileButton.TabIndex = 15;
            this.ViewInputFileButton.Text = "...";
            this.ViewInputFileButton.UseVisualStyleBackColor = true;
            this.ViewInputFileButton.Click += new System.EventHandler(this.ViewInputFileButton_Click);
            // 
            // InputFileLabel
            // 
            this.InputFileLabel.AutoSize = true;
            this.InputFileLabel.Location = new System.Drawing.Point(11, 103);
            this.InputFileLabel.Name = "InputFileLabel";
            this.InputFileLabel.Size = new System.Drawing.Size(43, 17);
            this.InputFileLabel.TabIndex = 14;
            this.InputFileLabel.Text = "Input:";
            // 
            // InputFileTextBox
            // 
            this.InputFileTextBox.Location = new System.Drawing.Point(72, 99);
            this.InputFileTextBox.Name = "InputFileTextBox";
            this.InputFileTextBox.Size = new System.Drawing.Size(200, 22);
            this.InputFileTextBox.TabIndex = 11;
            // 
            // SearchFileButton
            // 
            this.SearchFileButton.Location = new System.Drawing.Point(278, 98);
            this.SearchFileButton.Name = "SearchFileButton";
            this.SearchFileButton.Size = new System.Drawing.Size(70, 28);
            this.SearchFileButton.TabIndex = 16;
            this.SearchFileButton.Text = "Search";
            this.SearchFileButton.UseVisualStyleBackColor = true;
            this.SearchFileButton.Click += new System.EventHandler(this.SearchFileButtonClick);
            // 
            // ViewOutputFileButton
            // 
            this.ViewOutputFileButton.Location = new System.Drawing.Point(351, 132);
            this.ViewOutputFileButton.Name = "ViewOutputFileButton";
            this.ViewOutputFileButton.Size = new System.Drawing.Size(35, 28);
            this.ViewOutputFileButton.TabIndex = 19;
            this.ViewOutputFileButton.Text = "...";
            this.ViewOutputFileButton.UseVisualStyleBackColor = true;
            this.ViewOutputFileButton.Click += new System.EventHandler(this.ViewOutputFileButtonClick);
            // 
            // OutputFileLabel
            // 
            this.OutputFileLabel.AutoSize = true;
            this.OutputFileLabel.Location = new System.Drawing.Point(11, 137);
            this.OutputFileLabel.Name = "OutputFileLabel";
            this.OutputFileLabel.Size = new System.Drawing.Size(55, 17);
            this.OutputFileLabel.TabIndex = 18;
            this.OutputFileLabel.Text = "Output:";
            // 
            // OutputFileTextBox
            // 
            this.OutputFileTextBox.Location = new System.Drawing.Point(72, 134);
            this.OutputFileTextBox.Name = "OutputFileTextBox";
            this.OutputFileTextBox.Size = new System.Drawing.Size(276, 22);
            this.OutputFileTextBox.TabIndex = 17;
            // 
            // EncryptButton
            // 
            this.EncryptButton.Location = new System.Drawing.Point(14, 309);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(182, 36);
            this.EncryptButton.TabIndex = 21;
            this.EncryptButton.Text = "Encrypt";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButtonClick);
            // 
            // DecryptButton
            // 
            this.DecryptButton.Location = new System.Drawing.Point(202, 309);
            this.DecryptButton.Name = "DecryptButton";
            this.DecryptButton.Size = new System.Drawing.Size(182, 36);
            this.DecryptButton.TabIndex = 20;
            this.DecryptButton.Text = "Decrypt";
            this.DecryptButton.UseVisualStyleBackColor = true;
            this.DecryptButton.Click += new System.EventHandler(this.DecryptButtonClick);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(398, 350);
            this.Controls.Add(this.EncryptButton);
            this.Controls.Add(this.DecryptButton);
            this.Controls.Add(this.ViewOutputFileButton);
            this.Controls.Add(this.OutputFileLabel);
            this.Controls.Add(this.OutputFileTextBox);
            this.Controls.Add(this.SearchFileButton);
            this.Controls.Add(this.ViewInputFileButton);
            this.Controls.Add(this.InputFileLabel);
            this.Controls.Add(this.InputFileTextBox);
            this.Controls.Add(this.ViewKeyButton);
            this.Controls.Add(this.KetLabel);
            this.Controls.Add(this.GenerateKeyButton);
            this.Controls.Add(this.KeyTextBox);
            this.Controls.Add(this.ViewSeedButton);
            this.Controls.Add(this.SeedLabel);
            this.Controls.Add(this.RandomSeedButton);
            this.Controls.Add(this.AllOnesSeedButton);
            this.Controls.Add(this.SeedTextBox);
            this.Name = "MainForm";
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SeedTextBox;
        private System.Windows.Forms.Button AllOnesSeedButton;
        private System.Windows.Forms.Button RandomSeedButton;
        private System.Windows.Forms.Label SeedLabel;
        private System.Windows.Forms.Button ViewSeedButton;
        private System.Windows.Forms.Button ViewKeyButton;
        private System.Windows.Forms.Label KetLabel;
        private System.Windows.Forms.Button GenerateKeyButton;
        private System.Windows.Forms.TextBox KeyTextBox;
        private System.Windows.Forms.Button ViewInputFileButton;
        private System.Windows.Forms.Label InputFileLabel;
        private System.Windows.Forms.TextBox InputFileTextBox;
        private System.Windows.Forms.Button SearchFileButton;
        private System.Windows.Forms.Button ViewOutputFileButton;
        private System.Windows.Forms.Label OutputFileLabel;
        private System.Windows.Forms.TextBox OutputFileTextBox;
        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.Button DecryptButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
    }
}

