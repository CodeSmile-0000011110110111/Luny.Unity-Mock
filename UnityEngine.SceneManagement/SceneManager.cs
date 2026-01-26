using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.SceneManagement
{
	public struct Scene
	{
		public String name { get; set; }
		public String path { get; set; }
		public bool isLoaded { get; set; }
		public int buildIndex { get; set; }

		public GameObject[] GetRootGameObjects() => Object._allObjects.OfType<GameObject>().Where(go => go.transform.parent == null).ToArray();
	}

	public enum LoadSceneMode
	{
		Single,
		Additive,
	}

	public static class SceneManager
	{
		public static event Action<Scene, LoadSceneMode> sceneLoaded;
		public static event Action<Scene> sceneUnloaded;
		public static event Action<Scene, Scene> activeSceneChanged;

		private static Scene _activeScene = new Scene { name = "SampleScene", isLoaded = true };

		public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
		{
			if (mode == LoadSceneMode.Single)
			{
				var oldScene = _activeScene;
				sceneUnloaded?.Invoke(oldScene);
				
				// In a real mock we would destroy all objects, but for now we just clear _allObjects
				var objectsToDestroy = Object._allObjects.ToList();
				foreach (var obj in objectsToDestroy) Object.Destroy(obj);
				
				_activeScene = new Scene { name = sceneName, isLoaded = true };
				sceneLoaded?.Invoke(_activeScene, mode);
				activeSceneChanged?.Invoke(oldScene, _activeScene);
			}
			else
			{
				sceneLoaded?.Invoke(new Scene { name = sceneName, isLoaded = true }, mode);
			}
		}

		public static Scene GetActiveScene() => _activeScene;
	}
}
