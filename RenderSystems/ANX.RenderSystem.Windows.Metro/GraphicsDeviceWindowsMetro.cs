using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using SharpDX.DXGI;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class GraphicsDeviceWindowsMetro : INativeGraphicsDevice
	{
		#region Constants
		private const float ColorMultiplier = 1f / 255f;
		#endregion
		
		#region Private
		internal Effect_Metro currentEffect;
		private VertexBuffer currentVertexBuffer;
		private IndexBuffer currentIndexBuffer;
		private Dx11.Viewport currentViewport;
		private uint lastClearColor;
		private SharpDX.Color4 clearColor;
		private bool vSyncEnabled;

		internal NativeDxDevice NativeDevice
		{
			get;
			private set;
		}
		#endregion

		#region Public
		public bool VSync
		{
			get
			{
				return this.vSyncEnabled;
			}
			set
			{
				this.vSyncEnabled = value;
			}
		}
		#endregion

		#region Constructor
		public GraphicsDeviceWindowsMetro(PresentationParameters presentationParameters)
		{
			this.vSyncEnabled = true;

			NativeDevice = new NativeDxDevice(presentationParameters);
			
			ResizeRenderWindow(presentationParameters);

			currentViewport = new Dx11.Viewport(0, 0,
				presentationParameters.BackBufferWidth,
				presentationParameters.BackBufferHeight);
		}
		#endregion

		#region Clear
		public void Clear(ref Color color)
		{
			uint newClearColor = color.PackedValue;
			if (lastClearColor != newClearColor)
			{
				lastClearColor = newClearColor;
				clearColor.Red = color.R * ColorMultiplier;
				clearColor.Green = color.G * ColorMultiplier;
				clearColor.Blue = color.B * ColorMultiplier;
				clearColor.Alpha = color.A * ColorMultiplier;
			}

			NativeDevice.Clear(clearColor);
		}

		public void Clear(ClearOptions options, Vector4 color, float depth, int stencil)
		{
			if ((options & ClearOptions.Target) == ClearOptions.Target)
			{
				// Clear a RenderTarget (or BackBuffer)

				this.clearColor.Red = color.X;
				this.clearColor.Green = color.Y;
				this.clearColor.Blue = color.Z;
				this.clearColor.Alpha = color.W;
				this.lastClearColor = 0;

				NativeDevice.Clear(clearColor);
			}

			if ((options | ClearOptions.Stencil | ClearOptions.DepthBuffer) == options)
			{
				// Clear the stencil buffer

				NativeDevice.ClearDepthAndStencil(Dx11.DepthStencilClearFlags.Depth | Dx11.DepthStencilClearFlags.Stencil, depth, (byte)stencil);
			}
			else if ((options | ClearOptions.Stencil) == options)
			{
				NativeDevice.ClearDepthAndStencil(Dx11.DepthStencilClearFlags.Stencil, depth, (byte)stencil);
			}
			else
			{
				NativeDevice.ClearDepthAndStencil(Dx11.DepthStencilClearFlags.Depth, depth, (byte)stencil);
			}
		}

		#endregion

		#region Present
		public void Present()
		{
			NativeDevice.Present(this.vSyncEnabled ? 1 : 0);
		}

		#endregion // Present

		#region DrawPrimitives & DrawIndexedPrimitives
		public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount)
		{
			//Dx11.EffectPass pass; Dx11.EffectTechnique technique; ShaderBytecode passSignature;
			//SetupEffectForDraw(out pass, out technique, out passSignature);

			//SetupInputLayout(passSignature);

			//// Prepare All the stages
			//deviceContext.InputAssembler.PrimitiveTopology = FormatConverter.Translate(primitiveType);
			//deviceContext.Rasterizer.SetViewports(currentViewport);

			//deviceContext.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

			//for (int i = 0; i < technique.Description.PassCount; ++i)
			//{
			//    pass.Apply();
			//    deviceContext.DrawIndexed(CalculateVertexCount(primitiveType, primitiveCount), startIndex, baseVertex);
			//}
		}

		public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset, int primitiveCount)
		{
			//Dx11.EffectPass pass; Dx11.EffectTechnique technique; ShaderBytecode passSignature;
			//SetupEffectForDraw(out pass, out technique, out passSignature);

			//SetupInputLayout(passSignature);

			//// Prepare All the stages
			//deviceContext.InputAssembler.PrimitiveTopology = FormatConverter.Translate(primitiveType);
			//deviceContext.Rasterizer.SetViewports(currentViewport);

			//deviceContext.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

			//for (int i = 0; i < technique.Description.PassCount; ++i)
			//{
			//    pass.Apply();
			//    deviceContext.Draw(primitiveCount, vertexOffset);
			//}
		}

		#endregion // DrawPrimitives & DrawIndexedPrimitives

		#region DrawInstancedPrimitives
		public void DrawInstancedPrimitives(PrimitiveType primitiveType,
			int baseVertex, int minVertexIndex, int numVertices, int startIndex,
			int primitiveCount, int instanceCount)
		{
			NativeDevice.NativeContext.DrawIndexedInstanced(numVertices,
				instanceCount, startIndex, baseVertex, 0);
		}

		#endregion // DrawInstancedPrimitives

		#region DrawUserIndexedPrimitives<T>
		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration, IndexElementSize indexFormat) where T : struct, IVertexType
		{
			int vertexCount = vertexData.Length;
			int indexCount = indexData.Length;
			VertexBuffer_Metro vb11 = new VertexBuffer_Metro(NativeDevice.NativeDevice, vertexDeclaration, vertexCount, BufferUsage.None);
			vb11.SetData<T>(null, vertexData);

			Dx11.VertexBufferBinding nativeVertexBufferBindings = new Dx11.VertexBufferBinding(vb11.NativeBuffer, vertexDeclaration.VertexStride, 0);

			NativeDevice.NativeContext.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

			IndexBuffer_Metro idxMetro = new IndexBuffer_Metro(NativeDevice.NativeDevice, indexFormat, indexCount, BufferUsage.None);
			if (indexData.GetType() == typeof(Int16[]))
			{
				idxMetro.SetData<short>(null, (short[])indexData);
			}
			else
			{
				idxMetro.SetData<int>(null, (int[])indexData);
			}

			DrawIndexedPrimitives(primitiveType, 0, vertexOffset, numVertices, indexOffset, primitiveCount);
		}

		#endregion // DrawUserIndexedPrimitives<T>

		#region DrawUserPrimitives<T>
		public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct, IVertexType
		{
			int vertexCount = vertexData.Length;
			VertexBuffer_Metro vbMetro = new VertexBuffer_Metro(NativeDevice.NativeDevice, vertexDeclaration, vertexCount, BufferUsage.None);
			vbMetro.SetData<T>(null, vertexData);

			Dx11.VertexBufferBinding nativeVertexBufferBindings = new Dx11.VertexBufferBinding(vbMetro.NativeBuffer, vertexDeclaration.VertexStride, 0);

			NativeDevice.NativeContext.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

			DrawPrimitives(primitiveType, vertexOffset, primitiveCount);
		}

		#endregion // DrawUserPrimitives<T>

		//private void SetupEffectForDraw(out Dx11.EffectPass pass, out Dx11.EffectTechnique technique, out ShaderBytecode passSignature)
		//{
		//    // get the current effect
		//    //TODO: check for null and throw exception
		//    Effect_Metro effect = this.currentEffect;

		//    // get the input semantic of the current effect / technique that is used
		//    //TODO: check for null's and throw exceptions
		//    technique = effect.NativeEffect.GetTechniqueByIndex(0);
		//    pass = technique.GetPassByIndex(0);
		//    passSignature = pass.Description.Signature;
		//}

		/*
		private void SetupInputLayout(ShaderBytecode passSignature)
		{
			// get the VertexDeclaration from current VertexBuffer to create input layout for the input assembler
			//TODO: check for null and throw exception
			VertexDeclaration vertexDeclaration = currentVertexBuffer.VertexDeclaration;
			var layout = CreateInputLayout(NativeDevice.NativeDevice, passSignature, vertexDeclaration);

			NativeDevice.NativeContext.InputAssembler.InputLayout = layout;
		}*/

		#region CalculateVertexCount
		private int CalculateVertexCount(PrimitiveType type, int primitiveCount)
		{
			if (type == PrimitiveType.TriangleList)
			{
				return primitiveCount * 3;
			}
			else if (type == PrimitiveType.LineList)
			{
				return primitiveCount * 2;
			}
			else if (type == PrimitiveType.LineStrip)
			{
				return primitiveCount + 1;
			}
			else if (type == PrimitiveType.TriangleStrip)
			{
				return primitiveCount + 2;
			}
			else
			{
				throw new NotImplementedException("couldn't calculate vertex count for PrimitiveType '" + type.ToString() + "'");
			}
		}
		#endregion

		#region SetIndexBuffer
		public void SetIndexBuffer(IndexBuffer indexBuffer)
		{
			if (indexBuffer == null)
			{
				throw new ArgumentNullException("indexBuffer");
			}

			this.currentIndexBuffer = indexBuffer;

			IndexBuffer_Metro nativeIndexBuffer = indexBuffer.NativeIndexBuffer as IndexBuffer_Metro;

			if (nativeIndexBuffer != null)
			{
				NativeDevice.NativeContext.InputAssembler.SetIndexBuffer(nativeIndexBuffer.NativeBuffer, FormatConverter.Translate(indexBuffer.IndexElementSize), 0);
			}
			else
			{
				throw new Exception("couldn't fetch native DirectX10 IndexBuffer");
			}
		}
		#endregion

		#region SetVertexBuffers
		public void SetVertexBuffers(VertexBufferBinding[] vertexBuffers)
		{
			if (vertexBuffers == null)
			{
				throw new ArgumentNullException("vertexBuffers");
			}

			this.currentVertexBuffer = vertexBuffers[0].VertexBuffer;   //TODO: hmmmmm, not nice :-)

			Dx11.VertexBufferBinding[] nativeVertexBufferBindings = new Dx11.VertexBufferBinding[vertexBuffers.Length];
			for (int i = 0; i < vertexBuffers.Length; i++)
			{
				ANX.Framework.Graphics.VertexBufferBinding anxVertexBufferBinding = vertexBuffers[i];
				VertexBuffer_Metro nativeVertexBuffer = anxVertexBufferBinding.VertexBuffer.NativeVertexBuffer as VertexBuffer_Metro;

				if (nativeVertexBuffer != null)
				{
					nativeVertexBufferBindings[i] = new Dx11.VertexBufferBinding(nativeVertexBuffer.NativeBuffer, anxVertexBufferBinding.VertexBuffer.VertexDeclaration.VertexStride, anxVertexBufferBinding.VertexOffset);
				}
				else
				{
					throw new Exception("couldn't fetch native DirectX10 VertexBuffer");
				}
			}

			NativeDevice.NativeContext.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);
		}
		#endregion

		#region SetViewport
		public void SetViewport(Viewport viewport)
		{
			this.currentViewport = new Dx11.Viewport(viewport.X, viewport.Y,
				viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth);
		}
		#endregion

		/*
		/// <summary>
		/// This method creates a InputLayout which is needed by DirectX 10 for rendering primitives. The VertexDeclaration of ANX/XNA needs to be mapped
		/// to the DirectX 10 types. This is what this method is for.
		/// </summary>
		private Dx11.InputLayout CreateInputLayout(Dx11.Device1 device, ShaderBytecode passSignature, VertexDeclaration vertexDeclaration)
		{
			VertexElement[] vertexElements = vertexDeclaration.GetVertexElements();
			int elementCount = vertexElements.Length;
			var inputElements = new Dx11.InputElement[elementCount];

			for (int i = 0; i < elementCount; i++)
			{
				inputElements[i] = CreateInputElementFromVertexElement(vertexElements[i]);
			}

			// Layout from VertexShader input signature
			byte[] data = new byte[passSignature.BufferSize];
			unsafe
			{
				byte* ptr = (byte*)passSignature.BufferPointer;
				for (int index = 0; index < data.Length; index++)
				{
					data[index] = *ptr;
					ptr++;
				}
			}
			return new Dx11.InputLayout(device, data, inputElements);
		}*/

		#region CreateInputElementFromVertexElement
		private Dx11.InputElement CreateInputElementFromVertexElement(VertexElement vertexElement)
		{
			string elementName = FormatConverter.Translate(vertexElement.VertexElementUsage);

			Format elementFormat;
			switch (vertexElement.VertexElementFormat)
			{
				case VertexElementFormat.Vector2:
					elementFormat = Format.R32G32_Float;
					break;
				case VertexElementFormat.Vector3:
					elementFormat = Format.R32G32B32_Float;
					break;
				case VertexElementFormat.Vector4:
					elementFormat = Format.R32G32B32A32_Float;
					break;
				case VertexElementFormat.Color:
					elementFormat = Format.R8G8B8A8_UNorm;
					break;
				default:
					throw new Exception("can't map '" + vertexElement.VertexElementFormat.ToString() + "' to DXGI.Format in DirectX10 RenderSystem CreateInputElementFromVertexElement");
			}

			return new Dx11.InputElement(elementName, vertexElement.UsageIndex, elementFormat, vertexElement.Offset, 0);
		}
		#endregion

		#region SetRenderTargets (TODO)
		public void SetRenderTargets(params RenderTargetBinding[] renderTargets)
		{
			/*if (renderTargets == null)
			{
				// reset the RenderTarget to backbuffer
				if (renderTargetView != null)
				{
					renderTargetView.Dispose();
					renderTargetView = null;
				}

				//TODO: device.OutputMerger.SetRenderTargets(1, new RenderTargetView[] { this.renderView }, this.depthStencilView);
				deviceContext.OutputMerger.SetTargets(this.depthStencilView, this.renderView);
			}
			else
			{
				if (renderTargets.Length == 1)
				{
					RenderTarget2D renderTarget = renderTargets[0].RenderTarget as RenderTarget2D;
					RenderTarget2D_Metro nativeRenderTarget = renderTarget.NativeRenderTarget as RenderTarget2D_Metro;
					if (renderTarget != null)
					{
						if (renderTargetView != null)
						{
							renderTargetView.Dispose();
							renderTargetView = null;
						}
						this.renderTargetView = new RenderTargetView(deviceContext.Device, ((Texture2D_Metro)nativeRenderTarget).NativeShaderResourceView.Resource);
						DepthStencilView depthStencilView = null;
						deviceContext.OutputMerger.SetTargets(new RenderTargetView[] { this.renderTargetView });
						//deviceContext.OutputMerger.SetTargets(new RenderTargetView[] { this.renderTargetView }, depthStencilView);
						//TODO: set depthStencilView
					}
				}
				else
				{
					throw new NotImplementedException("handling of multiple RenderTargets are not yet implemented");
				}
			}*/
		}
		#endregion

		#region GetBackBufferData (TODO)
		public void GetBackBufferData<T>(Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetBackBufferData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetBackBufferData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region ResizeBuffers
		public void ResizeBuffers(PresentationParameters presentationParameters)
		{
			NativeDevice.Resize(presentationParameters);
			ResizeRenderWindow(presentationParameters);
		}
		#endregion

		#region ResizeRenderWindow (TODO)
		private void ResizeRenderWindow(PresentationParameters presentationParameters)
		{
			// TODO
			//WindowsGameWindow gameWindow = (WindowsGameHost.Instance.Window as WindowsGameWindow);
			//if (gameWindow.Form != null)
			//{
				//gameWindow.Form.Bounds
			//}
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (NativeDevice != null)
			{
				NativeDevice.Dispose();
				NativeDevice = null;
			}
		}
		#endregion
	}
}
