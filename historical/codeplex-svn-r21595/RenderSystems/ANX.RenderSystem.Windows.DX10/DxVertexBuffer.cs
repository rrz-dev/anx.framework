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
	public partial class DxVertexBuffer : INativeVertexBuffer, IDisposable
	{
        public Dx.Buffer NativeBuffer { get; protected set; }
        private Dx.Device device;

		#region Constructor
		public DxVertexBuffer(GraphicsDevice graphics, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			GraphicsDeviceDX gd10 = graphics.NativeDevice as GraphicsDeviceDX;
			this.device = gd10 != null ? gd10.NativeDevice as Dx.Device : null;

			InitializeBuffer(device, vertexDeclaration, vertexCount, usage);
		}

		internal DxVertexBuffer(Dx.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			InitializeBuffer(device, vertexDeclaration, vertexCount, usage);
		}
		#endregion

		#region InitializeBuffer
		private void InitializeBuffer(Dx.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			this.vertexStride = vertexDeclaration.VertexStride;

			//TODO: translate and use usage

			if (device != null)
			{
				var description = new Dx.BufferDescription()
				{
					Usage = Dx.ResourceUsage.Dynamic,
					SizeInBytes = vertexDeclaration.VertexStride * vertexCount,
					BindFlags = Dx.BindFlags.VertexBuffer,
					CpuAccessFlags = Dx.CpuAccessFlags.Write,
					OptionFlags = Dx.ResourceOptionFlags.None
				};

				NativeBuffer = new Dx.Buffer(device, description);
				//NativeBuffer.Unmap();
            }
		}
		#endregion

		private DataStream MapBufferWrite()
		{
			return NativeBuffer.Map(Dx.MapMode.WriteDiscard);
		}

		private DataStream MapBufferRead()
		{
			return NativeBuffer.Map(Dx.MapMode.Read);
		}

        private SharpDX.DataStream MapBufferRead(Dx.Buffer buffer)
        {
            return buffer.Map(Dx.MapMode.ReadWrite);
        }

		private void UnmapBuffer()
		{
			NativeBuffer.Unmap();
		}

        private void UnmapBuffer(Dx.Buffer buffer)
        {
            buffer.Unmap();
        }

        private void CopySubresource(Dx.Buffer source, Dx.Buffer destination)
        {
            this.device.CopyResource(source, destination);
        }

        private Dx.Buffer CreateStagingBuffer(int sizeInBytes)
        {
            var description = new Dx.BufferDescription()
            {
                Usage = Dx.ResourceUsage.Staging,
                SizeInBytes = sizeInBytes,
                CpuAccessFlags = Dx.CpuAccessFlags.Read | Dx.CpuAccessFlags.Write,
                OptionFlags = Dx.ResourceOptionFlags.None
            };

            return new Dx.Buffer(device, description);
        }
	}
}
