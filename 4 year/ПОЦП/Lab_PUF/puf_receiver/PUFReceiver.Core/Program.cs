using System.IO.Ports;

namespace PUFReceiver.Core
{
	public static class Program
	{
		private static int _maxSamples = 1_000_00;
		private static byte[] _buffer = new byte[_maxSamples];

		private static string _file = $"samples_{Guid.NewGuid()}";

		[STAThread]
		public static void Main(string[] args)
		{
			Initiate();
		}

		private static void Initiate()
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
					var lines = new List<string>(_maxSamples);
					var port = new SerialPort(portNameStr);
					port.StopBits = StopBits.One;
					port.BaudRate = 115200;
					port.Parity = Parity.None;
					port.Open();
					for (var i = 1; port.IsOpen && i < _maxSamples; i++)
					{
						var sample = port.ReadByte();
						var converted = Convert.ToString(sample, 2);
						var trailing = 8 - converted.Length;
						var sampleStr = new string('0', trailing) + converted + '\n';
						Console.WriteLine($"{i}) {sampleStr}");
						lines.Add(sampleStr);
					}
					File.WriteAllLines(_file, lines);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}

	}
}