using System;

namespace UnityEngine
{
	public enum LogType
	{
		Error,
		Assert,
		Warning,
		Log,
		Exception,
	}

	public static class Debug
	{
		public static void Log(object message) => Console.WriteLine(message);
		public static void LogWarning(object message) => Console.WriteLine($"[Warning] {message}");
		public static void LogError(object message) => Console.Error.WriteLine($"[Error] {message}");
		public static void LogException(Exception exception) => Console.Error.WriteLine($"[Exception] {exception}");
		public static void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			var message = string.Format(format, args);
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
				default:
					Console.WriteLine(message);
					break;
			}
		}

		public static void Break() => Console.WriteLine("[Debug.Break called]");
	}

	public static class Time
	{
		public static float deltaTime => throw new NotImplementedException("UnityEngine.Time.deltaTime");
		public static float unscaledDeltaTime => throw new NotImplementedException("UnityEngine.Time.unscaledDeltaTime");
		public static float fixedDeltaTime => throw new NotImplementedException("UnityEngine.Time.fixedDeltaTime");
		public static float time => throw new NotImplementedException("UnityEngine.Time.time");
		public static double realtimeSinceStartupAsDouble => throw new NotImplementedException("UnityEngine.Time.realtimeSinceStartupAsDouble");
		public static long frameCount => throw new NotImplementedException("UnityEngine.Time.frameCount");
	}

	public static class Application
	{
		public static string dataPath => throw new NotImplementedException("UnityEngine.Application.dataPath");
		public static string persistentDataPath => throw new NotImplementedException("UnityEngine.Application.persistentDataPath");
		public static bool isEditor => throw new NotImplementedException("UnityEngine.Application.isEditor");
		public static bool isPlaying => throw new NotImplementedException("UnityEngine.Application.isPlaying");
		public static void Quit() => throw new NotImplementedException("UnityEngine.Application.Quit");
		public static void Quit(int exitCode) => throw new NotImplementedException("UnityEngine.Application.Quit(int)");
	}

	public enum PrimitiveType
	{
		Sphere,
		Capsule,
		Cylinder,
		Cube,
		Plane,
		Quad,
	}

	public class Transform : Component
	{
	}
}
