using System;
using System.IO;
using ANX.Framework.Graphics;
using SharpDX;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.BaseDirectX
{
	public abstract class BaseVertexBuffer<T> : IDisposable where T : class, IDisposable
	{
		protected int vertexStride;
		public T NativeBuffer { get; protected set; }

		#region SetData
		public void SetData<S>(GraphicsDevice graphicsDevice, S[] data) where S : struct
		{
			SetData<S>(graphicsDevice, data, 0, data.Length);
		}

		public void SetData<S>(GraphicsDevice graphicsDevice, S[] data, int startIndex, int elementCount) where S : struct
		{
			SetData<S>(graphicsDevice, 0, data, startIndex, elementCount);
		}

		public void SetData<S>(GraphicsDevice graphicsDevice, int offsetInBytes, S[] data, int startIndex, int elementCount)
			where S : struct
		{
			//TODO: check offsetInBytes parameter for bounds etc.

			using (var stream = MapBufferWrite())
			{
				if (offsetInBytes > 0)
					stream.Seek(offsetInBytes, SeekOrigin.Current);

				if (startIndex > 0 || elementCount < data.Length)
					for (int i = startIndex; i < startIndex + elementCount; i++)
						stream.Write<S>(data[i]);
				else
					for (int i = 0; i < data.Length; i++)
						stream.Write<S>(data[i]);

				UnmapBuffer();
			}
		}

		public void SetData<S>(GraphicsDevice graphicsDevice, int offsetInBytes, S[] data, int startIndex, int elementCount,
			int vertexStride) where S : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetData
		public void GetData<S>(S[] data) where S : struct
		{
			GetData(data, 0, data.Length);
		}

		public void GetData<S>(S[] data, int startIndex, int elementCount) where S : struct
		{
			GetData(0, data, startIndex, elementCount, vertexStride);
		}

		public void GetData<S>(int offsetInBytes, S[] data, int startIndex, int elementCount, int vertexStride)
			where S : struct
		{
			using (var stream = MapBufferRead())
			{
				if (offsetInBytes > 0)
					stream.Seek(offsetInBytes, SeekOrigin.Current);

				stream.ReadRange(data, startIndex, elementCount);
				UnmapBuffer();
			}
		}
		#endregion

		protected abstract DataStream MapBufferWrite();
		protected abstract DataStream MapBufferRead();
		protected abstract void UnmapBuffer();

		#region Dispose
		public void Dispose()
		{
			if (NativeBuffer != null)
			{
				NativeBuffer.Dispose();
				NativeBuffer = null;
			}
		}
		#endregion
	}
}
