namespace Parser.Interface
{
	public interface IObjectFile
	{
		public string Path { get; }
		public ICollection<Vector3> Normals { get; set; }
		public ICollection<Vector4> Vertices { get; set; }
		public ICollection<Vector3> Textures { get; set; }
		public ICollection<Polygon> Polygons { get; set; }
	}
}