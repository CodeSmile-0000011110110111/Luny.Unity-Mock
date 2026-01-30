using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
	public class Object
	{
		private static Int32 _nextId = 1;
		internal static readonly HashSet<Object> _allObjects = new();

		public String name { get; set; }
		internal Int32 InstanceId { get; } = _nextId++;

		public static implicit operator Boolean(Object obj) => !ReferenceEquals(obj, null) && _allObjects.Contains(obj);

		public static Boolean operator ==(Object x, Object y) => ReferenceEquals(x, y) || ReferenceEquals(x, null) && y is not null && !y ||
		                                                         ReferenceEquals(y, null) && x is not null && !x;

		public static Boolean operator !=(Object x, Object y) => !(x == y);

		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				if (obj is MonoBehaviour mb)
					mb.InternalOnDestroy();
				_allObjects.Remove(obj);
			}
		}

		public static void DontDestroyOnLoad(Object target) {}

		internal static void Reset_UnitTestsOnly()
		{
			_nextId = 1;
			var objects = _allObjects.ToList();
			foreach (var obj in objects)
				_allObjects.Remove(obj);
		}

		public static T Instantiate<T>(T original) where T : Object
		{
			if (original == null)
				throw new ArgumentNullException(nameof(original));

			// Mock: simple instantiation by creating a new instance of the same type
			var instance = Activator.CreateInstance(original.GetType()) as T;
			if (instance is GameObject go && original is GameObject originalGo)
			{
				go.name = originalGo.name + "(Clone)";
				// Mock: we don't clone components for now to keep it simple
			}
			return instance;
		}

		public Object() => _allObjects.Add(this);

		public override Boolean Equals(System.Object obj) => ReferenceEquals(this, obj) || obj is Object other && this == other;
		public override Int32 GetHashCode() => InstanceId;

		public Int32 GetInstanceID() => InstanceId;
		public Int64 GetEntityId() => InstanceId;
	}
}
