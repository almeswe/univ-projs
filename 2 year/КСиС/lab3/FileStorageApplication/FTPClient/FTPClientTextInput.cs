using System;
using System.Windows.Forms;

namespace FTPClient
{
    public partial class FTPClientTextInput : Form
    {
        public static string ReturnedText = string.Empty;

        public FTPClientTextInput(string text)
        {
            this.InitializeComponent();
            this.TextBox.Text = text;
            ReturnedText = string.Empty;
        }

        private void ConfirmButtonClick(object sender, EventArgs e)
        {
            ReturnedText = this.TextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
