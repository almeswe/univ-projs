namespace Renderer
{
	public sealed class PhongModel
	{
		private Color _ambient;
		private Color _diffuse;
		private Color _specular;
		private float _ambientIntensity;
		private float _diffuseIntensity;
		private float _specularIntensity;
		private float _diffuseSourceIntensity;
		private float _specularSourceIntensity;

		public float Reflection { get; set; }
		public Vector3 Position { get; private set; }

		public PhongModel(Vector3 position, float kd, float ks, float ka = 0.1f)
		{
			this.Reflection = 50.0f;
			this.Position = position;
			this._ambientIntensity = ka;
			this._diffuseSourceIntensity = kd;
			this._specularSourceIntensity = ks;
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public Color Compute(Color pixelColor, Color pixelSpecularColor, Vector3 normal, Vector3 pixelPosition)
		{
			normal = Vector3.Normalize(normal);
			var view = pixelPosition - Camera.Eye;
			var light = Vector3.Normalize(this.Position - pixelPosition);
			this._ambient = Color.FromArgb(
				pixelColor.A,
				(byte)(pixelColor.R * this._ambientIntensity),
				(byte)(pixelColor.G * this._ambientIntensity),
				(byte)(pixelColor.B * this._ambientIntensity)
			);
			this._diffuseIntensity = this.ComputeDiffuseIntensity(normal, light);
			this._diffuse = Color.FromArgb(
				pixelColor.A,
				(byte)(pixelColor.R * this._diffuseIntensity),
				(byte)(pixelColor.G * this._diffuseIntensity),
				(byte)(pixelColor.B * this._diffuseIntensity)
			);
			this._specularIntensity = this.ComputeSpecularIntensity(normal, light, view);
			this._specular = Color.FromArgb(
				pixelColor.A,
				(byte)(pixelSpecularColor.R * this._specularIntensity),
				(byte)(pixelSpecularColor.G * this._specularIntensity),
				(byte)(pixelSpecularColor.B * this._specularIntensity)
			);
			var r = int.Min(this._ambient.R + this._diffuse.R + this._specular.R, 255);
			var g = int.Min(this._ambient.G + this._diffuse.G + this._specular.G, 255);
			var b = int.Min(this._ambient.B + this._diffuse.B + this._specular.B, 255);
			return Color.FromArgb(pixelColor.A, r, g, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		private float ComputeDiffuseIntensity(Vector3 normal, Vector3 light)
		{
			var dot = -Vector3.Dot(normal, light);
			if (dot < 0)
				return 0.0f;
			var intensity = dot * this._diffuseSourceIntensity;
			if (intensity > 1.0f)
				return 1.0f;
			return intensity;
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		private float ComputeSpecularIntensity(Vector3 normal, Vector3 light, Vector3 view)
		{
			var ray = this.ComputeReflectedRay(normal, light);
			var dot = Vector3.Dot(view, ray);
			if (dot < 0.0f)
				return 0.0f;
			dot = MathF.Pow(dot, this.Reflection);
			var intensity = dot * this._specularSourceIntensity;
			if (intensity > 1.0f)
				return 1.0f;
			return intensity;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private Vector3 ComputeReflectedRay(Vector3 normal, Vector3 light)
		{
			return light - 2 * Vector3.Dot(light, normal) * normal;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetSpeclar(float ks)
			=> this._specularSourceIntensity = ks;
	}
}
