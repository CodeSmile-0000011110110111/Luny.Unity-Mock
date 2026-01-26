using System;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Class)]
	public class DefaultExecutionOrderAttribute : Attribute
	{
		public DefaultExecutionOrderAttribute(int order) {}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class AddComponentMenuAttribute : Attribute
	{
		public AddComponentMenuAttribute(string menuName) {}
		public AddComponentMenuAttribute(string menuName, int order) {}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class DisallowMultipleComponentAttribute : Attribute
	{
	}

	public enum RuntimeInitializeLoadType
	{
		AfterSceneLoad,
		BeforeSceneLoad,
		AfterAssembliesLoaded,
		BeforeSplashScreen,
		SubsystemRegistration,
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class RuntimeInitializeOnLoadMethodAttribute : Attribute
	{
		public RuntimeInitializeOnLoadMethodAttribute() {}
		public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType) {}
	}
	[AttributeUsage(AttributeTargets.Field)]
	public class SerializeFieldAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Field)]
	public class HeaderAttribute : Attribute
	{
		public HeaderAttribute(string header) {}
	}
}
