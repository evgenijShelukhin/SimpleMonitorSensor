using Common;
using System;
using System.Net.Sockets;

namespace Sensor
{
	public interface ITCPSender:IDisposable {
		void ProcessMessage(string message);
	}

	public class TCPSender : TcpClientWrap, ITCPSender
	{
		private readonly IOutputLog _output;

		public TCPSender(TcpClient client, IOutputLog output)
			: base(client, output)
		{
			_output = output;
		}

		public void ProcessMessage(string message)
		{
			try
			{
				Send(message);
				var responceMessage = Read();
				_output.LogMessage($"Sent '{message}' response '{responceMessage}'");
			}
			catch (Exception ex)
			{
				_output.LogMessage(ex.Message);
			}
		}
	}
}