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
		public static void Log(object message) => throw new NotImplementedException("UnityEngine.Debug.Log");
		public static void LogWarning(object message) => throw new NotImplementedException("UnityEngine.Debug.LogWarning");
		public static void LogError(object message) => throw new NotImplementedException("UnityEngine.Debug.LogError");
		public static void LogException(Exception exception) => throw new NotImplementedException("UnityEngine.Debug.LogException");
		public static void LogFormat(LogType logType, Object context, string format, params object[] args) => throw new NotImplementedException("UnityEngine.Debug.LogFormat");
		public static void Break() => throw new NotImplementedException("UnityEngine.Debug.Break");
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
