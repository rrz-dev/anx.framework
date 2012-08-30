using System;
using System.Diagnostics;
using System.IO;

namespace ANX.Framework.NonXNA
{
	public static class Logger
	{
		#region Constants
		private const string Filename = "log.txt";
		#endregion

		#region Private
#if !WINDOWSMETRO
		private static FileStream outputStream;
		private static StreamWriter writer;
#endif

		private static string CurrentDateTimeStamp
		{
			get
			{
				DateTime stamp = DateTime.Now;
				return stamp.Year + "-" + stamp.Month.ToString("00") + "-" +
					stamp.Day.ToString("00") + " " + stamp.Hour.ToString("00") + ":" +
					stamp.Minute.ToString("00") + ":" + stamp.Second.ToString("00");
			}
		}

		private static string CurrentTimeStamp
		{
			get
			{
				DateTime stamp = DateTime.Now;
				return stamp.Hour.ToString("00") + ":" + stamp.Minute.ToString("00") +
					":" + stamp.Second.ToString("00");
			}
		}
		#endregion

		#region Public
		public static bool IsWritingToConsole;
		#endregion
		
		#region Constructor
		static Logger()
		{
			IsWritingToConsole = true;

#if !WINDOWSMETRO
			outputStream = File.Open(Filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
			writer = new StreamWriter(outputStream);
			writer.AutoFlush = true;
#endif

			WriteToFile("OS: " + OSInformation.GetName() + " - " + OSInformation.GetVersionString() + "(" +
				OSInformation.GetVersion() + ")");
			WriteToFile("StartTime: " + CurrentDateTimeStamp);
			WriteToFile("----------");
		}
		#endregion

		#region Info
		public static void Info(string message)
		{
			string text = CurrentTimeStamp + "| " + message;
			WriteToConsole(text);
			WriteToFile(text);
		}

		public static void Info(string message, params object[] formatObjects)
		{
			Info(String.Format(message, formatObjects));
		}
		#endregion

		#region Warning
		public static void Warning(string message)
		{
			string text = CurrentTimeStamp + "| Warning: " + message + BuildStackTrace();
			WriteToConsole(text);
			WriteToFile(text);
		}
		#endregion

		#region Error
        public static void Error(string message, Exception ex)
        {
            Error(String.Format("{0} - Exception: {1}", message, ex.Message));
        }

		public static void Error(string message)
		{
			string text = CurrentTimeStamp + "| Error: " + message + BuildStackTrace();
			WriteToConsole(text);
			WriteToFile(text);
		}
		#endregion

		#region BuildStackTrace
		private static string BuildStackTrace()
		{
            string result = "";
#if !WINDOWSMETRO
			string[] lines = new StackTrace(true).ToString().Split('\n');
			for (int index = 2; index < lines.Length; index++)
			{
				result += "\n" + lines[index].ToString();
			}
#endif
			return result;
		}
		#endregion

		#region WriteToConsole
		private static void WriteToConsole(string message)
		{
			if (IsWritingToConsole == false)
			{
				return;
			}

			if (Debugger.IsAttached)
			{
				Debug.WriteLine(message);
			}
			else
            {
#if !WINDOWSMETRO
				Console.WriteLine(message);
#endif
			}
		}
		#endregion

		#region WriteToFile
		private static void WriteToFile(string message)
        {
#if !WINDOWSMETRO
			writer.WriteLine(message);
#endif
		}
		#endregion
	}
}
