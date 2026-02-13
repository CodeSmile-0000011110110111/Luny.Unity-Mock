using System;
using System.Collections;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace UnityEngine
{
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
				try
				{
					Console.WriteLine($"[DEBUG_LOG] [{nameof(MonoBehaviour)}] {type.Name} => {methodName}()");
					method.Invoke(this, null);
				}
				catch (TargetInvocationException ex)
				{
					// To prevent exceptions changing to TargetInvocationException in tests,
					// we capture the original exception and re-throw it without losing the stack trace
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				}
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
