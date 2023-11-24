using Parser.Interface;

namespace Parser.Core
{
	public sealed class ObjectFile : IObjectFile
	{
		private string _path;

		public string Path => this._path;
		public ICollection<Vector3> Normals { get; set; }
		public ICollection<Vector4> Vertices { get; set; }
		public ICollection<Vector3> Textures { get; set; }
		public ICollection<Polygon> Polygons { get; set; }

		public ObjectFile(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException();
			this._path = path;
			this.Normals = new List<Vector3>();
			this.Vertices = new List<Vector4>();
			this.Textures = new List<Vector3>();
			this.Polygons = new List<Polygon>();
		}
	}
}
