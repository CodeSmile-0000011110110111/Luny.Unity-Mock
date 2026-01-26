using System;

namespace UnityEngine
{
	public class Component : Object
	{
		public GameObject gameObject => throw new NotImplementedException("UnityEngine.Component.gameObject");

		public bool TryGetComponent<T>(out T component) where T : Component => throw new NotImplementedException("UnityEngine.Component.TryGetComponent");
	}
}
