using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;

namespace ТИ_2
{
    public partial class MainForm : Form
    {
        private long _seed;
        private byte[] _stream;

        private int _size => 40;

        private readonly Random _random = new Random(
            DateTime.Now.Second);
        
        public MainForm() =>
            InitializeComponent();

        private string ValidateSeed(string seed)
        {
            var result = new StringBuilder(string.Empty);
            foreach (var c in seed)
                if (c == '1' || c == '0')
                    result.Append(c);
            return result.ToString();
        }

        private void ViewText(string text) =>
            new TextViewerForm(text).Show();

        private void ViewFile(string filePath)
        {
            try
            {
                Process.Start("notepad.exe", filePath);
                //this.ViewText(File.ReadAllText(
                 //   filePath));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RandomSeedButtonClick(object sender, EventArgs e)
        {
            var seed = new StringBuilder(string.Empty);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                    seed.Append((char)(this._random.Next(0, 2) + '0'));
                seed.Append('-');
                for (int j = 0; j < 4; j++)
                    seed.Append((char)(this._random.Next(0, 2) + '0'));
                seed.Append(' ');
            }
            this.SeedTextBox.Text = seed.ToString();
        }

        private void AllOnesSeedButtonClick(object sender, EventArgs e)
        {
            this.SeedTextBox.Clear();
            for (int i = 0; i < 5; i++)
                this.SeedTextBox.Text += "1111-1111 ";
        }

        private void ViewSeedButton_Click(object sender, EventArgs e) =>
            ViewText(this.SeedTextBox.Text);

        private void SearchFileButtonClick(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.InputFileTextBox.Text = this.OpenFileDialog.FileName;
            this.OutputFileTextBox.Text = $"{Path.GetDirectoryName(this.InputFileTextBox.Text)}" +
                $"\\output.bin";
        }

        private void ViewInputFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var bytes = File.ReadAllBytes(this.InputFileTextBox.Text);
                var result = new StringBuilder(string.Empty);
                foreach (var @byte in bytes)
                {
                    var binary = Convert.ToString(@byte, 2);
                    result.Append($"{new string('0', 8 - binary.Length)}{binary}");
                }
                this.ViewText(result.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void ViewOutputFileButtonClick(object sender, EventArgs e)
        {
            try
            {
                var bytes = File.ReadAllBytes(this.OutputFileTextBox.Text);
                var result = new StringBuilder(string.Empty);
                foreach (var @byte in bytes)
                {
                    var binary = Convert.ToString(@byte, 2);
                    result.Append($"{new string('0', 8 - binary.Length)}{binary}");
                }
                this.ViewText(result.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewKeyButton_Click(object sender, EventArgs e) =>
            this.ViewText(this.KeyTextBox.Text);

        private void GenerateKeyButton_Click(object sender, EventArgs e)
        {
            try
            {
                var size = new FileInfo(
                    this.InputFileTextBox.Text).Length;
                var seedString = this.ValidateSeed(
                    this.SeedTextBox.Text);
                if (seedString.Length != 40)
                    throw new ArgumentException("Seed is invalid.");
                this._seed = 0;
                foreach (var digit in seedString)
                    this._seed = (this._seed << 1) | (digit == '1' ? 1 : 0);
                this._stream = LFSR.Generate(this._seed, size);
                this.KeyTextBox.Clear();
                for (int i = 0; i < Math.Min(5, (int)size); i++)
                    for (int j = 0; j < 8; j++)
                        this.KeyTextBox.Text += seedString[8*i+j];
                for (int i = Math.Min(5, (int)size); i < Math.Min(this._stream.Length, 300); i++)
                {
                    var binary = Convert.ToString(this._stream[i], 2);
                    this.KeyTextBox.Text += $"{new string('0', 8 - binary.Length)}{binary}";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        private void EncryptButtonClick(object sender, EventArgs e)
        {
            try
            {
                var source = File.ReadAllBytes(
                    this.InputFileTextBox.Text);
                var writer = new BinaryWriter(File.OpenWrite(
                    this.OutputFileTextBox.Text));

                var result = new BitArray(source).Xor(new BitArray(this._stream));
                writer.Write(this.BitArrayToByteArray(result));
                writer.Flush();
                writer.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DecryptButtonClick(object sender, EventArgs e)
        {
            try
            {
                var source = File.ReadAllBytes(
                    this.OutputFileTextBox.Text);
                File.Delete(this.OutputFileTextBox.Text);
                var writer = new BinaryWriter(File.OpenWrite(
                    this.OutputFileTextBox.Text));

                var result = new BitArray(source).Xor(new BitArray(this._stream));
                writer.Write(this.BitArrayToByteArray(result));
                writer.Flush();
                writer.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
