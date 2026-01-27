using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

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

	public class Renderer : Component
	{
		public Boolean enabled { get; set; } = true;

		public Boolean isVisible => enabled && gameObject.activeInHierarchy;
	}

	public class Behaviour : Component
	{
		private Boolean _enabled = true;
		public Boolean enabled
		{
			get => _enabled;
			set
			{
				if (_enabled == value)
					return;

				_enabled = value;
				if (gameObject.activeInHierarchy)
				{
					if (_enabled && this is MonoBehaviour mb)
						mb.InternalOnEnable();
					else if (!_enabled && this is MonoBehaviour mb2)
						mb2.InternalOnDisable();
				}
			}
		}
		public Boolean isActiveAndEnabled => enabled && gameObject != null && gameObject.activeInHierarchy;
	}

	public class Transform : Behaviour, IEnumerable
	{
		private readonly List<Transform> _children = new();
		private Transform _parent;

		public Transform parent
		{
			get => _parent;
			set
			{
				if (_parent == value)
					return;

				_parent?._children.Remove(this);
				_parent = value;
				_parent?._children.Add(this);
			}
		}

		public Int32 childCount => _children.Count;

		public Vector3 localPosition { get; set; }
		public Vector3 position
		{
			get => parent == null ? localPosition : parent.position + localPosition;
			set => localPosition = parent == null ? value : value - parent.position;
		}

		public IEnumerator GetEnumerator() => _children.GetEnumerator();

		public Transform GetChild(Int32 index) => _children[index];
	}

	public class MonoBehaviour : Behaviour
	{
		internal Boolean _awakeCalled;
		internal Boolean _startCalled;

		public static void LogAllMethods(Type type)
		{
			Console.WriteLine($"[DEBUG_LOG] [{nameof(MonoBehaviour)}] Methods for {type.Name}:");
			foreach (var m in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
				Console.WriteLine($"[DEBUG_LOG] [{nameof(MonoBehaviour)}] - {m.Name} ({m.Attributes}) declared in {m.DeclaringType.Name}");
		}

		public Coroutine StartCoroutine(IEnumerator routine) => new();

		private void InvokeMessageMethod(Message message)
		{
			if (message == Message.Awake)
			{
				if (_awakeCalled)
					return;

				_awakeCalled = true;
			}
			else if (message == Message.Start)
			{
				if (_startCalled)
					return;

				_startCalled = true;
			}

			var methodName = message.ToString();
			var type = GetType();
			var method = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			if (method != null)
			{
				Console.WriteLine($"[DEBUG_LOG] [{nameof(MonoBehaviour)}] {type.Name} => {methodName}()");
				method.Invoke(this, null);
			}
			else
				Console.WriteLine($"[DEBUG_LOG] [{nameof(MonoBehaviour)}] {type.Name} does not implement: {methodName}()");
		}

		internal void InternalAwake() => InvokeMessageMethod(Message.Awake);
		internal void InternalOnDestroy() => InvokeMessageMethod(Message.OnDestroy);
		internal void InternalOnEnable() => InvokeMessageMethod(Message.OnEnable);
		internal void InternalOnDisable() => InvokeMessageMethod(Message.OnDisable);
		internal void InternalStart() => InvokeMessageMethod(Message.Start);
		internal void InternalFixedUpdate() => InvokeMessageMethod(Message.FixedUpdate);
		internal void InternalUpdate() => InvokeMessageMethod(Message.Update);
		internal void InternalLateUpdate() => InvokeMessageMethod(Message.LateUpdate);

		private enum Message
		{
			Awake,
			OnDestroy,
			OnEnable,
			OnDisable,
			Start,
			FixedUpdate,
			Update,
			LateUpdate,
		}
	}
}
