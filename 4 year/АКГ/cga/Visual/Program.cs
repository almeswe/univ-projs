namespace Visual
{
	public static class Program
	{
		[STAThread]
		public static void Main()
		{
			ApplicationConfiguration.Initialize();
			Application.Run(new SceneForm(@"C:\Users\HP\Desktop\hand.obj"));
		}
	}
}	