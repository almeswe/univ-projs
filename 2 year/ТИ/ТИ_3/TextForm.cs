using System.Windows.Forms;

namespace ТИ_3
{
    public partial class TextForm : Form
    {
        public TextForm(string text)
        {
            InitializeComponent();
            this.TextBox.Text = text;
        }
    }
}
