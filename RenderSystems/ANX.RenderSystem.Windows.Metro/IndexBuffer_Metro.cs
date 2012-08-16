using System;
using System.IO;
using System.Runtime.InteropServices;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class IndexBuffer_Metro : INativeIndexBuffer, IDisposable
	{
		#region Private
		private Dx11.Buffer buffer;
		private IndexElementSize size;
		private int indexSizeInBytes;
		#endregion

		#region Public
		public Dx11.Buffer NativeBuffer
		{
			get
			{
				return this.buffer;
			}
		}
		#endregion

		#region Constructor
		public IndexBuffer_Metro(GraphicsDevice graphics, IndexElementSize size,
			int indexCount, BufferUsage usage)
		{
			this.size = size;
			indexSizeInBytes = size == IndexElementSize.SixteenBits ? 2 : 4;

			//TODO: translate and use usage

			GraphicsDeviceWindowsMetro gdMetro =
				graphics.NativeDevice as GraphicsDeviceWindowsMetro;
			var device = gdMetro.NativeDevice.NativeDevice;

			InitializeBuffer(device, indexCount, usage);
		}

		internal IndexBuffer_Metro(Dx11.Device device, IndexElementSize size,
			int indexCount, BufferUsage usage)
		{
			this.size = size;

			InitializeBuffer(device, indexCount, usage);
		}
		#endregion

		#region InitializeBuffer
		private void InitializeBuffer(Dx11.Device device,
			int indexCount, BufferUsage usage)
		{
			var description = new Dx11.BufferDescription()
			{
				Usage = Dx11.ResourceUsage.Dynamic,
				SizeInBytes = indexSizeInBytes * indexCount,
				BindFlags = Dx11.BindFlags.IndexBuffer,
				CpuAccessFlags = Dx11.CpuAccessFlags.Write,
				OptionFlags = Dx11.ResourceOptionFlags.None
			};

			this.buffer = new Dx11.Buffer(device, description);
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
		{
			SetData<T>(graphicsDevice, data, 0, data.Length);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
			T[] data, int startIndex, int elementCount) where T : struct
		{
			//TODO: check offsetInBytes parameter for bounds etc.

			GCHandle pinnedArray = GCHandle.Alloc(data, GCHandleType.Pinned);
			IntPtr dataPointer = pinnedArray.AddrOfPinnedObject();

			int dataLength = Marshal.SizeOf(typeof(T)) * data.Length;

			unsafe
			{
				using (var vData = new SharpDX.DataStream(dataPointer, dataLength, true, true))
				{
					if (offsetInBytes > 0)
					{
						vData.Seek(offsetInBytes / indexSizeInBytes, SeekOrigin.Begin);
					}

					SharpDX.DataStream stream;
					SharpDX.DataBox box = NativeDxDevice.Current.MapSubresource(buffer, out stream);
					if (startIndex > 0 || elementCount < data.Length)
					{
						for (int i = startIndex; i < startIndex + elementCount; i++)
						{
							vData.Write<T>(data[i]);
						}
					}
					else
					{
						vData.CopyTo(stream);
					}
					NativeDxDevice.Current.UnmapSubresource(buffer, 0);
				}
			}

			pinnedArray.Free();
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
		}
		#endregion
		
		#region GetData (TODO)
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex,
			int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (buffer != null)
			{
				buffer.Dispose();
				buffer = null;
			}
		}
		#endregion
	}
}
