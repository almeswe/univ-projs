using System.Drawing;
using System.Windows.Forms;
using Laba1.Map;

namespace Laba1.Player
{
    public class Player
    {
        public Level CurrentLevel { get; set; }
        public PlaceAction CurrentAction { get; private set; }

        private int _vel = 10;
        private Color _color = Color.Yellow;
        public Point _position = new Point(50, 50);

        public void SetStart(PlaceAction startPlace) => this.CurrentAction = startPlace;

        public void Render(Graphics g)
        {
            this.CurrentLevel.Render(g);
            g.FillRectangle(
                new SolidBrush(_color),
                new Rectangle(this._position, new Size(10, 10))
            );
        }

        public void MoveRight()
        {
            this._position.X += this._vel;
            this.CheckMovementVariants();
        }

        public void MoveLeft()
        {
                this._position.X -= this._vel;
            this.CheckMovementVariants();
        }

        public void MoveDown()
        {
                this._position.Y += this._vel;
            this.CheckMovementVariants();
        }

        public void MoveUp()
        {
            if (this._position.Y - this._vel < 200)
                this._position.Y -= this._vel;
            this.CheckMovementVariants();
        }

        public void CheckMovementVariants()
        {
            if (this.IsEnteredStairs())
                this.CurrentLevel = this.CurrentLevel.SwitchDown();
            else if (this.IsEnteredLift())
                this.CurrentLevel = this.CurrentLevel.SwitchUp();
            else if (this.IsEnteredFinish() && this.CurrentLevel.Index == 1)
            {
                MessageBox.Show("Congratulations!");
                Application.Exit();
            }
        }
        private bool IsEnteredLift()
        {
            if (!this.CurrentLevel.LiftsAreAble)
               return false;
            else
            {
                if (this._position == new Point(30, 30) ||
                    this._position == new Point(180, 180))
                    return true;
                else
                    return false;
            }    
        }
        private bool IsEnteredStairs()
        {
            if (!this.CurrentLevel.StairsIsAble)
                return false;
            else
            {
                if (this._position == new Point(30, 180) ||
                    this._position == new Point(180, 30))
                    return true;
                else
                    return false;
            }
        }
        private bool IsEnteredFinish() => this._position == new Point(10, 80);
    }
}
