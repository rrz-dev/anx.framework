using System;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;
using OpenTK.Graphics.OpenGL;

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Windows.GL3
{
	/// <summary>
	/// Native OpenGL implementation of a Index Buffer.
	/// </summary>
	public class IndexBufferGL3 : INativeBuffer
	{
		#region Private
		/// <summary>
		/// Native index buffer handle.
		/// </summary>
		private int bufferHandle;

		private int indexCount;

		private IndexElementSize elementSize;

		private BufferUsage usage;

		private BufferUsageHint usageHint;
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new Index Buffer object.
		/// </summary>
		internal IndexBufferGL3(IndexElementSize setElementSize,
			int setIndexCount, BufferUsage setUsage)
		{
			indexCount = setIndexCount;
			elementSize = setElementSize;
			usage = setUsage;

			// TODO: evaluate whats best
			// StaticDraw: set once, use often
			// DynamicDraw: set frequently, use repeatadly
			// StreamDraw: set every tick, use once
			usageHint = BufferUsageHint.DynamicDraw;

			GL.GenBuffers(1, out bufferHandle);
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data)
			where T : struct
		{
			BufferData(data, 0);
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			if (startIndex != 0 ||
				elementCount != data.Length)
			{
				T[] subArray = new T[elementCount];
				Array.Copy(data, startIndex, subArray, 0, elementCount);
				BufferData(subArray, 0);
			}
			else
			{
				BufferData(data, 0);
			}
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
			T[] data, int startIndex, int elementCount) where T : struct
		{
			if (startIndex != 0 ||
				elementCount != data.Length)
			{
				T[] subArray = new T[elementCount];
				Array.Copy(data, startIndex, subArray, 0, elementCount);
				BufferData(subArray, offsetInBytes);
			}
			else
			{
				BufferData(data, offsetInBytes);
			}
		}
		#endregion

		#region BufferData (private helper)
		private void BufferData<T>(T[] data, int offset) where T : struct
		{
			IntPtr size = (IntPtr)((elementSize == IndexElementSize.SixteenBits ?
				2 : 4) * data.Length);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferHandle);

			if (offset != 0)
			{
				GL.BufferData(BufferTarget.ElementArrayBuffer, size, data, usageHint);
			}
			else
			{
				GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)offset,
					size, data);
			}
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose the native index buffer data.
		/// </summary>
		public void Dispose()
		{
			GL.DeleteBuffers(1, ref bufferHandle);
		}
		#endregion
	}
}
