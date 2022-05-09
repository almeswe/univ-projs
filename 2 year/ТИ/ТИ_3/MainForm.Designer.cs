
namespace ТИ_3
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
            this.PTextBox = new System.Windows.Forms.TextBox();
            this.XTextBox = new System.Windows.Forms.TextBox();
            this.KTextBox = new System.Windows.Forms.TextBox();
            this.PLabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.KLabel = new System.Windows.Forms.Label();
            this.GComboBox = new System.Windows.Forms.ComboBox();
            this.GLabel = new System.Windows.Forms.Label();
            this.PublicKeyLabel = new System.Windows.Forms.Label();
            this.PublicKeyTextBox = new System.Windows.Forms.TextBox();
            this.GenPublicKeyButton = new System.Windows.Forms.Button();
            this.EncryptButton = new System.Windows.Forms.Button();
            this.PrimeRootsCountLabel = new System.Windows.Forms.Label();
            this.GeneratePButton = new System.Windows.Forms.Button();
            this.PrivateKeyTextBox = new System.Windows.Forms.TextBox();
            this.PrivateKeyLabel = new System.Windows.Forms.Label();
            this.SetSourceFileButton = new System.Windows.Forms.Button();
            this.SourceFileTextBox = new System.Windows.Forms.TextBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OutputFileTextBox = new System.Windows.Forms.TextBox();
            this.SetOutputFileButton = new System.Windows.Forms.Button();
            this.DecryptButton = new System.Windows.Forms.Button();
            this.OpenOutputFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PTextBox
            // 
            this.PTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PTextBox.Location = new System.Drawing.Point(77, 8);
            this.PTextBox.Name = "PTextBox";
            this.PTextBox.Size = new System.Drawing.Size(184, 20);
            this.PTextBox.TabIndex = 0;
            // 
            // XTextBox
            // 
            this.XTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.XTextBox.Location = new System.Drawing.Point(77, 34);
            this.XTextBox.Name = "XTextBox";
            this.XTextBox.Size = new System.Drawing.Size(218, 20);
            this.XTextBox.TabIndex = 1;
            this.XTextBox.TextChanged += new System.EventHandler(this.XTextBoxTextChanged);
            // 
            // KTextBox
            // 
            this.KTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KTextBox.Location = new System.Drawing.Point(77, 60);
            this.KTextBox.Name = "KTextBox";
            this.KTextBox.Size = new System.Drawing.Size(218, 20);
            this.KTextBox.TabIndex = 2;
            this.KTextBox.TextChanged += new System.EventHandler(this.KTextBoxTextChanged);
            // 
            // PLabel
            // 
            this.PLabel.AutoSize = true;
            this.PLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PLabel.Location = new System.Drawing.Point(8, 10);
            this.PLabel.Name = "PLabel";
            this.PLabel.Size = new System.Drawing.Size(63, 14);
            this.PLabel.TabIndex = 4;
            this.PLabel.Text = "Enter p:";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.XLabel.Location = new System.Drawing.Point(8, 36);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(63, 14);
            this.XLabel.TabIndex = 5;
            this.XLabel.Text = "Enter x:";
            // 
            // KLabel
            // 
            this.KLabel.AutoSize = true;
            this.KLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KLabel.Location = new System.Drawing.Point(7, 62);
            this.KLabel.Name = "KLabel";
            this.KLabel.Size = new System.Drawing.Size(63, 14);
            this.KLabel.TabIndex = 6;
            this.KLabel.Text = "Enter k:";
            // 
            // GComboBox
            // 
            this.GComboBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GComboBox.FormattingEnabled = true;
            this.GComboBox.Location = new System.Drawing.Point(77, 86);
            this.GComboBox.Name = "GComboBox";
            this.GComboBox.Size = new System.Drawing.Size(218, 21);
            this.GComboBox.TabIndex = 8;
            // 
            // GLabel
            // 
            this.GLabel.AutoSize = true;
            this.GLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GLabel.Location = new System.Drawing.Point(7, 88);
            this.GLabel.Name = "GLabel";
            this.GLabel.Size = new System.Drawing.Size(70, 14);
            this.GLabel.TabIndex = 9;
            this.GLabel.Text = "Select g:";
            // 
            // PublicKeyLabel
            // 
            this.PublicKeyLabel.AutoSize = true;
            this.PublicKeyLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PublicKeyLabel.Location = new System.Drawing.Point(26, 218);
            this.PublicKeyLabel.Name = "PublicKeyLabel";
            this.PublicKeyLabel.Size = new System.Drawing.Size(28, 14);
            this.PublicKeyLabel.TabIndex = 10;
            this.PublicKeyLabel.Text = "Ko:";
            // 
            // PublicKeyTextBox
            // 
            this.PublicKeyTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PublicKeyTextBox.Location = new System.Drawing.Point(60, 216);
            this.PublicKeyTextBox.Name = "PublicKeyTextBox";
            this.PublicKeyTextBox.ReadOnly = true;
            this.PublicKeyTextBox.Size = new System.Drawing.Size(155, 20);
            this.PublicKeyTextBox.TabIndex = 11;
            this.PublicKeyTextBox.Text = "(...,...,...)";
            this.PublicKeyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GenPublicKeyButton
            // 
            this.GenPublicKeyButton.Location = new System.Drawing.Point(215, 215);
            this.GenPublicKeyButton.Name = "GenPublicKeyButton";
            this.GenPublicKeyButton.Size = new System.Drawing.Size(63, 22);
            this.GenPublicKeyButton.TabIndex = 13;
            this.GenPublicKeyButton.Text = "gen";
            this.GenPublicKeyButton.UseVisualStyleBackColor = true;
            this.GenPublicKeyButton.Click += new System.EventHandler(this.GenPublicKeyButtonClick);
            // 
            // EncryptButton
            // 
            this.EncryptButton.Location = new System.Drawing.Point(215, 238);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(63, 22);
            this.EncryptButton.TabIndex = 14;
            this.EncryptButton.Text = "enc";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButtonClick);
            // 
            // PrimeRootsCountLabel
            // 
            this.PrimeRootsCountLabel.AutoSize = true;
            this.PrimeRootsCountLabel.Location = new System.Drawing.Point(269, 110);
            this.PrimeRootsCountLabel.Name = "PrimeRootsCountLabel";
            this.PrimeRootsCountLabel.Size = new System.Drawing.Size(16, 13);
            this.PrimeRootsCountLabel.TabIndex = 15;
            this.PrimeRootsCountLabel.Text = "...";
            // 
            // GeneratePButton
            // 
            this.GeneratePButton.Location = new System.Drawing.Point(261, 7);
            this.GeneratePButton.Name = "GeneratePButton";
            this.GeneratePButton.Size = new System.Drawing.Size(34, 22);
            this.GeneratePButton.TabIndex = 16;
            this.GeneratePButton.Text = "go";
            this.GeneratePButton.UseVisualStyleBackColor = true;
            this.GeneratePButton.Click += new System.EventHandler(this.GeneratePButtonClick);
            // 
            // PrivateKeyTextBox
            // 
            this.PrivateKeyTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PrivateKeyTextBox.Location = new System.Drawing.Point(60, 239);
            this.PrivateKeyTextBox.Name = "PrivateKeyTextBox";
            this.PrivateKeyTextBox.ReadOnly = true;
            this.PrivateKeyTextBox.Size = new System.Drawing.Size(93, 20);
            this.PrivateKeyTextBox.TabIndex = 17;
            this.PrivateKeyTextBox.Text = "(...)";
            this.PrivateKeyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PrivateKeyLabel
            // 
            this.PrivateKeyLabel.AutoSize = true;
            this.PrivateKeyLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PrivateKeyLabel.Location = new System.Drawing.Point(26, 241);
            this.PrivateKeyLabel.Name = "PrivateKeyLabel";
            this.PrivateKeyLabel.Size = new System.Drawing.Size(28, 14);
            this.PrivateKeyLabel.TabIndex = 18;
            this.PrivateKeyLabel.Text = "Kз:";
            // 
            // SetSourceFileButton
            // 
            this.SetSourceFileButton.Location = new System.Drawing.Point(231, 130);
            this.SetSourceFileButton.Name = "SetSourceFileButton";
            this.SetSourceFileButton.Size = new System.Drawing.Size(63, 22);
            this.SetSourceFileButton.TabIndex = 19;
            this.SetSourceFileButton.Text = "...";
            this.SetSourceFileButton.UseVisualStyleBackColor = true;
            this.SetSourceFileButton.Click += new System.EventHandler(this.SetSourceFileButtonClick);
            // 
            // SourceFileTextBox
            // 
            this.SourceFileTextBox.Location = new System.Drawing.Point(10, 131);
            this.SourceFileTextBox.Name = "SourceFileTextBox";
            this.SourceFileTextBox.ReadOnly = true;
            this.SourceFileTextBox.Size = new System.Drawing.Size(221, 20);
            this.SourceFileTextBox.TabIndex = 20;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // OutputFileTextBox
            // 
            this.OutputFileTextBox.Location = new System.Drawing.Point(10, 154);
            this.OutputFileTextBox.Name = "OutputFileTextBox";
            this.OutputFileTextBox.ReadOnly = true;
            this.OutputFileTextBox.Size = new System.Drawing.Size(221, 20);
            this.OutputFileTextBox.TabIndex = 22;
            // 
            // SetOutputFileButton
            // 
            this.SetOutputFileButton.Location = new System.Drawing.Point(231, 153);
            this.SetOutputFileButton.Name = "SetOutputFileButton";
            this.SetOutputFileButton.Size = new System.Drawing.Size(63, 22);
            this.SetOutputFileButton.TabIndex = 21;
            this.SetOutputFileButton.Text = "...";
            this.SetOutputFileButton.UseVisualStyleBackColor = true;
            this.SetOutputFileButton.Click += new System.EventHandler(this.SetOutputFileButtonClick);
            // 
            // DecryptButton
            // 
            this.DecryptButton.Location = new System.Drawing.Point(153, 238);
            this.DecryptButton.Name = "DecryptButton";
            this.DecryptButton.Size = new System.Drawing.Size(63, 22);
            this.DecryptButton.TabIndex = 23;
            this.DecryptButton.Text = "dec";
            this.DecryptButton.UseVisualStyleBackColor = true;
            this.DecryptButton.Click += new System.EventHandler(this.DecryptButtonClick);
            // 
            // OpenOutputFileButton
            // 
            this.OpenOutputFileButton.Location = new System.Drawing.Point(10, 176);
            this.OpenOutputFileButton.Name = "OpenOutputFileButton";
            this.OpenOutputFileButton.Size = new System.Drawing.Size(284, 23);
            this.OpenOutputFileButton.TabIndex = 24;
            this.OpenOutputFileButton.Text = "open in decimal";
            this.OpenOutputFileButton.UseVisualStyleBackColor = true;
            this.OpenOutputFileButton.Click += new System.EventHandler(this.OpenOutputFileButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 275);
            this.Controls.Add(this.OpenOutputFileButton);
            this.Controls.Add(this.DecryptButton);
            this.Controls.Add(this.OutputFileTextBox);
            this.Controls.Add(this.SetOutputFileButton);
            this.Controls.Add(this.SourceFileTextBox);
            this.Controls.Add(this.SetSourceFileButton);
            this.Controls.Add(this.PrivateKeyLabel);
            this.Controls.Add(this.PrivateKeyTextBox);
            this.Controls.Add(this.GeneratePButton);
            this.Controls.Add(this.PrimeRootsCountLabel);
            this.Controls.Add(this.EncryptButton);
            this.Controls.Add(this.GenPublicKeyButton);
            this.Controls.Add(this.PublicKeyTextBox);
            this.Controls.Add(this.PublicKeyLabel);
            this.Controls.Add(this.GLabel);
            this.Controls.Add(this.GComboBox);
            this.Controls.Add(this.KLabel);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.PLabel);
            this.Controls.Add(this.KTextBox);
            this.Controls.Add(this.XTextBox);
            this.Controls.Add(this.PTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PTextBox;
        private System.Windows.Forms.TextBox XTextBox;
        private System.Windows.Forms.TextBox KTextBox;
        private System.Windows.Forms.Label PLabel;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.Label KLabel;
        private System.Windows.Forms.ComboBox GComboBox;
        private System.Windows.Forms.Label GLabel;
        private System.Windows.Forms.Label PublicKeyLabel;
        private System.Windows.Forms.TextBox PublicKeyTextBox;
        private System.Windows.Forms.Button GenPublicKeyButton;
        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.Label PrimeRootsCountLabel;
        private System.Windows.Forms.Button GeneratePButton;
        private System.Windows.Forms.TextBox PrivateKeyTextBox;
        private System.Windows.Forms.Label PrivateKeyLabel;
        private System.Windows.Forms.Button SetSourceFileButton;
        private System.Windows.Forms.TextBox SourceFileTextBox;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.TextBox OutputFileTextBox;
        private System.Windows.Forms.Button SetOutputFileButton;
        private System.Windows.Forms.Button DecryptButton;
        private System.Windows.Forms.Button OpenOutputFileButton;
    }
}

