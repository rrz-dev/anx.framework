#region Using Statements
using System;
using ANX.Framework.Graphics;
using SharpDX;
using System.IO;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if DX10
namespace ANX.RenderSystem.Windows.DX10
#endif
#if DX11
namespace ANX.RenderSystem.Windows.DX11
#endif
{
	public partial class DxIndexBuffer : IDisposable
	{
		protected IndexElementSize elementSize;

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
		#endregion

		#region GetData
		public void GetData<S>(S[] data) where S : struct
		{
			GetData(0, data, 0, data.Length);
		}

		public void GetData<S>(S[] data, int startIndex, int elementCount) where S : struct
		{
			GetData(0, data, 0, data.Length);
		}

		public void GetData<S>(int offsetInBytes, S[] data, int startIndex, int elementCount) where S : struct
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

		protected int GetSizeInBytes(int indexCount)
		{
			return (elementSize == IndexElementSize.SixteenBits ? 2 : 4) * indexCount;
		}

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
