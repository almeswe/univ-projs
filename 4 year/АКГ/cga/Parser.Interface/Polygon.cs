namespace Parser.Interface
{
	public sealed class Polygon
	{
		public bool IsCulled { get; set; }
		
		public Vector3[] Normals { get; set; }
		public Vector3[] Textures { get; set; }

		public int Triangles { get; set; }

		public ICollection<ValueTuple<int, int, int>> Arguments { get; set; }

		public Polygon()
		{
			this.IsCulled = false;
		}

		public Polygon(IEnumerable<ValueTuple<int, int, int>> arguments) : base()
		{
			this.Arguments = new List<ValueTuple<int, int, int>>();
			foreach (var arg in arguments)
				this.Arguments.Add(arg);
			this.Triangles = this.Arguments.Count - 2;
			this.Normals = new Vector3[this.Arguments.Count];
			this.Textures = new Vector3[this.Arguments.Count];
		}

		public Polygon(Span<ValueTuple<int, int, int>> arguments) : base()
		{
			this.Arguments = new List<ValueTuple<int, int, int>>(arguments.Length);
			foreach (var arg in arguments)
				this.Arguments.Add(arg);
			this.Triangles = this.Arguments.Count - 2;
			this.Normals = new Vector3[this.Arguments.Count];
			this.Textures = new Vector3[this.Arguments.Count];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ZeroNormals()
		{
			Array.Fill(this.Normals, Vector3.Zero);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public void NormalizeNormals()
		{
			for (var i = 0; i < this.Arguments.Count; i++)
				this.Normals[i] = Vector3.Normalize(this.Normals[i]);
		}
	}
}