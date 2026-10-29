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
    public partial class DxIndexBuffer : INativeIndexBuffer, IDisposable
	{
        public Dx11.Buffer NativeBuffer { get; protected set; }

		#region Constructor
		public DxIndexBuffer(GraphicsDevice graphics, IndexElementSize size, int indexCount, BufferUsage usage)
		{
			elementSize = size;
			GraphicsDeviceDX gd11 = graphics.NativeDevice as GraphicsDeviceDX;
			Dx11.DeviceContext context = gd11 != null ? gd11.NativeDevice as Dx11.DeviceContext : null;

			InitializeBuffer(context.Device, size, indexCount, usage);
		}

		internal DxIndexBuffer(SharpDX.Direct3D11.Device device, IndexElementSize size, int indexCount, BufferUsage usage)
		{
			elementSize = size;
			InitializeBuffer(device, size, indexCount, usage);
		}
		#endregion

		#region InitializeBuffer
		private void InitializeBuffer(Dx11.Device device, IndexElementSize size, int indexCount, BufferUsage usage)
		{
			//TODO: translate and use usage
			var description = new Dx11.BufferDescription()
			{
				// TODO: translate usage
				Usage = Dx11.ResourceUsage.Dynamic,
				SizeInBytes = GetSizeInBytes(indexCount),
				BindFlags = Dx11.BindFlags.IndexBuffer,
				CpuAccessFlags = Dx11.CpuAccessFlags.Write,
				OptionFlags = Dx11.ResourceOptionFlags.None
			};

			NativeBuffer = new SharpDX.Direct3D11.Buffer(device, description);
		}
		#endregion

		protected DataStream MapBufferWrite()
		{
			Dx11.DeviceContext context = NativeBuffer.Device.ImmediateContext;
			DataStream stream;
			context.MapSubresource(NativeBuffer, Dx11.MapMode.WriteDiscard, Dx11.MapFlags.None, out stream);
			return stream;
		}

		protected DataStream MapBufferRead()
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
