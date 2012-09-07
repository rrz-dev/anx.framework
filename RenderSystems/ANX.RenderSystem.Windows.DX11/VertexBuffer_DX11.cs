using System;
using ANX.BaseDirectX;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
	public class VertexBuffer_DX11 : BaseVertexBuffer<Dx11.Buffer>, INativeVertexBuffer, IDisposable
	{
		#region Constructor
		public VertexBuffer_DX11(GraphicsDevice graphics, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			var gd11 = graphics.NativeDevice as GraphicsDeviceWindowsDX11;
			Dx11.DeviceContext context = gd11 != null ? gd11.NativeDevice as Dx11.DeviceContext : null;

			InitializeBuffer(context.Device, vertexDeclaration, vertexCount, usage);
		}

		internal VertexBuffer_DX11(Dx11.Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
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

		protected override SharpDX.DataStream MapBufferWrite()
		{
			Dx11.DeviceContext context = NativeBuffer.Device.ImmediateContext;
			DataStream stream;
			context.MapSubresource(NativeBuffer, Dx11.MapMode.WriteDiscard, Dx11.MapFlags.None, out stream);
			return stream;
		}

		protected override SharpDX.DataStream MapBufferRead()
		{
			Dx11.DeviceContext context = NativeBuffer.Device.ImmediateContext;
			DataStream stream;
			context.MapSubresource(NativeBuffer, Dx11.MapMode.Read, Dx11.MapFlags.None, out stream);
			return stream;
		}

		protected override void UnmapBuffer()
		{
			Dx11.DeviceContext context = NativeBuffer.Device.ImmediateContext;
			context.UnmapSubresource(NativeBuffer, 0);
		}
	}
}
