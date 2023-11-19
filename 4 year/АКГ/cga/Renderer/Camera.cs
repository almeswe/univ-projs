using System.Numerics;

namespace Renderer
{
	public static class Camera
	{
		public static Vector3 Up { get; set; }
		public static Vector3 Eye { get; set; }
		public static Vector3 Target { get; set; }

		public static float Far { get; set; }
		public static float Near { get; set; }
		public static float Fov { get; set; }

		static Camera()
		{
			Far = 1000.0f;
			Near = 1.0f;
			Fov = MathF.PI / 4;
			Up = Vector3.UnitY;
			Target = new Vector3(0.0f, 0.0f, 3.0f);
			Eye = new Vector3(0.0f, 0.0f, 2.0f);
		}

		public static Matrix4x4 CreateTranslation()
		{
			var zAxis = Vector3.Normalize(Eye - Target);
			var xAxis = Vector3.Normalize(Vector3.Cross(Up, zAxis));
			var yAxis = Vector3.Cross(zAxis, xAxis);
			return new Matrix4x4
			(
				xAxis.X, yAxis.X, zAxis.X, 0.0f,
				xAxis.Y, yAxis.Y, zAxis.Y, 0.0f,
				xAxis.Z, yAxis.Z, zAxis.Z, 0.0f,
				-Vector3.Dot(xAxis, Eye), -Vector3.Dot(yAxis, Eye), -Vector3.Dot(zAxis, Eye), 1.0f
			);
		}
	}
}
