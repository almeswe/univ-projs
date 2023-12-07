namespace Renderer
{
	public abstract class FlatLightModel
	{
		public Vector3 Position { get; private set; }
		public float Intensity { get; private set; }

		public FlatLightModel(Vector3 position, float intensity)
		{
			this.Position = position;
			this.Intensity = intensity;
		}

		public abstract float Compute(Vector3 normal, Vector3 position);
	}
}