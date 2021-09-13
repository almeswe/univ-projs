using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Laba1.Map;
using Laba1.Player;

namespace Laba1
{
    public partial class Form1 : Form
    {
        Player.Player Player;

        public Form1()
        {
            InitializeComponent();
            this.Player = new Player.Player();
            this.Player.CurrentLevel = new BrownLevel();
            this.Player.SetStart(new PlaceAction("asd", null, PlaceAction.ActionType.Move, new Point(20, 20)));
        }

        private void CanvasOnPaint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            this.Player.Render(e.Graphics);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (char.ToLower((char)e.KeyChar))
            {
                case 'd':
                    this.Player.MoveRight();
                    break;
                case 'a':
                    this.Player.MoveLeft();
                    break;
                case 's':
                    this.Player.MoveDown();
                    break;
                case 'w':
                    this.Player.MoveUp();
                    break;
            }
            this.Canvas.Invalidate();
        }
    }
}
