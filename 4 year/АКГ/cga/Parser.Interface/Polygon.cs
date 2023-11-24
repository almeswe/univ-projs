namespace Parser.Interface
{
	public struct Polygon
	{
		public ICollection<ValueTuple<int, int, int>> Arguments { get; set; }

		public Polygon(IEnumerable<ValueTuple<int, int, int>> arguments)
		{
			this.Arguments = new List<ValueTuple<int, int, int>>();
			foreach (var arg in arguments)
				this.Arguments.Add(arg);
		}

		public Polygon(Span<ValueTuple<int, int, int>> arguments)
		{
			this.Arguments = new List<ValueTuple<int, int, int>>(arguments.Length);
			foreach (var arg in arguments)
				this.Arguments.Add(arg);
		}
	}
}
