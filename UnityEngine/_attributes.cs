using System;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Class)]
	public class AddComponentMenuAttribute : Attribute
	{
		public AddComponentMenuAttribute(String menuName) {}
		public AddComponentMenuAttribute(String menuName, Int32 order) {}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class DefaultExecutionOrderAttribute : Attribute
	{
		public DefaultExecutionOrderAttribute(Int32 order) {}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class DisallowMultipleComponentAttribute : Attribute {}

	[AttributeUsage(AttributeTargets.Field)]
	public class HeaderAttribute : Attribute
	{
		public HeaderAttribute(String header) {}
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class RuntimeInitializeOnLoadMethodAttribute : Attribute
	{
		public RuntimeInitializeOnLoadMethodAttribute() {}
		public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType) {}
	}

	[AttributeUsage(AttributeTargets.Field)]
	public class SerializeField : Attribute {}
}
