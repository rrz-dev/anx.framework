using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using OpenTK.Graphics.OpenGL;
using ANX.RenderSystem.Windows.GL3.Helpers;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.GL3
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
