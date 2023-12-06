namespace Visual
{
	public class TextureMap
	{
		public string Path { get; private set; }
		public Bitmap Bitmap { get; private set; }

		private BitmapData _bitmapData;
		private int _bitmapBytesPerPixel;

		public TextureMap(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException();
			this.Path = path;
			this.Bitmap = Image.FromFile(path) as Bitmap;
			var rect = new Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height);
			this._bitmapData = this.Bitmap.LockBits(rect, ImageLockMode.ReadOnly, this.Bitmap.PixelFormat);
			this._bitmapBytesPerPixel = Image.GetPixelFormatSize(this._bitmapData.PixelFormat) / 8;
		}

		~TextureMap()
		{
			this.Bitmap.UnlockBits(this._bitmapData);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Vector3 GetPixelAsNormal(Vector3 at)
		{
			var color = this.GetPixel(at);
			return 2 * (new Vector3(color.R, color.G, color.B) / 255.0f) - Vector3.One;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetPixelAsSpecular(Vector3 at)
		{
			var color = this.GetPixel(at);
			return (color.R + color.G + color.B) / 3.0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Color GetPixel(Vector3 at)
		{
			var atX = Convert.ToInt32(at.X * (this._bitmapData.Width - 1));
			var atY = Convert.ToInt32((1 - at.Y) * (this._bitmapData.Height - 1));
			var pixel = (int*)(
				this._bitmapData.Scan0 +
				(atY * this._bitmapData.Stride +
					atX * this._bitmapBytesPerPixel)
			);
			return Color.FromArgb(*pixel);
		}
	}
}
