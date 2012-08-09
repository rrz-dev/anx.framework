using System;
using System.Runtime.InteropServices;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using OpenTK.Graphics.OpenGL;
using ANX.RenderSystem.Windows.GL3.Helpers;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.GL3
{
	/// <summary>
	/// Native OpenGL implementation of a Vertex Buffer.
	/// <para />
	/// Great tutorial about VBO/IBO directly for OpenTK:
	/// http://www.opentk.com/doc/graphics/geometry/vertex-buffer-objects
	/// </summary>
	public class VertexBufferGL3 : INativeVertexBuffer
	{
		#region Private
		private VertexBuffer managedBuffer;

		/// <summary>
		/// Native vertex buffer handle.
		/// </summary>
		private int bufferHandle;
		internal int BufferHandle
		{
			get
			{
				return bufferHandle;
			}
		}

		private VertexDeclaration vertexDeclaration;

		private BufferUsage usage;

		private int vertexCount;

		private BufferUsageHint usageHint;

		internal bool IsDisposed;
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new Vertex Buffer object.
		/// </summary>
		internal VertexBufferGL3(VertexBuffer setManagedBuffer,
			VertexDeclaration setVertexDeclaration, int setVertexCount,
			BufferUsage setUsage)
		{
			GraphicsResourceManager.UpdateResource(this, true);

			managedBuffer = setManagedBuffer;
			vertexDeclaration = setVertexDeclaration;
			usage = setUsage;
			vertexCount = setVertexCount;

			bool isDynamicBuffer = managedBuffer is DynamicVertexBuffer;

			usageHint = isDynamicBuffer ?
				BufferUsageHint.DynamicDraw :
				BufferUsageHint.StaticDraw;

			CreateBuffer();
		}

		~VertexBufferGL3()
		{
			GraphicsResourceManager.UpdateResource(this, false);
		}
		#endregion

		#region CreateBuffer
		private void CreateBuffer()
		{
			GL.GenBuffers(1, out bufferHandle);
			ErrorHelper.Check("GenBuffers");
			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
			ErrorHelper.Check("BindBuffer");
			int size = vertexDeclaration.VertexStride * vertexCount;
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)size, IntPtr.Zero,
				usageHint);
			ErrorHelper.Check("BufferData");

			int setSize;
			GL.GetBufferParameter(BufferTarget.ArrayBuffer,
				BufferParameterName.BufferSize, out setSize);
			if (setSize != size)
			{
				throw new Exception("Failed to set the vertexBuffer data. DataSize=" +
					size + " SetSize=" + setSize);
			}
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
			BufferData(data, 0);
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			if (startIndex != 0 ||
				elementCount != data.Length)
			{
				T[] subArray = new T[elementCount];
				Array.Copy(data, startIndex, subArray, 0, elementCount);
				BufferData(subArray, 0);
			}
			else
			{
				BufferData(data, 0);
			}
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
			T[] data, int startIndex, int elementCount) where T : struct
		{
			if (startIndex != 0 ||
				elementCount != data.Length)
			{
				T[] subArray = new T[elementCount];
				Array.Copy(data, startIndex, subArray, 0, elementCount);
				BufferData(subArray, offsetInBytes);
			}
			else
			{
				BufferData(data, offsetInBytes);
			}
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
			T[] data, int startIndex, int elementCount, int vertexStride) where T : struct
		{
			T[] elements;
			if (startIndex != 0 ||
				elementCount != data.Length)
			{
				elements = new T[elementCount];
				Array.Copy(data, startIndex, elements, 0, elementCount);
			}
			else
			{
				elements = data;
			}

			int size = Marshal.SizeOf(typeof(T));

			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
			ErrorHelper.Check("BindBuffer");

			for (int index = 0; index < elementCount; index++)
			{
				GL.BufferSubData<T>(BufferTarget.ArrayBuffer,
					(IntPtr)offsetInBytes + (index * vertexStride),
					(IntPtr)size, ref elements[index]);
				ErrorHelper.Check("BufferSubData");
			}
		}
		#endregion

		#region BufferData (private helper)
		private void BufferData<T>(T[] data, int offset) where T : struct
		{
			int size = Marshal.SizeOf(typeof(T)) * data.Length;

			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
			ErrorHelper.Check("BindBuffer");

			GL.BufferSubData<T>(BufferTarget.ArrayBuffer, (IntPtr)offset,
				(IntPtr)size, data);
			ErrorHelper.Check("BufferSubData");
		}
		#endregion

		#region GetData
		public void GetData<T>(T[] data) where T : struct
		{
			int size = Marshal.SizeOf(typeof(T)) * data.Length;

			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
			ErrorHelper.Check("BindBuffer");

			GL.GetBufferSubData<T>(BufferTarget.ArrayBuffer, IntPtr.Zero,
				(IntPtr)size, data);
			ErrorHelper.Check("GetBufferSubData");
		}
		#endregion

		#region GetData
		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			T[] copyElements = new T[elementCount];
			int size = Marshal.SizeOf(typeof(T)) * elementCount;

			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
			ErrorHelper.Check("BindBuffer");

			GL.GetBufferSubData<T>(BufferTarget.ArrayBuffer, (IntPtr)0,
				(IntPtr)size, copyElements);
			ErrorHelper.Check("GetBufferSubData");

			Array.Copy(copyElements, 0, data, startIndex, elementCount);
		}
		#endregion

		#region GetData
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex,
			int elementCount, int vertexStride) where T : struct
		{
			T[] copyElements = new T[elementCount];
			int size = Marshal.SizeOf(typeof(T));

			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
			ErrorHelper.Check("BindBuffer");

			for (int index = 0; index < elementCount; index++)
			{
				GL.GetBufferSubData<T>(BufferTarget.ArrayBuffer,
					(IntPtr)offsetInBytes + (index * vertexStride),
					(IntPtr)size,
					ref copyElements[index]);
				ErrorHelper.Check("GetBufferSubData");
			}

			Array.Copy(copyElements, 0, data, startIndex, elementCount);
		}
		#endregion

		#region Bind
		public void Bind(EffectGL3 activeEffect)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, BufferHandle);
			ErrorHelper.Check("BindBuffer");
			MapVertexDeclaration(activeEffect);
		}
		#endregion

		#region MapVertexDeclaration
		private void MapVertexDeclaration(EffectGL3 effect)
		{
			EffectTechniqueGL3 currentTechnique = effect.CurrentTechnique;

			ShaderAttributeGL3[] attributes = currentTechnique.activeAttributes;
			VertexElement[] elements = vertexDeclaration.GetVertexElements();

			if (elements.Length != attributes.Length)
			{
				throw new InvalidOperationException("Mapping the VertexDeclaration " +
					"onto the glsl attributes failed because we have " +
					attributes.Length + " Shader Attributes and " +
					elements.Length + " elements in the vertex declaration which " +
					"doesn't fit!");
			}

			for (int index = 0; index < attributes.Length; index++)
			{
				int location = attributes[index].Location;
				attributes[index].Bind(elements[location].VertexElementUsage,
					vertexDeclaration.VertexStride, elements[location].Offset);
			}
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose the native index buffer data.
		/// </summary>
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
			if (bufferHandle != -1 &&
				GraphicsDeviceWindowsGL3.IsContextCurrent)
			{
				GL.DeleteBuffers(1, ref bufferHandle);
				ErrorHelper.Check("DeleteBuffers");
				bufferHandle = -1;
			}
		}
		#endregion
	}
}
