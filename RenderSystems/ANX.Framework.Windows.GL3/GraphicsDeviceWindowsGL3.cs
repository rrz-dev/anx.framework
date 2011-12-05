using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using System.Runtime.InteropServices;

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

        #region Interop
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner 
            public int Top;         // y position of upper-left corner 
            public int Right;       // x position of lower-right corner 
            public int Bottom;      // y position of lower-right corner 
        }

        [DllImport("libX11")]
        static extern IntPtr XCreateColormap(IntPtr display, IntPtr window, IntPtr visual, int alloc);

        [DllImport("libX11", EntryPoint = "XGetVisualInfo")]
        static extern IntPtr XGetVisualInfoInternal(IntPtr display, IntPtr vinfo_mask, ref XVisualInfo template, out int nitems);

        static IntPtr XGetVisualInfo(IntPtr display, int vinfo_mask, ref XVisualInfo template, out int nitems)
        {
            return XGetVisualInfoInternal(display, (IntPtr)vinfo_mask, ref template, out nitems);
        }

        [DllImport("libX11")]
        extern static int XPending(IntPtr diplay);

        [StructLayout(LayoutKind.Sequential)]
        struct XVisualInfo
        {
            public IntPtr Visual;
            public IntPtr VisualID;
            public int Screen;
            public int Depth;
            public int Class;
            public long RedMask;
            public long GreenMask;
            public long blueMask;
            public int ColormapSize;
            public int BitsPerRgb;

            public override string ToString()
            {
                return String.Format("id ({0}), screen ({1}), depth ({2}), class ({3})",
                    VisualID, Screen, Depth, Class);
            }
        }

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

		internal static VertexBufferGL3[] boundVertexBuffers =
			new VertexBufferGL3[0];
		internal static IndexBufferGL3 boundIndexBuffer;
		internal static EffectGL3 activeEffect;
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

            GraphicsMode graphicsMode = new GraphicsMode(DatatypesMapping.SurfaceToColorFormat(presentationParameters.BackBufferFormat),
                                                         depth, 
                                                         stencil,
                                                         presentationParameters.MultiSampleCount // AntiAlias Samples: 2/4/8/16/32
                                                        );

            if (OpenTK.Configuration.RunningOnWindows)
            {
                nativeWindowInfo = Utilities.CreateWindowsWindowInfo(presentationParameters.DeviceWindowHandle);
            }
            else if (OpenTK.Configuration.RunningOnX11)
            {
                // Use reflection to retrieve the necessary values from Mono's Windows.Forms implementation.
                Type xplatui = Type.GetType("System.Windows.Forms.XplatUIX11, System.Windows.Forms");
                if (xplatui == null) throw new PlatformNotSupportedException(
                        "System.Windows.Forms.XplatUIX11 missing. Unsupported platform or Mono runtime version, aborting.");

                // get the required handles from the X11 API.
                IntPtr display = (IntPtr)GetStaticFieldValue(xplatui, "DisplayHandle");
                IntPtr rootWindow = (IntPtr)GetStaticFieldValue(xplatui, "RootWindow");
                int screen = (int)GetStaticFieldValue(xplatui, "ScreenNo");

                // get the XVisualInfo for this GraphicsMode
                XVisualInfo info = new XVisualInfo();
                info.VisualID = graphicsMode.Index.Value;
                int dummy;
                IntPtr infoPtr = XGetVisualInfo(display, 1 /* VisualInfoMask.ID */, ref info, out dummy);
                info = (XVisualInfo)Marshal.PtrToStructure(infoPtr, typeof(XVisualInfo));

                // set the X11 colormap.
                SetStaticFieldValue(xplatui, "CustomVisual", info.Visual);
                SetStaticFieldValue(xplatui, "CustomColormap", XCreateColormap(display, rootWindow, info.Visual, 0));

                nativeWindowInfo = Utilities.CreateX11WindowInfo(display, screen, presentationParameters.DeviceWindowHandle, rootWindow, infoPtr);
            }
            else
            {
                throw new NotImplementedException();
            }

            ResizeRenderWindow(presentationParameters);

			nativeContext = new GraphicsContext(graphicsMode, nativeWindowInfo);
			nativeContext.MakeCurrent(nativeWindowInfo);
			nativeContext.LoadAll();

			//string version = GL.GetString(StringName.Version);
			//nativeContext.Dispose();
			//nativeContext = null;
			//string[] parts = version.Split('.');
			//nativeContext = new GraphicsContext(graphicsMode, nativeWindowInfo,
			//  int.Parse(parts[0]), int.Parse(parts[1]), GraphicsContextFlags.Default);
			//nativeContext.MakeCurrent(nativeWindowInfo);
			//nativeContext.LoadAll();
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
			ErrorHelper.Check("SetViewport");
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
			if (WindowsGameWindow.Form != null &&
				WindowsGameWindow.Form.IsDisposed == false)
			{
				nativeContext.SwapBuffers();
			}
		}
		#endregion

		#region DrawIndexedPrimitives
		public void DrawIndexedPrimitives(PrimitiveType primitiveType,
				int baseVertex, int minVertexIndex, int numVertices, int startIndex,
				int primitiveCount)
		{
			// TODO: baseVertex, minVertexIndex, numVertices, startIndex, primitiveCount
			DrawElementsType elementsType =
				boundIndexBuffer.elementSize == IndexElementSize.SixteenBits ?
				DrawElementsType.UnsignedShort :
				DrawElementsType.UnsignedInt;

			int count;
			BeginMode mode = DatatypesMapping.PrimitiveTypeToBeginMode(primitiveType,
				primitiveCount, out count);

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

		#region SetVertexBuffers
		public void SetVertexBuffers(VertexBufferBinding[] vertexBuffers)
		{
			boundVertexBuffers = new VertexBufferGL3[vertexBuffers.Length];
			for (int index = 0; index < vertexBuffers.Length; index++)
			{
				boundVertexBuffers[index] =
					(VertexBufferGL3)vertexBuffers[index].VertexBuffer.NativeVertexBuffer;
				GL.BindBuffer(BufferTarget.ArrayBuffer,
					boundVertexBuffers[index].BufferHandle);
				ErrorHelper.Check("BindBuffer");
				boundVertexBuffers[index].MapVertexDeclaration(
					activeEffect.programHandle);
			}
		}
		#endregion

		#region SetIndexBuffer
		public void SetIndexBuffer(IndexBuffer indexBuffer)
		{
			IndexBufferGL3 nativeBuffer =
				(IndexBufferGL3)indexBuffer.NativeIndexBuffer;

			boundIndexBuffer = nativeBuffer;
			GL.BindBuffer(BufferTarget.ElementArrayBuffer,
				nativeBuffer.BufferHandle);
			ErrorHelper.Check("BindBuffer");
		}
		#endregion

        private void ResizeRenderWindow(PresentationParameters presentationParameters)
        {
            if (OpenTK.Configuration.RunningOnWindows)
            {
                RECT windowRect;
                RECT clientRect;
                if (GetWindowRect(presentationParameters.DeviceWindowHandle, out windowRect) &&
                    GetClientRect(presentationParameters.DeviceWindowHandle, out clientRect))
                {
                    int width = presentationParameters.BackBufferWidth + ((windowRect.Right - windowRect.Left) - clientRect.Right);
                    int height = presentationParameters.BackBufferHeight + ((windowRect.Bottom - windowRect.Top) - clientRect.Bottom);

                    SetWindowPos(presentationParameters.DeviceWindowHandle, IntPtr.Zero, windowRect.Left, windowRect.Top, width, height, 0);
                }
            }
        }

        static object GetStaticFieldValue(Type type, string fieldName)
        {
            return type.GetField(fieldName,
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(null);
        }

        static void SetStaticFieldValue(Type type, string fieldName, object value)
        {
            type.GetField(fieldName,
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).SetValue(null, value);
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
            ResizeRenderWindow(presentationParameters);

			throw new NotImplementedException();
		}

        public void Dispose()
        {
            //TODO: implement
        }

    }
}
