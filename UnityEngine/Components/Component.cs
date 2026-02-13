using System;

namespace UnityEngine
{
	public class Component : Object
	{
		public GameObject gameObject { get; internal set; }
		public Transform transform => gameObject.transform;

		public T GetComponent<T>() where T : Component => gameObject.GetComponent<T>();

		public T[] GetComponentsInChildren<T>(Boolean includeInactive = false) where T : Component =>
			gameObject.GetComponentsInChildren<T>(includeInactive);

		public Boolean TryGetComponent<T>(out T component) where T : Component => gameObject.TryGetComponent(out component);
	}
}
