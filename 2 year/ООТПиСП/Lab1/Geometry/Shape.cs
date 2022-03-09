using System;
using System.Drawing;

namespace Geometry
{
    public abstract class Shape
    {
        protected Pen _defaultPen => new Pen(Color.Black);

        public virtual void Draw(Graphics g) =>
            throw new NotImplementedException();
    }
}
