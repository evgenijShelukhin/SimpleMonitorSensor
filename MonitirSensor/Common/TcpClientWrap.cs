using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public class TcpClientWrap : IDisposable
	{
		private TcpClient _client;
		private NetworkStream _stream;
		private readonly IOutputLog _output;

		public TcpClientWrap(TcpClient client, IOutputLog output)
		{
			_client = client;
			_stream = _client.GetStream();
			_output = output;
		}

		protected async virtual Task Send(string message)
		{
			byte[] data = Encoding.Unicode.GetBytes(message);
			await _stream.WriteAsync(data, 0, data.Length);
		}

		protected async virtual Task<string> Read()
		{
			byte[] data = new byte[64]; //type of expected value is long
			StringBuilder builder = new StringBuilder();
			int bytes = 0;
			do
			{
				bytes = await _stream.ReadAsync(data, 0, data.Length);
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