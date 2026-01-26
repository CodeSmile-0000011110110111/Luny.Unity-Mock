using System;

namespace UnityEngine
{
	public static class UnityExtensions
	{
		public static Int64 GetEntityId(this GameObject gameObject) => gameObject.GetInstanceID();
	}
}
