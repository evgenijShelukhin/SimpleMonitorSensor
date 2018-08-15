using Common;
using System;

namespace Sensor
{
	internal class Sensor : IDisposable
	{
		private readonly ITCPSender _client;
		private readonly IMetricsProvider _metricPovider;
		private readonly IOutputLog _outputLog;
		private bool isActive;

		public Sensor(IMetricsProvider metricPovider, ITCPSender client, IOutputLog outputLog)
		{
			_client = client;
			_metricPovider = metricPovider;
			_outputLog = outputLog;
		}

		public void Start()
		{
			_outputLog.LogMessage($"Connecting to Monitor");
			_metricPovider.metricUpdated += SendMessage;
		}

		private void SendMessage(object sender, EventArgs e)
		{
			_client.ProcessMessage(_metricPovider.metricValue.ToString());
		}

		public void Stop()
		{
			_metricPovider.metricUpdated += SendMessage;
			isActive = false;
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}