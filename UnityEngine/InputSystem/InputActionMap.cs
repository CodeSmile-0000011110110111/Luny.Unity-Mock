using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem
{
	public sealed class InputActionMap
	{
		private readonly List<InputAction> _actions = new();

		public String name { get; }
		public IReadOnlyList<InputAction> actions => _actions;
		public Boolean enabled { get; private set; }

		public InputActionMap(String name = default)
		{
			this.name = name;
		}

		public void Enable()
		{
			enabled = true;
			foreach (var action in _actions)
				action.Enable();
		}

		public void Disable()
		{
			enabled = false;
			foreach (var action in _actions)
				action.Disable();
		}

		public InputAction FindAction(String name, Boolean throwIfNotFound = false)
		{
			var action = _actions.FirstOrDefault(a => a.name == name);
			if (action == null && throwIfNotFound)
				throw new ArgumentException($"Action '{name}' not found in action map '{this.name}'");
			return action;
		}

		/// <summary>
		/// Internal method for tests to add actions to this map.
		/// </summary>
		internal void InternalAddAction(InputAction action) => _actions.Add(action);
	}
}
