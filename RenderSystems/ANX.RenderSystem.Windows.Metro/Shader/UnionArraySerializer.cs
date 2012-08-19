using System;
using System.Runtime.InteropServices;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro.Shader
{
	public static class UnionArraySerializer
	{
		#region Union (private helper struct)
		[StructLayout(LayoutKind.Explicit)]
		private struct Union
		{
			[FieldOffset(0)]
			public byte[] bytes;
			[FieldOffset(0)]
			public float[] floats;
			[FieldOffset(0)]
			public int[] ints;
		}
		#endregion

		#region Private
		private static readonly UIntPtr BYTE_ARRAY;
		private static unsafe readonly int PTR_SIZE = sizeof(UIntPtr);
		#endregion

		#region Constructor
		static unsafe UnionArraySerializer()
		{
			var byteArray = new byte[1];

			fixed (byte* pBytes = byteArray)
			{
				BYTE_ARRAY = *(UIntPtr*)(pBytes - 2 * PTR_SIZE);
			}
		}
		#endregion

		#region Unify (float[])
		public static unsafe byte[] Unify(float[] floats)
		{
			var union = new Union();
			union.floats = floats;

			fixed (float* pValues = union.floats)
				UpdatePointers((byte*)pValues, floats.Length * 4);

			return union.bytes;
		}
		#endregion

		#region Unify (int[])
		public static unsafe byte[] Unify(int[] ints)
		{
			var union = new Union();
			union.ints = ints;

			fixed (int* pValues = union.ints)
				UpdatePointers((byte*)pValues, ints.Length * 4);

			return union.bytes;
		}
		#endregion

		#region UpdatePointers
		private static unsafe void UpdatePointers(byte* pBytes, int byteSize)
		{
			var pSize = (UIntPtr*)(pBytes - PTR_SIZE);
			var pArrayType = (UIntPtr*)(pBytes - 2 * PTR_SIZE);

			*pSize = (UIntPtr)byteSize;
			*pArrayType = BYTE_ARRAY;
		}
		#endregion
	}
}
