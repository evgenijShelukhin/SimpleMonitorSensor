using Common;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sensor
{
	public interface ITCPSender : IDisposable
	{
		Task ProcessMessage(string message);
	}

	public class TCPSender : TcpClientWrap, ITCPSender
	{
		private readonly IOutputLog _output;

		public TCPSender(TcpClient client, IOutputLog output)
			: base(client, output)
		{
			_output = output;
		}

		public async Task ProcessMessage(string message)
		{
			try
			{
				await Send(message);
				var responceMessage = await Read();
				_output.LogMessage($"Sent '{message}' response '{responceMessage}'");
			}
			catch (Exception ex)
			{
				_output.LogMessage(ex.Message);
			}
		}
	}
}