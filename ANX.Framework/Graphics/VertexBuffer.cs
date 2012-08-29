using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.RenderSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann")]
	public class VertexBuffer : GraphicsResource, IGraphicsResource
	{
		#region Private
		private VertexDeclaration vertexDeclaration;
		private int vertexCount;
		private BufferUsage bufferUsage;
		private INativeVertexBuffer nativeVertexBuffer;
		#endregion

		#region Public
		// This is now internal because via befriending the assemblies
		// it's usable in the modules but doesn't confuse the enduser.
		internal INativeVertexBuffer NativeVertexBuffer
		{
			get
			{
				if (this.nativeVertexBuffer == null)
				{
					CreateNativeBuffer();
				}

				return this.nativeVertexBuffer;
			}
		}

		public BufferUsage BufferUsage
		{
			get
			{
				return this.bufferUsage;
			}
		}

		public int VertexCount
		{
			get
			{
				return this.vertexCount;
			}
		}

		public VertexDeclaration VertexDeclaration
		{
			get
			{
				return this.vertexDeclaration;
			}
		}
		#endregion

		#region Constructor
		public VertexBuffer(GraphicsDevice graphicsDevice, Type vertexType,
			int vertexCount, BufferUsage usage)
			: this(graphicsDevice, VertexBuffer.TypeToVertexDeclaration(vertexType),
		vertexCount, usage)
		{
		}

		public VertexBuffer(GraphicsDevice graphicsDevice,
			VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
			: base(graphicsDevice)
		{
			this.vertexCount = vertexCount;
			this.vertexDeclaration = vertexDeclaration;
			this.bufferUsage = usage;

			base.GraphicsDevice.ResourceCreated += GraphicsDevice_ResourceCreated;
			base.GraphicsDevice.ResourceDestroyed += GraphicsDevice_ResourceDestroyed;

			CreateNativeBuffer();
		}

		~VertexBuffer()
		{
			Dispose();
			base.GraphicsDevice.ResourceCreated -= GraphicsDevice_ResourceCreated;
			base.GraphicsDevice.ResourceDestroyed -= GraphicsDevice_ResourceDestroyed;
		}
		#endregion

		#region GraphicsDevice_ResourceDestroyed
		private void GraphicsDevice_ResourceDestroyed(object sender, ResourceDestroyedEventArgs e)
		{
			if (nativeVertexBuffer != null)
			{
				nativeVertexBuffer.Dispose();
				nativeVertexBuffer = null;
			}
		}
		#endregion

		#region GraphicsDevice_ResourceCreated
		private void GraphicsDevice_ResourceCreated(object sender, ResourceCreatedEventArgs e)
		{
			if (nativeVertexBuffer != null)
			{
				nativeVertexBuffer.Dispose();
				nativeVertexBuffer = null;
			}

			CreateNativeBuffer();
		}
		#endregion

		#region CreateNativeBuffer
		private void CreateNativeBuffer()
		{
			this.nativeVertexBuffer =
				AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateVertexBuffer(GraphicsDevice, this, vertexDeclaration, vertexCount, bufferUsage);
		}
		#endregion

		#region GetData
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex,
			int elementCount, int vertexStride) where T : struct
		{
			NativeVertexBuffer.GetData(offsetInBytes, data, startIndex,
				elementCount, vertexStride);
		}

		public void GetData<T>(T[] data) where T : struct
		{
			NativeVertexBuffer.GetData(data);
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			NativeVertexBuffer.GetData(data, startIndex, elementCount);
		}
		#endregion

		#region SetData
		public void SetData<T>(int offsetInBytes, T[] data, int startIndex,
			int elementCount, int vertexStride) where T : struct
		{
			NativeVertexBuffer.SetData(GraphicsDevice, offsetInBytes, data,
				startIndex, elementCount, vertexStride);
		}

		public void SetData<T>(T[] data) where T : struct
		{
			NativeVertexBuffer.SetData(GraphicsDevice, data);
		}

		public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			NativeVertexBuffer.SetData(GraphicsDevice, data, startIndex, elementCount);
		}
		#endregion

		#region TypeToVertexDeclaration
		private static VertexDeclaration TypeToVertexDeclaration(Type t)
		{
			IVertexType vt = Activator.CreateInstance(t) as IVertexType;
			if (vt != null)
			{
				return vt.VertexDeclaration;
			}

			return null;
		}
		#endregion

		#region Dispose
		public override void Dispose()
		{
			if (nativeVertexBuffer != null)
			{
				nativeVertexBuffer.Dispose();
				nativeVertexBuffer = null;
			}

			if (vertexDeclaration != null)
			{
				// do not dispose the VertexDeclaration here, because it's only a reference
				vertexDeclaration = null;
			}
		}

		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
