using ANX.Framework.Graphics;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dx = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
    public abstract class Buffer : IDisposable
    {
        public Dx.Buffer NativeBuffer
        {
            get { return nativeBuffer; }
            protected set 
            {
                if (value != null)
                    SizeInBytes = value.Description.SizeInBytes;
                else
                    SizeInBytes = 0;

                nativeBuffer = value;
            }
        }

        private Dx.Buffer nativeBuffer;

        private Dx.Device device;
        private BufferUsage usage;
        private bool isDynamic;

        public int SizeInBytes
        {
            get;
            private set;
        }

        protected Buffer(Dx.Device device, BufferUsage usage, bool isDynamic)
        {
            this.device = device;
            this.usage = usage;
            this.isDynamic = isDynamic;
        }

        protected DataStream MapBuffer(Dx.Buffer buffer, ResourceMapping mapping)
        {
            if ((mapping & ResourceMapping.Read) != 0 && usage == BufferUsage.WriteOnly)
                throw new NotSupportedException("Resource was created with WriteOnly, reading from it is not supported.");

            if (isDynamic && mapping == ResourceMapping.Write)
                return buffer.Map(Dx.MapMode.WriteDiscard);

            return buffer.Map(mapping.ToMapMode());
        }

        protected void UnmapBuffer(Dx.Buffer buffer)
        {
            buffer.Unmap();
        }

        protected void CopySubresource(Dx.Buffer source, Dx.Buffer destination)
        {
            BufferHelper.ValidateCopyResource(source, destination);

            this.device.CopyResource(source, destination);
        }

        protected bool WriteNeedsStaging
        {
            get { return !isDynamic; }
        }

        protected Dx.Buffer CreateStagingBuffer(ResourceMapping mapping)
        {
            var description = new Dx.BufferDescription()
            {
                Usage = Dx.ResourceUsage.Staging,
                SizeInBytes = this.SizeInBytes,
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
