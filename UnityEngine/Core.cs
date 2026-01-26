using System;

namespace UnityEngine
{
	public class Coroutine {}
	public class YieldInstruction {}
	public class WaitForEndOfFrame : YieldInstruction {}

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

	public static class Time
	{
		public static Single deltaTime { get; internal set; } = 1f / 60f;
		public static Single unscaledDeltaTime => deltaTime;
		public static Single fixedDeltaTime => 0.02f;
		public static Single time { get; internal set; }
		public static Double realtimeSinceStartupAsDouble => time;
		public static Int64 frameCount { get; internal set; }
	}

	public static class Application
	{
		public static String dataPath => "MockDataPath";
		public static String persistentDataPath => "MockPersistentDataPath";
		public static Boolean isEditor => true;
		public static Boolean isPlaying => true;
		public static void Quit() => Console.WriteLine("Application.Quit called");
		public static void Quit(Int32 exitCode) => Console.WriteLine($"Application.Quit({exitCode}) called");
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

	public struct Vector3
	{
		public Single x, y, z;

		public Vector3(Single x, Single y, Single z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static Vector3 zero => new(0, 0, 0);
		public static Vector3 one => new(1, 1, 1);
		public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
		public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
	}
}
