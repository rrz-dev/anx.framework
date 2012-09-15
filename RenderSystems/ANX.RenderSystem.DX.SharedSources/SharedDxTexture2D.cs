#region Using Statements
using System;
using System.Runtime.InteropServices;
using ANX.Framework.Graphics;
using SharpDX;

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
	public partial class DxTexture2D : IDisposable 
	{
		#region Private
		protected int mipCount;
		protected int tempSubresource;
		protected int pitch;
		private int formatSize;
		protected SurfaceFormat surfaceFormat;
		protected bool useRenderTexture;
		#endregion

		#region Public
		public GraphicsDevice GraphicsDevice
		{
			get;
			protected set;
		}
		#endregion

		#region Constructor
		protected DxTexture2D(GraphicsDevice graphicsDevice, SurfaceFormat setSurfaceFormat, int setMipCount)
		{
			mipCount = setMipCount;
			useRenderTexture = mipCount > 1;
			GraphicsDevice = graphicsDevice;
			surfaceFormat = setSurfaceFormat;

			// description of texture formats of DX10: http://msdn.microsoft.com/en-us/library/bb694531(v=VS.85).aspx
			// more helpfull information on DX10 textures: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205131(v=vs.85).aspx
			formatSize = DxFormatConverter.FormatSize(surfaceFormat);
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, 0, data.Length);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data, int startIndex, int elementCount) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data, int startIndex, int elementCount)
			 where T : struct
		{
			//TODO: handle offsetInBytes parameter
			//TODO: handle startIndex parameter
			//TODO: handle elementCount parameter

			unsafe
			{
				GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
				byte* colorData = (byte*)handle.AddrOfPinnedObject();

				switch (surfaceFormat)
				{
					case SurfaceFormat.Color:
						SetDataColor(0, offsetInBytes, colorData, startIndex, elementCount);
						return;

					case SurfaceFormat.Dxt1:
					case SurfaceFormat.Dxt3:
					case SurfaceFormat.Dxt5:
						SetDataDxt(0, offsetInBytes, colorData, startIndex, elementCount, data.Length);
						return;
				}

				handle.Free();
			}

			throw new Exception(String.Format("creating textures of format {0} not yet implemented...", surfaceFormat));
		}
		#endregion

		#region SetDataColor
		private unsafe void SetDataColor(int level, int offsetInBytes, byte* colorData, int startIndex, int elementCount)
		{
			int mipmapWidth = Math.Max(Width >> level, 1);
			int mipmapHeight = Math.Max(Height >> level, 1);

			IntPtr dataPtr = MapWrite(level);
			int srcIndex = 0;

			byte* pTexels = (byte*)dataPtr;
			for (int row = 0; row < mipmapHeight; row++)
			{
				int rowStart = row * pitch;

				for (int col = 0; col < mipmapWidth; col++)
				{
					int colStart = rowStart + (col * formatSize);
					pTexels[colStart++] = colorData[srcIndex++];
					pTexels[colStart++] = colorData[srcIndex++];
					pTexels[colStart++] = colorData[srcIndex++];
					pTexels[colStart++] = colorData[srcIndex++];
				}
			}

			Unmap();
		}
		#endregion

		#region SetDataDxt
		private unsafe void SetDataDxt(int level, int offsetInBytes, byte* colorData, int startIndex, int elementCount,
			int dataLength)
		{
			int mipmapWidth = Math.Max(Width >> level, 1);
			int mipmapHeight = Math.Max(Height >> level, 1);

			int w = (mipmapWidth + 3) >> 2;
			int h = (mipmapHeight + 3) >> 2;
			formatSize = (surfaceFormat == SurfaceFormat.Dxt1) ? 8 : 16;

			IntPtr dataPtr = MapWrite(level);
			var ds = new DataStream(dataPtr, mipmapWidth * mipmapHeight * 4 * 2, true, true);
			int col = 0;
			int index = 0; // startIndex
			int count = dataLength; // elementCount
			int actWidth = w * formatSize;

			for (int i = 0; i < h; i++)
			{
				ds.Position = (i * pitch) + (col * formatSize);
				if (count <= 0)
					break;
				else if (count < actWidth)
				{
					for (int idx = index; idx < index + count; idx++)
						ds.WriteByte(colorData[idx]);
					break;
				}

				for (int idx = index; idx < index + actWidth; idx++)
					ds.WriteByte(colorData[idx]);

				index += actWidth;
				count -= actWidth;
			}

			Unmap();
		}
		#endregion

		#region SetData (TODO)
		public void SetData<T>(int level, Framework.Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			//TODO: handle rect parameter
			if (rect != null)
				throw new Exception("Texture2D SetData with rectangle is not yet implemented!");

			unsafe
			{
				GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
				byte* colorData = (byte*)handle.AddrOfPinnedObject();

				switch (surfaceFormat)
				{
					case SurfaceFormat.Color:
						SetDataColor(level, 0, colorData, startIndex, elementCount);
						return;

					case SurfaceFormat.Dxt1:
					case SurfaceFormat.Dxt3:
					case SurfaceFormat.Dxt5:
						SetDataDxt(level, 0, colorData, startIndex, elementCount, data.Length);
						return;
				}

				handle.Free();
			}

			throw new Exception(String.Format("creating textures of format {0} not yet implemented...", surfaceFormat));
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
            if (NativeShaderResourceView != null)
            {
                NativeShaderResourceView.Dispose();
                NativeShaderResourceView = null;
            }

            if (NativeTextureStaging != null)
            {
                NativeTextureStaging.Dispose();
                NativeTextureStaging = null;
            }
        }
        #endregion
	}
}
