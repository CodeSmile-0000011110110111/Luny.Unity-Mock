using System;

namespace UnityEngine
{
	public struct Vector2 : IEquatable<Vector2>
	{
		private System.Numerics.Vector2 _value;

		public Single x
		{
			get => _value.X;
			set => _value.X = value;
		}

		public Single y
		{
			get => _value.Y;
			set => _value.Y = value;
		}

		public Single this[Int32 index]
		{
			get => index switch
			{
				0 => x,
				1 => y,
				var _ => throw new IndexOutOfRangeException("Invalid Vector2 index!"),
			};
			set
			{
				switch (index)
				{
					case 0:
						x = value;
						break;
					case 1:
						y = value;
						break;
					default:
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
				}
			}
		}

		public Vector2(Single x, Single y) => _value = new System.Numerics.Vector2(x, y);

		public static Vector2 zero => new(0, 0);
		public static Vector2 one => new(1, 1);
		public static Vector2 up => new(0, 1);
		public static Vector2 down => new(0, -1);
		public static Vector2 left => new(-1, 0);
		public static Vector2 right => new(1, 0);
		public static Vector2 positiveInfinity => new(Single.PositiveInfinity, Single.PositiveInfinity);
		public static Vector2 negativeInfinity => new(Single.NegativeInfinity, Single.NegativeInfinity);

		public Single magnitude => _value.Length();
		public Single sqrMagnitude => _value.LengthSquared();
		public Vector2 normalized
		{
			get
			{
				var len = _value.Length();
				return len > 1e-05f ? FromNumerics(_value / len) : zero;
			}
		}

		public void Normalize()
		{
			var len = _value.Length();
			if (len > 1e-05f)
				_value /= len;
			else
				_value = System.Numerics.Vector2.Zero;
		}

		public void Set(Single newX, Single newY) => _value = new System.Numerics.Vector2(newX, newY);
		public void Scale(Vector2 scale) => _value *= scale._value;

		public static Single Dot(Vector2 lhs, Vector2 rhs) => System.Numerics.Vector2.Dot(lhs._value, rhs._value);
		public static Single Distance(Vector2 a, Vector2 b) => System.Numerics.Vector2.Distance(a._value, b._value);
		public static Vector2 Perpendicular(Vector2 inDirection) => new(-inDirection.y, inDirection.x);

		public static Vector2 Lerp(Vector2 a, Vector2 b, Single t) =>
			FromNumerics(System.Numerics.Vector2.Lerp(a._value, b._value, Math.Clamp(t, 0f, 1f)));

		public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, Single t) =>
			FromNumerics(System.Numerics.Vector2.Lerp(a._value, b._value, t));

		public static Vector2 Max(Vector2 lhs, Vector2 rhs) => FromNumerics(System.Numerics.Vector2.Max(lhs._value, rhs._value));
		public static Vector2 Min(Vector2 lhs, Vector2 rhs) => FromNumerics(System.Numerics.Vector2.Min(lhs._value, rhs._value));
		public static Vector2 Scale(Vector2 a, Vector2 b) => FromNumerics(a._value * b._value);

		public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal) =>
			FromNumerics(System.Numerics.Vector2.Reflect(inDirection._value, inNormal._value));

		public static Vector2 Normalize(Vector2 value) => value.normalized;

		public static implicit operator Vector3(Vector2 v) => new(v.x, v.y, 0f);
		public static explicit operator Vector2(Vector3 v) => new(v.x, v.y);

		public static Vector2 operator +(Vector2 a, Vector2 b) => FromNumerics(a._value + b._value);
		public static Vector2 operator -(Vector2 a, Vector2 b) => FromNumerics(a._value - b._value);
		public static Vector2 operator -(Vector2 a) => FromNumerics(-a._value);
		public static Vector2 operator *(Vector2 a, Single d) => FromNumerics(a._value * d);
		public static Vector2 operator *(Single d, Vector2 a) => FromNumerics(a._value * d);
		public static Vector2 operator *(Vector2 a, Vector2 b) => FromNumerics(a._value * b._value);
		public static Vector2 operator /(Vector2 a, Single d) => FromNumerics(a._value / d);
		public static Vector2 operator /(Vector2 a, Vector2 b) => FromNumerics(a._value / b._value);

		public static Boolean operator ==(Vector2 lhs, Vector2 rhs) => (lhs._value - rhs._value).LengthSquared() < 9.99999944e-11f;
		public static Boolean operator !=(Vector2 lhs, Vector2 rhs) => !(lhs == rhs);

		public Boolean Equals(Vector2 other) => _value.Equals(other._value);
		public override Boolean Equals(System.Object obj) => obj is Vector2 other && Equals(other);
		public override Int32 GetHashCode() => _value.GetHashCode();
		public override String ToString() => $"({x:F1}, {y:F1})";

		private static Vector2 FromNumerics(System.Numerics.Vector2 v)
		{
			var result = new Vector2();
			result._value = v;
			return result;
		}
	}
}
