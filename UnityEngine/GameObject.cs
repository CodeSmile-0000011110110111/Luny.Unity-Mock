using System;

namespace UnityEngine
{
	public class GameObject : Object
	{
		public GameObject() => throw new NotImplementedException("UnityEngine.GameObject.ctor");
		public GameObject(string name) => throw new NotImplementedException("UnityEngine.GameObject.ctor(string)");

		public bool activeSelf { get; private set; }
		public bool activeInHierarchy => throw new NotImplementedException("UnityEngine.GameObject.activeInHierarchy");

		public Transform transform => throw new NotImplementedException("UnityEngine.GameObject.transform");

		public void SetActive(bool value) => throw new NotImplementedException("UnityEngine.GameObject.SetActive");

		public T GetComponent<T>() where T : Component => throw new NotImplementedException("UnityEngine.GameObject.GetComponent");
		public T[] GetComponentsInChildren<T>(bool includeInactive = false) where T : Component => throw new NotImplementedException("UnityEngine.GameObject.GetComponentsInChildren");
		public T AddComponent<T>() where T : Component => throw new NotImplementedException("UnityEngine.GameObject.AddComponent");
		public bool TryGetComponent<T>(out T component) where T : Component => throw new NotImplementedException("UnityEngine.GameObject.TryGetComponent");

		public static GameObject CreatePrimitive(PrimitiveType type) => throw new NotImplementedException("UnityEngine.GameObject.CreatePrimitive");
		public static GameObject Find(string name) => throw new NotImplementedException("UnityEngine.GameObject.Find");
	}
}
