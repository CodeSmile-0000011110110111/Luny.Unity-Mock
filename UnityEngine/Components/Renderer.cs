using System;

namespace UnityEngine
{
	public class Renderer : Component
	{
		public Boolean enabled { get; set; } = true;

		public Boolean isVisible => enabled && gameObject.activeInHierarchy;
	}
}
