using System.Configuration;

namespace Common
{
	public interface IConfigProvider
	{
		string ipAddress { get; }
		int port { get; }
	}

	public class ConfigProvider : IConfigProvider
	{
		private readonly string _ipAddress;
		private readonly int _port;

		public string ipAddress { get { return _ipAddress; } }
		public int port { get { return _port; } }

		public ConfigProvider(string ipAddress=null, int? port=null)
		{
			_ipAddress = ipAddress??ConfigurationManager.AppSettings["IpAddress"];
			_port = port??int.Parse(ConfigurationManager.AppSettings["Port"]);
		}
	}
}