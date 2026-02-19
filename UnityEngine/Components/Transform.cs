using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
	public sealed class Transform : Behaviour, IEnumerable
	{
		private readonly List<Transform> _children = new();
		private Transform _parent;

		// ── Hierarchy ──────────────────────────────────────────────────────────────

		public Transform parent
		{
			get => _parent;
			set => SetParent(value);
		}

		public Transform root
		{
			get
			{
				var t = this;
				while (t._parent != null)
					t = t._parent;
				return t;
			}
		}

		public Int32 childCount => _children.Count;

		public void SetParent(Transform p) => SetParent(p, worldPositionStays: true);

		public void SetParent(Transform p, Boolean worldPositionStays)
		{
			if (_parent == p)
				return;

			var worldPos = position;
			var worldRot = rotation;

			_parent?._children.Remove(this);
			_parent = p;
			_parent?._children.Add(this);

			if (worldPositionStays)
			{
				position = worldPos;
				rotation = worldRot;
			}
		}

		public Transform GetChild(Int32 index) => _children[index];

		public Boolean IsChildOf(Transform parent)
		{
			var t = _parent;
			while (t != null)
			{
				if (t == parent)
					return true;
				t = t._parent;
			}
			return false;
		}

		public Int32 GetSiblingIndex() => _parent == null ? 0 : _parent._children.IndexOf(this);

		public void SetSiblingIndex(Int32 index)
		{
			if (_parent == null)
				return;
			_parent._children.Remove(this);
			_parent._children.Insert(Math.Clamp(index, 0, _parent._children.Count), this);
		}

		public void SetAsFirstSibling() => SetSiblingIndex(0);
		public void SetAsLastSibling() => SetSiblingIndex(Int32.MaxValue);

		public void DetachChildren()
		{
			foreach (var child in _children)
				child._parent = null;
			_children.Clear();
		}

		public IEnumerator GetEnumerator() => _children.GetEnumerator();

		// ── World / Local position ─────────────────────────────────────────────────

		public Vector3 localPosition { get; set; }

		public Vector3 position
		{
			get => _parent == null ? localPosition : _parent.position + _parent.rotation * localPosition;
			set => localPosition = _parent == null ? value : Quaternion.Inverse(_parent.rotation) * (value - _parent.position);
		}

		// ── World / Local rotation ─────────────────────────────────────────────────

		public Quaternion localRotation { get; set; } = Quaternion.identity;

		public Quaternion rotation
		{
			get => _parent == null ? localRotation : _parent.rotation * localRotation;
			set => localRotation = _parent == null ? value : Quaternion.Inverse(_parent.rotation) * value;
		}

		public Vector3 eulerAngles
		{
			get => rotation.eulerAngles;
			set => rotation = Quaternion.Euler(value);
		}

		public Vector3 localEulerAngles
		{
			get => localRotation.eulerAngles;
			set => localRotation = Quaternion.Euler(value);
		}

		// ── Scale ─────────────────────────────────────────────────────────────────

		public Vector3 localScale { get; set; } = Vector3.one;

		// ── Direction vectors ──────────────────────────────────────────────────────

		public Vector3 forward => rotation * Vector3.forward;
		public Vector3 back => rotation * Vector3.back;
		public Vector3 up => rotation * Vector3.up;
		public Vector3 down => rotation * Vector3.down;
		public Vector3 right => rotation * Vector3.right;
		public Vector3 left => rotation * Vector3.left;

		// ── Coordinate conversion ──────────────────────────────────────────────────

		public Vector3 TransformPoint(Vector3 point) => position + rotation * Vector3.Scale(point, localScale);
		public Vector3 InverseTransformPoint(Vector3 point) => Vector3.Scale(Quaternion.Inverse(rotation) * (point - position), new Vector3(1f / localScale.x, 1f / localScale.y, 1f / localScale.z));
		public Vector3 TransformDirection(Vector3 direction) => rotation * direction;
		public Vector3 InverseTransformDirection(Vector3 direction) => Quaternion.Inverse(rotation) * direction;
		public Vector3 TransformVector(Vector3 vector) => rotation * Vector3.Scale(vector, localScale);
		public Vector3 InverseTransformVector(Vector3 vector) => Vector3.Scale(Quaternion.Inverse(rotation) * vector, new Vector3(1f / localScale.x, 1f / localScale.y, 1f / localScale.z));

		// ── Orientation helpers ────────────────────────────────────────────────────

		public void LookAt(Transform target) => LookAt(target.position, Vector3.up);
		public void LookAt(Transform target, Vector3 worldUp) => LookAt(target.position, worldUp);
		public void LookAt(Vector3 worldPosition) => LookAt(worldPosition, Vector3.up);

		public void LookAt(Vector3 worldPosition, Vector3 worldUp)
		{
			var dir = worldPosition - position;
			if (dir.sqrMagnitude > 0f)
				rotation = Quaternion.LookRotation(dir.normalized, worldUp);
		}

		public void Rotate(Vector3 eulerAngles, Space relativeTo = Space.Self)
		{
			var q = Quaternion.Euler(eulerAngles);
			if (relativeTo == Space.Self)
				localRotation *= q;
			else
				rotation *= Quaternion.Inverse(rotation) * q * rotation;
		}

		public void Rotate(Vector3 axis, Single angle, Space relativeTo = Space.Self)
			=> Rotate(axis * angle, relativeTo);

		public void RotateAround(Vector3 point, Vector3 axis, Single angle)
		{
			var q = Quaternion.AngleAxis(angle, axis);
			position = q * (position - point) + point;
			rotation = q * rotation;
		}

		public void Translate(Vector3 translation, Space relativeTo = Space.Self)
		{
			if (relativeTo == Space.Self)
				position += rotation * translation;
			else
				position += translation;
		}
	}
}
