using System.Numerics;

namespace World
{
	public static class Axes
	{
		public static Vector3 X { get; set; }
		public static Vector3 Y { get; set; }
		public static Vector3 Z { get; set; }
	
		static Axes()
		{
			X = new Vector3(1, 0, 0);
			Y = new Vector3(0, 1, 0);
			Z = new Vector3(0, 0, 1);
		}
	}
}