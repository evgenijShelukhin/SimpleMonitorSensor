using System;
using System.Net.Sockets;
using System.Text;

namespace Common
{
	public class TcpClientWrap : IDisposable
	{
		private readonly TcpClient _client;
		private readonly NetworkStream _stream;
		private readonly IOutputLog _output;

		public TcpClientWrap(TcpClient client, IOutputLog output)
		{
			_client = client;
			_stream = _client.GetStream();
			_output = output;
		}

		protected virtual void Send(string message)
		{
			byte[] data = Encoding.Unicode.GetBytes(message);
			_stream.Write(data, 0, data.Length);
		}

		protected virtual string Read()
		{
			byte[] data = new byte[64]; //long is type of expected value
			StringBuilder builder = new StringBuilder();
			int bytes = 0;
			do
			{
				bytes = _stream.Read(data, 0, data.Length);
				builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
			}
			while (_stream.DataAvailable);
			return builder.ToString();
		}

		public void Dispose()
		{
			if (_stream != null)
				_stream.Close();
			if (_client != null)
				_client.Close();
		}
	}
}