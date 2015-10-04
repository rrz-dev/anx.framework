#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx11 = SharpDX.Direct3D11;

namespace ANX.RenderSystem.Windows.DX11
{
    public partial class DxIndexBuffer : Buffer, INativeIndexBuffer
    {
#if DEBUG
        private static int indexBufferCount = 0;
        private static int dynamicIndexBufferCount = 0;
#endif

        public DxIndexBuffer(GraphicsDeviceDX graphics, IndexElementSize size, int indexCount, BufferUsage usage, bool dynamic)
            : base(graphics.NativeDevice.Device, usage, dynamic)
        {
            InitializeBuffer(graphics.NativeDevice.Device, size, indexCount, usage, dynamic);
        }

        internal DxIndexBuffer(SharpDX.Direct3D11.Device device, IndexElementSize size, int indexCount, BufferUsage usage, bool dynamic)
            : base(device, usage, dynamic)
        {
            elementSize = size;
            InitializeBuffer(device, size, indexCount, usage, dynamic);
        }

        private void InitializeBuffer(Dx11.Device device, IndexElementSize size, int indexCount, BufferUsage usage, bool dynamic)
        {
            var description = new Dx11.BufferDescription()
            {
                Usage = dynamic ? Dx11.ResourceUsage.Dynamic : Dx11.ResourceUsage.Default,
                SizeInBytes = GetSizeInBytes(indexCount),
                BindFlags = Dx11.BindFlags.IndexBuffer,
                CpuAccessFlags = dynamic ? Dx11.CpuAccessFlags.Write : Dx11.CpuAccessFlags.None,
                OptionFlags = Dx11.ResourceOptionFlags.None
            };

            NativeBuffer = new SharpDX.Direct3D11.Buffer(device, description);
#if DEBUG
            if (dynamic)
                NativeBuffer.DebugName = "DynamicIndexBuffer_" + dynamicIndexBufferCount++;
            else
                NativeBuffer.DebugName = "IndexBuffer_" + indexBufferCount++;
#endif
        }
    }
}
