using System.Numerics;

namespace Renderer
{
	public static class ViewPort
	{
		private static int _w = 500;
		private static int _h = 500;

		public static int Width 
		{ 
			get => _w; 
		}
		
		public static int Height 
		{
			get => _h; 
		}

		public static float Aspect => _w / (float)_h;

		public static void UpdateWidth(int w)
		{
			_w = w;
		}

		public static void UpdateHeight(int h)
		{
			_h = h;
		}

		public static Matrix4x4 CreateOrtho()
		{
			return new Matrix4x4
			(
				2.0f / _w, 0.0f, 0.0f, 0.0f,
				0.0f, 2.0f / _h, 0.0f, 0.0f,
				0.0f, 0.0f, 1.0f / (Camera.Near - Camera.Far), 0.0f,
				0.0f, 0.0f, Camera.Near / (Camera.Near - Camera.Far), 1.0f 
			);
		}

		public static Matrix4x4 CreatePerspective()
		{
			return Matrix4x4.CreatePerspectiveFieldOfView(Camera.Fov, Aspect, Camera.Near, Camera.Far);
		}

		public static Matrix4x4 CreateTranslation()
		{
			return new Matrix4x4
			(
				_w / 2.0f,  0.0f, 0.0f, 0.0f,
				0.0f, -_h / 2.0f, 0.0f, 0.0f,
				0.0f, 0.0f, 1.0f, 0.0f,
				_w / 2.0f, _h / 2.0f, 0.0f, 1.0f
			);
		}
	}
}