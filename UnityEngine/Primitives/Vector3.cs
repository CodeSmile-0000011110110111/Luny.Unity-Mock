using System;

namespace UnityEngine
{
	public struct Vector3 : IEquatable<Vector3>
	{
		private System.Numerics.Vector3 _value;

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

		public Single z
		{
			get => _value.Z;
			set => _value.Z = value;
		}

		public Single this[Int32 index]
		{
			get => index switch
			{
				0 => x,
				1 => y,
				2 => z,
				var _ => throw new IndexOutOfRangeException("Invalid Vector3 index!"),
			};
			set
			{
				switch (index)
				{
					case 0: x = value; break;
					case 1: y = value; break;
					case 2: z = value; break;
					default: throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		public Vector3(Single x, Single y, Single z) => _value = new System.Numerics.Vector3(x, y, z);
		public Vector3(Single x, Single y) => _value = new System.Numerics.Vector3(x, y, 0f);

		public static Vector3 zero => new(0, 0, 0);
		public static Vector3 one => new(1, 1, 1);
		public static Vector3 up => new(0, 1, 0);
		public static Vector3 down => new(0, -1, 0);
		public static Vector3 left => new(-1, 0, 0);
		public static Vector3 right => new(1, 0, 0);
		public static Vector3 forward => new(0, 0, 1);
		public static Vector3 back => new(0, 0, -1);
		public static Vector3 positiveInfinity => new(Single.PositiveInfinity, Single.PositiveInfinity, Single.PositiveInfinity);
		public static Vector3 negativeInfinity => new(Single.NegativeInfinity, Single.NegativeInfinity, Single.NegativeInfinity);

		public Single magnitude => _value.Length();
		public Single sqrMagnitude => _value.LengthSquared();
		public Vector3 normalized
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
				_value = System.Numerics.Vector3.Zero;
		}

		public void Set(Single newX, Single newY, Single newZ) => _value = new System.Numerics.Vector3(newX, newY, newZ);
		public void Scale(Vector3 scale) => _value *= scale._value;

		public static Single Dot(Vector3 lhs, Vector3 rhs) => System.Numerics.Vector3.Dot(lhs._value, rhs._value);
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs) => FromNumerics(System.Numerics.Vector3.Cross(lhs._value, rhs._value));
		public static Single Distance(Vector3 a, Vector3 b) => System.Numerics.Vector3.Distance(a._value, b._value);

		public static Vector3 Lerp(Vector3 a, Vector3 b, Single t) =>
			FromNumerics(System.Numerics.Vector3.Lerp(a._value, b._value, Math.Clamp(t, 0f, 1f)));

		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, Single t) =>
			FromNumerics(System.Numerics.Vector3.Lerp(a._value, b._value, t));

		public static Vector3 Max(Vector3 lhs, Vector3 rhs) => FromNumerics(System.Numerics.Vector3.Max(lhs._value, rhs._value));
		public static Vector3 Min(Vector3 lhs, Vector3 rhs) => FromNumerics(System.Numerics.Vector3.Min(lhs._value, rhs._value));
		public static Vector3 Scale(Vector3 a, Vector3 b) => FromNumerics(a._value * b._value);
		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal) => FromNumerics(System.Numerics.Vector3.Reflect(inDirection._value, inNormal._value));
		public static Vector3 Normalize(Vector3 value) => value.normalized;

		public static Vector3 operator +(Vector3 a, Vector3 b) => FromNumerics(a._value + b._value);
		public static Vector3 operator -(Vector3 a, Vector3 b) => FromNumerics(a._value - b._value);
		public static Vector3 operator -(Vector3 a) => FromNumerics(-a._value);
		public static Vector3 operator *(Vector3 a, Single d) => FromNumerics(a._value * d);
		public static Vector3 operator *(Single d, Vector3 a) => FromNumerics(a._value * d);
		public static Vector3 operator /(Vector3 a, Single d) => FromNumerics(a._value / d);

		public static Boolean operator ==(Vector3 lhs, Vector3 rhs) => (lhs._value - rhs._value).LengthSquared() < 9.99999944e-11f;
		public static Boolean operator !=(Vector3 lhs, Vector3 rhs) => !(lhs == rhs);

		public Boolean Equals(Vector3 other) => _value.Equals(other._value);
		public override Boolean Equals(System.Object obj) => obj is Vector3 other && Equals(other);
		public override Int32 GetHashCode() => _value.GetHashCode();
		public override String ToString() => $"({x:F1}, {y:F1}, {z:F1})";

		private static Vector3 FromNumerics(System.Numerics.Vector3 v)
		{
			var result = new Vector3();
			result._value = v;
			return result;
		}
	}
}
