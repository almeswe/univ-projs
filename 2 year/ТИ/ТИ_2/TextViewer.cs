﻿using System.Windows.Forms;

namespace ТИ_2
{
    public partial class TextViewerForm : Form
    {
        public TextViewerForm(string text)
        {
            InitializeComponent();
            this.TextBox.Text = text;
        }
    }
}
