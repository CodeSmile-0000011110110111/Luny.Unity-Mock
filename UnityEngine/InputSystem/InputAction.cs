using System;

namespace UnityEngine.InputSystem
{
	public sealed class InputAction
	{
		public event Action<CallbackContext> started;
		public event Action<CallbackContext> performed;
		public event Action<CallbackContext> canceled;
		private System.Object _lastValue;
		private InputActionPhase _phase = InputActionPhase.Disabled;

		public String name { get; }
		public InputActionType type { get; }
		public String expectedControlType { get; }
		public InputActionPhase phase => _phase;
		public Boolean enabled => _phase != InputActionPhase.Disabled;

		public InputAction(String name = default, InputActionType type = default, String expectedControlType = default)
		{
			this.name = name;
			this.type = type;
			this.expectedControlType = expectedControlType;
		}

		public void Enable()
		{
			if (_phase == InputActionPhase.Disabled)
				_phase = InputActionPhase.Waiting;
		}

		public void Disable() => _phase = InputActionPhase.Disabled;

		public TValue ReadValue<TValue>() where TValue : struct => _lastValue is TValue v ? v : default;

		/// <summary>
		/// Internal method for tests to trigger the started callback.
		/// </summary>
		internal void InternalTriggerStarted<T>(T value) where T : struct
		{
			_lastValue = value;
			_phase = InputActionPhase.Started;
			started?.Invoke(new CallbackContext(this, _phase, value));
		}

		/// <summary>
		/// Internal method for tests to trigger the performed callback.
		/// </summary>
		internal void InternalTriggerPerformed<T>(T value) where T : struct
		{
			_lastValue = value;
			_phase = InputActionPhase.Performed;
			performed?.Invoke(new CallbackContext(this, _phase, value));
		}

		/// <summary>
		/// Internal method for tests to trigger the canceled callback.
		/// </summary>
		internal void InternalTriggerCanceled<T>(T value) where T : struct
		{
			_lastValue = value;
			_phase = InputActionPhase.Canceled;
			canceled?.Invoke(new CallbackContext(this, _phase, value));
			_phase = InputActionPhase.Waiting;
		}

		public readonly struct CallbackContext
		{
			private readonly System.Object _value;

			public InputAction action { get; }
			public InputActionPhase phase { get; }

			internal CallbackContext(InputAction action, InputActionPhase phase, System.Object value)
			{
				this.action = action;
				this.phase = phase;
				_value = value;
			}

			public TValue ReadValue<TValue>() where TValue : struct => _value is TValue v ? v : default;
		}
	}
}
