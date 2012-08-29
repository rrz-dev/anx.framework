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
	public class IndexBuffer : GraphicsResource, IGraphicsResource
	{
		#region Private
		private int indexCount;
		private BufferUsage bufferUsage;
		private IndexElementSize indexElementSize;
		private INativeIndexBuffer nativeIndexBuffer;
		#endregion

		#region Public
		// This is now internal because via befriending the assemblies
		// it's usable in the modules but doesn't confuse the enduser.
		internal INativeIndexBuffer NativeIndexBuffer
		{
			get
			{
				if (nativeIndexBuffer == null)
				{
					CreateNativeBuffer();
				}

				return this.nativeIndexBuffer;
			}
		}

		public BufferUsage BufferUsage
		{
			get
			{
				return this.bufferUsage;
			}
		}

		public int IndexCount
		{
			get
			{
				return this.indexCount;
			}
		}

		public IndexElementSize IndexElementSize
		{
			get
			{
				return this.indexElementSize;
			}
		}
		#endregion

		#region Constructor
		public IndexBuffer(GraphicsDevice graphicsDevice, IndexElementSize indexElementSize, int indexCount, BufferUsage usage)
			: base(graphicsDevice)
		{
			this.indexElementSize = indexElementSize;
			this.indexCount = indexCount;
			this.bufferUsage = usage;

			base.GraphicsDevice.ResourceCreated += GraphicsDevice_ResourceCreated;
			base.GraphicsDevice.ResourceDestroyed += GraphicsDevice_ResourceDestroyed;

			CreateNativeBuffer();
		}

		public IndexBuffer(GraphicsDevice graphicsDevice, Type indexType, int indexCount, BufferUsage usage)
			: base(graphicsDevice)
		{
			if (indexType == typeof(int) ||
					indexType == typeof(uint))
			{
				this.indexElementSize = IndexElementSize.ThirtyTwoBits;
			}
			else if (indexType == typeof(short) ||
				indexType == typeof(ushort))
			{
				this.indexElementSize = IndexElementSize.SixteenBits;
			}
			else
			{
				throw new Exception("can't use IndexType " + indexType.ToString());
			}

			this.indexCount = indexCount;
			this.bufferUsage = usage;

			CreateNativeBuffer();
		}

		~IndexBuffer()
		{
			Dispose();
			base.GraphicsDevice.ResourceCreated -= GraphicsDevice_ResourceCreated;
			base.GraphicsDevice.ResourceDestroyed -= GraphicsDevice_ResourceDestroyed;
		}
		#endregion

		#region GraphicsDevice_ResourceDestroyed
		private void GraphicsDevice_ResourceDestroyed(object sender, ResourceDestroyedEventArgs e)
		{
			if (this.nativeIndexBuffer != null)
			{
				this.nativeIndexBuffer.Dispose();
				this.nativeIndexBuffer = null;
			}
		}
		#endregion

		#region GraphicsDevice_ResourceCreated
		private void GraphicsDevice_ResourceCreated(object sender, ResourceCreatedEventArgs e)
		{
			if (nativeIndexBuffer != null)
			{
				nativeIndexBuffer.Dispose();
				nativeIndexBuffer = null;
			}

			CreateNativeBuffer();
		}
		#endregion

		#region CreateNativeBuffer
		private void CreateNativeBuffer()
		{
			this.nativeIndexBuffer =
				AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateIndexBuffer(GraphicsDevice, this, indexElementSize, indexCount, bufferUsage);
		}
		#endregion

		#region GetData
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex,
			int elementCount) where T : struct
		{
			NativeIndexBuffer.GetData(offsetInBytes, data, startIndex, elementCount);
		}

		public void GetData<T>(T[] data) where T : struct
		{
			NativeIndexBuffer.GetData(data);
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			NativeIndexBuffer.GetData(data, startIndex, elementCount);
		}
		#endregion

		#region SetData
		public void SetData<T>(int offsetInBytes, T[] data, int startIndex,
			int elementCount) where T : struct
		{
			NativeIndexBuffer.SetData(GraphicsDevice, offsetInBytes, data,
				startIndex, elementCount);
		}

		public void SetData<T>(T[] data) where T : struct
		{
			NativeIndexBuffer.SetData(GraphicsDevice, data);
		}

		public void SetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			NativeIndexBuffer.SetData(GraphicsDevice, data, startIndex, elementCount);
		}
		#endregion

		#region Dispose
		public override void Dispose()
		{
			Dispose(true);
		}

		protected override void Dispose(
			[MarshalAs(UnmanagedType.U1)] bool disposeManaged)
		{
			if (this.nativeIndexBuffer != null)
			{
				this.nativeIndexBuffer.Dispose();
				this.nativeIndexBuffer = null;
			}
		}
		#endregion
	}
}
