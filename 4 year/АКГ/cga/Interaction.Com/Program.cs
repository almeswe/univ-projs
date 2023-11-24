using Visual;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Reflection;

namespace Interaction.Com
{
	public class Program
	{
		private static SceneForm _scene = null;
		private static MethodInfo _onSceneKeyDown = null;

		[STAThread]
		public static void Main(string[] args)
		{
			_scene = new SceneForm(@"C:\Users\HP\Desktop\car.obj");
			_onSceneKeyDown = typeof(SceneForm).GetMethod("OnSceneKeyDown", 
				BindingFlags.NonPublic | 
				BindingFlags.Instance);
			if (ThreadPool.QueueUserWorkItem((o) => StartComCommunication()))
				_scene.ShowDialog();
		}

		private static void StartComCommunication()
		{
			while (true)
			{
				var portNames = SerialPort.GetPortNames();
				if (portNames.Length == 0)
				{
					Console.WriteLine("There are no available COM ports!\nTimeout...");
					Thread.Sleep(5000);
					continue;
				}
				Console.WriteLine($"Select COM port:");
				foreach (var portName in portNames)
					Console.WriteLine($"-- {portName}");
				var portNameStr = Console.ReadLine();
				try
				{
					var port = new SerialPort(portNameStr);
					port.NewLine = "\r\n";
					port.BaudRate = 115200;
					port.Encoding = Encoding.ASCII;
					port.Open();
					var value = int.MaxValue;
					while (port.IsOpen)
						ComProcessReceivedData(port, ref value);
				}
				catch (Exception e) 
				{
					Console.WriteLine(e.Message);	
				}
			}
		}

		private static void ComProcessReceivedData(SerialPort port, ref int value)
		{
			var scale = 10;
			var outValue = 0;
			var data = port.ReadLine();
			if (!data.Contains("CHCK"))
				return;
			var valueStr = data.Split("CHCK")[1].Trim();
			Console.WriteLine($"{data} -> {valueStr}");
			if (!int.TryParse(valueStr, out outValue))
				return;
			if (value != int.MaxValue)
			{
				var diff = value - outValue;
				var events = int.Abs(diff / scale);
				for (int i = 0; i < events; i++);
					SetSceneKeyDown(diff < 0 ? Keys.Left : Keys.Right);
			}
			value = outValue;
		}

		private static void SetSceneKeyDown(Keys key)
		{
			var eventArgs = new KeyEventArgs(key);
			_onSceneKeyDown.Invoke(_scene, new object[] { null, eventArgs });
		}
	}
}