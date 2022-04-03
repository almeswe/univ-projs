using System;
using System.Drawing;
using System.Collections.Generic;

namespace Geometry
{
    public class Polygon : Shape
    {
        protected Point[] _points;

        public Polygon(params Point[] points) : base() =>
            this._points = points;

        public Polygon(Dictionary<string, List<int>> dump) =>
            this.FromDump(dump);

        public override void Draw(Graphics g)
        {
            if (this._points.Length <= 0)
                throw new ArgumentException();
            g.DrawPolygon(this._defaultPen, this._points);
        }

        public override void FromDump(Dictionary<string, List<int>> dump)
        {
            var xCoordinates = this.GetFieldFromDumpOrThrow(dump, "x");
            var yCoordinates = this.GetFieldFromDumpOrThrow(dump, "y");
            if (xCoordinates.Count != yCoordinates.Count)
                throw new ArgumentException("Cannot create pairs of coordinates, shape: polygon.");
            var points = new List<Point>();
            for (int i = 0; i < xCoordinates.Count; i++)
                points.Add(new Point(xCoordinates[i], yCoordinates[i]));
            this._points = points.ToArray();
        }
    }

    public sealed class Line : Polygon
    {
        public Line(Point start, Point end)
            : base(start, end) { }

        public Line(int x1, int y1, int x2, int y2)
            : this(new Point(x1, y1), new Point(x2, y2)) { }

        public Line(Dictionary<string, List<int>> dump) 
            : base(dump) { }

        public override void Draw(Graphics g) =>
            g.DrawLine(this._defaultPen,
                this._points[0], this._points[1]);

        public override void FromDump(Dictionary<string, List<int>> dump)
        {
            var xCoordinates = this.GetFieldFromDumpOrThrow(dump, "x");
            var yCoordinates = this.GetFieldFromDumpOrThrow(dump, "y");
            if (xCoordinates.Count != 2 || yCoordinates.Count != 2)
                throw new ArgumentException("Should be two pairs of (x, y) coordinates, shape: line.");
            base.FromDump(dump);
        }
    }

    public sealed class Triangle : Polygon
    {
        public Triangle(Point left, Point center, Point right)
            : base(left, center, right) { }

        public Triangle(int x1, int y1, int x2, int y2, int x3, int y3)
            : this(new Point(x1, y1), new Point(x2, y2), new Point(x3, y3)) { }

        public Triangle(Dictionary<string, List<int>> dump)
            : base(dump) { }

        public override void FromDump(Dictionary<string, List<int>> dump)
        {
            var xCoordinates = this.GetFieldFromDumpOrThrow(dump, "x");
            var yCoordinates = this.GetFieldFromDumpOrThrow(dump, "y");
            if (xCoordinates.Count != 3 || yCoordinates.Count != 3)
                throw new ArgumentException("Should be three pairs of (x, y) coordinates, shape: line.");
            base.FromDump(dump);
        }
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

        public Rect(Dictionary<string, List<int>> dump)
            : base(dump) { }

        public override void FromDump(Dictionary<string, List<int>> dump)
        {
            var xCoordinates = this.GetFieldFromDumpOrThrow(dump, "x");
            var yCoordinates = this.GetFieldFromDumpOrThrow(dump, "y");
            if (xCoordinates.Count != 1 || yCoordinates.Count != 1)
                throw new ArgumentException("Upper left rect point must be specified, shape: rect.");
            var width = this.GetFieldFromDumpOrThrow(dump, "width")[0];
            var height = this.GetFieldFromDumpOrThrow(dump, "height")[0];
            var point = new Point(xCoordinates[0], yCoordinates[0]);
            this._points = new Point[]
            {
                point, new Point(point.X + width, point.Y),
                  new Point(point.X + width, point.Y + height),
                    new Point(point.X, point.Y + height)
            };
        }
    }

    public sealed class Square : Rect
    {
        public Square(Point point, int side) 
            : base(point, side, side) { }

        public Square(Dictionary<string, List<int>> dump)
            : base(dump) { }

        public override void FromDump(Dictionary<string, List<int>> dump)
        {
            var xCoordinates = this.GetFieldFromDumpOrThrow(dump, "x");
            var yCoordinates = this.GetFieldFromDumpOrThrow(dump, "y");
            if (xCoordinates.Count != 1 || yCoordinates.Count != 1)
                throw new ArgumentException("Upper left rect point must be specified, shape: rect.");
            var side = this.GetFieldFromDumpOrThrow(dump, "side")[0];
            var point = new Point(xCoordinates[0], yCoordinates[0]);
            this._points = new Point[]
            {
                point, new Point(point.X + side, point.Y),
                  new Point(point.X + side, point.Y + side),
                    new Point(point.X, point.Y + side)
            };
        }
    }
}