#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using SharpDX.Direct3D10;
using ANX.Framework.Graphics;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA.RenderSystem;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class IndexBuffer_DX10 : INativeIndexBuffer, IDisposable
    {
        private SharpDX.Direct3D10.Buffer buffer;
        private IndexElementSize size;

        public IndexBuffer_DX10(GraphicsDevice graphics, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            this.size = size;

            //TODO: translate and use usage

            GraphicsDeviceWindowsDX10 gd10 = graphics.NativeDevice as GraphicsDeviceWindowsDX10;
            SharpDX.Direct3D10.Device device = gd10 != null ? gd10.NativeDevice as SharpDX.Direct3D10.Device : null;

            InitializeBuffer(device, size, indexCount, usage);
        }

        internal IndexBuffer_DX10(SharpDX.Direct3D10.Device device, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            this.size = size;

            InitializeBuffer(device, size, indexCount, usage);
        }

        private void InitializeBuffer(SharpDX.Direct3D10.Device device, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            BufferDescription description = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = (size == IndexElementSize.SixteenBits ? 2 : 4) * indexCount,
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None
            };

            this.buffer = new SharpDX.Direct3D10.Buffer(device, description);
            this.buffer.Unmap();
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
        {
            SetData<T>(graphicsDevice, data, 0, data.Length);
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
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
                        vData.Seek(offsetInBytes / (size == IndexElementSize.SixteenBits ? 2 : 4), System.IO.SeekOrigin.Begin);
                    }

                    using (var d = buffer.Map(MapMode.WriteDiscard))
                    {
                        if (startIndex > 0 || elementCount < data.Length)
                        {
                            for (int i = startIndex; i < startIndex + elementCount; i++)
                            {
                                d.Write<T>(data[i]);
                            }
                        }
                        else
                        {
                            vData.CopyTo(d);
                        } 
                        buffer.Unmap();
                    }
                }
            }

            pinnedArray.Free(); 
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, T[] data, int startIndex, int elementCount) where T : struct
        {
            SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
        }

        public SharpDX.Direct3D10.Buffer NativeBuffer
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
