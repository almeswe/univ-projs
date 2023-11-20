using System.Numerics;

namespace Renderer
{
	public static class World
	{
		public static Vector3 XAxis { get; private set; }
		public static Vector3 YAxis { get; private set; }
		public static Vector3 ZAxis { get; private set; }

		public static FlatLightModel Light { get; private set; }

		static World()
		{
			XAxis = Vector3.UnitX;
			YAxis = Vector3.UnitY;
			ZAxis = Vector3.UnitZ;
			Light = new LambertModel(new Vector3(-5.0f, 0.0f, 3.0f), 100.0f);
			//Light = new LambertModel(Camera.Eye, 100.0f);
		}

		public static Matrix4x4 CreateTranslation(Vector3 translation)
		{
			return new Matrix4x4
			(
				XAxis.X, YAxis.X, ZAxis.X, 0.0f,
				XAxis.Y, YAxis.Y, ZAxis.Y, 0.0f,
				XAxis.Z, YAxis.Z, ZAxis.Z, 0.0f,
				translation.X, translation.Y, translation.Z, 1.0f
			);
		}

		public static Matrix4x4 CreateScale(float scale)
		{
			return new Matrix4x4
			(
				scale, 0.0f, 0.0f, 0.0f,
				0.0f, scale, 0.0f, 0.0f,
				0.0f, 0.0f, scale, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f
			);
		}

		public static Matrix4x4 CreateRotationX(float radians)
		{
			float c = MathF.Cos(radians);
			float s = MathF.Sin(radians);
			return new Matrix4x4
			(
				1.0f, 0.0f, 0.0f, 0.0f,
				0.0f, c, s, 0.0f,
				0.0f, -s, c, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f
			);
		}

		public static Matrix4x4 CreateRotationY(float radians)
		{
			float c = MathF.Cos(radians);
			float s = MathF.Sin(radians);
			return new Matrix4x4
			(
				c, 0.0f, -s, 0.0f,
				0.0f, 1.0f, 0.0f, 0.0f,
				s, 0.0f, c, 0.0f,
				0.0f, 0.0f, 0.0f, 1.0f
			);
		}
	}
}