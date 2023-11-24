using System.Numerics;

namespace World
{
	public static class ViewPort
	{
		public static int Width { get; set; }
		public static int Height { get; set; }

		static ViewPort()
		{
			Width = 500;
			Height = 500;
		}
	}
}