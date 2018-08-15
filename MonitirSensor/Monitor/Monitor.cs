using Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MonitorProject
{
	internal class Monitor
	{
		private readonly IConfigProvider _configProvider;
		private readonly IOutputLog _outputLog;
		private TcpListener listener;
		private bool isActive;

		public Monitor(IConfigProvider configProvider, IOutputLog outputLog)
		{
			_configProvider = configProvider;
			_outputLog = outputLog;
		}

		public void Start(bool isForMultipleClients = true)
		{
			try
			{
				isActive = true;
				listener = new TcpListener(IPAddress.Parse(_configProvider.ipAddress), _configProvider.port);
				listener.Start();
				_outputLog.LogMessage("Waiting for clients...");

				while (true)
				{
					TcpClient client = listener.AcceptTcpClient();
					TCPReciever reciever = new TCPReciever(client, _outputLog);
					_outputLog.LogMessage("New client connected");
					Thread clientThread = new Thread(new ThreadStart(reciever.ProcessMessages));
					clientThread.Start();

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