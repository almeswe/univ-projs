using System.Drawing;

namespace Geometry
{
    public class Ellipse : Shape
    {
        protected Rectangle _rect;

        public Ellipse(Size scale, Point center) =>
            this._rect = new Rectangle(center, scale);

        public Ellipse(int width, int height, Point center) =>
            this._rect = new Rectangle(center, new Size(width, height));

        public override void Draw(Graphics g) =>
            g.DrawEllipse(this._defaultPen, this._rect);
    }

    public class Circle : Ellipse
    {
        public Circle(int radius, Point center) 
            : base(radius, radius, center) { }

        public Circle(int radius, int x, int y) 
            : this(radius, new Point(x, y)) { }
    }

    public sealed class Dot : Circle
    {
        public Dot(Point point) 
            : base(1, point) { }

        public Dot(int x, int y)
            : this(new Point(x, y)) { }
    }
}
