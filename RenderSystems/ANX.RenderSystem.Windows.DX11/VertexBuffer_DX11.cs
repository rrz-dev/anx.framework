using System;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
	public class VertexBuffer_DX11 : INativeVertexBuffer, IDisposable
	{
		int vertexStride;

		public SharpDX.Direct3D11.Buffer NativeBuffer
		{
			get;
			private set;
		}

		#region Constructor
		public VertexBuffer_DX11(GraphicsDevice graphics, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			GraphicsDeviceWindowsDX11 gd11 = graphics.NativeDevice as GraphicsDeviceWindowsDX11;
			SharpDX.Direct3D11.DeviceContext context = gd11 != null ?
				gd11.NativeDevice as SharpDX.Direct3D11.DeviceContext :
				null;

			InitializeBuffer(context.Device, vertexDeclaration, vertexCount, usage);
		}

		internal VertexBuffer_DX11(SharpDX.Direct3D11.Device device, VertexDeclaration vertexDeclaration, int vertexCount,
			BufferUsage usage)
		{
			InitializeBuffer(device, vertexDeclaration, vertexCount, usage);
		}
		#endregion

		#region InitializeBuffer (TODO)
		private void InitializeBuffer(SharpDX.Direct3D11.Device device, VertexDeclaration vertexDeclaration, int vertexCount,
			BufferUsage usage)
		{
			this.vertexStride = vertexDeclaration.VertexStride;

			//TODO: translate and use usage

			if (device != null)
			{
				BufferDescription description = new BufferDescription()
				{
					Usage = ResourceUsage.Dynamic,
					SizeInBytes = vertexDeclaration.VertexStride * vertexCount,
					BindFlags = BindFlags.VertexBuffer,
					CpuAccessFlags = CpuAccessFlags.Write,
					OptionFlags = ResourceOptionFlags.None
				};

				NativeBuffer = new SharpDX.Direct3D11.Buffer(device, description);
			}
		}
		#endregion

		#region SetData (TODO)
		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data, int startIndex, int elementCount)
			where T : struct
		{
			GraphicsDeviceWindowsDX11 dx11GraphicsDevice = graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX11;
			DeviceContext context = dx11GraphicsDevice.NativeDevice;

			//TODO: check offsetInBytes parameter for bounds etc.

			SharpDX.DataStream stream;
			context.MapSubresource(NativeBuffer, MapMode.WriteDiscard, MapFlags.None, out stream);

			if (offsetInBytes > 0)
				stream.Seek(offsetInBytes, SeekOrigin.Current);

			if (startIndex > 0 || elementCount < data.Length)
				for (int i = startIndex; i < startIndex + elementCount; i++)
					stream.Write<T>(data[i]);
			else
				for (int i = 0; i < data.Length; i++)
					stream.Write<T>(data[i]);

			context.UnmapSubresource(NativeBuffer, 0);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
		{
			SetData<T>(graphicsDevice, data, 0, data.Length);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data, int startIndex, int elementCount) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data, int startIndex, int elementCount,
			int vertexStride) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (NativeBuffer != null)
			{
				NativeBuffer.Dispose();
				NativeBuffer = null;
			}
		}
		#endregion

		#region GetData (TODO)
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
