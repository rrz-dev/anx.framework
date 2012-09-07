using System;
using System.Runtime.InteropServices;
using ANX.Framework.Graphics;
using SharpDX;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.BaseDirectX
{
	public abstract class BaseTexture2D<S> : IDisposable where S : class, IDisposable
	{
		#region Private
		protected int tempSubresource;
		protected int pitch;
		protected int formatSize;
		protected SurfaceFormat surfaceFormat;

		protected bool IsDxtTexture
		{
			get
			{
				return surfaceFormat == SurfaceFormat.Dxt5 || surfaceFormat == SurfaceFormat.Dxt3 ||
					surfaceFormat == SurfaceFormat.Dxt1;
			}
		}
		#endregion

		#region Public
		public abstract int Width { get; }
		public abstract int Height { get; }

		public S NativeTexture
		{
			get;
			protected set;
		}

		public GraphicsDevice GraphicsDevice
		{
			get;
			protected set;
		}
		#endregion

		#region Constructor
		protected BaseTexture2D(GraphicsDevice graphicsDevice)
		{
			GraphicsDevice = graphicsDevice;
		}

		protected BaseTexture2D(GraphicsDevice graphicsDevice, SurfaceFormat setSurfaceFormat)
		{
			GraphicsDevice = graphicsDevice;
			surfaceFormat = setSurfaceFormat;

			// description of texture formats of DX10: http://msdn.microsoft.com/en-us/library/bb694531(v=VS.85).aspx
			// more helpfull information on DX10 textures: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205131(v=vs.85).aspx
			formatSize = BaseFormatConverter.FormatSize(surfaceFormat);
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
			
			if (surfaceFormat == SurfaceFormat.Color)
			{
				IntPtr dataPtr = MapWrite();

				unsafe
				{
					GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
					byte* colorData = (byte*)handle.AddrOfPinnedObject();

					byte* pTexels = (byte*)dataPtr;
					int srcIndex = 0;

					for (int row = 0; row < Height; row++)
					{
						int rowStart = row * pitch;

						for (int col = 0; col < Width; col++)
						{
							int colStart = col * formatSize;
							pTexels[rowStart + colStart + 0] = colorData[srcIndex++];
							pTexels[rowStart + colStart + 1] = colorData[srcIndex++];
							pTexels[rowStart + colStart + 2] = colorData[srcIndex++];
							pTexels[rowStart + colStart + 3] = colorData[srcIndex++];
						}
					}

					handle.Free();
				}

				Unmap();
			}
			else if (IsDxtTexture)
			{
				unsafe
				{
					GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
					byte* colorData = (byte*)handle.AddrOfPinnedObject();

					int w = (Width + 3) >> 2;
					int h = (Height + 3) >> 2;
					formatSize = (surfaceFormat == SurfaceFormat.Dxt1) ? 8 : 16;

					IntPtr dataPtr = MapWrite();
					var ds = new DataStream(dataPtr, Width * Height * 4 * 2, true, true);
					int col = 0;
					int index = 0; // startIndex
					int count = data.Length; // elementCount
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

					handle.Free();

					Unmap();
				}
			}
			else
				throw new Exception(String.Format("creating textures of format {0} not yet implemented...", surfaceFormat));
		}
		#endregion

		#region SetData (TODO)
		public void SetData<T>(int level, Framework.Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		protected abstract IntPtr MapWrite();
		protected abstract IntPtr MapRead();
		protected abstract void Unmap();

		#region Dispose
		public virtual void Dispose()
		{
			if (NativeTexture != null)
			{
				NativeTexture.Dispose();
				NativeTexture = null;
			}
		}
		#endregion
	}
}
