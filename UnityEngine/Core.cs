using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
	public class Object
	{
		private static int _nextId = 1;
		internal static readonly HashSet<Object> _allObjects = new();

		internal static void Reset_UnitTestsOnly()
		{
			_nextId = 1;
			_allObjects.Clear();
		}

		public String name { get; set; }
		internal int InstanceId { get; } = _nextId++;

		public Object() => _allObjects.Add(this);

		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				if (obj is MonoBehaviour mb) mb.InternalOnDestroy();
				_allObjects.Remove(obj);
			}
		}

		public static void DontDestroyOnLoad(Object target) { }

		public static implicit operator Boolean(Object obj) => !ReferenceEquals(obj, null) && _allObjects.Contains(obj);

		public static bool operator ==(Object x, Object y) => ReferenceEquals(x, y) || (ReferenceEquals(x, null) && y is not null && !y) || (ReferenceEquals(y, null) && x is not null && !x);
		public static bool operator !=(Object x, Object y) => !(x == y);

		public override bool Equals(object obj) => ReferenceEquals(this, obj) || (obj is Object other && this == other);
		public override int GetHashCode() => InstanceId;

		public int GetInstanceID() => InstanceId;
	}

	public class Component : Object
	{
		public GameObject gameObject { get; internal set; }
		public Transform transform => gameObject.transform;

		public T GetComponent<T>() where T : Component => gameObject.GetComponent<T>();
		public T[] GetComponentsInChildren<T>(bool includeInactive = false) where T : Component => gameObject.GetComponentsInChildren<T>(includeInactive);
		public bool TryGetComponent<T>(out T component) where T : Component => gameObject.TryGetComponent(out component);
	}

	public class Behaviour : Component
	{
		private bool _enabled = true;
		public bool enabled
		{
			get => _enabled;
			set
			{
				if (_enabled == value) return;
				_enabled = value;
				if (gameObject.activeInHierarchy)
				{
					if (_enabled && this is MonoBehaviour mb) mb.InternalOnEnable();
					else if (!_enabled && this is MonoBehaviour mb2) mb2.InternalOnDisable();
				}
			}
		}
		public bool isActiveAndEnabled => enabled && gameObject != null && gameObject.activeInHierarchy;
	}

	public class MonoBehaviour : Behaviour
	{
		internal bool _awakeCalled;
		internal bool _startCalled;

		public Coroutine StartCoroutine(System.Collections.IEnumerator routine) => new Coroutine();

		protected virtual void Awake() { }
		protected virtual void Start() { }
		protected virtual void Update() { }
		protected virtual void FixedUpdate() { }
		protected virtual void OnEnable() { }
		protected virtual void OnDisable() { }
		protected virtual void OnDestroy() { }

		internal void InternalAwake() => Awake();
		internal void InternalStart() => Start();
		internal void InternalUpdate() => Update();
		internal void InternalFixedUpdate() => FixedUpdate();
		internal void InternalOnEnable() => OnEnable();
		internal void InternalOnDisable() => OnDisable();
		internal void InternalOnDestroy() => OnDestroy();
	}

	public class Coroutine { }
	public class YieldInstruction { }
	public class WaitForEndOfFrame : YieldInstruction { }

	public class Renderer : Component
	{
		public bool enabled { get; set; } = true;

		public bool isVisible => enabled && gameObject.activeInHierarchy;
	}

	public class Transform : Behaviour, System.Collections.IEnumerable
	{
		private Transform _parent;
		private readonly List<Transform> _children = new();

		public Transform parent
		{
			get => _parent;
			set
			{
				if (_parent == value) return;
				_parent?._children.Remove(this);
				_parent = value;
				_parent?._children.Add(this);
			}
		}

		public int childCount => _children.Count;

		public Transform GetChild(int index) => _children[index];

		public System.Collections.IEnumerator GetEnumerator() => _children.GetEnumerator();

		public Vector3 localPosition { get; set; }
		public Vector3 position
		{
			get => parent == null ? localPosition : parent.position + localPosition;
			set => localPosition = parent == null ? value : value - parent.position;
		}
	}

	public class GameObject : Object
	{
		private readonly List<Component> _components = new();
		private readonly Transform _transform;

		public GameObject() : this("New GameObject") { }

		public GameObject(string name)
		{
			this.name = name;
			_transform = AddComponent<Transform>();
			activeSelf = true;
		}

		public bool activeSelf { get; private set; }
		public bool activeInHierarchy => activeSelf && (transform.parent == null || transform.parent.gameObject.activeInHierarchy);

		public Transform transform => _transform;

		public void SetActive(bool value)
		{
			if (activeSelf == value) return;
			
			bool wasActiveInHierarchy = activeInHierarchy;
			activeSelf = value;
			bool isNowActiveInHierarchy = activeInHierarchy;

			if (wasActiveInHierarchy != isNowActiveInHierarchy)
			{
				if (isNowActiveInHierarchy)
				{
					foreach (var mb in GetComponentsInChildren<MonoBehaviour>(true))
					{
						if (mb.enabled) mb.InternalOnEnable();
					}
				}
				else
				{
					foreach (var mb in GetComponentsInChildren<MonoBehaviour>(true))
					{
						if (mb.enabled) mb.InternalOnDisable();
					}
				}
			}
		}

		public T GetComponent<T>() where T : Component => _components.OfType<T>().FirstOrDefault();

		public T[] GetComponentsInChildren<T>(bool includeInactive = false) where T : Component
		{
			var results = new List<T>();
			if (activeInHierarchy || includeInactive)
			{
				results.AddRange(_components.OfType<T>());
			}

			foreach (Transform child in transform)
			{
				results.AddRange(child.gameObject.GetComponentsInChildren<T>(includeInactive));
			}

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
					mb.InternalAwake();
					if (activeInHierarchy && mb.enabled)
					{
						mb.InternalOnEnable();
					}
				}
			}
			return component;
		}

		public bool TryGetComponent<T>(out T component) where T : Component
		{
			component = GetComponent<T>();
			return component != null;
		}

		public static GameObject CreatePrimitive(PrimitiveType type) => new(type.ToString());
		public static GameObject Find(string name) => _allObjects.OfType<GameObject>().FirstOrDefault(go => go.name == name);
	}

	public enum LogType
	{
		Error,
		Assert,
		Warning,
		Log,
		Exception,
	}

	public static class Debug
	{
		public static void Log(object message) => Console.WriteLine(message);
		public static void LogWarning(object message) => Console.WriteLine($"[Warning] {message}");
		public static void LogError(object message) => Console.Error.WriteLine($"[Error] {message}");
		public static void LogException(Exception exception) => Console.Error.WriteLine($"[Exception] {exception}");
		public static void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			var message = string.Format(format, args);
			switch (logType)
			{
				case LogType.Error:
				case LogType.Assert:
				case LogType.Exception:
					Console.Error.WriteLine($"[{logType}] {message}");
					break;
				case LogType.Warning:
					Console.WriteLine($"[Warning] {message}");
					break;
				case LogType.Log:
					Console.WriteLine(message);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
			}
		}

		public static void Break() => Console.WriteLine("[Debug.Break called]");
	}

	public static class Time
	{
		public static float deltaTime { get; internal set; } = 1f / 60f;
		public static float unscaledDeltaTime => deltaTime;
		public static float fixedDeltaTime => 0.02f;
		public static float time { get; internal set; }
		public static double realtimeSinceStartupAsDouble => time;
		public static long frameCount { get; internal set; }
	}

	public static class Application
	{
		public static string dataPath => "MockDataPath";
		public static string persistentDataPath => "MockPersistentDataPath";
		public static bool isEditor => true;
		public static bool isPlaying => true;
		public static void Quit() => Console.WriteLine("Application.Quit called");
		public static void Quit(int exitCode) => Console.WriteLine($"Application.Quit({exitCode}) called");
	}

	public enum PrimitiveType
	{
		Sphere,
		Capsule,
		Cylinder,
		Cube,
		Plane,
		Quad,
	}

	public struct Vector3
	{
		public float x, y, z;
		public Vector3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
		public static Vector3 zero => new(0, 0, 0);
		public static Vector3 one => new(1, 1, 1);
		public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
		public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
	}
}
