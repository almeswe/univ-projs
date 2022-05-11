using System;
using System.IO;
using System.Text;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ТИ_3
{
    public partial class MainForm : Form
    {
        private ulong _y = 0;
        private Random _random = new Random();

        public MainForm()
        {
            this.InitializeComponent();
            this.GeneratePButtonClick(null, null);
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

        private void SetGeneratorValues(ulong p)
        {
            this.GComboBox.Items.Clear();
            this.GComboBox.BeginUpdate();
            this.GComboBox.Items.AddRange(Elgamal.Elgamal.GetPrimeRoots(p)
                .Select(v => v.ToString()).ToArray());
            this.GComboBox.EndUpdate();
            this.PrimeRootsCountLabel.Text = this.GComboBox.Items.Count.ToString();
            if (this.GComboBox.Items.Count != 0)
                this.GComboBox.SelectedIndex = 0;
        }

        private void SetRandomX(ulong p) =>
            this.XTextBox.Text = $"{this._random.Next(1, (int)p)}";

        private void SetRandomK(ulong p) =>
            this.KTextBox.Text = $"{this._random.Next(2, (int)(p - 1))}";

        private void ShowError(string message) =>
            MessageBox.Show(message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void WriteCipherToFile(ulong[] cipher, string path)
        {
            try
            {
                var data = new byte[cipher.Length * 8];
                for (int item = 0; item < cipher.Length; item++)
                    for (int i = 1; i <= 8; i++)
                        data[item * 8 + (i - 1)] = (byte)(cipher[item] >> (64 - i * 8));
                System.IO.File.WriteAllBytes(path, data);
            }
            catch (Exception ex)
            {
                this.ShowError($"Error occured: {ex.Message}");
            }
        }

        private ulong[] ReadCipherFromFile(string path)
        {
            try
            {
                var data = System.IO.File.ReadAllBytes(path);
                var cipher = new ulong[data.Length / 8];
                for (int i = 0; i < cipher.Length; i++)
                    for (int j = 0; j < 8; j++)
                        cipher[i] |= (ulong)(data[j + (i * 8)] << 64 - 8 * (j + 1));
                return cipher;
            }
            catch (Exception ex)
            {
                this.ShowError($"Error occured: {ex.Message}");
            }
            return null;
        }

        private void XTextBoxTextChanged(object sender, EventArgs e)
        {
            ulong value = 1;
            bool isEnabled = ulong.TryParse(this.XTextBox.Text, out value) &&
                value != 1 && value < ulong.Parse(this.PTextBox.Text)-1;
            this.SetTextControls(isEnabled, this.XTextBox, this.PTextBox);
        }

        private void KTextBoxTextChanged(object sender, EventArgs e)
        {
            ulong value = 1;
            bool isEnabled = ulong.TryParse(this.KTextBox.Text, out value) &&
                value > 1 && value < ulong.Parse(this.PTextBox.Text) - 1;
            isEnabled &= Elgamal.Elgamal.GetCommonDivisor(
                value, ulong.Parse(this.PTextBox.Text) - 1) == 1;
            this.SetTextControls(isEnabled, this.KTextBox, this.PTextBox);
        }

        private void GenPublicKeyButtonClick(object sender, EventArgs e)
        {
            try
            {
                var p = ulong.Parse(this.PTextBox.Text);
                var g = ulong.Parse(this.GComboBox.GetItemText(
                    this.GComboBox.SelectedItem));
                var x = ulong.Parse(this.XTextBox.Text);
                var publicKey = Elgamal.Elgamal.GeneratePublicKey(p, g, x);
                this.PublicKeyTextBox.Text = $"({publicKey[0]},{publicKey[1]},{this._y = publicKey[2]})";
                this.PrivateKeyTextBox.Text = $"({x})";
            }
            catch (Exception ex)
            {
                this.ShowError($"Error occured: {ex.Message}");
            }
        }

        private void EncryptButtonClick(object sender, EventArgs e)
        {
            try
            {
                ulong p = ulong.Parse(this.PTextBox.Text);
                ulong k = ulong.Parse(this.KTextBox.Text);
                ulong g = ulong.Parse(this.GComboBox.GetItemText(
                        this.GComboBox.SelectedItem));
                var cipher = Elgamal.Elgamal.Encrypt(File.ReadAllBytes(
                    this.SourceFileTextBox.Text), p, g, k, this._y);
                this.WriteCipherToFile(cipher, this.OutputFileTextBox.Text);
            }
            catch (Exception ex)
            {
                this.ShowError($"Error occured: {ex.Message}");
            }
        }

        private void DecryptButtonClick(object sender, EventArgs e)
        {
            try
            {
                ulong p = ulong.Parse(this.PTextBox.Text);
                ulong x = ulong.Parse(this.XTextBox.Text);
                var cipher = this.ReadCipherFromFile(this.OutputFileTextBox.Text);
                var plain = Elgamal.Elgamal.Decrypt(cipher, p, x);
                File.WriteAllBytes(this.OutputFileTextBox.Text, plain);
            }
            catch (Exception ex)
            {
                this.ShowError($"Error occured: {ex.Message}");
            }
        }

        private void SetSourceFileButtonClick(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
                this.SourceFileTextBox.Text = this.OpenFileDialog.FileName;
        }

        private void SetOutputFileButtonClick(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
                this.OutputFileTextBox.Text = this.OpenFileDialog.FileName;
        }

        private void GeneratePButtonClick(object sender, EventArgs e)
        {
            ulong value = 1;
            bool isEnabled = ulong.TryParse(this.PTextBox.Text, out value)
                && Elgamal.Elgamal.IsPrime(value);
            this.SetTextControls(isEnabled, this.PTextBox);
            if (isEnabled)
            {
                this.SetRandomX(value);
                this.SetRandomK(value);
                this.SetGeneratorValues(value);
            }
        }

        private void OpenOutputFileButtonClick(object sender, EventArgs e)
        {
            try
            {
                var cipher = this.ReadCipherFromFile(this.OutputFileTextBox.Text);
                var text = new StringBuilder(string.Empty);
                for (int i = 0; i < cipher.Length; i+=2)
                    text.Append($"({cipher[i]}, {cipher[i+1]}) ");
                new TextForm(text.ToString()).Show();
            }
            catch (Exception ex)
            {
                this.ShowError($"Error occured: {ex.Message}");
            }
        }
    }
}