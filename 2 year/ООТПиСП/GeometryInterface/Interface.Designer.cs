
namespace GeometryInterface
{
    partial class Interface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interface));
            this.ScriptTextBox = new System.Windows.Forms.TextBox();
            this.EvaluateButton = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.LoadScriptButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScriptTextBox
            // 
            this.ScriptTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScriptTextBox.Font = new System.Drawing.Font("Consolas", 12F);
            this.ScriptTextBox.Location = new System.Drawing.Point(4, 3);
            this.ScriptTextBox.Multiline = true;
            this.ScriptTextBox.Name = "ScriptTextBox";
            this.ScriptTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ScriptTextBox.Size = new System.Drawing.Size(435, 593);
            this.ScriptTextBox.TabIndex = 0;
            this.ScriptTextBox.Text = resources.GetString("ScriptTextBox.Text");
            // 
            // EvaluateButton
            // 
            this.EvaluateButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EvaluateButton.Location = new System.Drawing.Point(3, 602);
            this.EvaluateButton.Name = "EvaluateButton";
            this.EvaluateButton.Size = new System.Drawing.Size(436, 32);
            this.EvaluateButton.TabIndex = 2;
            this.EvaluateButton.Text = "EVALUATE";
            this.EvaluateButton.UseVisualStyleBackColor = true;
            this.EvaluateButton.Click += new System.EventHandler(this.EvaluateButtonClick);
            // 
            // PictureBox
            // 
            this.PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox.Location = new System.Drawing.Point(0, 0);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(965, 664);
            this.PictureBox.TabIndex = 3;
            this.PictureBox.TabStop = false;
            this.PictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxPaint);
            // 
            // LoadScriptButton
            // 
            this.LoadScriptButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadScriptButton.Location = new System.Drawing.Point(3, 636);
            this.LoadScriptButton.Name = "LoadScriptButton";
            this.LoadScriptButton.Size = new System.Drawing.Size(436, 32);
            this.LoadScriptButton.TabIndex = 4;
            this.LoadScriptButton.Text = "LOAD SCRIPT";
            this.LoadScriptButton.UseVisualStyleBackColor = true;
            this.LoadScriptButton.Click += new System.EventHandler(this.LoadScriptButtonClick);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // Panel
            // 
            this.Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel.Controls.Add(this.PictureBox);
            this.Panel.Location = new System.Drawing.Point(442, 3);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(967, 666);
            this.Panel.TabIndex = 5;
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 672);
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.LoadScriptButton);
            this.Controls.Add(this.EvaluateButton);
            this.Controls.Add(this.ScriptTextBox);
            this.Name = "Interface";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.Panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ScriptTextBox;
        private System.Windows.Forms.Button EvaluateButton;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button LoadScriptButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Panel Panel;
    }
}

