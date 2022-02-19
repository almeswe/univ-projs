using System;
using System.IO;
using System.Windows.Forms;

namespace ТИ_1
{
    public partial class MainForm : Form
    {
        public MainForm() =>
            this.InitializeComponent();

        private ICryptography DetermineMethod()
        {
            try
            {
                if (this.ColumnMethidRadioButton.Checked)
                    return new ColumnMethod(this.KeyTextBox.Text);
                else if (this.DecimationMethodRadioButton.Checked)
                    return new DecimationMethod(this.KeyTextBox.Text);
                else
                    return new VigenereMethod(this.KeyTextBox.Text);
            }
            catch (ArgumentException ae)
            { 
                MessageBox.Show(ae.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            return null;
        }

        private void EncryptButtonClick(object sender, EventArgs e) =>
            this.OutputTextBox.Text = this.DetermineMethod()?
                .Encrypt(this.InputTextBox.Text);

        private void DecryptButtonClick(object sender, EventArgs e) =>
            this.OutputTextBox.Text = this.DetermineMethod()?
                .Decrypt(this.InputTextBox.Text);

        private void InputFromFileButtonClick(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.OpenFileDialog.ShowDialog())
                this.InputTextBox.Text = File.ReadAllText(this.OpenFileDialog.FileName);
        }

        private void SaveOutputButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.OpenFileDialog.ShowDialog())
                 File.WriteAllText(this.OpenFileDialog.FileName, this.OutputTextBox.Text);
        }
    }
}