#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using SharpDX.Direct3D11;
using ANX.Framework.Graphics;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA.RenderSystem;
using System.IO;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
	public class IndexBuffer_DX11 : INativeIndexBuffer, IDisposable
    {
        private SharpDX.Direct3D11.Buffer buffer;
        private IndexElementSize size;

        public IndexBuffer_DX11(GraphicsDevice graphics, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            this.size = size;

            //TODO: translate and use usage

            GraphicsDeviceWindowsDX11 gd11 = graphics.NativeDevice as GraphicsDeviceWindowsDX11;
            SharpDX.Direct3D11.DeviceContext context = gd11 != null ? gd11.NativeDevice as SharpDX.Direct3D11.DeviceContext : null;

            InitializeBuffer(context.Device, size, indexCount, usage);
        }

        internal IndexBuffer_DX11(SharpDX.Direct3D11.Device device, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            this.size = size;

            InitializeBuffer(device, size, indexCount, usage);
        }

        private void InitializeBuffer(SharpDX.Direct3D11.Device device, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            BufferDescription description = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = (size == IndexElementSize.SixteenBits ? 2 : 4) * indexCount,
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None
            };

            this.buffer = new SharpDX.Direct3D11.Buffer(device, description);
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
        {
            SetData<T>(graphicsDevice, data, 0, data.Length);
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data, int startIndex, int elementCount)
			where T : struct
        {
            GraphicsDeviceWindowsDX11 dx11GraphicsDevice = graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX11;
            DeviceContext context = dx11GraphicsDevice.NativeDevice;

            //TODO: check offsetInBytes parameter for bounds etc.

			SharpDX.DataStream stream;
			context.MapSubresource(this.buffer, MapMode.WriteDiscard, MapFlags.None, out stream);

			if (offsetInBytes > 0)
				stream.Seek(offsetInBytes, SeekOrigin.Current);

			if (startIndex > 0 || elementCount < data.Length)
				for (int i = startIndex; i < startIndex + elementCount; i++)
					stream.Write<T>(data[i]);
			else
				for (int i = 0; i < data.Length; i++)
					stream.Write<T>(data[i]);

			context.UnmapSubresource(this.buffer, 0);
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, T[] data, int startIndex, int elementCount) where T : struct
        {
            SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
        }

        public SharpDX.Direct3D11.Buffer NativeBuffer
        {
            get
            {
                return this.buffer;
            }
        }

        public void Dispose()
        {
            if (this.buffer != null)
            {
                buffer.Dispose();
                buffer = null;
            }
        }

				#region INativeIndexBuffer Member

				public void GetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
				{
					throw new NotImplementedException();
				}

				#endregion

				#region INativeBuffer Member


				public void GetData<T>(T[] data) where T : struct
				{
					throw new NotImplementedException();
				}

				public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
				{
					throw new NotImplementedException();
				}

				#endregion
		}
}
