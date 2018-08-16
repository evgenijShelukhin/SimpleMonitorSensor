using Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MonitorProject
{
	internal class Monitor
	{
		private readonly IConfigProvider _configProvider;
		private readonly IOutputLog _outputLog;
		private TcpListener listener;
		private int clientsCount;

		public Monitor(IConfigProvider configProvider, IOutputLog outputLog)
		{
			_configProvider = configProvider;
			_outputLog = outputLog;
		}

		public async Task Start()
		{
			try
			{
				listener = new TcpListener(IPAddress.Parse(_configProvider.ipAddress), _configProvider.port);
				listener.Start();
				_outputLog.LogMessage("Waiting for clients...");

				while (true)
				{
					TcpClient client = await listener.AcceptTcpClientAsync();
					TCPReciever reciever = new TCPReciever(client, _outputLog); //TODO create objects with object factory
					clientsCount++;
					_outputLog.LogMessage("New client connected");
					var task = Task.Factory.StartNew(() => reciever.ProcessMessage());

					if (_configProvider.maxClientCont>0 && clientsCount >= _configProvider.maxClientCont)
					{
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				if (listener != null)
					listener.Stop();
			}
		}
	}
}