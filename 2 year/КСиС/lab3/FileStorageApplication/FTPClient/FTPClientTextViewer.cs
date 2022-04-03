using System.Windows.Forms;

namespace FTPClient
{
    public partial class FTPClientTextViewer : Form
    {
        public FTPClientTextViewer(string text)
        {
            this.InitializeComponent();
            this.TextBox.Text = text;
        }
    }
}
