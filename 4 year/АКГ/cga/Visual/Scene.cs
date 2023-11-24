using Renderer;
using Parser.Core;
using Parser.Interface;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Visual
{
	public partial class SceneForm : Form
	{
		private IObjectFile _objectFile;
		private IObjectParser _objectParser;

		private Vector3 _objectPosition;
		private Vector2 _objectRotation;
		private List<Vector4> _objectFileVectors;

		private Color _lineColor = Color.Gray;
		private Color _sceneColor = Color.FromArgb(0, 49, 83);

		private float _movingFactor = 0.01f;
		private float _rotationFactor = 0.05f;
		private float _scaleFactor = 0.05f;
		private float _scaleFactorStep => 0.005f;

		private int _bitmapBytesPerPixel = 0;
		private BitmapData _bitmapData = null;
		private Rectangle _bitmapRect => new Rectangle(0, 0, ViewPort.Width, ViewPort.Height);

		public SceneForm(string path)
		{
			this.InitializeComponent();
			this._objectParser = new ObjectParser();
			this._objectFile = this._objectParser.Parse(path);
			this._objectRotation = new Vector2(0.0f, 0.0f);
			this._objectPosition = new Vector3(0.0f, 0.0f, 0.0f);
			this._objectFileVectors = new List<Vector4>(this._objectFile.Vertices.Count);
			this.Size = new Size(500, 500);
			this.SizeChanged += (o, s) => ViewPort.UpdateWidth(this.Width);
			this.SizeChanged += (o, s) => ViewPort.UpdateHeight(this.Height);
			this.DoubleBuffered = true;
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}
		
		private Matrix4x4 CreateSrMatrix()
		{
			var scaleMatrix = World.CreateScale(this._scaleFactor);
			var rotateXMatrix = World.CreateRotationX(this._objectRotation.X);
			var rotateYMatrix = World.CreateRotationY(this._objectRotation.Y);
			return scaleMatrix * rotateXMatrix * rotateYMatrix;
		}

		private Matrix4x4 CreatePvmMatrix()
		{
			var projectionMatrix = ViewPort.CreatePerspective();
			var viewMatrix = Camera.CreateTranslation(); 
			var modelMatrix = World.CreateTranslation(this._objectPosition);
			return modelMatrix * viewMatrix * projectionMatrix;
		}

		private Matrix4x4 CreateViewPortMatrix()
		{
			return ViewPort.CreateTranslation();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void DrawPixel(Point at)
		{
			if (at.X > 0 && 
				at.Y > 0 && 
				this._bitmapData.Width > at.X && 
				this._bitmapData.Height > at.Y)
			{
				byte* pixel = (byte*)(
					this._bitmapData.Scan0 +
					(at.Y * this._bitmapData.Stride +
					 at.X * this._bitmapBytesPerPixel)
				);
				pixel[0] = Color.Gray.R;
				pixel[1] = Color.Gray.G;
				pixel[2] = Color.Gray.B;
				pixel[3] = Color.Gray.A;
			}
		}

		private void DrawDDALine(Bitmap bitmap, Vector4 start, Vector4 end)
		{
			var at = new PointF(start.X, start.Y);
			var l = float.Max(Math.Abs(start.X - end.X), Math.Abs(start.Y - end.Y));
			for (int i = 0; i < (int)l; i++)
			{
				at.X += (end.X - start.X) / l;
				at.Y += (end.Y - start.Y) / l;
				this.DrawPixel(new Point(Convert.ToInt32(Math.Round(at.X)), Convert.ToInt32(Math.Round(at.Y))));
			}
		}

		private Bitmap DrawScene()
		{
			var start = DateTime.Now;
			this.UpdateVectors();
			var bitmap = new Bitmap(ViewPort.Width, ViewPort.Height);
			this._bitmapData = bitmap.LockBits(this._bitmapRect, 
				ImageLockMode.ReadWrite, bitmap.PixelFormat);
			this._bitmapBytesPerPixel = Image.GetPixelFormatSize(this._bitmapData.PixelFormat) / 8;
			foreach (var p in this._objectFile.Polygons)
			{
				var angles = p.Arguments.Count;
				for (var i = 0; i < angles; i++)
				{
					var startIndex = p.Arguments.ElementAt(i).Item1;
					var endIndex = p.Arguments.ElementAt((i + 1) % angles).Item1;
					var startPoint = this._objectFileVectors[startIndex - 1];
					var endPoint = this._objectFileVectors[endIndex - 1];
					this.DrawDDALine(bitmap, startPoint, endPoint);
				}
			}
			unchecked
			{
				this.Text = (1000 / ((int)(DateTime.Now - start).TotalMilliseconds + 1)).ToString();
			}
			bitmap.UnlockBits(this._bitmapData);
			return bitmap;
		}

		private void UpdateVectors()
		{
			this._objectFileVectors.Clear();
			var sr = this.CreateSrMatrix();
			var pvm = sr * this.CreatePvmMatrix();
			var vpm = this.CreateViewPortMatrix();
			for (var i = 0; i < this._objectFile.Vertices.Count; i++)
			{
				var vector = this._objectFile.Vertices.ElementAt(i);
				var transformed = Vector4.Transform(vector, pvm);
				transformed /= transformed.W;
				transformed = Vector4.Transform(transformed, vpm);
				this._objectFileVectors.Add(transformed);
			}
		}

		private void OnScenePaint(object sender, PaintEventArgs e)
		{
			var graphics = e.Graphics;
			var scene = this.DrawScene();
			graphics.Clear(this._sceneColor);
			graphics.DrawImage(scene, Point.Empty);
		}

		private void OnSceneScroll(ref Message m)
		{
			int scrollValue = (short)(m.WParam >> 16);
			this._scaleFactor += scrollValue > 0 ? 
				this._scaleFactorStep : -this._scaleFactorStep;
		}

		protected override void WndProc(ref Message m)
		{
			const int wmMouseWheel = 0x020A;
			if (m.Msg == wmMouseWheel)
				this.OnSceneScroll(ref m);
			base.WndProc(ref m);
		}

		public void OnSceneKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Left:
					this._objectRotation.Y -= this._rotationFactor;
					break;
				case Keys.Right:
					this._objectRotation.Y += this._rotationFactor;
					break;
				case Keys.Up: 
					this._objectRotation.X += this._rotationFactor;
					break;
				case Keys.Down:
					this._objectRotation.X -= this._rotationFactor;
					break;
				case Keys.W:
					this._objectPosition.Y += this._movingFactor;
					break;
				case Keys.S:
					this._objectPosition.Y -= this._movingFactor;
					break;
				case Keys.A:
					this._objectPosition.X -= this._movingFactor;
					break;
				case Keys.D:
					this._objectPosition.X += this._movingFactor;
					break;
			}
		}

		private void SceneTimerTick(object sender, EventArgs e)
		{
			this.Invalidate();
		}
	}
}