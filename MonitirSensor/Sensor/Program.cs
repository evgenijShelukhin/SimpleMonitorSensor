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
			using (var tcpSender = new TCPSender(new TcpClient(config.ipAddress, config.port), outputLog))
			using (var sensor = new Sensor(new MetricsProvider(), tcpSender, outputLog))
			{
				sensor.Start();
				outputLog.LogMessage("Press any key to exit");
				Console.ReadKey();
			}
		}
	}
}