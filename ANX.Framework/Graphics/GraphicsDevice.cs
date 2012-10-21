using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.RenderSystem;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(75)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class GraphicsDevice : IDisposable
	{
		#region Private Members
		private IndexBuffer indexBuffer;
		private SamplerStateCollection samplerStateCollection;
		private Viewport viewport;
		private BlendState blendState;
		private RasterizerState rasterizerState;
		private DepthStencilState depthStencilState;
		private GraphicsAdapter currentAdapter;
		private PresentationParameters currentPresentationParameters;
		private bool isDisposed;
		private GraphicsProfile graphicsProfile;
		private VertexBufferBinding[] currentVertexBufferBindings;
		private RenderTargetBinding[] currentRenderTargetBindings;
		private TextureCollection vertexTextureCollection;
		private TextureCollection textureCollection;

		#endregion // Private Members

		#region Events
		public event EventHandler<EventArgs> Disposing;
		public event EventHandler<ResourceDestroyedEventArgs> ResourceDestroyed;
		public event EventHandler<ResourceCreatedEventArgs> ResourceCreated;
		public event EventHandler<EventArgs> DeviceLost;
		public event EventHandler<EventArgs> DeviceReset;
		public event EventHandler<EventArgs> DeviceResetting;

		#endregion // Events

		#region Constructor & Destructor
		public GraphicsDevice(GraphicsAdapter adapter, GraphicsProfile graphicsProfile,
			PresentationParameters presentationParameters)
		{
			this.currentAdapter = adapter;
			this.graphicsProfile = graphicsProfile;

			this.viewport = new Viewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight);

			Recreate(presentationParameters);

			// TODO: get maximum number of sampler states from capabilities
			this.samplerStateCollection = new SamplerStateCollection(this, 8);
			this.textureCollection = new TextureCollection(16);
			this.vertexTextureCollection = new TextureCollection(16);

            samplerStateCollection.InitializeDeviceState();
			this.BlendState = BlendState.Opaque;
			this.DepthStencilState = DepthStencilState.Default;
			this.RasterizerState = RasterizerState.CullCounterClockwise;
		}

		~GraphicsDevice()
		{
			this.Dispose(false);
		}
		#endregion // Constructor & Destructor

		#region Clear
		public void Clear(Color color)
		{
			ClearOptions options = ClearOptions.Target;
			if (this.currentPresentationParameters.DepthStencilFormat != DepthFormat.None)
				options |= ClearOptions.DepthBuffer;
			if (this.currentPresentationParameters.DepthStencilFormat == DepthFormat.Depth24Stencil8)
				options |= ClearOptions.Stencil;

			Clear(options, color, 1, 0);
			// nativeDevice.Clear(ref color);
		}

		public void Clear(ClearOptions options, Color color, float depth, int stencil)
		{
			Clear(options, color.ToVector4(), depth, stencil);
		}

		public void Clear(ClearOptions options, Vector4 color, float depth, int stencil)
		{
			if ((options & ClearOptions.DepthBuffer) == ClearOptions.DepthBuffer &&
				this.currentPresentationParameters.DepthStencilFormat == DepthFormat.None)
			{
				throw new InvalidOperationException("The depth buffer can only be cleared if it exists. The current " +
					"DepthStencilFormat is DepthFormat.None");
			}

			if ((options & ClearOptions.Stencil) == ClearOptions.Stencil &&
				this.currentPresentationParameters.DepthStencilFormat != DepthFormat.Depth24Stencil8)
			{
				throw new InvalidOperationException("The stencil buffer can only be cleared if it exists. The current " +
					"DepthStencilFormat is not DepthFormat.Depth24Stencil8");
			}

			NativeDevice.Clear(options, color, depth, stencil);
		}

		#endregion // Clear

		#region Present
		public void Present()
		{
			NativeDevice.Present();
		}

		public void Present(Nullable<Rectangle> sourceRectangle, Nullable<Rectangle> destinationRectangle,
			IntPtr overrideWindowHandle)
		{
			//TODO: implement
			throw new NotImplementedException();
		}

		#endregion // Present

		#region DrawPrimitives & DrawIndexedPrimitives
		public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount)
		{
            if (this.indexBuffer == null)
            {
                throw new InvalidOperationException("you have to set a index buffer before you can draw using DrawIndexedPrimitives");
            }

			NativeDevice.DrawIndexedPrimitives(primitiveType, baseVertex, minVertexIndex, numVertices, startIndex, primitiveCount, this.indexBuffer);
		}

		public void DrawPrimitives(PrimitiveType primitiveType, int startVertex, int primitiveCount)
		{
			NativeDevice.DrawPrimitives(primitiveType, startVertex, primitiveCount);
		}
		#endregion

		#region DrawInstancedPrimitives
		public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices,
			int startIndex, int primitiveCount, int instanceCount)
		{
			NativeDevice.DrawInstancedPrimitives(primitiveType, baseVertex, minVertexIndex, numVertices, startIndex,
				primitiveCount, instanceCount, this.indexBuffer);
		}
		#endregion

		#region DrawUserIndexedPrimitives<T>
		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			short[] indexData, int indexOffset, int primitiveCount) where T : struct, IVertexType
		{
			var vertexDeclaration = VertexTypeHelper.GetDeclaration<T>();
			NativeDevice.DrawUserIndexedPrimitives<T>(primitiveType, vertexData, vertexOffset, numVertices, indexData,
				indexOffset, primitiveCount, vertexDeclaration, IndexElementSize.SixteenBits);
		}

		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			short[] indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration)
			where T : struct, IVertexType
		{
			NativeDevice.DrawUserIndexedPrimitives<T>(primitiveType, vertexData, vertexOffset, numVertices, indexData,
				indexOffset, primitiveCount, vertexDeclaration, IndexElementSize.SixteenBits);
		}

		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			int[] indexData, int indexOffset, int primitiveCount) where T : struct, IVertexType
		{
			var vertexDeclaration = VertexTypeHelper.GetDeclaration<T>();
			NativeDevice.DrawUserIndexedPrimitives<T>(primitiveType, vertexData, vertexOffset, numVertices, indexData,
				indexOffset, primitiveCount, vertexDeclaration, IndexElementSize.ThirtyTwoBits);
		}

		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			int[] indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration)
			where T : struct, IVertexType
		{
			NativeDevice.DrawUserIndexedPrimitives<T>(primitiveType, vertexData, vertexOffset, numVertices, indexData,
				indexOffset, primitiveCount, vertexDeclaration, IndexElementSize.ThirtyTwoBits);
		}
		#endregion

		#region DrawUserPrimitives<T>
		public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount)
			where T : struct, IVertexType
		{
			var vertexDeclaration = VertexTypeHelper.GetDeclaration<T>();
			NativeDevice.DrawUserPrimitives<T>(primitiveType, vertexData, vertexOffset, primitiveCount, vertexDeclaration);
		}

		public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount,
			VertexDeclaration vertexDeclaration) where T : struct, IVertexType
		{
			NativeDevice.DrawUserPrimitives<T>(primitiveType, vertexData, vertexOffset, primitiveCount, vertexDeclaration);
		}
		#endregion
		
#if XNAEXT
		#region SetConstantBuffer
		/// <summary>
		/// Binds a ConstantBuffer to the current GraphicsDevice
		/// </summary>
		/// <param name="slot">The index of the constant buffer used in the shader</param>
		/// <param name="constantBuffer">The managed constant buffer object to bind.</param>
		public void SetConstantBuffer(int slot, ConstantBuffer constantBuffer)
		{
			NativeDevice.SetConstantBuffer(slot, constantBuffer);
		}

		/// <summary>
		/// Binds ConstantBuffer objects to the current GraphicsDevice.
		/// </summary>
		/// <param name="constantBuffers">The array of managed constant buffer objects to bind.</param>
		/// <remarks>The constant buffer objects are bound in the order found in the passed array.</remarks>
		public void SetConstantBuffers(params ConstantBuffer[] constantBuffers)
		{
			for (int slot = 0; slot < constantBuffers.Length; slot++)
				NativeDevice.SetConstantBuffer(slot, constantBuffers[slot]);
		}

		#endregion
#endif

		#region SetVertexBuffer
		public void SetVertexBuffer(VertexBuffer vertexBuffer)
		{
            if (vertexBuffer != null)
            {
                VertexBufferBinding[] bindings = new VertexBufferBinding[] { new VertexBufferBinding(vertexBuffer) };
                this.currentVertexBufferBindings = bindings;
                NativeDevice.SetVertexBuffers(bindings);
            }
            else
            {
                SetVertexBuffers(null);
            }
		}

		public void SetVertexBuffer(VertexBuffer vertexBuffer, int vertexOffset)
		{
            if (vertexBuffer != null)
            {
                VertexBufferBinding[] bindings = new VertexBufferBinding[] { new VertexBufferBinding(vertexBuffer, vertexOffset) };
                this.currentVertexBufferBindings = bindings;
                NativeDevice.SetVertexBuffers(bindings);
            }
            else
            {
                SetVertexBuffers(null);
            }
		}

		public void SetVertexBuffers(params Graphics.VertexBufferBinding[] vertexBuffers)
		{
			this.currentVertexBufferBindings = vertexBuffers;
			NativeDevice.SetVertexBuffers(vertexBuffers);
		}

		#endregion // SetVertexBuffer

		#region SetRenderTarget
		public void SetRenderTarget(RenderTarget2D renderTarget)
		{
			if (renderTarget != null)
			{
				RenderTargetBinding[] renderTargetBindings = new RenderTargetBinding[] { new RenderTargetBinding(renderTarget) };
				this.currentRenderTargetBindings = renderTargetBindings;
				NativeDevice.SetRenderTargets(renderTargetBindings);
			}
			else
				NativeDevice.SetRenderTargets(null);
		}

		public void SetRenderTarget(RenderTargetCube renderTarget, CubeMapFace cubeMapFace)
		{
			RenderTargetBinding[] renderTargetBindings = new RenderTargetBinding[] { new RenderTargetBinding(renderTarget, cubeMapFace) };
			this.currentRenderTargetBindings = renderTargetBindings;
			NativeDevice.SetRenderTargets(renderTargetBindings);
		}

		public void SetRenderTargets(params RenderTargetBinding[] renderTargets)
		{
			this.currentRenderTargetBindings = renderTargets;
			NativeDevice.SetRenderTargets(renderTargets);
		}

		#endregion // SetRenderTarget

		#region GetBackBufferData<T>
		public void GetBackBufferData<T>(Nullable<Rectangle> rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			NativeDevice.GetBackBufferData<T>(rect, data, startIndex, elementCount);
		}

		public void GetBackBufferData<T>(T[] data) where T : struct
		{
			NativeDevice.GetBackBufferData<T>(data);
		}

		public void GetBackBufferData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			NativeDevice.GetBackBufferData<T>(data, startIndex, elementCount);
		}

		#endregion // GetBackBufferData<T>

		public VertexBufferBinding[] GetVertexBuffers()
		{
			return this.currentVertexBufferBindings;
		}

		public RenderTargetBinding[] GetRenderTargets()
		{
			return this.currentRenderTargetBindings;
		}

		#region Reset
		public void Reset()
		{
			this.Reset(this.currentPresentationParameters, this.currentAdapter);
		}

		public void Reset(PresentationParameters presentationParameters)
		{
			this.Reset(presentationParameters, this.currentAdapter);
		}

		public void Reset(PresentationParameters presentationParameters, GraphicsAdapter adapter)
		{
			if (presentationParameters == null)
			{
				throw new ArgumentNullException("presentationParameters");
			}

			if (adapter == null)
			{
				throw new ArgumentNullException("adapter");
			}

			if (this.currentAdapter != adapter)
			{
				throw new InvalidOperationException("adapter switch not yet implemented");
			}

			raise_DeviceResetting(this, EventArgs.Empty);

			// As it seems that no hardware Depth24 exists we handle Depth24
			// and Depth24Stencil8 the same way. Problem is that the Clear method
			// checks for Depth24Stencil8 when trying to clear the stencil buffer
			// and the format is set to Depth24. Internally Depth24 is already
			// handled as Depth24Stencil8 so it is interchangeable.
			if ((this.currentPresentationParameters.DepthStencilFormat == DepthFormat.Depth24 ||
				this.currentPresentationParameters.DepthStencilFormat == DepthFormat.Depth24Stencil8) &&
				(presentationParameters.DepthStencilFormat == DepthFormat.Depth24 ||
				presentationParameters.DepthStencilFormat == DepthFormat.Depth24Stencil8))
			{
				this.currentPresentationParameters.DepthStencilFormat = presentationParameters.DepthStencilFormat;
			}

			// reset presentation parameters
			NativeDevice.ResizeBuffers(presentationParameters);
			this.viewport = new Graphics.Viewport(0, 0, presentationParameters.BackBufferWidth,
				presentationParameters.BackBufferHeight);

            samplerStateCollection.InitializeDeviceState();

			raise_DeviceReset(this, EventArgs.Empty);
		}

		#endregion // Reset

		#region Dispose (TODO)
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
		{
			if (isDisposed == false)
			{
				isDisposed = true;
				if (NativeDevice != null)
				{
					NativeDevice.Dispose();
					NativeDevice = null;
				}

				raise_Disposing(this, EventArgs.Empty);
				//TODO: more to dispose?
			}
		}
		#endregion

		#region Public Properties
		public IndexBuffer Indices
		{
			get
			{
				return this.indexBuffer;
			}
			set
			{
				this.indexBuffer = value;
			}
		}

		public Viewport Viewport
		{
			get
			{
				return this.viewport;
			}
			set
			{
                this.viewport = value;
                NativeDevice.SetViewport(this.viewport);    //TODO: this is not optimal...
			}
		}

		public BlendState BlendState
		{
			get
			{
				return this.blendState;
			}
			set
			{
				if (this.blendState != value)
				{
					if (this.blendState != null)
					{
						this.blendState.NativeBlendState.Release();
					}

					this.blendState = value;

					this.blendState.NativeBlendState.Apply(this);
				}
			}
		}

		public DepthStencilState DepthStencilState
		{
			get
			{
				return this.depthStencilState;
			}
			set
			{
				if (this.depthStencilState != value)
				{
					if (this.depthStencilState != null)
					{
						this.depthStencilState.NativeDepthStencilState.Release();
					}

					this.depthStencilState = value;

					this.depthStencilState.NativeDepthStencilState.Apply(this);
				}
			}
		}

		public RasterizerState RasterizerState
		{
			get
			{
				return this.rasterizerState;
			}
			set
			{
				if (this.rasterizerState != value)
				{
					if (this.rasterizerState != null)
					{
						this.rasterizerState.NativeRasterizerState.Release();
					}

					this.rasterizerState = value;

					this.rasterizerState.NativeRasterizerState.Apply(this);
				}
			}
		}

		public SamplerStateCollection SamplerStates
		{
			get
			{
				return this.samplerStateCollection;
			}
		}

		public bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		public DisplayMode DisplayMode
		{
			get
			{
				return this.currentAdapter.CurrentDisplayMode;
			}
		}

		public GraphicsProfile GraphicsProfile
		{
			get
			{
				return this.graphicsProfile;
			}
		}

		public GraphicsAdapter Adapter
		{
			get
			{
				return this.currentAdapter;
			}
		}

		public PresentationParameters PresentationParameters
		{
			get
			{
				return this.currentPresentationParameters;
			}
		}

		public int ReferenceStencil
		{
			get
			{
				return DepthStencilState.ReferenceStencil;
			}
			set
			{
				if (DepthStencilState.ReferenceStencil != value)
				{
					DepthStencilState.ReferenceStencil = value;
				}
			}
		}

		public int MultiSampleMask
		{
			get
			{
				return BlendState.MultiSampleMask;
			}
			set
			{
				if (BlendState.MultiSampleMask != value)
				{
					BlendState.MultiSampleMask = value;
				}
			}
		}

		public Color BlendFactor
		{
			get
			{
				return BlendState.BlendFactor;
			}
			set
			{
				if (BlendState.BlendFactor != value)
				{
					BlendState.BlendFactor = value;
				}
			}
		}

		public TextureCollection VertexTextures
		{
			get
			{
				return this.vertexTextureCollection;
			}
		}

		public TextureCollection Textures
		{
			get
			{
				return this.textureCollection;
			}
		}

		#endregion // Public Properties

		public Rectangle ScissorRectangle
		{
			get { return NativeDevice.ScissorRectangle; }
			set { NativeDevice.ScissorRectangle = value; }
		}

		public GraphicsDeviceStatus GraphicsDeviceStatus
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public SamplerStateCollection VertexSamplerStates
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		internal INativeGraphicsDevice NativeDevice
		{
			get;
			set;
		}

		internal void Recreate(PresentationParameters presentationParameters)
		{
			if (NativeDevice != null)
			{
				NativeDevice.Dispose();
				raise_ResourceDestroyed(this, new ResourceDestroyedEventArgs("NativeGraphicsDevice", NativeDevice));
				NativeDevice = null;
			}

			if (NativeDevice == null)
			{
				this.currentPresentationParameters = presentationParameters;
				var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
				NativeDevice = creator.CreateGraphicsDevice(presentationParameters);
				this.viewport = new Viewport(0, 0, presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight);

				raise_ResourceCreated(this, new ResourceCreatedEventArgs(NativeDevice));
				GraphicsResourceTracker.Instance.UpdateGraphicsDeviceReference(this);

				if (this.indexBuffer != null)
					NativeDevice.SetIndexBuffer(this.indexBuffer);

				if (this.currentVertexBufferBindings != null)
					NativeDevice.SetVertexBuffers(this.currentVertexBufferBindings);

				if (this.blendState != null)
					this.blendState.NativeBlendState.Apply(this);

				if (this.rasterizerState != null)
					this.rasterizerState.NativeRasterizerState.Apply(this);

				if (this.depthStencilState != null)
					this.depthStencilState.NativeDepthStencilState.Apply(this);

				if (this.samplerStateCollection != null)
				{
					for (int i = 0; i < 8; i++)
					{
						if (this.samplerStateCollection[i] != null)
							this.samplerStateCollection[i].NativeSamplerState.Apply(this, i);
					}
				}
			}
		}

		protected void raise_Disposing(object sender, EventArgs args)
		{
			RaiseIfNotNull(Disposing, sender, args);
		}

		protected void raise_DeviceResetting(object sender, EventArgs args)
		{
			RaiseIfNotNull(DeviceResetting, sender, args);
		}

		protected void raise_DeviceReset(object sender, EventArgs args)
		{
			RaiseIfNotNull(DeviceReset, sender, args);
		}

		protected void raise_DeviceLost(object sender, EventArgs args)
		{
			RaiseIfNotNull(DeviceLost, sender, args);
		}

		protected void raise_ResourceCreated(object sender, ResourceCreatedEventArgs args)
		{
			RaiseIfNotNull(ResourceCreated, sender, args);
		}

		protected void raise_ResourceDestroyed(object sender, ResourceDestroyedEventArgs args)
		{
			RaiseIfNotNull(ResourceDestroyed, sender, args);
		}

		private void RaiseIfNotNull<T>(EventHandler<T> handler, object sender, T args) where T : EventArgs
		{
			if (handler != null)
				handler(sender, args);
		}
	}
}
