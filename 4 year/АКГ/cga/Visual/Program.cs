namespace Visual
{
	public static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			ApplicationConfiguration.Initialize();
			Application.Run(new SceneForm(@"C:\Users\HP\Desktop\human.obj"));
		}
	}
}	