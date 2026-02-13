using System;

namespace UnityEngine
{
	public static class Debug
	{
		public static void Log(System.Object message) => Console.WriteLine(message);
		public static void LogWarning(System.Object message) => Console.WriteLine($"[Warning] {message}");
		public static void LogError(System.Object message) => Console.Error.WriteLine($"[Error] {message}");
		public static void LogException(Exception exception) => Console.Error.WriteLine($"[Exception] {exception}");

		public static void LogFormat(LogType logType, Object context, String format, params System.Object[] args)
		{
			var message = String.Format(format, args);
			switch (logType)
			{
				case LogType.Error:
				case LogType.Assert:
				case LogType.Exception:
					Console.Error.WriteLine($"[{logType}] {message}");
					break;
				case LogType.Warning:
					Console.WriteLine($"[Warning] {message}");
					break;
				case LogType.Log:
					Console.WriteLine(message);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
			}
		}

		public static void Break() => Console.WriteLine("[Debug.Break called]");
	}
}
