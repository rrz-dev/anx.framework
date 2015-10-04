using ANX.Framework.Graphics;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dx = SharpDX.Direct3D11;

namespace ANX.RenderSystem.Windows.DX11
{
    public abstract class Buffer : IDisposable
    {
        public Dx.Buffer NativeBuffer { get; protected set; }
        private Dx.Device device;
        private BufferUsage usage;
        private bool isDynamic;

        protected Buffer(Dx.Device device, BufferUsage usage, bool isDynamic)
        {
            this.device = device;
            this.usage = usage;
            this.isDynamic = isDynamic;
        }

        protected DataStream MapBuffer(Dx.Buffer buffer, ResourceMapping mapping)
        {
            CheckUsage(mapping);

            DataStream dataStream;
            device.ImmediateContext.MapSubresource(buffer, mapping.ToMapMode(), Dx.MapFlags.None, out dataStream);
            return dataStream;
        }

        protected void UnmapBuffer(Dx.Buffer buffer)
        {
            device.ImmediateContext.UnmapSubresource(buffer, 0);
        }

        protected void CopySubresource(Dx.Buffer source, Dx.Buffer destination)
        {
            BufferHelper.ValidateCopyResource(source, destination);

            this.NativeBuffer.Device.ImmediateContext.CopyResource(source, destination);
        }

        protected bool WriteNeedsStaging
        {
            get { return !this.isDynamic; }
        }

        private void CheckUsage(ResourceMapping mapping)
        {
            if ((mapping & ResourceMapping.Write) != 0 && usage == BufferUsage.None)
                throw new NotSupportedException("Resource was created with WriteOnly, reading from it is not supported.");
        }

        protected Dx.Buffer CreateStagingBuffer(ResourceMapping mapping)
        {
            CheckUsage(mapping);

            var description = new Dx.BufferDescription()
            {
                Usage = Dx.ResourceUsage.Staging,
                SizeInBytes = NativeBuffer.Description.SizeInBytes,
                CpuAccessFlags = mapping.ToCpuAccessFlags(),
                OptionFlags = Dx.ResourceOptionFlags.None
            };

            return new Dx.Buffer(device, description);
        }

        public void Dispose()
        {
            if (NativeBuffer != null)
            {
                NativeBuffer.Dispose();
                NativeBuffer = null;
            }
        }
    }
}
