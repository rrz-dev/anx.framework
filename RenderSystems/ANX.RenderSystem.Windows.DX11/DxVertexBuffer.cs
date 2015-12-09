#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if DX10
using Dx = SharpDX.Direct3D10;
using DxDevice = SharpDX.Direct3D10.Device;

namespace ANX.RenderSystem.Windows.DX10
#endif
#if DX11
using Dx = SharpDX.Direct3D11;
using DxDevice = SharpDX.Direct3D11.Device;

namespace ANX.RenderSystem.Windows.DX11
#endif
{
    public partial class DxVertexBuffer : Buffer, INativeVertexBuffer
    {
#if DEBUG
        private static int vertexBufferCount = 0;
        private static int dynamicVertexBufferCount = 0;
#endif

        public DxVertexBuffer(GraphicsDeviceDX graphics, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage, bool dynamic)
            : base(graphics.NativeDevice.Device, usage, dynamic)
        {
            InitializeBuffer(graphics.NativeDevice.Device, vertexDeclaration, vertexCount, usage, dynamic);
        }

        internal DxVertexBuffer(Dx.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage, bool dynamic)
            : base(device, usage, dynamic)
        {
            InitializeBuffer(device, vertexDeclaration, vertexCount, usage, dynamic);
        }

        private void InitializeBuffer(Dx.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage, bool dynamic)
        {
            vertexStride = vertexDeclaration.VertexStride;

            var description = new Dx.BufferDescription()
            {
                Usage = dynamic ? Dx.ResourceUsage.Dynamic : Dx.ResourceUsage.Default,
                SizeInBytes = vertexDeclaration.VertexStride * vertexCount,
                BindFlags = Dx.BindFlags.VertexBuffer,
                CpuAccessFlags = dynamic ? Dx.CpuAccessFlags.Write : Dx.CpuAccessFlags.None,
                OptionFlags = Dx.ResourceOptionFlags.None
            };

            NativeBuffer = new Dx.Buffer(device, description);
#if DEBUG
            if (dynamic)
                NativeBuffer.DebugName = "DynamicVertexBuffer_" + dynamicVertexBufferCount++;
            else
                NativeBuffer.DebugName = "VertexBuffer_" + vertexBufferCount++;
#endif
        }
    }
}
