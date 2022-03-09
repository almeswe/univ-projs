using System;
using System.Drawing;
using System.Windows.Forms;

using Geometry;
using Geometry.ShapeList;

namespace Lab1
{
    public partial class MainForm : Form
    {
        private Shapes _shapes;

        public MainForm()
        {
            this._shapes = new Shapes()
            {
                new Dot(100, 20),
                new Circle(200, new Point(100, 100)),
                new Ellipse(100, 50, new Point(100, 100)),
                new Polygon(new Point(400, 20), new Point(150, 150), new Point(800, 400), new Point(500, 10)),
                new Triangle(new Point(100, 100), new Point(150, 50), new Point(200, 100)),
                new Line(new Point(0, 0), new Point(500, 500)),
                new Rect(new Point(10, 10), 100, 200),
                new Square(new Point(500, 500), 100)
            };
            this.InitializeComponent();
        }

        private void PictureBoxClick(object sender, EventArgs e)
        {
        }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            foreach (var shape in this._shapes)
                shape.Draw(e.Graphics);
        }
    }
}
