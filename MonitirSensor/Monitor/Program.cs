using Common;

namespace MonitorProject
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var monitor = new Monitor(new ConfigProvider(), new OutputLog());
			monitor.Start();
		}
	}
}