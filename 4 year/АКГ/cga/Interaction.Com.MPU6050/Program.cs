using Visual;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Reflection;
using System.Numerics;

namespace Interaction.Com.MPU6050
{
	public static class Program
	{
		private static SceneForm _scene = null;
		private static FieldInfo _objectRotation = null;

		private static Vector3 _objectRotationZero = Vector3.Zero;

		[STAThread]
		public static void Main(string[] args)
		{
			_scene = new SceneForm(@"C:\Users\HP\Desktop\car.obj");
			_objectRotation = typeof(SceneForm).GetField("_objectRotation", 
				BindingFlags.NonPublic | BindingFlags.Instance);
			//SetObjectRotation(new Vector3(0.0f, -3.3f, 0.0f));
			if (ThreadPool.QueueUserWorkItem((o) => StartMPU6050Com()))
				_scene.ShowDialog();
		}

		private static void StartMPU6050Com()
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
					_objectRotationZero = GetMPU6050Data(port);
					while (port.IsOpen)
						ProcessMPU6050Data(port);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}

		private static Vector3 GetMPU6050Data(SerialPort port)
		{
			while (true)
			{
				try
				{

					var rotation = Vector3.Zero;
					var data = port.ReadLine();
					var axes = data.Split(',');
					rotation.X = Convert.ToSingle(axes[0]);
					rotation.Y = Convert.ToSingle(axes[1]);
					rotation.Z = Convert.ToSingle(axes[2]);
					return rotation;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}

		private static void ProcessMPU6050Data(SerialPort port)
		{
			try
			{
				var rotation = GetMPU6050Data(port);
				rotation -= _objectRotationZero;
				SetObjectRotation(rotation / 50.0f);
				Console.WriteLine($"x: {rotation.X} y: {rotation.Y}, z: {rotation.Z}");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private static void SetObjectRotation(Vector3 rotation)
		{
			_objectRotation.SetValue(_scene, rotation);
		}
	}
}