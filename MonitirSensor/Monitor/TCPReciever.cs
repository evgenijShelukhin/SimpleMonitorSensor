using Common;
using System;
using System.Net.Sockets;

namespace MonitorProject
{
	public class TCPReciever : TcpClientWrap
	{
		private readonly IOutputLog _output;

		public TCPReciever(TcpClient client, IOutputLog output)
			: base(client, output)
		{
			_output = output;
		}

		public void ProcessMessages()
		{
			try
			{
				while (true)
				{
					var reseivedMessage = Read();
					Send("OK");
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