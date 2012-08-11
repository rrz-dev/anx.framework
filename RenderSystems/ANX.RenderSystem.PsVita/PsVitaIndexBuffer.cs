using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using VitaIndexBuffer = Sce.PlayStation.Core.Graphics.VertexBuffer;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaIndexBuffer : INativeIndexBuffer
	{
		#region Private
		private IndexBuffer managedBuffer;
		private VitaIndexBuffer nativeBuffer;

		private int indexCount;

		internal IndexElementSize elementSize;
		
		internal bool IsDisposed;
		#endregion

		#region Constructor
		internal PsVitaIndexBuffer(IndexBuffer setManagedBuffer,
			IndexElementSize setElementSize, int setIndexCount, BufferUsage setUsage)
		{
			managedBuffer = setManagedBuffer;
			indexCount = setIndexCount;
			elementSize = setElementSize;

			CreateBuffer();
		}
		#endregion

		#region CreateBuffer
		private void CreateBuffer()
		{
			nativeBuffer = new VitaIndexBuffer(0, indexCount);
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
			if(data is uint == false &&
				data is ushort == false)
			{
				throw new InvalidOperationException(
					"The index buffer data can only be uint or ushort!");
			}

			if(data is ushort)
			{
				nativeBuffer.SetIndices(data as ushort[], offset, 0, data.Length);
			}
			else if (data is uint)
			{
				uint[] convertData = data as uint[];
				ushort[] newIndices = new ushort[convertData.Length];
				for (int index = 0; index < convertData.Length; index++)
				{
					newIndices[index] = (ushort)convertData[index];
				}

				nativeBuffer.SetIndices(newIndices, offset, 0, data.Length);
			}
		}
		#endregion

		#region GetData
		public void GetData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetData
		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetData
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex,
			int elementCount) where T : struct
		{
			throw new NotImplementedException();
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
			if (nativeBuffer != null)
			{
				nativeBuffer.Dispose();
				nativeBuffer = null;
			}
		}
		#endregion
	}
}
