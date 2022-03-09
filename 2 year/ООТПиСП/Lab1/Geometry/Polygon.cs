using System;
using System.Drawing;

namespace Geometry
{
    public class Polygon : Shape
    {
        protected Point[] _points;

        public Polygon(params Point[] points) =>
            this._points = points;

        public override void Draw(Graphics g)
        {
            if (this._points.Length <= 0)
                throw new ArgumentException();
            g.DrawPolygon(this._defaultPen, this._points);
        }
    }

    public sealed class Line : Polygon
    {
        public Line(Point start, Point end)
            : base(start, end) { }

        public Line(int x1, int y1, int x2, int y2)
            : this(new Point(x1, y1), new Point(x2, y2)) { }

        public override void Draw(Graphics g) =>
            g.DrawLine(this._defaultPen,
                this._points[0], this._points[1]);
    }

    public sealed class Triangle : Polygon
    {
        public Triangle(Point left, Point center, Point right)
            : base(left, center, right) { }

        public Triangle(int x1, int y1, int x2, int y2, int x3, int y3)
            : this(new Point(x1, y1), new Point(x2, y2), new Point(x3, y3)) { }
    }

    public class Rect : Polygon
    {
        public Rect(Rectangle rect)
            : this(rect.Location, rect.Size) { }

        public Rect(Point point, Size size)
            : this(point, size.Width, size.Height) { }

        public Rect(Point point, int width, int height)
            : base(point, new Point(point.X + width, point.Y),
                  new Point(point.X + width, point.Y + height),
                    new Point(point.X, point.Y + height)) { }
    }

    public sealed class Square : Rect
    {
        public Square(Point point, int side) 
            : base(point, side, side) { }
    }
}