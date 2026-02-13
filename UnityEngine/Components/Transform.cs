using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
	public sealed class Transform : Behaviour, IEnumerable
	{
		private readonly List<Transform> _children = new();
		private Transform _parent;

		public Transform parent
		{
			get => _parent;
			set
			{
				if (_parent == value)
					return;

				_parent?._children.Remove(this);
				_parent = value;
				_parent?._children.Add(this);
			}
		}

		public Int32 childCount => _children.Count;

		public Vector3 localPosition { get; set; }
		public Vector3 position
		{
			get => parent == null ? localPosition : parent.position + localPosition;
			set => localPosition = parent == null ? value : value - parent.position;
		}

		public IEnumerator GetEnumerator() => _children.GetEnumerator();

		public Transform GetChild(Int32 index) => _children[index];
	}
}
