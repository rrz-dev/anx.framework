using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using Dx = SharpDX.Direct3D11;
using System.IO;
using SharpDX;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
    public class IndexBuffer_Metro : INativeIndexBuffer, IDisposable
    {
        #region Private
        private int indexSizeInBytes;
        #endregion

        #region Public
        public Dx.Buffer NativeBuffer
        {
            get;
            private set;
        }
        #endregion

        #region Constructor
        public IndexBuffer_Metro(GraphicsDevice graphics, IndexElementSize size,
            int indexCount, BufferUsage usage)
        {
            indexSizeInBytes = size == IndexElementSize.SixteenBits ? 2 : 4;
            
            GraphicsDeviceWindowsMetro gdMetro = graphics.NativeDevice as GraphicsDeviceWindowsMetro;
            var device = gdMetro.NativeDevice.NativeDevice;

            InitializeBuffer(device, indexCount, usage);
        }

        internal IndexBuffer_Metro(Dx.Device device, IndexElementSize size,
            int indexCount, BufferUsage usage)
        {
            indexSizeInBytes = size == IndexElementSize.SixteenBits ? 2 : 4;
            InitializeBuffer(device, indexCount, usage);
        }
        #endregion

        #region InitializeBuffer
        private void InitializeBuffer(Dx.Device device,
            int indexCount, BufferUsage usage)
        {
            var description = new Dx.BufferDescription()
            {
                Usage = FormatConverter.Translate(usage),
                SizeInBytes = indexSizeInBytes * indexCount,
                BindFlags = Dx.BindFlags.IndexBuffer,
                CpuAccessFlags = Dx.CpuAccessFlags.Write,
                OptionFlags = Dx.ResourceOptionFlags.None
            };

            NativeBuffer = new Dx.Buffer(device, description);
        }
        #endregion

        #region SetData
        public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
        {
            SetData<T>(graphicsDevice, data, 0, data.Length);
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, T[] data, int startIndex,
            int elementCount) where T : struct
        {
            SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data,
            int startIndex, int elementCount) where T : struct
        {
            GraphicsDeviceWindowsMetro gdMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
            var device = gdMetro.NativeDevice;

            //TODO: check offsetInBytes parameter for bounds etc.

            SharpDX.DataStream stream = device.MapSubresource(NativeBuffer);

            if (offsetInBytes > 0)
                stream.Seek(offsetInBytes, SeekOrigin.Current);

            if (startIndex > 0 || elementCount < data.Length)
                for (int i = startIndex; i < startIndex + elementCount; i++)
                    stream.Write<T>(data[i]);
            else
                for (int i = 0; i < data.Length; i++)
                    stream.Write<T>(data[i]);

            device.UnmapSubresource(NativeBuffer, 0);
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
            Dx.Buffer stagingBuffer = CreateStagingBuffer(indexSizeInBytes * elementCount);
            CopySubresource(NativeBuffer, stagingBuffer);

            using (var stream = MapBufferRead(stagingBuffer))
            {
                if (offsetInBytes > 0)
                    stream.Seek(offsetInBytes, SeekOrigin.Current);

                stream.ReadRange(data, startIndex, elementCount);
                UnmapBuffer(stagingBuffer);
            }
        }
        #endregion

        private SharpDX.DataStream MapBufferRead(Dx.Buffer buffer)
        {
            Dx.DeviceContext context = buffer.Device.ImmediateContext;
            DataStream stream;
            context.MapSubresource(buffer, Dx.MapMode.Read, Dx.MapFlags.None, out stream);
            return stream;
        }

        private void UnmapBuffer(Dx.Buffer buffer)
        {
            Dx.DeviceContext context = buffer.Device.ImmediateContext;
            context.UnmapSubresource(buffer, 0);
        }

        private void CopySubresource(Dx.Buffer source, Dx.Buffer destination)
        {
            this.NativeBuffer.Device.ImmediateContext.CopyResource(source, destination);
        }

        private Dx.Buffer CreateStagingBuffer(int sizeInBytes)
        {
            var description = new Dx.BufferDescription()
            {
                Usage = Dx.ResourceUsage.Staging,
                SizeInBytes = sizeInBytes,
                CpuAccessFlags = Dx.CpuAccessFlags.Read,
                OptionFlags = Dx.ResourceOptionFlags.None,
            };

            return new Dx.Buffer(NativeBuffer.Device, description);
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
