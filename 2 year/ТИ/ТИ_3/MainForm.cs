using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ТИ_3.Elgamal;

namespace ТИ_3
{
    public partial class MainForm : Form
    {
        private Random _random = new Random();

        public MainForm()
        {
            this.InitializeComponent();
            this.PTextBoxTextChanged(null, null);
        }

        private void SetTextControls(bool enable, 
            params Control[] except)
        {
            foreach (var control in this.Controls)
                if (control.GetType().BaseType == typeof(TextBoxBase) ||
                    control.GetType().BaseType == typeof(ListControl))
                    if (!except.Contains(control))
                        control.GetType().GetProperty("Enabled")
                            .SetValue(control, enable);
        }

        private void SetGeneratorValues(long p)
        {
            this.GComboBox.Items.Clear();
            this.GComboBox.Items.AddRange(Elgamal.Elgamal.GetPrimeRoots(p)
                .Select(v => v.ToString()).ToArray());
            if (this.GComboBox.Items.Count != 0)
                this.GComboBox.SelectedIndex = 0;
        }

        private void SetRandomX(long p) =>
            this.XTextBox.Text = $"{this._random.Next(1, (int)p)}";

        private void SetRandomK(long p) =>
            this.KTextBox.Text = $"{this._random.Next(2, (int)(p - 1))}";

        private void PTextBoxTextChanged(object sender, EventArgs e)
        {
            long value = 1;
            bool isEnabled = long.TryParse(this.PTextBox.Text, out value)
                && Elgamal.Elgamal.IsPrime(value);
            this.SetTextControls(isEnabled, this.PTextBox);
            if (isEnabled)
            {
                this.SetRandomX(value);
                this.SetRandomK(value);
                this.SetGeneratorValues(value);
            }
        }

        private void XTextBoxTextChanged(object sender, EventArgs e)
        {
            long value = 1;
            bool isEnabled = long.TryParse(this.XTextBox.Text, out value) &&
                value != 1 && value < long.Parse(this.PTextBox.Text)-1;
            this.SetTextControls(isEnabled, this.XTextBox, this.PTextBox);
        }

        private void KTextBoxTextChanged(object sender, EventArgs e)
        {
            long value = 1;
            bool isEnabled = long.TryParse(this.KTextBox.Text, out value) &&
                value > 1 && value < long.Parse(this.PTextBox.Text) - 1;
            isEnabled &= Elgamal.Elgamal.GetCommonDivisor(
                value, long.Parse(this.PTextBox.Text) - 1) == 1;
            this.SetTextControls(isEnabled, this.KTextBox, this.PTextBox);
        }

        private void GenPublicKeyButtonClick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            long[] cipher = Elgamal.Elgamal.Encrypt(Encoding.UTF8.GetBytes("string"), 13, 7, 7, 11);
            byte[] plain = Elgamal.Elgamal.Decrypt(cipher, 13, 5);
            var a = Encoding.UTF8.GetString(plain);
        }
    }
}