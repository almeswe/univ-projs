using System.IO.Ports;

namespace PUFReceiver.Core
{
	internal static class Extension
	{
		internal static int Read(this SerialPort port)
		{
			var value = port.ReadByte();
			Thread.Sleep(50);
			port.DiscardInBuffer();
			return value;
		}
	}
}
