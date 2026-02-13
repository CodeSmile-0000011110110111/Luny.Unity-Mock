using System;
using System.Linq;

namespace UnityEngine.SceneManagement
{
	public static class SceneManager
	{
		public static event Action<Scene, LoadSceneMode> sceneLoaded;
		public static event Action<Scene> sceneUnloaded;
		public static event Action<Scene, Scene> activeSceneChanged;

		private static Scene _activeScene;

		public static void LoadScene(String sceneName, LoadSceneMode mode = LoadSceneMode.Single)
		{
			if (mode == LoadSceneMode.Single)
			{
				var oldScene = _activeScene;
				sceneUnloaded?.Invoke(oldScene);

				// In a real mock we would destroy all objects, but for now we just clear _allObjects
				var objectsToDestroy = Object._allObjects.ToList();
				foreach (var obj in objectsToDestroy)
					Object.Destroy(obj);

				var s = new Scene();
				s.name = sceneName;
				s.path = $"Assets/Scenes/{sceneName}.unity";
				s.isLoaded = true;
				_activeScene = s;
				sceneLoaded?.Invoke(_activeScene, mode);
				activeSceneChanged?.Invoke(oldScene, _activeScene);
			}
			else
			{
				var s = new Scene();
				s.name = sceneName;
				s.path = $"Assets/Scenes/{sceneName}.unity";
				s.isLoaded = true;
				sceneLoaded?.Invoke(s, mode);
			}
		}

		public static Scene GetActiveScene() => _activeScene;

		static SceneManager()
		{
			var s = new Scene();
			s.name = "SampleScene";
			s.path = "Assets/Scenes/Untitled.unity";
			s.isLoaded = true;
			_activeScene = s;
		}
	}
}
