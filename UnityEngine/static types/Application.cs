using System;

namespace UnityEngine
{
	public static class Application
	{
		public static String dataPath => "MockDataPath";
		public static String persistentDataPath => "MockPersistentDataPath";
		public static Boolean isEditor => true;
		public static Boolean isPlaying => true;
		public static void Quit() => Console.WriteLine("Application.Quit called");
		public static void Quit(Int32 exitCode) => Console.WriteLine($"Application.Quit({exitCode}) called");
	}
}
