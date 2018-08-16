using System;
using System.Configuration;

namespace Common
{
	public interface IConfigProvider
	{
		string ipAddress { get; }
		int port { get; }
		int maxClientCont { get; }
	}

	public class ConfigProvider : IConfigProvider
	{
		private readonly string _ipAddress;
		private readonly int _port;
		private readonly int _maxClientCont;

		public string ipAddress { get { return _ipAddress; } }
		public int port { get { return _port; } }
		public int maxClientCont { get { return _maxClientCont; } }

		public ConfigProvider(string ipAddress = null, int? port = null, int? maxClientCont = null)
		{
			_ipAddress = ipAddress ?? ConfigurationManager.AppSettings["IpAddress"];
			_port = port ?? ParcedSettingValue <int>("Port");
			_maxClientCont = maxClientCont ?? ParcedSettingValue<int>("MaxClientCont");
		}

		private T ParcedSettingValue<T>(string settingName) where T : struct
		{
			if (ConfigurationManager.AppSettings[settingName] == null)
			{
				return default(T);
			}

			object value = ConfigurationManager.AppSettings[settingName];
			return (T)Convert.ChangeType(value, typeof(T));
		}
	}
}