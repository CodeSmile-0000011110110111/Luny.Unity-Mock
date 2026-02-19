using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem
{
	public sealed class InputActionAsset : Object
	{
		private readonly List<InputActionMap> _actionMaps = new();

		public IReadOnlyList<InputActionMap> actionMaps => _actionMaps;
		public String enabled { get; set; }

		public void Enable()
		{
			foreach (var map in _actionMaps)
				map.Enable();
		}

		public void Disable()
		{
			foreach (var map in _actionMaps)
				map.Disable();
		}

		public InputActionMap FindActionMap(String name, Boolean throwIfNotFound = false)
		{
			var map = _actionMaps.FirstOrDefault(m => m.name == name);
			if (map == null && throwIfNotFound)
				throw new ArgumentException($"Action map '{name}' not found");

			return map;
		}

		/// <summary>
		/// Internal method for tests to add action maps to this asset.
		/// </summary>
		internal void InternalAddActionMap(InputActionMap map) => _actionMaps.Add(map);
	}
}
