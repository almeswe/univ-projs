using Renderer;
using Parser.Core;
using Parser.Interface;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Drawing.Imaging;
using System.Buffers;

namespace Visual
{
	public partial class SceneForm : Form
	{
		private IObjectFile _objectFile;
		private IObjectParser _objectParser;

		private Vector3 _objectPosition;
		private Vector2 _objectRotation;
		private List<Vector4> _objectFileVectices;

		private Color _objectColor = Color.Red;
		private Color _sceneColor = Color.FromArgb(0, 49, 83);

		private float _movingFactor = 0.05f;
		private float _rotationFactor = 0.10f;
		private float _scaleFactor = 0.05f;
		private float _scaleFactorStep => 0.005f + this._scaleFactor / 10;

		#region Bitmap
		private int _bitmapBytesPerPixel = 0;
		private BitmapData _bitmapData = null;
		private Rectangle _bitmapRect => new Rectangle(0, 0, ViewPort.Width, ViewPort.Height);
		#endregion

		private float[] _zBuffer = null;
		
		private int _objectRenderTime = 0;
		private long _objectTriangles = 0;
		private long _objectTrianglesRendered = 0;

		private Font _textFont = new Font("Consolas", 7);
		private SolidBrush _textBrush = new SolidBrush(Color.White);

		public SceneForm(string path)
		{
			this.InitializeComponent();
			this._objectParser = new ObjectParser();
			this._objectFile = this._objectParser.Parse(path);
			this._objectRotation = new Vector2(0.0f, 0.0f);
			this._objectPosition = Camera.Target;
			this._objectFileVectices = new List<Vector4>(this._objectFile.Vertices.Count);
			this._objectTriangles = this._objectFile.Polygons.Sum(p => p.Triangles);
			this.Size = new Size(500, 500);
			this.SizeChanged += (o, s) => ViewPort.UpdateWidth(this.Width);
			this.SizeChanged += (o, s) => ViewPort.UpdateHeight(this.Height);
			this.SizeChanged += (o, s) => this.CreateZBuffer();
			this.CreateZBuffer();
			this.DoubleBuffered = true;
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}
		
		private void CreateZBuffer()
		{
			if (this._zBuffer != null)
				ArrayPool<float>.Shared.Return(this._zBuffer);
			var zBufferCapacity = ViewPort.Width * ViewPort.Height;
			this._zBuffer = ArrayPool<float>.Shared.Rent(zBufferCapacity);
			this.ClearZBuffer();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ClearZBuffer()
		{
			var zBufferCapacity = ViewPort.Width * ViewPort.Height;
			Array.Fill(this._zBuffer, Camera.Far, 0, zBufferCapacity);
		}

		private Matrix4x4 CreateSrMatrix()
		{
			var scaleMatrix = World.CreateScale(this._scaleFactor);
			var rotateXMatrix = World.CreateRotationX(this._objectRotation.X);
			var rotateYMatrix = World.CreateRotationY(this._objectRotation.Y);
			return scaleMatrix * rotateXMatrix * rotateYMatrix;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private Matrix4x4 CreateVmMatrix()
		{
			var viewMatrix = Camera.CreateTranslation(); 
			var modelMatrix = World.CreateTranslation(this._objectPosition);
			return modelMatrix * viewMatrix;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private Matrix4x4 CreateProjectionMatrix()
		{
			return ViewPort.CreatePerspective();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private Matrix4x4 CreateViewPortMatrix()
		{
			return ViewPort.CreateTranslation();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe float GetZBufferValue(Point at)
		{
			var index = at.Y * ViewPort.Width + at.X;
			return this._zBuffer[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void SetZBufferValue(Point at, float value)
		{
			var index = at.Y * ViewPort.Width + at.X;
			this._zBuffer[index] = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe bool ShouldBeCulled(Vector4 v1, Vector4 v2, Vector4 v3)
		{
			var e1 = new Vector3(v2.X - v1.X, v2.Y - v1.Y, v2.Z - v1.Z);
			var e2 = new Vector3(v3.X - v1.X, v3.Y - v1.Y, v3.Z - v1.Z);
			var n = Vector3.Cross(e1, e2);
			var v = new Vector3(v1.X, v1.Y, v1.Z) - Camera.Eye;
			return Vector3.Dot(n, v) > 0;
		}

		private float GetZInsideTriangle(Vector4 v1, Vector4 v2, Vector4 v3, Point p)
		{
			var det = (v2.Z - v3.Z) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y);
			var l1 = ((v2.Z - v3.Z) * (p.X - v3.X) + (v3.X - v2.X) * (p.Y - v3.Y)) / det;
			var l2 = ((v3.Z - v1.Z) * (p.X - v3.X) + (v1.X - v3.X) * (p.Y - v3.Y)) / det;
			var l3 = 1.0f - l1 - l2;
			return l1 * v1.Z + l2 * v2.Z + l3 * v3.Z;
		}

		private bool IsPointInTriangle(Point p, Vector4 v1, Vector4 v2, Vector4 v3)
		{
			// Вычисление барицентрических координат
			float alpha = ((v2.Y - v3.Y) * (p.X - v3.X) + (v3.X - v2.X) * (p.Y - v3.Y)) /
						  ((v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y));
			float beta = ((v3.Y - v1.Y) * (p.X - v3.X) + (v1.X - v3.X) * (p.Y - v3.Y)) /
						 ((v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y));
			float gamma = 1.0f - alpha - beta;

			// Проверка, находится ли точка внутри треугольника
			return alpha >= 0 && beta >= 0 && gamma >= 0;
		}

		private unsafe bool Rasterize(Vector4 v1, Vector4 v2, Vector4 v3, float intensity)
		{
			var sx1 = v1.X;
			var sx2 = v2.X;
			var sx3 = v3.X;
			var sy1 = v1.Y;
			var sy2 = v2.Y;
			var sy3 = v3.Y;
			
			var xmax = sx1 > sx2 ? (sx1 > sx3 ? sx1 : sx3) : (sx2 > sx3 ? sx2 : sx3);
			var ymax = sy1 > sy2 ? (sy1 > sy3 ? sy1 : sy3) : (sy2 > sy3 ? sy2 : sy3);
			var xmin = sx1 < sx2 ? (sx1 < sx3 ? sx1 : sx3) : (sx2 < sx3 ? sx2 : sx3);
			var ymin = sy1 < sy2 ? (sy1 < sy3 ? sy1 : sy3) : (sy2 < sy3 ? sy2 : sy3);

			var box = new Rectangle((int)xmin, (int)ymin, 
				(int)(xmax - xmin) + 1, (int)(ymax - ymin) + 1);

			var rasterized = false;

			for (var y = box.Y; y <= box.Bottom; y++)
			{
				for (var x = box.X; x <= box.Right; x++)
				{
					// check if inside viewport
					if (x < 0 || y < 0 || 
						this._bitmapData.Width <= x || 
						this._bitmapData.Height <= y)
						continue;
					// check if point inside triangle
					var p = new Point(x, y);
					if (!this.IsPointInTriangle(p, v1, v2, v3))
						continue;
					// z-buffer logic
					var bZIndex = this.GetZBufferValue(p);
					var pZIndex = this.GetZInsideTriangle(v1, v2, v3, p);
					if (pZIndex > bZIndex)
						continue;
					this.SetZBufferValue(p, pZIndex);
					rasterized = true;
					this.DrawPixel(p, intensity);
				}
			}
			return rasterized;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void DrawPixel(Point at, float intensity)
		{
			byte* pixel = (byte*)(
				this._bitmapData.Scan0 +
				(at.Y * this._bitmapData.Stride +
					at.X * this._bitmapBytesPerPixel)
			);
			pixel[0] = (byte)(this._objectColor.B * intensity);
			pixel[1] = (byte)(this._objectColor.G * intensity);
			pixel[2] = (byte)(this._objectColor.R * intensity);
			pixel[3] = this._objectColor.A;
		}

		private void DrawDDALine(Vector4 v1, Vector4 v2, float intensity)
		{
			var at = new PointF(v1.X, v1.Y);
			var l = float.Max(Math.Abs(v1.X - v2.X), Math.Abs(v1.Y - v2.Y));
			for (int i = 0; i < (int)l; i++)
			{
				at.X += (v2.X - v1.X) / l;
				at.Y += (v2.Y - v1.Y) / l;
				var p = new Point(
					Convert.ToInt32(Math.Round(at.X)),
					Convert.ToInt32(Math.Round(at.Y))
				);
				if (p.X < 0 || p.Y < 0 ||
					this._bitmapData.Width <= p.X ||
					this._bitmapData.Height <= p.Y)
					continue;

				//var bZIndex = this.GetZBufferValue(p);
				//var pZIndex = this.GetZInsideTriangle(v1, v2, v3, p);
				//if (pZIndex > bZIndex)
				//	continue;
				//this.SetZBufferValue(p, pZIndex);
				this.DrawPixel(p, intensity);
			}
		}

		private float GetIntensity(Vector4 v1, Vector4 v2, Vector4 v3)
		{
			//return 1.0f;
			var e1 = new Vector3(v2.X - v1.X, v2.Y - v1.Y, v2.Z - v1.Z);
			var e2 = new Vector3(v3.X - v1.X, v3.Y - v1.Y, v3.Z - v1.Z);
			var n = -Vector3.Cross(e1, e2);
			var f1 = World.Light.Compute(n, new Vector3(v1.X, v1.Y, v1.Z));
			var f2 = World.Light.Compute(n, new Vector3(v2.X, v2.Y, v2.Z));
			var f3 = World.Light.Compute(n, new Vector3(v3.X, v3.Y, v3.Z));
			return (f1 + f2 + f3) / 3.0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		private unsafe void SortVerticesClockwise(ref Vector4 v1, ref Vector4 v2, ref Vector4 v3)
		{
			var center = (v1 + v2 + v3) / 3;
			var vertices = stackalloc Vector4[] { v1, v2, v3 };
			for (var i = 0; i < 3; i++)
				for (var j = i + 1; j < 3; j++)
					if (MathF.Atan2(vertices[i].Y - center.Y, vertices[i].X - center.X) >
						MathF.Atan2(vertices[j].Y - center.Y, vertices[j].X - center.X))
						(vertices[i], vertices[j]) = (vertices[j], vertices[i]);
			v1 = vertices[0];
			v2 = vertices[1];
			v3 = vertices[2];
			////// Вычисляем центр треугольника
			//var center = (v1 + v2 + v3) / 3;
			//// Создаем список вершин
			//var vertices = new List<Vector4> { v1, v2, v3 };
			//// Сортируем вершины по углу относительно центра
			//vertices.Sort((a, b) =>
			//{
			//	float aAngle = (float)Math.Atan2(a.Y - center.Y, a.X - center.X);
			//	float bAngle = (float)Math.Atan2(b.Y - center.Y, b.X - center.X);

			//	return aAngle.CompareTo(bAngle);
			//});
			//// Обновляем вершины
			//v1 = vertices[0];
			//v2 = vertices[1];
			//v3 = vertices[2];
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		private unsafe void SortVerticesCounterClockwise(ref Vector4 v1, ref Vector4 v2, ref Vector4 v3)
		{
			var center = (v1 + v2 + v3) / 3;
			var vertices = stackalloc Vector4[] { v1, v2, v3 };
			for (var i = 0; i < 3; i++)
				for (var j = i + 1; j < 3; j++)
					if (MathF.Atan2(vertices[i].Y - center.Y, vertices[i].X - center.X) <
						MathF.Atan2(vertices[j].Y - center.Y, vertices[j].X - center.X))
						(vertices[i], vertices[j]) = (vertices[j], vertices[i]);
			v1 = vertices[0];
			v2 = vertices[1];
			v3 = vertices[2]; 
		}

		private Bitmap DrawScene()
		{
			var start = DateTime.Now;
			this.ClearZBuffer();
			this.UpdateVertices();
			this._objectTrianglesRendered = 0;
			var bitmap = new Bitmap(ViewPort.Width, ViewPort.Height);
			this._bitmapData = bitmap.LockBits(this._bitmapRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
			this._bitmapBytesPerPixel = Image.GetPixelFormatSize(this._bitmapData.PixelFormat) / 8;
			unchecked
			{
				for (var i = 0; i < this._objectFile.Polygons.Count; i++)
				{
					var p = this._objectFile.Polygons.ElementAt(i);
					var angles = p.Arguments.Count;
					for (var a = 1; !p.Culled && a <= angles - 2; a++)
					{
						var v1 = this._objectFileVectices[p.Arguments.ElementAt(0).Item1 - 1];
						var v2 = this._objectFileVectices[p.Arguments.ElementAt((a + 0) % angles).Item1 - 1];
						var v3 = this._objectFileVectices[p.Arguments.ElementAt((a + 1) % angles).Item1 - 1];
						//this.SortVerticesClockwise(ref v1, ref v2, ref v3);
						//var v12 = v2 - v1;
						//var v13 = v3 - v1;
						//var res = v12.X * v13.Y - v12.Y * v13.X;
						//if (this.ShouldBeCulled(v1, v2, v3))
						//	continue;
						if (this.Rasterize(v1, v2, v3, this.GetIntensity(v1, v2, v3)))
							this._objectTrianglesRendered++;
					}
				}
			}
			//for (var i = 0; i < this._objectFile.Polygons.Count; i++)
			//{
			//	var p = this._objectFile.Polygons.ElementAt(i);
			//	var angles = p.Arguments.Count;
			//	for (var a = 1; !p.Culled && a <= angles - 2; a++)
			//	{
			//		var v1 = this._objectFileVectices[p.Arguments.ElementAt(0).Item1 - 1];
			//		var v2 = this._objectFileVectices[p.Arguments.ElementAt((a + 0) % angles).Item1 - 1];
			//		var v3 = this._objectFileVectices[p.Arguments.ElementAt((a + 1) % angles).Item1 - 1];
			//		//this.SortVerticesCounterClockwise(ref v1, ref v2, ref v3);
			//		//var v12 = v2 - v1;
			//		//var v13 = v3 - v1;
			//		//var res = v12.X * v13.Y - v12.Y * v13.X;
			//		//if (this.ShouldBeCulled(v1, v2, v3))
			//		//	continue;
			//		var intensity = this.GetIntensity(v1, v2, v3);
			//		if (a == 1)
			//			this.DrawDDALine(v1, v2, intensity);
			//		this.DrawDDALine(v1, v3, intensity);
			//		this.DrawDDALine(v2, v3, intensity);
			//		this._objectTrianglesRendered++;
			//	}
			//}
			unchecked
			{
				this._objectRenderTime = (int)(DateTime.Now - start).TotalMilliseconds;
			}
			bitmap.UnlockBits(this._bitmapData);
			return bitmap;
		}

		private void UpdateVertices()
		{
			this._objectFileVectices.Clear();
			var sr = this.CreateSrMatrix();
			var vm = sr * this.CreateVmMatrix();
			for (var i = 0; i < this._objectFile.Vertices.Count; i++)
			{
				var vector = this._objectFile.Vertices.ElementAt(i);
				var transformed = Vector4.Transform(vector, vm);
				this._objectFileVectices.Add(transformed);
			}
			for (var i = 0; i < this._objectFile.Polygons.Count; i++)
			{
				var p = this._objectFile.Polygons.ElementAt(i);
				var angles = p.Arguments.Count;
				for (var a = 1; a <= angles - 2; a++)
				{
					var v1 = this._objectFileVectices[p.Arguments.ElementAt(0).Item1 - 1];
					var v2 = this._objectFileVectices[p.Arguments.ElementAt((a + 0) % angles).Item1 - 1];
					var v3 = this._objectFileVectices[p.Arguments.ElementAt((a + 1) % angles).Item1 - 1];
					//this.SortVerticesCounterClockwise(ref v1, ref v2, ref v3);
					if (p.Culled = this.ShouldBeCulled(v1, v2, v3))
						break;
				}
			}
			var pm = this.CreateProjectionMatrix();
			var vpm = this.CreateViewPortMatrix();
			for (var i = 0; i < this._objectFile.Vertices.Count; i++)
			{
				var vector = this._objectFileVectices[i];
				var transformed = Vector4.Transform(vector, pm);
				transformed /= transformed.W;
				transformed = Vector4.Transform(transformed, vpm);
				this._objectFileVectices[i] = transformed;
			}
		}

		private void DrawText(Graphics g, params string[] lines)
		{
			var y = 0.0f;
			for (var i = 0; i < lines.Length; i++, y += 10)
				g.DrawString(lines[i], this._textFont, this._textBrush, new PointF(0, y));
		}

		private void OnScenePaint(object sender, PaintEventArgs e)
		{
			var graphics = e.Graphics;
			var scene = this.DrawScene();
			graphics.Clear(this._sceneColor);
			graphics.DrawImage(scene, Point.Empty);
			this.DrawText(graphics,
				$"x: {this._objectPosition.X}, " +
				$"y: {this._objectPosition.Y}, " + 
				$"z: {this._objectPosition.Z}",
				$"scale: {this._scaleFactor}",
				$"triangles: {this._objectTriangles}",
				$"rendered: {this._objectTrianglesRendered}",
				$"frame rendered: {this._objectRenderTime}ms"
			);
		}

		private void OnSceneScroll(ref Message m)
		{
			const int viKeyMkControl = 0x0008;
			int scrollValue = (short)(m.WParam >> 16);
			int viKeysValue = (short)(m.WParam & 0xff);
			if (viKeysValue == viKeyMkControl)
			{
				this._scaleFactor += scrollValue > 0 ?
					this._scaleFactorStep : -this._scaleFactorStep;
				this._scaleFactor = float.Max(0.05f, this._scaleFactor);
			}
			else
			{
				this._objectPosition.Z += scrollValue > 0 ?
					this._movingFactor : -this._movingFactor;
			}
		}

		protected override void WndProc(ref Message m)
		{
			const int wmMouseWheel = 0x020A;
			if (m.Msg == wmMouseWheel)
				this.OnSceneScroll(ref m);
			base.WndProc(ref m);
		}

		private void OnSceneKeyDown(object sender, KeyEventArgs e)
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