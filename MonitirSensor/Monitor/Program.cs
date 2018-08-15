using Common;

namespace MonitorProject
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var output = new OutputLog();
			var monitor = new Monitor(new ConfigProvider(), output);
			monitor.Start().Wait();
		}
	}
}