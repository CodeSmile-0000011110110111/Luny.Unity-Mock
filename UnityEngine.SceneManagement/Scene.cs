using System;
using System.Linq;

namespace UnityEngine.SceneManagement
{
	public struct Scene
	{
		public String name { get; set; }
		public String path { get; set; }
		public Boolean isLoaded { get; set; }
		public Int32 buildIndex { get; set; }

		public GameObject[] GetRootGameObjects() => Object._allObjects.OfType<GameObject>().Where(go => go.transform.parent == null).ToArray();
	}
}
