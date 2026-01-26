using System;

namespace UnityEngine.SceneManagement
{
	public struct Scene
	{
		public String name { get; set; }
		public String path { get; set; }
		public bool isLoaded { get; set; }
		public GameObject[] GetRootGameObjects() => throw new NotImplementedException("UnityEngine.SceneManagement.Scene.GetRootGameObjects");
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

		public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single) => throw new NotImplementedException("UnityEngine.SceneManagement.SceneManager.LoadScene");
		public static Scene GetActiveScene() => throw new NotImplementedException("UnityEngine.SceneManagement.SceneManager.GetActiveScene");
	}
}
