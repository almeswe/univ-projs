using System;
using System.Drawing;
using System.Windows.Forms;

using GeometryScript.FrontEnd.Input;
using GeometryScript.FrontEnd.Tokenizer;
using GeometryScript.FrontEnd.Script.Parser;

namespace GeometryInterface
{
    public partial class Interface : Form
    {
        private bool _scriptExecution = false;

        public Interface() =>
            this.InitializeComponent();

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            try
            {
                if (this._scriptExecution)
                    this.EvaluateScript(e.Graphics);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        private void EvaluateScript(Graphics graphics)
        {
            var tokenizer = new Tokenizer(
                new StringInput(this.ScriptTextBox.Text));
            new ScriptParser(tokenizer.Tokens)
                .Script.RenderOn(graphics);
            this._scriptExecution = false;
        }

        private void ShowError(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, 
                MessageBoxIcon.Error);

        private void SetScriptText()
        {
            try
            {
                if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
                    this.ScriptTextBox.Text = System.IO.File
                        .ReadAllText(this.OpenFileDialog.FileName);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        private void LoadScriptButtonClick(object sender, EventArgs e) =>
            this.SetScriptText();

        private void EvaluateButtonClick(object sender, EventArgs e)
        {
            this._scriptExecution = true;
            this.PictureBox.Invalidate();
        }
    }
}