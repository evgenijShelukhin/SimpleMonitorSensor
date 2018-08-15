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

		public Monitor(IConfigProvider configProvider, IOutputLog outputLog)
		{
			_configProvider = configProvider;
			_outputLog = outputLog;
		}

		public async Task Start(bool isForMultipleClients = true)
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

					_outputLog.LogMessage("New client connected");
					var task = Task.Factory.StartNew(() => reciever.ProcessMessage());

					if (!isForMultipleClients)
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