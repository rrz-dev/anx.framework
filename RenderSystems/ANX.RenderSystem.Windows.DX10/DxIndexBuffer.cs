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
	public partial class DxIndexBuffer : INativeIndexBuffer, IDisposable
	{
        public Dx10.Buffer NativeBuffer { get; protected set; }

		#region Constructor
		public DxIndexBuffer(GraphicsDevice graphics, IndexElementSize size, int indexCount, BufferUsage usage)
		{
			elementSize = size;
			GraphicsDeviceDX gd10 = graphics.NativeDevice as GraphicsDeviceDX;
			Dx10.Device device = gd10 != null ? gd10.NativeDevice as Dx10.Device : null;

			InitializeBuffer(device, size, indexCount, usage);
		}

		internal DxIndexBuffer(Dx10.Device device, IndexElementSize size, int indexCount, BufferUsage usage)
		{
			elementSize = size;
			InitializeBuffer(device, size, indexCount, usage);
		}
		#endregion

		#region InitializeBuffer
		private void InitializeBuffer(Dx10.Device device, IndexElementSize size, int indexCount, BufferUsage usage)
		{
			//TODO: translate and use usage
			var description = new Dx10.BufferDescription()
			{
				Usage = Dx10.ResourceUsage.Dynamic,
				SizeInBytes = GetSizeInBytes(indexCount),
				BindFlags = Dx10.BindFlags.IndexBuffer,
				CpuAccessFlags = Dx10.CpuAccessFlags.Write,
				OptionFlags = Dx10.ResourceOptionFlags.None
			};

			NativeBuffer = new Dx10.Buffer(device, description);
			NativeBuffer.Unmap();
		}
		#endregion

		protected DataStream MapBufferWrite()
		{
			return NativeBuffer.Map(Dx10.MapMode.WriteDiscard);
		}

		protected DataStream MapBufferRead()
		{
			return NativeBuffer.Map(Dx10.MapMode.Read);
		}

		protected void UnmapBuffer()
		{
			NativeBuffer.Unmap();
		}
	}
}
