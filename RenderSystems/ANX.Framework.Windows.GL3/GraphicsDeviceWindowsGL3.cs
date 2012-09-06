using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.GL3.Helpers;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.GL3
{
	public class GraphicsDeviceWindowsGL3 : INativeGraphicsDevice
	{
		#region Constants
		private const float ColorMultiplier = 1f / 255f;
		#endregion

		#region Private
		private GraphicsContext nativeContext;
		private IWindowInfo nativeWindowInfo;
		private GraphicsMode graphicsMode;

		private int cachedVersionMinor = -1;
		private int cachedVersionMajor = -1;

		internal static VertexBufferGL3[] boundVertexBuffers;
		private static RenderTarget2DGL3[] boundRenderTargets;
		internal static IndexBufferGL3 boundIndexBuffer;
		internal static EffectGL3 activeEffect;

		internal static GraphicsDeviceWindowsGL3 Current
		{
			get;
			private set;
		}

		internal static bool IsContextCurrent
		{
			get
			{
				return (Current == null || Current.nativeContext == null) ? false : Current.nativeContext.IsCurrent;
			}
		}
		#endregion

		#region Public
		#region VSync
		public bool VSync
		{
			get
			{
				return nativeContext.VSync;
			}
			set
			{
				nativeContext.VSync = value;
			}
		}
		#endregion
		#endregion

		#region Constructor
		internal GraphicsDeviceWindowsGL3(PresentationParameters presentationParameters)
		{
			Current = this;
			ResetDevice(presentationParameters);
		}
		#endregion

		#region ResetDevice
		private void ResetDevice(PresentationParameters presentationParameters)
		{
			if (nativeContext != null)
				Dispose();

			boundVertexBuffers = new VertexBufferGL3[0];
			boundRenderTargets = new RenderTarget2DGL3[0];
			boundIndexBuffer = null;
			activeEffect = null;

			int depth = 0;
			int stencil = 0;
			switch (presentationParameters.DepthStencilFormat)
			{
				case DepthFormat.None:
					break;

				case DepthFormat.Depth16:
					depth = 16;
					break;

				case DepthFormat.Depth24:
					depth = 24;
					break;

				case DepthFormat.Depth24Stencil8:
					depth = 24;
					stencil = 8;
					break;
			}

			ResizeRenderWindow(presentationParameters);

			var colorFormat = DatatypesMapping.SurfaceToColorFormat(presentationParameters.BackBufferFormat);
			graphicsMode = new GraphicsMode(colorFormat, depth, stencil, presentationParameters.MultiSampleCount);

			CreateWindowInfo(presentationParameters.DeviceWindowHandle, graphicsMode.Index.Value);
			
			nativeContext = new GraphicsContext(graphicsMode, nativeWindowInfo);
			nativeContext.MakeCurrent(nativeWindowInfo);
			nativeContext.LoadAll();

			LogOpenGLInformation();

			GL.Viewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight);

			GraphicsResourceManager.RecreateAllResources();
		}
		#endregion

		#region LogOpenGLInformation
		private void LogOpenGLInformation()
		{
			string version = GL.GetString(StringName.Version);
			string vendor = GL.GetString(StringName.Vendor);
			string renderer = GL.GetString(StringName.Renderer);
			string shadingLanguageVersion = GL.GetString(StringName.ShadingLanguageVersion);
			Logger.Info("OpenGL version: " + version + " (" + vendor + " - " + renderer + ")");
			Logger.Info("GLSL version: " + shadingLanguageVersion);
			string[] parts = version.Split(new char[] { '.', ' ' });
			cachedVersionMajor = int.Parse(parts[0]);
			cachedVersionMinor = int.Parse(parts[1]);
		}
		#endregion

		#region CreateWindowInfo
		private void CreateWindowInfo(IntPtr windowHandle, IntPtr graphicsModeHandle)
		{
			if (OpenTK.Configuration.RunningOnWindows)
				nativeWindowInfo = Utilities.CreateWindowsWindowInfo(windowHandle);
			else if (OpenTK.Configuration.RunningOnX11)
				nativeWindowInfo = LinuxInterop.CreateX11WindowInfo(windowHandle, graphicsModeHandle);
			else if (OpenTK.Configuration.RunningOnMacOS)
				nativeWindowInfo = Utilities.CreateMacOSCarbonWindowInfo(windowHandle, false, true);
			else
				throw new NotImplementedException();

		}
		#endregion

		#region SetViewport
		public void SetViewport(Viewport viewport)
		{
			GL.Viewport(viewport.X, viewport.Y, viewport.Width, viewport.Height);
			ErrorHelper.Check("SetViewport");
		}
		#endregion

		#region Clear
		private uint? lastClearColor;
		/// <summary>
		/// Clear the current screen by the specified clear color.
		/// </summary>
		/// <param name="color">Clear color.</param>
		public void Clear(ref Color color)
		{
			uint newClearColor = color.PackedValue;
			if (lastClearColor.HasValue == false ||
				lastClearColor != newClearColor)
			{
				lastClearColor = newClearColor;
				GL.ClearColor(color.R * ColorMultiplier, color.G * ColorMultiplier,
						color.B * ColorMultiplier, color.A * ColorMultiplier);
				ErrorHelper.Check("ClearColor");
			}
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			ErrorHelper.Check("Clear");
		}

		/// <summary>
		/// Clear the current screen by the specified clear color and options.
		/// </summary>
		/// <param name="options">Clear options defining which components
		/// should be cleared.</param>
		/// <param name="color">Clear color.</param>
		/// <param name="depth">Depth value.</param>
		/// <param name="stencil">Stencil value.</param>
		public void Clear(ClearOptions options, Vector4 color, float depth,
				int stencil)
		{
			Color anxColor;
			DatatypesMapping.Convert(ref color, out anxColor);
			uint newClearColor = anxColor.PackedValue;
			if (lastClearColor != newClearColor)
			{
				lastClearColor = newClearColor;
				GL.ClearColor(anxColor.R * ColorMultiplier, anxColor.G * ColorMultiplier,
						anxColor.B * ColorMultiplier, anxColor.A * ColorMultiplier);
				ErrorHelper.Check("ClearColor");
			}

			ClearBufferMask mask = (ClearBufferMask)0;
			if ((options | ClearOptions.Target) == options)
			{
				mask |= ClearBufferMask.ColorBufferBit;
			}
			if ((options | ClearOptions.Stencil) == options)
			{
				mask |= ClearBufferMask.StencilBufferBit;
			}
			if ((options | ClearOptions.DepthBuffer) == options)
			{
				mask |= ClearBufferMask.DepthBufferBit;
			}

			GL.ClearDepth(depth);
			ErrorHelper.Check("ClearDepth");
			GL.ClearStencil(stencil);
			ErrorHelper.Check("ClearStencil");
			GL.Clear(mask);
			ErrorHelper.Check("Clear");
		}
		#endregion

		#region Present
		/// <summary>
		/// Swap the graphics buffers.
		/// </summary>
		public void Present()
		{
			if (nativeContext != null)
			{
				nativeContext.SwapBuffers();
			}
		}
		#endregion

		#region DrawIndexedPrimitives
		public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices,
			int startIndex, int primitiveCount)
		{
			// TODO: baseVertex, minVertexIndex, numVertices, startIndex
			DrawElementsType elementsType = boundIndexBuffer.elementSize == IndexElementSize.SixteenBits ?
				DrawElementsType.UnsignedShort :
				DrawElementsType.UnsignedInt;

			int count;
			BeginMode mode = DatatypesMapping.PrimitiveTypeToBeginMode(primitiveType, primitiveCount, out count);

			GL.DrawElements(mode, count, elementsType, 0);
			ErrorHelper.Check("DrawElements");
		}
		#endregion

		#region DrawInstancedPrimitives (TODO)
		public void DrawInstancedPrimitives(PrimitiveType primitiveType,
				int baseVertex, int minVertexIndex, int numVertices, int startIndex,
				int primitiveCount, int instanceCount)
		{
			//GL.DrawArraysInstanced(
			//  DatatypesMapping.PrimitiveTypeToBeginMode(primitiveType),
			//  baseVertex, numVertices, instanceCount);
			throw new NotImplementedException();
		}
		#endregion

		#region DrawUserIndexedPrimitives (TODO)
		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType,
			T[] vertexData, int vertexOffset, int numVertices, Array indexData,
			int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
			IndexElementSize indexFormat) where T : struct, IVertexType
		{
			//BeginMode mode = DatatypesMapping.PrimitiveTypeToBeginMode(primitiveType);

			//if (indexFormat == IndexElementSize.SixteenBits)
			//{
			//  ushort[] indices = new ushort[indexData.Length];
			//  indexData.CopyTo(indices, 0);
			//  GL.DrawElements(mode, numVertices, DrawElementsType.UnsignedShort,
			//    indices);
			//}
			//else
			//{
			//  uint[] indices = new uint[indexData.Length];
			//  indexData.CopyTo(indices, 0);
			//  GL.DrawElements(mode, numVertices, DrawElementsType.UnsignedInt,
			//    indices);
			//}

			throw new NotImplementedException();
		}
		#endregion

		#region DrawUserPrimitives (TODO)
		public void DrawUserPrimitives<T>(PrimitiveType primitiveType,
				T[] vertexData, int vertexOffset, int primitiveCount,
				VertexDeclaration vertexDeclaration) where T : struct, IVertexType
		{
			throw new NotImplementedException();
		}
		#endregion

		#region DrawPrimitives (TODO: check)
		public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset,
				int primitiveCount)
		{
			int count;
			BeginMode mode = DatatypesMapping.PrimitiveTypeToBeginMode(primitiveType,
				primitiveCount, out count);
			GL.DrawArrays(mode, vertexOffset, count);
			ErrorHelper.Check("DrawArrays");
		}
		#endregion

		#region SetConstantBuffer (TODO)
#if XNAEXT
		public void SetConstantBuffer(int slot, ConstantBuffer constantBuffer)
		{
			if (constantBuffer == null)
				throw new ArgumentNullException("constantBuffer");

			throw new NotImplementedException();
		}
#endif
		#endregion

		#region SetVertexBuffers
		public void SetVertexBuffers(VertexBufferBinding[] vertexBuffers)
		{
			boundVertexBuffers = new VertexBufferGL3[vertexBuffers.Length];
			for (int index = 0; index < vertexBuffers.Length; index++)
			{
				var nativeBuffer = (VertexBufferGL3)vertexBuffers[index].VertexBuffer.NativeVertexBuffer;
				boundVertexBuffers[index] = nativeBuffer;
				nativeBuffer.Bind(activeEffect);
			}
		}
		#endregion

		#region SetIndexBuffer
		public void SetIndexBuffer(IndexBuffer indexBuffer)
		{
			boundIndexBuffer = (IndexBufferGL3)indexBuffer.NativeIndexBuffer;
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, boundIndexBuffer.BufferHandle);
			ErrorHelper.Check("BindBuffer");
		}
		#endregion

		#region ResizeRenderWindow
		private void ResizeRenderWindow(PresentationParameters presentationParameters)
		{
			if (OpenTK.Configuration.RunningOnWindows)
			{
				WindowsInterop.ResizeWindow(presentationParameters.DeviceWindowHandle,
					presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight);
			}
			else
			{
				LinuxInterop.ResizeWindow(presentationParameters.DeviceWindowHandle,
					presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight);
			}
		}
		#endregion

		#region SetRenderTargets
		public void SetRenderTargets(params RenderTargetBinding[] renderTargets)
		{
			if (renderTargets == null)
			{
				if (boundRenderTargets.Length > 0)
				{
					for (int index = 0; index < boundRenderTargets.Length; index++)
					{
						boundRenderTargets[index].Unbind();
					}
					boundRenderTargets = new RenderTarget2DGL3[0];
				}
			}
			else
			{
				boundRenderTargets = new RenderTarget2DGL3[renderTargets.Length];
				for (int index = 0; index < renderTargets.Length; index++)
				{
					RenderTarget2D renderTarget =
						renderTargets[index].RenderTarget as RenderTarget2D;
					RenderTarget2DGL3 nativeRenderTarget =
						renderTarget.NativeRenderTarget as RenderTarget2DGL3;
					boundRenderTargets[index] = nativeRenderTarget;

					nativeRenderTarget.Bind();
				}
			}
		}
		#endregion

		#region GetBackBufferData (TODO)
		public void GetBackBufferData<T>(Rectangle? rect, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetBackBufferData<T>(T[] data) where T : struct
		{
			//glReadPixels(0, 0, nWidth, nHeight, GL_RGB, GL_UNSIGNED_BYTE, m_pPixelData)
			throw new NotImplementedException();
		}

		public void GetBackBufferData<T>(T[] data, int startIndex,
			int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region ResizeBuffers
		public void ResizeBuffers(PresentationParameters presentationParameters)
		{
			ResizeRenderWindow(presentationParameters);

			GL.Viewport(0, 0, presentationParameters.BackBufferWidth,
				presentationParameters.BackBufferHeight);

			ResetDevice(presentationParameters);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			GraphicsResourceManager.DisposeAllResources();

			lastClearColor = new uint?();
			boundVertexBuffers = null;
			boundIndexBuffer = null;
			activeEffect = null;
			boundRenderTargets = null;

			if (nativeWindowInfo != null)
			{
				nativeWindowInfo.Dispose();
				nativeWindowInfo = null;
			}
			if (nativeContext != null)
			{
				nativeContext.Dispose();
				nativeContext = null;
			}
		}
		#endregion
	}
}
