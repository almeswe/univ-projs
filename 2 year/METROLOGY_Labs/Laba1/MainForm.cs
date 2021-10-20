using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using Translator.JsTranslator.Lexer;
using Translator.JsTranslator.Lexer.Input;
using Translator.JsTranslator.Exceptions;

namespace Laba1
{
    public partial class MainForm : Form
    {
        private bool _darkTheme;

        public MainForm()
        {
            this._darkTheme = false;
            InitializeComponent();
        }

        private void BrowseFileButtonClick(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.CodeTextBox.Clear();
                this.CodeTextBox.Text = File.ReadAllText(this.OpenFileDialog.FileName);
            }
        }

        private void IncreaseFontButtonClick(object sender, EventArgs e) =>
            this.CodeTextBox.Font = new Font(this.CodeTextBox.Font.FontFamily.Name, this.CodeTextBox.Font.Size + 1);

        private void DecreaseFontButtonClick(object sender, EventArgs e) =>
            this.CodeTextBox.Font = new Font(this.CodeTextBox.Font.FontFamily.Name, this.CodeTextBox.Font.Size - 1);

        private void SwitchThemeButtonClick(object sender, EventArgs e)
        {
            this._darkTheme = !this._darkTheme;
            this.CodeTextBox.BackColor = this._darkTheme ?
                Color.FromArgb(32, 32, 32) : Color.White;
            this.CodeTextBox.ForeColor = this._darkTheme ?
                Color.Silver : Color.Black;
            this.CodeTextBox.Invalidate();
        }

        private void AnalyzeButtonClick(object sender, EventArgs e)
        {
            try
            {
                var lexer = new Lexer(new StringInput(this.CodeTextBox.Text + "\0"));
                var tokens = lexer.Tokenize();
                var metrics = new MetricsAnalyzer(tokens);

                int i = 0;
                string a = string.Empty;
                foreach (var op in metrics.Operators)
                {
                    i++;
                    a += $"[{i}] {op.Key} : {op.Value}\n";
                }
                foreach (var opd in metrics.Operands)
                {
                    i++;
                    a += $"[{i}] {opd.Key} : {opd.Value}\n";
                }
                File.WriteAllText("test.txt", a);
                return;

            }
            catch (TranslatorException te)
            {
                MessageBox.Show($"Tokenizing error occured!\nMessage: {te}", "Translator Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
