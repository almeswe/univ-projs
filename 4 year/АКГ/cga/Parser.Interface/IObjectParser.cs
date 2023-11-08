namespace Parser.Interface
{
	public interface IObjectParser
	{
		public IObjectFile Parse(string path);
	}
}