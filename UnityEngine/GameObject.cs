using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
	public class GameObject : Object
	{
		private readonly List<Component> _components = new();
		private readonly Transform _transform;

		public Boolean activeSelf { get; private set; }
		public Boolean activeInHierarchy => activeSelf && (transform.parent == null || transform.parent.gameObject.activeInHierarchy);

		public Transform transform => _transform;

		public static GameObject CreatePrimitive(PrimitiveType type) => new(type.ToString());
		public static GameObject Find(String name) => _allObjects.OfType<GameObject>().FirstOrDefault(go => go.name == name);

		public GameObject()
			: this("New GameObject") {}

		public GameObject(String name)
		{
			activeSelf = true; // Set this before adding components so OnEnable can fire
			this.name = name;
			Console.WriteLine($"[DEBUG_LOG] Created GameObject '{name}'");
			_transform = AddComponent<Transform>();
		}

		public void SetActive(Boolean value)
		{
			if (activeSelf == value)
				return;

			var wasActiveInHierarchy = activeInHierarchy;
			activeSelf = value;
			var isNowActiveInHierarchy = activeInHierarchy;

			if (wasActiveInHierarchy != isNowActiveInHierarchy)
			{
				if (isNowActiveInHierarchy)
				{
					// Unity triggers OnEnable for all components in the subtree that are enabled and have had Awake called
					foreach (var mb in GetComponentsInChildren<MonoBehaviour>(true))
					{
						if (mb.enabled && mb.gameObject.activeInHierarchy)
						{
							if (!mb._awakeCalled)
							{
								mb._awakeCalled = true;
								mb.InternalAwake();
							}
							mb.InternalOnEnable();
						}
					}
				}
				else
				{
					foreach (var mb in GetComponentsInChildren<MonoBehaviour>(true))
						// OnDisable is called if the component was active and enabled
						// Simplified check here
						mb.InternalOnDisable();
				}
			}
		}

		public T GetComponent<T>() where T : Component => _components.OfType<T>().FirstOrDefault();

		public T[] GetComponentsInChildren<T>(Boolean includeInactive = false) where T : Component
		{
			var results = new List<T>();
			if (activeInHierarchy || includeInactive)
				results.AddRange(_components.OfType<T>());

			foreach (Transform child in transform)
				results.AddRange(child.gameObject.GetComponentsInChildren<T>(includeInactive));

			return results.ToArray();
		}

		public T AddComponent<T>() where T : Component
		{
			var component = Activator.CreateInstance(typeof(T), true) as T;
			if (component != null)
			{
				component.gameObject = this;
				_components.Add(component);

				if (component is MonoBehaviour mb)
				{
					// Awake is ALWAYS called immediately upon AddComponent in Unity
					// REGARDLESS of active state
					mb._awakeCalled = true;
					mb.InternalAwake();

					// OnEnable is called ONLY if the GameObject is active in hierarchy AND the component is enabled
					if (activeInHierarchy && mb.enabled)
						mb.InternalOnEnable();
				}
			}
			return component;
		}

		public Boolean TryGetComponent<T>(out T component) where T : Component
		{
			component = GetComponent<T>();
			return component != null;
		}
	}
}
