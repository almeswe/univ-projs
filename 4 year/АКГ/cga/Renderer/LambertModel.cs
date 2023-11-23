using System.Numerics;

namespace Renderer
{
	public sealed class LambertModel : FlatLightModel
	{
		public LambertModel(Vector3 position, float intensity) 
			: base(position, intensity)
		{ }

		public override float Compute(Vector3 normal, Vector3 position)
		{
			if (normal == Vector3.Zero || position == Vector3.Zero)
				return 0.0f;
			normal = Vector3.Normalize(normal);
			position = Vector3.Normalize(this.Position - position);
			var dot = Vector3.Dot(normal, position);
			if (dot < 0)
				return 0.0f;
			var intensity = dot * this.Intensity;
			if (intensity > 1.0f)
				return 1.0f;
			return intensity;
		}
	}
}