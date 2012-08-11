using System;
using System.Runtime.InteropServices;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using VitaVertexBuffer = Sce.PlayStation.Core.Graphics.VertexBuffer;
using VitaVertexFormat = Sce.PlayStation.Core.Graphics.VertexFormat;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	/// <summary>
	/// Native OpenGL implementation of a Vertex Buffer.
	/// <para />
	/// Great tutorial about VBO/IBO directly for OpenTK:
	/// http://www.opentk.com/doc/graphics/geometry/vertex-buffer-objects
	/// </summary>
	public class PsVitaVertexBuffer : INativeVertexBuffer
	{
		#region Private
		private VertexBuffer managedBuffer;
		private VitaVertexBuffer nativeBuffer;

		private VertexDeclaration vertexDeclaration;
		private VitaVertexFormat[] nativeFormat;

		private int vertexCount;

		internal bool IsDisposed;
		#endregion

		#region Constructor
		internal PsVitaVertexBuffer(VertexBuffer setManagedBuffer,
			VertexDeclaration setVertexDeclaration, int setVertexCount,
			BufferUsage setUsage)
		{
			managedBuffer = setManagedBuffer;
			vertexDeclaration = setVertexDeclaration;
			vertexCount = setVertexCount;

			VertexElement[] elements = vertexDeclaration.GetVertexElements();
			nativeFormat = new VitaVertexFormat[elements.Length];
			for (int index = 0; index < elements.Length; index++)
			{
				nativeFormat[index] = VitaVertexFormatFrom(elements[index].VertexElementFormat);
			}

			CreateBuffer();
		}
		#endregion

		#region CreateBuffer
		private void CreateBuffer()
		{
			nativeBuffer = new VitaVertexBuffer(vertexCount, nativeFormat);
		}
		#endregion

		#region RecreateData
		internal void RecreateData()
		{
			CreateBuffer();
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data)
			where T : struct
		{
			nativeBuffer.SetVertices(data);
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			nativeBuffer.SetVertices(data, 0, startIndex, elementCount);
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
			T[] data, int startIndex, int elementCount) where T : struct
		{
			nativeBuffer.SetVertices(data, offsetInBytes / vertexDeclaration.VertexStride,
				startIndex, elementCount);
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
			T[] data, int startIndex, int elementCount, int vertexStride) where T : struct
		{
			nativeBuffer.SetVertices(data, offsetInBytes / vertexStride,
				startIndex, elementCount);
		}
		#endregion

		#region BufferData (private helper)
		private void BufferData<T>(T[] data, int offset) where T : struct
		{
			nativeBuffer.SetVertices(data, offset, 0, data.Length);
		}
		#endregion

		#region GetData
		public void GetData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetData
		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetData
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex,
			int elementCount, int vertexStride) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion
		
		#region VitaVertexFormatFrom
		private VitaVertexFormat VitaVertexFormatFrom(VertexElementFormat format)
		{
			switch (format)
			{
				case VertexElementFormat.Byte4:
					return VitaVertexFormat.Byte4;

				case VertexElementFormat.Color:
					return VitaVertexFormat.Byte4N;

				case VertexElementFormat.HalfVector2:
					return VitaVertexFormat.Half2;

				case VertexElementFormat.HalfVector4:
					return VitaVertexFormat.Half4;

				case VertexElementFormat.NormalizedShort2:
					return VitaVertexFormat.Short2N;

				case VertexElementFormat.NormalizedShort4:
					return VitaVertexFormat.Short4N;

				case VertexElementFormat.Short2:
					return VitaVertexFormat.Short2;

				case VertexElementFormat.Short4:
					return VitaVertexFormat.Short4;

				case VertexElementFormat.Single:
					return VitaVertexFormat.Float;

				case VertexElementFormat.Vector2:
					return VitaVertexFormat.Float2;

				case VertexElementFormat.Vector3:
					return VitaVertexFormat.Float3;

				case VertexElementFormat.Vector4:
					return VitaVertexFormat.Float4;

				default:
					throw new Exception("Vertex element format '" + format + "' can't be used!");
			}
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (IsDisposed == false)
			{
				IsDisposed = true;
				DisposeResource();
			}
		}

		internal void DisposeResource()
		{
			if (nativeBuffer != null)
			{
				nativeBuffer.Dispose();
				nativeBuffer = null;
			}
		}
		#endregion
	}
}
