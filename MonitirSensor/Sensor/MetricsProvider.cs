using System;
using System.Timers;

namespace Sensor
{
	public interface IMetricsProvider
	{
		long metricValue { get; set; }

		event EventHandler metricUpdated;
	}

	public class MetricsProvider : IMetricsProvider
	{
		private readonly Timer metricsTimer;
		public long metricValue { get; set; }

		public event EventHandler metricUpdated;

		public MetricsProvider()
		{
			metricsTimer = new Timer(3000);
			metricsTimer.Elapsed += new ElapsedEventHandler(GenerateMetric);
			metricsTimer.Enabled = true;
		}

		private void GenerateMetric(object sender, ElapsedEventArgs e)
		{
			metricValue++;
			metricUpdated?.Invoke(this, new EventArgs());
		}
	}
}