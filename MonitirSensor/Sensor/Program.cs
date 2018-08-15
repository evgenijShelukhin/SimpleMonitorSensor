using Common;
using System;
using System.Net.Sockets;

namespace Sensor
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var config = new ConfigProvider();
			var outputLog = new OutputLog();
			using (var sensor = new Sensor(new MetricsProvider(),
				new TCPSender(new TcpClient(config.ipAddress, config.port), outputLog),
				outputLog))
			{
				sensor.Start();
				outputLog.LogMessage("Press any key to exit");
				Console.ReadKey();
			}
		}
	}
}