using Common;
using Moq;
using NUnit.Framework;
using Sensor;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Tests
{
	[TestFixture]
	public class SensorTests
	{
		private ConfigProvider config;
		private Task monitorServiceThread;
		private IOutputLog monitorOutputLogMock;

		[SetUp]
		public void Setup()
		{
			monitorOutputLogMock = Mock.Of<IOutputLog>();
			Mock.Get(monitorOutputLogMock).Setup(x => x.LogMessage(It.IsAny<string>()));

			config = new ConfigProvider("127.0.0.1", 4321);
			var m = new MonitorProject.Monitor(config, monitorOutputLogMock);
			monitorServiceThread = Task.Run(() => m.Start(false));
		}

		[Test]
		public void test()
		{
			var sensorOutputLogMock = Mock.Of<IOutputLog>();
			Mock.Get(sensorOutputLogMock).Setup(x => x.LogMessage(It.IsAny<string>()));

			var sender = new TCPSender(new TcpClient(config.ipAddress, config.port), sensorOutputLogMock);
			sender.ProcessMessage("42").Wait();

			//Check Monitor side output
			Mock.Get(monitorOutputLogMock).Verify(x =>
				x.LogMessage("42"), Times.Exactly(1));

			//Check Sensor side output
			Mock.Get(sensorOutputLogMock).Verify(x =>
				x.LogMessage("Sent '42' response 'OK'"), Times.Exactly(1));
		}

		[TearDown]
		public void Dispose()
		{
			//TODO add switch off the Monitor in case it still running
		}
	}
}