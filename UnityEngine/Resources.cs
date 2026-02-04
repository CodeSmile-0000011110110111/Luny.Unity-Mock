using System;
using System.Collections.Generic;
using System.IO;

namespace UnityEngine
{
	public static class Resources
	{
		private static readonly Dictionary<String, Object> _loadedAssets = new();

		public static Object Load(String path)
		{
			if (_loadedAssets.TryGetValue(path, out var asset))
				return asset;

			// Mock: if path contains "Prefab", return a GameObject as prefab
			if (path.Contains("Prefab"))
			{
				var go = new GameObject(Path.GetFileName(path));
				go.SetActive(false); // Prefabs are inactive
				_loadedAssets[path] = go;
				return go;
			}

			return null;
		}

		public static T Load<T>(String path) where T : Object => Load(path) as T;

		internal static void Reset_UnitTestsOnly() => _loadedAssets.Clear();
	}
}
