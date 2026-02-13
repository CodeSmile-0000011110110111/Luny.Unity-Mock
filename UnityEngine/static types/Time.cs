using System;

namespace UnityEngine
{
	public static class Time
	{
		public static Single deltaTime { get; internal set; } = 1f / 60f;
		public static Single unscaledDeltaTime => deltaTime;
		public static Single fixedDeltaTime => 0.02f;
		public static Single time { get; internal set; }
		public static Double realtimeSinceStartupAsDouble => time;
		public static Int64 frameCount { get; internal set; }
	}
}
