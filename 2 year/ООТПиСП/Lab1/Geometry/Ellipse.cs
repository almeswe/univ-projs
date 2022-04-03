using System;
using System.Drawing;
using System.Collections.Generic;

namespace Geometry
{
    public class Ellipse : Shape
    {
        protected Rectangle _rect;

        public Ellipse(Size scale, Point center) : base() =>
            this._rect = new Rectangle(center, scale) ;

        public Ellipse(int width, int height, Point center) : base() =>
            this._rect = new Rectangle(center, new Size(width, height));

        public Ellipse(Dictionary<string, List<int>> dump) =>
            this.FromDump(dump);

        public override void Draw(Graphics g) =>
            g.DrawEllipse(this._defaultPen, this._rect);

        public override void FromDump(Dictionary<string, List<int>> dump)
        {
            var center = this.GetFieldFromDumpOrThrow(dump, "center");
            var width = this.GetFieldFromDumpOrThrow(dump, "width")[0];
            var height = this.GetFieldFromDumpOrThrow(dump, "height")[0];
            if (center.Count != 2)
                throw new ArgumentException("Center must be specified as (x, y), shape: Ellipse.");
            this._rect = new Rectangle(new Point(center[0], center[1]), 
                new Size(width, height));
        }
    }

    public class Circle : Ellipse
    {
        public Circle(int radius, Point center) 
            : base(radius, radius, center) { }

        public Circle(int radius, int x, int y) 
            : this(radius, new Point(x, y)) { }

        public Circle(Dictionary<string, List<int>> dump) 
            : base(dump) { }

        public override void FromDump(Dictionary<string, List<int>> dump)
        {
            var center = this.GetFieldFromDumpOrThrow(dump, "center");
            var radius = this.GetFieldFromDumpOrThrow(dump, "radius")[0];
            if (center.Count != 2)
                throw new ArgumentException("Center must be specified as (x, y), shape: Circle.");
            this._rect = new Rectangle(new Point(center[0], center[1]),
                new Size(radius, radius));
        }
    }

    public sealed class Dot : Circle
    {
        public Dot(Point point) 
            : base(1, point) { }

        public Dot(int x, int y)
            : this(new Point(x, y)) { }

        public Dot(Dictionary<string, List<int>> dump)
            : base(dump) { }

        public override void FromDump(Dictionary<string, List<int>> dump)
        {
            var center = this.GetFieldFromDumpOrThrow(dump, "center");
            if (center.Count != 2)
                throw new ArgumentException("Center must be specified as (x, y), shape: Dot.");
            this._rect = new Rectangle(new Point(center[0], center[1]), new Size(1, 1));
        }
    }
}
