namespace Parser.Interface
{
	public sealed class Polygon
	{
		public int Triangles { get; set; }
		public bool Culled { get; set; }
		public float Intensity { get; set; }
		public ICollection<ValueTuple<int, int, int>> Arguments { get; set; }

		public Polygon(IEnumerable<ValueTuple<int, int, int>> arguments)
		{
			this.Culled = false;
			this.Intensity = float.NegativeInfinity;
			this.Arguments = new List<ValueTuple<int, int, int>>();
			foreach (var arg in arguments)
				this.Arguments.Add(arg);
			this.Triangles = this.Arguments.Count - 2;
		}

		public Polygon(Span<ValueTuple<int, int, int>> arguments)
		{
			this.Culled = false;
			this.Intensity = float.NegativeInfinity;
			this.Arguments = new List<ValueTuple<int, int, int>>(arguments.Length);
			foreach (var arg in arguments)
				this.Arguments.Add(arg);
			this.Triangles = this.Arguments.Count - 2;
		}
	}
}
