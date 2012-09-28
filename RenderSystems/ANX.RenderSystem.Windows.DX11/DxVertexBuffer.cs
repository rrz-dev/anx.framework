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
        private Dx.DeviceContext context;

		#region Constructor
		public DxVertexBuffer(GraphicsDevice graphics, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			var gd11 = graphics.NativeDevice as GraphicsDeviceDX;
			this.context = gd11 != null ? gd11.NativeDevice as Dx.DeviceContext : null;

			InitializeBuffer(context.Device, vertexDeclaration, vertexCount, usage);
		}

		internal DxVertexBuffer(Dx.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			InitializeBuffer(device, vertexDeclaration, vertexCount, usage);
		}
		#endregion

		#region InitializeBuffer (TODO)
		private void InitializeBuffer(Dx.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			vertexStride = vertexDeclaration.VertexStride;

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
			}
		}
		#endregion

		private SharpDX.DataStream MapBufferWrite()
		{
			Dx.DeviceContext context = NativeBuffer.Device.ImmediateContext;
			DataStream stream;
			context.MapSubresource(NativeBuffer, Dx.MapMode.WriteDiscard, Dx.MapFlags.None, out stream);
			return stream;
		}

		private SharpDX.DataStream MapBufferRead()
		{
			Dx.DeviceContext context = NativeBuffer.Device.ImmediateContext;
			DataStream stream;
			context.MapSubresource(NativeBuffer, Dx.MapMode.Read, Dx.MapFlags.None, out stream);
			return stream;
		}

        private SharpDX.DataStream MapBufferRead(Dx.Resource buffer)
        {
            DataStream stream;
            buffer.Device.ImmediateContext.MapSubresource(buffer, 0, Dx.MapMode.Read, Dx.MapFlags.None, out stream);
            return stream;
        }

		private void UnmapBuffer()
		{
			Dx.DeviceContext context = NativeBuffer.Device.ImmediateContext;
			context.UnmapSubresource(NativeBuffer, 0);
		}

        private void CopySubresource(Dx.Buffer source, Dx.Buffer destination)
        {
            this.context.CopyResource(source, destination);
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
                BindFlags = Dx.BindFlags.VertexBuffer,
                CpuAccessFlags = Dx.CpuAccessFlags.Write | Dx.CpuAccessFlags.Read,
                OptionFlags = Dx.ResourceOptionFlags.None
            };

            return new Dx.Buffer(context.Device, description);
        }
	}
}
