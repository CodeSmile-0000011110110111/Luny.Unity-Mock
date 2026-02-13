using System;

namespace UnityEngine
{
	public struct Vector3
	{
		public Single x, y, z;

		public Vector3(Single x, Single y, Single z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static Vector3 zero => new(0, 0, 0);
		public static Vector3 one => new(1, 1, 1);
		public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
		public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
	}
}
