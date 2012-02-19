using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.RenderSystem;

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
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
