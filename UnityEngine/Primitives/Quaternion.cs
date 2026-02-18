using System;

namespace UnityEngine
{
	public struct Quaternion : IEquatable<Quaternion>
	{
		private System.Numerics.Quaternion _value;

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

		public Single w
		{
			get => _value.W;
			set => _value.W = value;
		}

		public Single this[Int32 index]
		{
			get => index switch
			{
				0 => x,
				1 => y,
				2 => z,
				3 => w,
				var _ => throw new IndexOutOfRangeException("Invalid Quaternion index!"),
			};
			set
			{
				switch (index)
				{
					case 0: x = value; break;
					case 1: y = value; break;
					case 2: z = value; break;
					case 3: w = value; break;
					default: throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
			}
		}

		public Quaternion(Single x, Single y, Single z, Single w) => _value = new System.Numerics.Quaternion(x, y, z, w);

		public static Quaternion identity => new(0, 0, 0, 1);

		public Quaternion normalized
		{
			get
			{
				var n = System.Numerics.Quaternion.Normalize(_value);
				return FromNumerics(n);
			}
		}

		public void Normalize() => _value = System.Numerics.Quaternion.Normalize(_value);
		public void Set(Single newX, Single newY, Single newZ, Single newW) => _value = new System.Numerics.Quaternion(newX, newY, newZ, newW);

		public static Single Dot(Quaternion a, Quaternion b) => System.Numerics.Quaternion.Dot(a._value, b._value);
		public static Quaternion Inverse(Quaternion rotation) => FromNumerics(System.Numerics.Quaternion.Inverse(rotation._value));

		public static Quaternion Slerp(Quaternion a, Quaternion b, Single t) =>
			FromNumerics(System.Numerics.Quaternion.Slerp(a._value, b._value, Math.Clamp(t, 0f, 1f)));

		public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, Single t) =>
			FromNumerics(System.Numerics.Quaternion.Slerp(a._value, b._value, t));

		public static Quaternion Lerp(Quaternion a, Quaternion b, Single t) =>
			FromNumerics(System.Numerics.Quaternion.Lerp(a._value, b._value, Math.Clamp(t, 0f, 1f)));

		public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, Single t) =>
			FromNumerics(System.Numerics.Quaternion.Lerp(a._value, b._value, t));

		public static Quaternion AngleAxis(Single angle, Vector3 axis) =>
			FromNumerics(System.Numerics.Quaternion.CreateFromAxisAngle(
				new System.Numerics.Vector3(axis.x, axis.y, axis.z), angle * (MathF.PI / 180f)));

		public static Quaternion operator *(Quaternion lhs, Quaternion rhs) =>
			FromNumerics(System.Numerics.Quaternion.Multiply(lhs._value, rhs._value));

		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			var q = rotation._value;
			var u = new System.Numerics.Vector3(q.X, q.Y, q.Z);
			var s = q.W;
			var p = new System.Numerics.Vector3(point.x, point.y, point.z);
			var result = 2f * System.Numerics.Vector3.Dot(u, p) * u
				+ (s * s - System.Numerics.Vector3.Dot(u, u)) * p
				+ 2f * s * System.Numerics.Vector3.Cross(u, p);
			return new Vector3(result.X, result.Y, result.Z);
		}

		public static Boolean operator ==(Quaternion lhs, Quaternion rhs) =>
			MathF.Abs(Dot(lhs, rhs)) > 1f - 1e-06f;

		public static Boolean operator !=(Quaternion lhs, Quaternion rhs) => !(lhs == rhs);

		public Boolean Equals(Quaternion other) => _value.Equals(other._value);
		public override Boolean Equals(System.Object obj) => obj is Quaternion other && Equals(other);
		public override Int32 GetHashCode() => _value.GetHashCode();
		public override String ToString() => $"({x:F1}, {y:F1}, {z:F1}, {w:F1})";

		private static Quaternion FromNumerics(System.Numerics.Quaternion v)
		{
			var result = new Quaternion();
			result._value = v;
			return result;
		}
	}
}
