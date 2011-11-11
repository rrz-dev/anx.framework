using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

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

namespace ANX.Framework.Windows.GL3
{
	/// <summary>
	/// Native OpenGL implementation for a graphics device.
	/// </summary>
	public class GraphicsDeviceWindowsGL3 : INativeGraphicsDevice
	{
		#region Constants
		private const float ColorMultiplier = 1f / 255f;
		#endregion

		#region Private
		/// <summary>
		/// Native graphics context.
		/// </summary>
		private GraphicsContext nativeContext;

		/// <summary>
		/// The OpenTK window info helper class to provide window informations
		/// to the graphics device.
		/// </summary>
		private IWindowInfo nativeWindowInfo;
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new OpenGL graphics context.
		/// </summary>
		/// <param name="presentationParameters">Parameters for the window
		/// and graphics context.</param>
		internal GraphicsDeviceWindowsGL3(
				PresentationParameters presentationParameters)
		{
			ResetDevice(presentationParameters);
		}
		#endregion

		#region ResetDevice
		/// <summary>
		/// Reset the graphics device with the given presentation paramters.
		/// If a device is currently set, then we dispose the old one.
		/// </summary>
		/// <param name="presentationParameters">Parameters for the
		/// graphics device.</param>
		private void ResetDevice(PresentationParameters presentationParameters)
		{
			#region Validation
			if (nativeContext != null)
			{
				nativeContext.Dispose();
				nativeContext = null;
			}

			if (nativeWindowInfo != null)
			{
				nativeWindowInfo.Dispose();
				nativeWindowInfo = null;
			}
			#endregion

			// OpenGL Depth Buffer Size: 0/16/24/32
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

			nativeWindowInfo = Utilities.CreateWindowsWindowInfo(
					presentationParameters.DeviceWindowHandle);

			GraphicsMode graphicsMode = new GraphicsMode(
					DatatypesMapping.SurfaceToColorFormat(
							presentationParameters.BackBufferFormat),
					depth, stencil,
				// AntiAlias Samples: 2/4/8/16/32
					presentationParameters.MultiSampleCount);

			nativeContext = new GraphicsContext(graphicsMode, nativeWindowInfo);
			nativeContext.MakeCurrent(nativeWindowInfo);
			nativeContext.LoadAll();
		}
		#endregion

		#region SetViewport
		/// <summary>
		/// Set the OpenGL viewport.
		/// </summary>
		/// <param name="viewport">Viewport data to set natively.</param>
		public void SetViewport(Viewport viewport)
		{
			GL.Viewport(viewport.X, viewport.Y, viewport.Width, viewport.Height);
		}
		#endregion

		#region Clear
		private uint lastClearColor;
		/// <summary>
		/// Clear the current screen by the specified clear color.
		/// </summary>
		/// <param name="color">Clear color.</param>
		public void Clear(ref Color color)
		{
			uint newClearColor = color.PackedValue;
			if (lastClearColor != newClearColor)
			{
				lastClearColor = newClearColor;
				GL.ClearColor(color.R * ColorMultiplier, color.G * ColorMultiplier,
						color.B * ColorMultiplier, color.A * ColorMultiplier);
			}
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
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
			GL.ClearStencil(stencil);
			GL.Clear(mask);
		}
		#endregion

		#region Present
		/// <summary>
		/// Swap the graphics buffers.
		/// </summary>
		public void Present()
		{
			if (WindowsGameWindow.Form != null &&
				WindowsGameWindow.Form.IsDisposed == false)
			{
				nativeContext.SwapBuffers();
			}
		}
		#endregion

		public void DrawIndexedPrimitives(PrimitiveType primitiveType,
				int baseVertex, int minVertexIndex, int numVertices, int startIndex,
				int primitiveCount)
		{
			throw new NotImplementedException();
		}

		public void DrawInstancedPrimitives(PrimitiveType primitiveType,
				int baseVertex, int minVertexIndex, int numVertices, int startIndex,
				int primitiveCount, int instanceCount)
		{
			throw new NotImplementedException();
		}

		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType,
				T[] vertexData, int vertexOffset, int numVertices, Array indexData,
				int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
				IndexElementSize indexFormat) where T : struct, IVertexType
		{
			throw new NotImplementedException();
		}

		public void DrawUserPrimitives<T>(PrimitiveType primitiveType,
				T[] vertexData, int vertexOffset, int primitiveCount,
				VertexDeclaration vertexDeclaration) where T : struct, IVertexType
		{
			throw new NotImplementedException();
		}

		public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset,
				int primitiveCount)
		{
			throw new NotImplementedException();
		}

		public void SetVertexBuffers(VertexBufferBinding[] vertexBuffers)
		{
			throw new NotImplementedException();
		}

		public void SetIndexBuffer(IndexBuffer indexBuffer)
		{
			throw new NotImplementedException();
		}

		public void SetRenderTargets(params RenderTargetBinding[] renderTargets)
		{
			throw new NotImplementedException();
		}

		public void GetBackBufferData<T>(Rectangle? rect, T[] data,
				int startIndex, int elementCount) where T : struct
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

		public void ResizeBuffers(PresentationParameters presentationParameters)
		{
			throw new NotImplementedException();
		}


        public bool VSync
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
