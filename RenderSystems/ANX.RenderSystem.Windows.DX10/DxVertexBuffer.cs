#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
    public partial class DxVertexBuffer : Buffer, INativeVertexBuffer, IDisposable
    {
#if DEBUG
        private static int vertexBufferCount = 0;
        private static int dynamicVertexBufferCount = 0;
#endif

        #region Constructor
        public DxVertexBuffer(GraphicsDeviceDX graphics, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage, bool dynamic)
            : base(graphics.NativeDevice, usage, dynamic)
        {
            InitializeBuffer(graphics.NativeDevice, vertexDeclaration, vertexCount, usage, dynamic);
        }

        internal DxVertexBuffer(Dx.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage, bool dynamic)
            : base(device, usage, dynamic)
        {
            InitializeBuffer(device, vertexDeclaration, vertexCount, usage, dynamic);
        }
        #endregion

        #region InitializeBuffer
        private void InitializeBuffer(Dx.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage, bool dynamic)
        {
            this.vertexStride = vertexDeclaration.VertexStride;

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
        #endregion
    }
}
