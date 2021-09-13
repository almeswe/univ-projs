using System.Collections.Generic;
using System.Drawing;

namespace Laba1.Map
{
    public class PlaceAction
    {
        public enum ActionType
        {
            Move,
            Escape,
            Failure,
            SwitchLevelUp,
            SwitchLevelDown,
        }

        public Point Location { get; set; }
        public string Info { get; private set; }
        public ActionType Type { get; private set; }
        public List<PlaceAction> Variants { get; private set; }

        public PlaceAction(string info, List<PlaceAction> variants, ActionType type, Point location)
        {
            this.Info = info;
            this.Type = type;
            this.Location = location;
            this.Variants = variants;
        }

        public override string ToString() => Info;
    }

    public abstract class Level
    {
        public abstract int Index { get; }
        public abstract Color Color { get; }
        public abstract void Render(Graphics g);

        public abstract bool LiftsAreAble { get; }
        public abstract bool StairsIsAble { get; }

        protected void RenderArea(Graphics g)
        {
            for (int x = 1; x <= 20; x++)
                for (int y = 1; y <= 20; y++)
                    if (y == 1 || y == 20)
                        g.DrawRectangle(
                            new Pen(this.Color),
                            new Rectangle(new Point(x*10, y*10), new Size(10, 10))
                        );

            for (int x = 1; x <= 20; x++)
                for (int y = 1; y <= 20; y++)
                    if (x == 1 || x == 20)
                        g.DrawRectangle(
                            new Pen(this.Color),
                            new Rectangle(new Point(x * 10, y * 10), new Size(10, 10))
                        );
        }

        protected void RenderLift(Graphics g, Point location, Color color)
        {
            g.FillRectangle(
                new SolidBrush(color),
                new Rectangle(new Point(location.X + 10, location.Y), new Size(10, 10))
            );
            g.FillRectangle(
                new SolidBrush(color),
                new Rectangle(new Point(location.X + 10, location.Y + 20), new Size(10, 10))
            );
            g.FillRectangle(
                new SolidBrush(color),
                new Rectangle(new Point(location.X, location.Y + 10), new Size(10, 10))
            );
            g.FillRectangle(
                new SolidBrush(color),
                new Rectangle(new Point(location.X + 20, location.Y + 10), new Size(10, 10))
            );
        }

        protected void RenderStairs(Graphics g, Point location, Color color)
        {
            for (int x = 0; x < 3; x++)
                g.FillRectangle(
                    new SolidBrush(color),
                    new Rectangle(new Point(location.X + x * 10, location.Y), new Size(10, 10))
                );
            for (int x = 0; x < 3; x++)
                g.FillRectangle(
                    new SolidBrush(color),
                    new Rectangle(new Point(location.X + x * 10, location.Y + 20), new Size(10, 10))
                );
            g.FillRectangle(
                new SolidBrush(color),
                new Rectangle(new Point(location.X + 10, location.Y + 10), new Size(10, 10))
            );
        }

        protected void RenderCenterArea(Graphics g, Point p)
        {
            for (int x = 5; x <= 10; x++)
                for (int y = 5; y <= 10; y++)
                    if (y == 5 || y == 10)
                        g.DrawRectangle(
                            new Pen(this.Color),
                            new Rectangle(new Point(p.X  + x * 10, p.Y + y * 10), new Size(10, 10))
                        );

            for (int x = 5; x <= 10; x++)
                for (int y = 5; y <= 10; y++)
                    if (x == 5 || x == 10)
                        g.DrawRectangle(
                            new Pen(this.Color),
                            new Rectangle(new Point(p.X + x * 10, p.Y + y * 10), new Size(10, 10))
                        );
        }

        public Level SwitchUp()
        {
            if (this.Index + 1 > 3)
                return this;
            switch (this.Index + 1)
            {
                case 2:
                    return new BlueLevel();
                case 3:
                    return new BrownLevel();
                default:
                    return this;
            }
        }
        public Level SwitchDown()
        {
            if (this.Index - 1 <= 0)
                return this;
            switch (this.Index - 1)
            {
                case 1:
                    return new GreenLevel();
                case 2:
                    return new BlueLevel();
                default:
                    return this;
            }
        }
    }

    public sealed class GreenLevel : Level
    {
        public override int Index => 1;
        public override Color Color => Color.Green;
        
        public override bool LiftsAreAble => true;
        public override bool StairsIsAble => false;

        public void RenderFinish(Graphics g)
        {
            g.FillRectangle(
                new SolidBrush(Color.Crimson),
                new Rectangle(new Point(10, 80), new Size(10, 10))
            );
        }

        public override void Render(Graphics g)
        {
            this.RenderArea(g);
            this.RenderLift(g, new Point(20, 20), Color.LightGreen);
            this.RenderLift(g, new Point(170, 170), Color.LightGreen);

            this.RenderStairs(g, new Point(170, 20), Color.Gray);
            this.RenderStairs(g, new Point(20, 170), Color.Gray);

            this.RenderCenterArea(g, new Point(60, 60)); 

            this.RenderFinish(g);
        }
    }
    
    public sealed class BlueLevel : Level
    {
        public override int Index => 2;
        public override Color Color => Color.Blue;

        public override bool LiftsAreAble => true;
        public override bool StairsIsAble => true;

        public override void Render(Graphics g)
        {
            this.RenderArea(g);
            this.RenderLift(g, new Point(20, 20), Color.LightGreen);
            this.RenderLift(g, new Point(170, 170), Color.LightGreen);

            this.RenderStairs(g, new Point(170, 20), Color.LightGreen);
            this.RenderStairs(g, new Point(20, 170), Color.LightGreen);

            this.RenderCenterArea(g, new Point(40, 40));

        }
    }

    public sealed class BrownLevel : Level
    {
        public override int Index => 3;
        public override Color Color => Color.Brown;

        public override bool LiftsAreAble => false;
        public override bool StairsIsAble => true;

        public override void Render(Graphics g)
        {
            this.RenderArea(g);
            this.RenderLift(g, new Point(20, 20), Color.DarkGray);
            this.RenderLift(g, new Point(170, 170), Color.DarkGray);

            this.RenderStairs(g, new Point(170, 20), Color.LightGreen);
            this.RenderStairs(g, new Point(20, 170), Color.LightGreen);

            this.RenderCenterArea(g, new Point(60, 60));
        }
    }

    public class MapPlace
    {
        public List<PlaceAction> Actions { get; set; }
    }

    public class Map
    {
    }
}