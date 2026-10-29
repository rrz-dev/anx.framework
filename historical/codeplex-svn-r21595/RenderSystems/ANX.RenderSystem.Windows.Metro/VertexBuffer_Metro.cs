using System;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class VertexBuffer_Metro : INativeVertexBuffer, IDisposable
	{
		#region Private
		private int vertexStride;
		#endregion

		#region Public
		public Dx11.Buffer NativeBuffer
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		public VertexBuffer_Metro(GraphicsDevice graphics, VertexDeclaration vertexDeclaration,
			int vertexCount, BufferUsage usage)
		{
			GraphicsDeviceWindowsMetro gdMetro = graphics.NativeDevice as GraphicsDeviceWindowsMetro;
			var device = gdMetro.NativeDevice.NativeDevice;

			vertexStride = vertexDeclaration.VertexStride;
			InitializeBuffer(device, vertexCount, usage);
		}

		internal VertexBuffer_Metro(Dx11.Device device, VertexDeclaration vertexDeclaration,
			int vertexCount, BufferUsage usage)
		{
			vertexStride = vertexDeclaration.VertexStride;
			InitializeBuffer(device, vertexCount, usage);
		}
		#endregion

		#region InitializeBuffer
		private void InitializeBuffer(Dx11.Device device, int vertexCount,
			BufferUsage usage)
		{
			if (device != null)
			{
				var description = new Dx11.BufferDescription()
				{
					Usage = FormatConverter.Translate(usage),
					SizeInBytes = vertexStride * vertexCount,
					BindFlags = Dx11.BindFlags.VertexBuffer,
					CpuAccessFlags = Dx11.CpuAccessFlags.Write,
					OptionFlags = Dx11.ResourceOptionFlags.None
				};

				NativeBuffer = new Dx11.Buffer(device, description);
			}
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
			T[] data, int startIndex, int elementCount) where T : struct
		{
			SetData<T>(graphicsDevice, offsetInBytes, data, startIndex, elementCount, vertexStride);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
		{
			SetData<T>(graphicsDevice, data, 0, data.Length);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data, int startIndex,
			int elementCount) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data,
			int startIndex, int elementCount, int vertexStride) where T : struct
		{
			GraphicsDeviceWindowsMetro gdMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
			var device = gdMetro.NativeDevice;

			//TODO: check offsetInBytes parameter for bounds etc.

			SharpDX.DataStream stream = device.MapSubresource(NativeBuffer);

			if (offsetInBytes > 0)
				stream.Seek(offsetInBytes, SeekOrigin.Current);

			if (startIndex > 0 || elementCount < data.Length)
				for (int i = startIndex; i < startIndex + elementCount; i++)
					stream.Write<T>(data[i]);
			else
				for (int i = 0; i < data.Length; i++)
					stream.Write<T>(data[i]);
			
			device.UnmapSubresource(NativeBuffer, 0);
		}
		#endregion

		#region GetData (TODO)
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex,
			int elementCount, int vertexStride) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
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
	}
}
