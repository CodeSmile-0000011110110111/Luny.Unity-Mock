using System;

namespace UnityEngine
{
	public class MonoBehaviour : Behaviour
	{
		public Coroutine StartCoroutine(System.Collections.IEnumerator routine) => throw new NotImplementedException("UnityEngine.MonoBehaviour.StartCoroutine");
	}

	public class Coroutine {}
	public class YieldInstruction {}
	public class WaitForEndOfFrame : YieldInstruction {}
}
