using System;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using Dx = SharpDX.Direct3D11;
using System.Runtime.InteropServices;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
    public class VertexBuffer_Metro : INativeVertexBuffer, IDisposable
    {
        #region Private
        private int vertexStride;
        #endregion

        #region Public
        public Dx.Buffer NativeBuffer
        {
            get;
            private set;
        }
        #endregion

        #region Constructor
        public VertexBuffer_Metro(GraphicsDevice graphics, VertexDeclaration vertexDeclaration,
            int vertexCount, BufferUsage usage)
        {
            GraphicsDeviceWindowsMetro gdMetro = graphics.NativeDevice as GraphicsDeviceWindowsMetro;
            var device = gdMetro.NativeDevice.NativeDevice;

            vertexStride = vertexDeclaration.VertexStride;
            InitializeBuffer(device, vertexCount, usage);
        }

        internal VertexBuffer_Metro(Dx.Device device, VertexDeclaration vertexDeclaration,
            int vertexCount, BufferUsage usage)
        {
            vertexStride = vertexDeclaration.VertexStride;
            InitializeBuffer(device, vertexCount, usage);
        }
        #endregion

        #region InitializeBuffer
        private void InitializeBuffer(Dx.Device device, int vertexCount,
            BufferUsage usage)
        {
            if (device != null)
            {
                var description = new Dx.BufferDescription()
                {
                    Usage = FormatConverter.Translate(usage),
                    SizeInBytes = vertexStride * vertexCount,
                    BindFlags = Dx.BindFlags.VertexBuffer,
                    CpuAccessFlags = Dx.CpuAccessFlags.Write,
                    OptionFlags = Dx.ResourceOptionFlags.None
                };

                NativeBuffer = new Dx.Buffer(device, description);
            }
        }
        #endregion

        #region SetData
        public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
            T[] data, int startIndex, int elementCount) where T : struct
        {
            SetData<T>(graphicsDevice, offsetInBytes, data, startIndex, elementCount, vertexStride);
        }

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
            int startIndex, int elementCount, int vertexStride) where T : struct
        {
            if (offsetInBytes + elementCount * Marshal.SizeOf(typeof(T)) > NativeBuffer.Description.SizeInBytes)
                throw new ArgumentOutOfRangeException(string.Format("The offset by \"{0}\" plus the byte length described by \"{1}\" is over the bounds of the buffer.", "offsetInBytes", "elementCount"));

            if (startIndex + elementCount > data.Length)
                throw new ArgumentOutOfRangeException(string.Format("The parameters {0} + {1} must be smaller than {2}.", "startIndex", "elementCount", "data.Length"));

            GraphicsDeviceWindowsMetro gdMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
            var device = gdMetro.NativeDevice;

            SharpDX.DataStream stream = device.MapSubresource(NativeBuffer);

            if (offsetInBytes > 0)
                stream.Seek(offsetInBytes, SeekOrigin.Current);

            for (int i = startIndex; i < startIndex + elementCount; i++)
                stream.Write<T>(data[i]);

            device.UnmapSubresource(NativeBuffer, 0);
        }
        #endregion

        #region GetData
        public void GetData<T>(T[] data) where T : struct
        {
            GetData(data, 0, data.Length);
        }

        public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            GetData(0, data, startIndex, elementCount, vertexStride);
        }

        public void GetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride)
            where T : struct
        {
            Dx.Buffer stagingBuffer = CreateStagingBuffer(elementCount * vertexStride);
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

        private SharpDX.DataStream MapBufferRead(Dx.Resource buffer)
        {
            SharpDX.DataStream stream;
            buffer.Device.ImmediateContext.MapSubresource(buffer, 0, Dx.MapMode.Read, Dx.MapFlags.None, out stream);
            return stream;
        }

        private void CopySubresource(Dx.Buffer source, Dx.Buffer destination)
        {
            this.NativeBuffer.Device.ImmediateContext.CopyResource(source, destination);
        }

        private void UnmapBuffer(Dx.Resource buffer)
        {
            buffer.Device.ImmediateContext.UnmapSubresource(buffer, 0);
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
