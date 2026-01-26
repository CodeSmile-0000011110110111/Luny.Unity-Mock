using System;

namespace UnityEngine
{
	public class Object
	{
		public String name { get; set; }

		public static void Destroy(Object obj) => throw new NotImplementedException("UnityEngine.Object.Destroy");
		public static void DontDestroyOnLoad(Object target) => throw new NotImplementedException("UnityEngine.Object.DontDestroyOnLoad");

		public static implicit operator Boolean(Object obj) => obj != null;

		public static bool operator ==(Object x, Object y) => ReferenceEquals(x, y);
		public static bool operator !=(Object x, Object y) => !ReferenceEquals(x, y);

		public override bool Equals(object obj) => base.Equals(obj);
		public override int GetHashCode() => base.GetHashCode();
	}
}
