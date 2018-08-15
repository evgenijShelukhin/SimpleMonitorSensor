using System;

namespace Common
{
	public interface IOutputLog
	{
		void LogMessage(string message);
	}

	public class OutputLog : IOutputLog
	{
		public void LogMessage(string message)
		{
			Console.WriteLine(message);
		}
	}
}