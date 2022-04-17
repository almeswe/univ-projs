using System.Windows.Forms;

namespace ТИ_2
{
    public partial class TextViewerForm : Form
    {
        public TextViewerForm(string text)
        {
            this.InitializeComponent();
            this.TextBox.Text = text;
        }
    }
}
