using System;
using System.IO;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace ТИ_4
{
    public partial class EdsForm : Form
    {
        private BigInteger _p, _q, _d;
        private BigInteger _phi, _r, _e;
        private BigInteger _eds, _hash;

        public EdsForm()
        {
            this.InitializeComponent();
            this._hash = this._eds = 0;
        }

        private void ShowError(string message) =>
            MessageBox.Show(message, "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void UpdateUI()
        {
            this.PTextBox.Text = this._p.ToString();
            this.QTextBox.Text = this._q.ToString();
            this.RTextBox.Text = this._r.ToString();
            this.DTextBox.Text = this._d.ToString();
            this.ETextBox.Text = this._e.ToString();
            this.PhiTextBox.Text = this._phi.ToString();
            this.UpdateHashAndEds();
        }

        private void UpdateHashAndEds()
        {
            this.EdsTextBox.Text = this._eds.ToString();
            this.HashTextBox.Text = this._hash.ToString();
        }

        private void GeneratePublicKey()
        {
            BigInteger y;
            RSA.XGCD(this._phi, this._d, out this._e, out y);
            this._e += this._e < 0 ? this._phi : 0;
        }

        private void GenerateHashAndEds(string path)
        {
            var data = File.ReadAllBytes(path);
            this._hash = StudentHashFunction.Hash(data, this._r);
            this._eds = BigInteger.ModPow(_hash, this._d, this._r);
        }

        private void SaveEdsWithMessage(string path, string pathToMessageFile) =>
            File.WriteAllText(path, $"{this._eds} {File.ReadAllText(pathToMessageFile)}");

        private bool ValidateEdsAndHash(string pathToEdsFile)
        {
            var readData = File.ReadAllText(pathToEdsFile);
            var edsString = new StringBuilder(string.Empty);
            for (int i = 0; i < readData.Length && readData[i] != ' '; i++)
                edsString.Append(readData[i]);
            var eds = BigInteger.Parse(edsString.ToString());
            var hash = StudentHashFunction.Hash(Encoding.ASCII.GetBytes(
                readData.Substring(edsString.Length+1, readData.Length- edsString.Length-1)), this._r);
            var decryptedHash = BigInteger.ModPow(eds, this._e, this._r);
            this.ValidationListBox.Items.Clear();
            this.ValidationListBox.Items.Add($"Read EDS from file: {eds}");
            this.ValidationListBox.Items.Add($"Calculated file hash: {hash}");
            this.ValidationListBox.Items.Add($"Decrypted hash from EDS: {decryptedHash}");
            this.ValidationListBox.Items.Add($"EDS is correct: {decryptedHash == hash}");
            return decryptedHash == hash;
        }

        private bool ValidateArguments()
        {
            var result = BigInteger.TryParse(this.PTextBox.Text, out this._p);
            result &= BigInteger.TryParse(this.QTextBox.Text, out this._q);
            result &= BigInteger.TryParse(this.DTextBox.Text, out this._d);
            if (!result)
                this.ShowError("Invalid input, conversion error!");
            else
            {
                if (RSA.IsPrime(this._q) && RSA.IsPrime(this._p))
                {
                    this._r = this._q*this._p;
                    this._phi = (this._q-1)*(this._p-1);
                    if (this._d > 1 && this._d < this._phi)
                        if (RSA.GCD(this._d, this._phi) == 1)
                            return true;
                }
                this.ShowError("Invalid input, validation error!");
            }
            return false;
        }

        private void ApplyButtonClick(object sender, EventArgs e)
        {
            if (this.GroupBox.Enabled = this.ValidateArguments())
            {
                this.GeneratePublicKey();
                this.UpdateUI();
            }
        }

        private void ValidateEdsButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.ValidateEdsAndHash(this.ValidationFileTextBox.Text);
            }
            catch (Exception ex)
            {
                this.ShowError($"Error occured: {ex.Message}");
            }
        }

        private void InputFileSearchButtonClick(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.InputTextBox.Text = this.OpenFileDialog.FileName;
                    this.GenerateHashAndEds(this.InputTextBox.Text);
                    this.UpdateHashAndEds();
                }
                catch (Exception ex)
                {
                    this.ShowError($"Error occured: {ex.Message}");
                }
            }
        }

        private void ValidationFileSearchButtonClick(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
                this.ValidationFileTextBox.Text = this.OpenFileDialog.FileName;
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            if (this.SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var path = this.SaveFileDialog.FileName;
                    this.SaveEdsWithMessage(path, this.InputTextBox.Text);
                }
                catch (Exception ex)
                {
                    this.ShowError($"Error occured: {ex.Message}");
                }
            }
        }

    }
}