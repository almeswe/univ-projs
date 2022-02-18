
namespace ТИ_1
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
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.SaveOutputButton = new System.Windows.Forms.Button();
            this.FromFileButton = new System.Windows.Forms.Button();
            this.MethodGroup = new System.Windows.Forms.GroupBox();
            this.VigenereMethidRadioButton = new System.Windows.Forms.RadioButton();
            this.DecimationMethodRadioButton = new System.Windows.Forms.RadioButton();
            this.ColumnMethidRadioButton = new System.Windows.Forms.RadioButton();
            this.KeyTextBox = new System.Windows.Forms.TextBox();
            this.DecryptButton = new System.Windows.Forms.Button();
            this.EncryptButton = new System.Windows.Forms.Button();
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ControlsPanel.SuspendLayout();
            this.MethodGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ControlsPanel.Controls.Add(this.SaveOutputButton);
            this.ControlsPanel.Controls.Add(this.FromFileButton);
            this.ControlsPanel.Controls.Add(this.MethodGroup);
            this.ControlsPanel.Controls.Add(this.KeyTextBox);
            this.ControlsPanel.Controls.Add(this.DecryptButton);
            this.ControlsPanel.Controls.Add(this.EncryptButton);
            this.ControlsPanel.Location = new System.Drawing.Point(3, 2);
            this.ControlsPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(269, 542);
            this.ControlsPanel.TabIndex = 0;
            // 
            // SaveOutputButton
            // 
            this.SaveOutputButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SaveOutputButton.Location = new System.Drawing.Point(0, 480);
            this.SaveOutputButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveOutputButton.Name = "SaveOutputButton";
            this.SaveOutputButton.Size = new System.Drawing.Size(269, 31);
            this.SaveOutputButton.TabIndex = 6;
            this.SaveOutputButton.Text = "Save output to file";
            this.SaveOutputButton.UseVisualStyleBackColor = true;
            this.SaveOutputButton.Click += new System.EventHandler(this.SaveOutputButton_Click);
            // 
            // FromFileButton
            // 
            this.FromFileButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FromFileButton.Location = new System.Drawing.Point(0, 511);
            this.FromFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FromFileButton.Name = "FromFileButton";
            this.FromFileButton.Size = new System.Drawing.Size(269, 31);
            this.FromFileButton.TabIndex = 5;
            this.FromFileButton.Text = "Input from file";
            this.FromFileButton.UseVisualStyleBackColor = true;
            this.FromFileButton.Click += new System.EventHandler(this.InputFromFileButtonClick);
            // 
            // MethodGroup
            // 
            this.MethodGroup.Controls.Add(this.VigenereMethidRadioButton);
            this.MethodGroup.Controls.Add(this.DecimationMethodRadioButton);
            this.MethodGroup.Controls.Add(this.ColumnMethidRadioButton);
            this.MethodGroup.Location = new System.Drawing.Point(3, 105);
            this.MethodGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MethodGroup.Name = "MethodGroup";
            this.MethodGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MethodGroup.Size = new System.Drawing.Size(263, 127);
            this.MethodGroup.TabIndex = 3;
            this.MethodGroup.TabStop = false;
            this.MethodGroup.Text = "Methods";
            // 
            // VigenereMethidRadioButton
            // 
            this.VigenereMethidRadioButton.AutoSize = true;
            this.VigenereMethidRadioButton.Location = new System.Drawing.Point(31, 86);
            this.VigenereMethidRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VigenereMethidRadioButton.Name = "VigenereMethidRadioButton";
            this.VigenereMethidRadioButton.Size = new System.Drawing.Size(181, 21);
            this.VigenereMethidRadioButton.TabIndex = 2;
            this.VigenereMethidRadioButton.Text = "Метод Виженера (рус.)";
            this.VigenereMethidRadioButton.UseVisualStyleBackColor = true;
            // 
            // DecimationMethodRadioButton
            // 
            this.DecimationMethodRadioButton.AutoSize = true;
            this.DecimationMethodRadioButton.Location = new System.Drawing.Point(31, 59);
            this.DecimationMethodRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DecimationMethodRadioButton.Name = "DecimationMethodRadioButton";
            this.DecimationMethodRadioButton.Size = new System.Drawing.Size(195, 21);
            this.DecimationMethodRadioButton.TabIndex = 1;
            this.DecimationMethodRadioButton.Text = "Метод децимации (англ.)";
            this.DecimationMethodRadioButton.UseVisualStyleBackColor = true;
            // 
            // ColumnMethidRadioButton
            // 
            this.ColumnMethidRadioButton.AutoSize = true;
            this.ColumnMethidRadioButton.Checked = true;
            this.ColumnMethidRadioButton.Location = new System.Drawing.Point(31, 32);
            this.ColumnMethidRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ColumnMethidRadioButton.Name = "ColumnMethidRadioButton";
            this.ColumnMethidRadioButton.Size = new System.Drawing.Size(201, 21);
            this.ColumnMethidRadioButton.TabIndex = 0;
            this.ColumnMethidRadioButton.TabStop = true;
            this.ColumnMethidRadioButton.Text = "Столбцовый метод (англ.)";
            this.ColumnMethidRadioButton.UseVisualStyleBackColor = true;
            // 
            // KeyTextBox
            // 
            this.KeyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KeyTextBox.Location = new System.Drawing.Point(3, 2);
            this.KeyTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.KeyTextBox.Name = "KeyTextBox";
            this.KeyTextBox.Size = new System.Drawing.Size(263, 34);
            this.KeyTextBox.TabIndex = 2;
            // 
            // DecryptButton
            // 
            this.DecryptButton.Location = new System.Drawing.Point(3, 68);
            this.DecryptButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DecryptButton.Name = "DecryptButton";
            this.DecryptButton.Size = new System.Drawing.Size(263, 31);
            this.DecryptButton.TabIndex = 1;
            this.DecryptButton.Text = "Decrypt";
            this.DecryptButton.UseVisualStyleBackColor = true;
            this.DecryptButton.Click += new System.EventHandler(this.DecryptButtonClick);
            // 
            // EncryptButton
            // 
            this.EncryptButton.Location = new System.Drawing.Point(3, 37);
            this.EncryptButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(263, 32);
            this.EncryptButton.TabIndex = 0;
            this.EncryptButton.Text = "Encrypt";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButtonClick);
            // 
            // InputTextBox
            // 
            this.InputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.InputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InputTextBox.Location = new System.Drawing.Point(277, 2);
            this.InputTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InputTextBox.Multiline = true;
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(479, 542);
            this.InputTextBox.TabIndex = 3;
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OutputTextBox.Location = new System.Drawing.Point(763, 2);
            this.OutputTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.Size = new System.Drawing.Size(200, 542);
            this.OutputTextBox.TabIndex = 4;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 549);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.InputTextBox);
            this.Controls.Add(this.ControlsPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ControlsPanel.ResumeLayout(false);
            this.ControlsPanel.PerformLayout();
            this.MethodGroup.ResumeLayout(false);
            this.MethodGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ControlsPanel;
        private System.Windows.Forms.Button DecryptButton;
        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.TextBox KeyTextBox;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.GroupBox MethodGroup;
        private System.Windows.Forms.RadioButton VigenereMethidRadioButton;
        private System.Windows.Forms.RadioButton DecimationMethodRadioButton;
        private System.Windows.Forms.RadioButton ColumnMethidRadioButton;
        private System.Windows.Forms.Button FromFileButton;
        private System.Windows.Forms.Button SaveOutputButton;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
    }
}

