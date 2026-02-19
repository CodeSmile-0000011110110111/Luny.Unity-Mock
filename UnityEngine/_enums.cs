namespace UnityEngine
{
	public enum Space
	{
		World,
		Self,
	}

	public enum LogType
	{
		Error,
		Assert,
		Warning,
		Log,
		Exception,
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

	public enum RuntimeInitializeLoadType
	{
		AfterSceneLoad,
		BeforeSceneLoad,
		AfterAssembliesLoaded,
		BeforeSplashScreen,
		SubsystemRegistration,
	}
}
