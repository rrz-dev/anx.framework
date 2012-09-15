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
	public partial class DxVertexBuffer : INativeVertexBuffer, IDisposable
	{
        public Dx11.Buffer NativeBuffer { get; protected set; }

		#region Constructor
		public DxVertexBuffer(GraphicsDevice graphics, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			var gd11 = graphics.NativeDevice as GraphicsDeviceDX;
			Dx11.DeviceContext context = gd11 != null ? gd11.NativeDevice as Dx11.DeviceContext : null;

			InitializeBuffer(context.Device, vertexDeclaration, vertexCount, usage);
		}

		internal DxVertexBuffer(Dx11.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			InitializeBuffer(device, vertexDeclaration, vertexCount, usage);
		}
		#endregion

		#region InitializeBuffer (TODO)
		private void InitializeBuffer(Dx11.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			vertexStride = vertexDeclaration.VertexStride;

			//TODO: translate and use usage

			if (device != null)
			{
				var description = new Dx11.BufferDescription()
				{
					Usage = Dx11.ResourceUsage.Dynamic,
					SizeInBytes = vertexDeclaration.VertexStride * vertexCount,
					BindFlags = Dx11.BindFlags.VertexBuffer,
					CpuAccessFlags = Dx11.CpuAccessFlags.Write,
					OptionFlags = Dx11.ResourceOptionFlags.None
				};

				NativeBuffer = new Dx11.Buffer(device, description);
			}
		}
		#endregion

		protected SharpDX.DataStream MapBufferWrite()
		{
			Dx11.DeviceContext context = NativeBuffer.Device.ImmediateContext;
			DataStream stream;
			context.MapSubresource(NativeBuffer, Dx11.MapMode.WriteDiscard, Dx11.MapFlags.None, out stream);
			return stream;
		}

		protected SharpDX.DataStream MapBufferRead()
		{
			Dx11.DeviceContext context = NativeBuffer.Device.ImmediateContext;
			DataStream stream;
			context.MapSubresource(NativeBuffer, Dx11.MapMode.Read, Dx11.MapFlags.None, out stream);
			return stream;
		}

		protected void UnmapBuffer()
		{
			Dx11.DeviceContext context = NativeBuffer.Device.ImmediateContext;
			context.UnmapSubresource(NativeBuffer, 0);
		}
	}
}
