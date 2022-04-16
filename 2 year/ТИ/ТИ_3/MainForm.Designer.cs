
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GenPublicKeyButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PTextBox
            // 
            this.PTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PTextBox.Location = new System.Drawing.Point(77, 8);
            this.PTextBox.Name = "PTextBox";
            this.PTextBox.Size = new System.Drawing.Size(218, 20);
            this.PTextBox.TabIndex = 0;
            this.PTextBox.TextChanged += new System.EventHandler(this.PTextBoxTextChanged);
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
            this.PublicKeyLabel.Location = new System.Drawing.Point(20, 127);
            this.PublicKeyLabel.Name = "PublicKeyLabel";
            this.PublicKeyLabel.Size = new System.Drawing.Size(28, 14);
            this.PublicKeyLabel.TabIndex = 10;
            this.PublicKeyLabel.Text = "Ko:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(77, 125);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(155, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "(...,...,...)";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GenPublicKeyButton
            // 
            this.GenPublicKeyButton.Location = new System.Drawing.Point(232, 124);
            this.GenPublicKeyButton.Name = "GenPublicKeyButton";
            this.GenPublicKeyButton.Size = new System.Drawing.Size(63, 22);
            this.GenPublicKeyButton.TabIndex = 13;
            this.GenPublicKeyButton.Text = "gen";
            this.GenPublicKeyButton.UseVisualStyleBackColor = true;
            this.GenPublicKeyButton.Click += new System.EventHandler(this.GenPublicKeyButtonClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 22);
            this.button1.TabIndex = 14;
            this.button1.Text = "enc";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 275);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GenPublicKeyButton);
            this.Controls.Add(this.textBox1);
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button GenPublicKeyButton;
        private System.Windows.Forms.Button button1;
    }
}

