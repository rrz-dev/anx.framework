#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx10 = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
    public partial class DxIndexBuffer : Buffer, INativeIndexBuffer, IDisposable
    {
#if DEBUG
        private static int indexBufferCount = 0;
        private static int dynamicIndexBufferCount = 0;
#endif
        #region Constructor
        public DxIndexBuffer(GraphicsDeviceDX graphics, IndexElementSize size, int indexCount, BufferUsage usage, bool dynamic)
            : base(graphics.NativeDevice, usage, dynamic)
        {
            elementSize = size;

            InitializeBuffer(graphics.NativeDevice, size, indexCount, usage, dynamic);
        }

        internal DxIndexBuffer(Dx10.Device device, IndexElementSize size, int indexCount, BufferUsage usage, bool dynamic)
            : base(device, usage, dynamic)
        {
            elementSize = size;
            InitializeBuffer(device, size, indexCount, usage, dynamic);
        }
        #endregion

        #region InitializeBuffer
        private void InitializeBuffer(Dx10.Device device, IndexElementSize size, int indexCount, BufferUsage usage, bool dynamic)
        {
            //TODO: translate and use usage
            var description = new Dx10.BufferDescription()
            {
                Usage = dynamic ? Dx10.ResourceUsage.Dynamic : Dx10.ResourceUsage.Default,
                SizeInBytes = GetSizeInBytes(indexCount),
                BindFlags = Dx10.BindFlags.IndexBuffer,
                CpuAccessFlags = dynamic ? Dx10.CpuAccessFlags.Write : Dx10.CpuAccessFlags.None,
                OptionFlags = Dx10.ResourceOptionFlags.None
            };

            NativeBuffer = new Dx10.Buffer(device, description);
#if DEBUG
            if (dynamic)
                NativeBuffer.DebugName = "DynamicIndexBuffer_" + dynamicIndexBufferCount++;
            else
                NativeBuffer.DebugName = "IndexBuffer_" + indexBufferCount++;
#endif
        }
        #endregion
    }
}
