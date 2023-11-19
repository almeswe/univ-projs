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
			var direction = Vector3.Normalize(this.Position - position);
			var theta = Vector3.Dot(normal, direction);
			return theta < 0 ? 0.0f : this.Intensity * theta;
		}
	}
}