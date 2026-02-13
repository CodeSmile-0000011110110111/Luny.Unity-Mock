using System;

namespace UnityEngine
{
	public class Behaviour : Component
	{
		private Boolean _enabled = true;
		public Boolean enabled
		{
			get => _enabled;
			set
			{
				if (_enabled == value)
					return;

				_enabled = value;
				if (gameObject.activeInHierarchy)
				{
					if (_enabled && this is MonoBehaviour mb)
						mb.InternalOnEnable();
					else if (!_enabled && this is MonoBehaviour mb2)
						mb2.InternalOnDisable();
				}
			}
		}
		public Boolean isActiveAndEnabled => enabled && gameObject != null && gameObject.activeInHierarchy;
	}
}
