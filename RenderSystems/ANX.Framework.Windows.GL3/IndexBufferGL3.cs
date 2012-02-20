using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using ANX.Framework.Windows.GL3.Helpers;
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
	public class IndexBufferGL3 : INativeIndexBuffer
	{
		#region Private
		private IndexBuffer managedBuffer;

		private int bufferHandle;
		/// <summary>
		/// Native index buffer handle.
		/// </summary>
		internal int BufferHandle
		{
			get
			{
				return bufferHandle;
			}
		}

		private int indexCount;

		internal IndexElementSize elementSize;

		private BufferUsage usage;

		private BufferUsageHint usageHint;

		internal bool IsDisposed;
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new Index Buffer object.
		/// </summary>
		internal IndexBufferGL3(IndexBuffer setManagedBuffer,
			IndexElementSize setElementSize, int setIndexCount, BufferUsage setUsage)
		{
			GraphicsResourceManager.UpdateResource(this, true);

			managedBuffer = setManagedBuffer;
			indexCount = setIndexCount;
			elementSize = setElementSize;
			usage = setUsage;

			bool isDynamicBuffer = managedBuffer is DynamicIndexBuffer;

			usageHint = isDynamicBuffer ?
				BufferUsageHint.DynamicDraw :
				BufferUsageHint.StaticDraw;

			CreateBuffer();
		}

		~IndexBufferGL3()
		{
			GraphicsResourceManager.UpdateResource(this, false);
		}
		#endregion

		#region CreateBuffer
		private void CreateBuffer()
		{
			GL.GenBuffers(1, out bufferHandle);
			ErrorHelper.Check("GenBuffers");
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferHandle);
			ErrorHelper.Check("BindBuffer");
			int size = indexCount *
				(elementSize == IndexElementSize.SixteenBits ? 16 : 32);
			GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)size, IntPtr.Zero,
				usageHint);
			ErrorHelper.Check("BufferData");

			int setSize;
			GL.GetBufferParameter(BufferTarget.ElementArrayBuffer,
				BufferParameterName.BufferSize, out setSize);
			if (setSize != size)
			{
				throw new Exception("Failed to set the vertexBuffer data. DataSize=" +
					size + " SetSize=" + setSize);
			}
		}
		#endregion

		#region RecreateData
		internal void RecreateData()
		{
			CreateBuffer();
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
			int size = (elementSize == IndexElementSize.SixteenBits ?
				2 : 4) * data.Length;

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferHandle);
			ErrorHelper.Check("BindBuffer");

			if (offset == 0)
			{
				GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)size, data,
					usageHint);
				ErrorHelper.Check("BufferData size=" + size);
			}
			else
			{
				GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)offset,
					(IntPtr)size, data);
				ErrorHelper.Check("BufferSubData offset=" + offset + " size=" + size);
			}

			int setSize;
			GL.GetBufferParameter(BufferTarget.ElementArrayBuffer,
				BufferParameterName.BufferSize, out setSize);
			if (setSize != size)
			{
				throw new Exception("Failed to set the indexBuffer data. DataSize=" +
				size + " SetSize=" + setSize);
			}
		}
		#endregion

		#region GetData
		public void GetData<T>(T[] data) where T : struct
		{
			BufferData(data, 0);
		}
		#endregion

		#region GetData
		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
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

		#region GetData
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex,
			int elementCount) where T : struct
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

		#region GetBufferData (private helper)
		private void GetBufferData<T>(T[] data, int offset) where T : struct
		{
			int size = (elementSize == IndexElementSize.SixteenBits ?
				2 : 4) * data.Length;

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferHandle);
			ErrorHelper.Check("BindBuffer");

			GL.GetBufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)offset,
				(IntPtr)size, data);
			ErrorHelper.Check("GetBufferSubData");
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose the native index buffer data.
		/// </summary>
		public void Dispose()
		{
			if (IsDisposed == false)
			{
				IsDisposed = true;
				DisposeResource();
			}
		}

		internal void DisposeResource()
		{
			if (bufferHandle != -1 &&
				GraphicsDeviceWindowsGL3.IsContextCurrent)
			{
				GL.DeleteBuffers(1, ref bufferHandle);
				ErrorHelper.Check("DeleteBuffers");
				bufferHandle = -1;
			}
		}
		#endregion
	}
}
