using System;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
	public class VertexBuffer_DX10 : INativeVertexBuffer, IDisposable
	{
		int vertexStride;

		public SharpDX.Direct3D10.Buffer NativeBuffer { get; private set; }

		#region Constructor
		public VertexBuffer_DX10(GraphicsDevice graphics, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			GraphicsDeviceWindowsDX10 gd10 = graphics.NativeDevice as GraphicsDeviceWindowsDX10;
			Device device = gd10 != null ? gd10.NativeDevice as Device : null;

			InitializeBuffer(device, vertexDeclaration, vertexCount, usage);
		}

		internal VertexBuffer_DX10(Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			InitializeBuffer(device, vertexDeclaration, vertexCount, usage);
		}
		#endregion

		#region InitializeBuffer
		private void InitializeBuffer(Device device, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
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

				NativeBuffer = new SharpDX.Direct3D10.Buffer(device, description);
				NativeBuffer.Unmap();
			}
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data, int startIndex, int elementCount)
			where T : struct
		{
			//TODO: check offsetInBytes parameter for bounds etc.

			using (var stream = NativeBuffer.Map(MapMode.WriteDiscard))
			{
				if (offsetInBytes > 0)
					stream.Seek(offsetInBytes, SeekOrigin.Current);

				if (startIndex > 0 || elementCount < data.Length)
					for (int i = startIndex; i < startIndex + elementCount; i++)
						stream.Write<T>(data[i]);
				else
					for (int i = 0; i < data.Length; i++)
						stream.Write<T>(data[i]);

				NativeBuffer.Unmap();
			}
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

		#region GetData
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride) where T : struct
		{
			using (var stream = NativeBuffer.Map(MapMode.Read))
			{
				if (offsetInBytes > 0)
					stream.Seek(offsetInBytes, SeekOrigin.Current);

				stream.ReadRange(data, startIndex, elementCount);
				NativeBuffer.Unmap();
			}
		}

		public void GetData<T>(T[] data) where T : struct
		{
			using (var stream = NativeBuffer.Map(MapMode.Read))
			{
				stream.ReadRange(data, 0, data.Length);
				NativeBuffer.Unmap();
			}
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			using (var stream = NativeBuffer.Map(MapMode.Read))
			{
				stream.ReadRange(data, startIndex, elementCount);
				NativeBuffer.Unmap();
			}
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (this.NativeBuffer != null)
			{
				NativeBuffer.Dispose();
				NativeBuffer = null;
			}
		}
		#endregion
	}
}
