using Common;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MonitorProject
{
	public interface ITCPReciever : IDisposable
	{
		Task ProcessMessage();
	}

	public class TCPReciever : TcpClientWrap, ITCPReciever
	{
		private readonly IOutputLog _output;

		public TCPReciever(TcpClient client, IOutputLog output)
			: base(client, output)
		{
			_output = output;
		}

		public async Task ProcessMessage()
		{
			try
			{
				while (true)
				{
					var reseivedMessage = await Read();
					await Send("OK");
					_output.LogMessage(reseivedMessage);
				}
			}
			catch (Exception ex)
			{
				_output.LogMessage(ex.Message);
			}
			finally
			{
				Dispose();
			}
		}
	}
}