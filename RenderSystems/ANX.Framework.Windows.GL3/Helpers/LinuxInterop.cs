using System;
using System.Reflection;
using System.Runtime.InteropServices;
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

namespace ANX.Framework.Windows.GL3.Helpers
{
	internal static class LinuxInterop
	{
		#region XVisualInfo (Helper struct)
		[StructLayout(LayoutKind.Sequential)]
		private struct XVisualInfo
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

		#region Invokes
		[DllImport("libX11")]
		private static extern IntPtr XCreateColormap(IntPtr display, IntPtr window,
			IntPtr visual, int alloc);

		[DllImport("libX11", EntryPoint = "XGetVisualInfo")]
		private static extern IntPtr XGetVisualInfoInternal(IntPtr display,
			IntPtr vinfo_mask, ref XVisualInfo template, out int nitems);

		[DllImport("libX11")]
		private static extern int XPending(IntPtr diplay);

		[DllImport("libX11")]
		private static extern void XResizeWindow(IntPtr display, IntPtr window,
			int width, int height);
		#endregion

		#region GetStaticFieldValue
		private static object GetStaticFieldValue(Type type, string fieldName)
		{
			return type.GetField(fieldName,
				BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
		}
		#endregion

		#region SetStaticFieldValue
		private static void SetStaticFieldValue(Type type, string fieldName,
			object value)
		{
			type.GetField(fieldName,
				BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, value);
		}
		#endregion

		#region CreateX11WindowInfo
		public static IWindowInfo CreateX11WindowInfo(IntPtr windowHandle,
			IntPtr graphicsModeHandle)
		{
			// Use reflection to retrieve the necessary values from Mono's
			// Windows.Forms implementation.
			Type xplatui = Type.GetType(
				"System.Windows.Forms.XplatUIX11, System.Windows.Forms");
			if (xplatui == null)
			{
				throw new PlatformNotSupportedException(
					"System.Windows.Forms.XplatUIX11 missing. Unsupported platform " +
					"or Mono runtime version, aborting.");
			}

			// get the required handles from the X11 API.
			IntPtr display = (IntPtr)GetStaticFieldValue(xplatui, "DisplayHandle");
			IntPtr rootWindow = (IntPtr)GetStaticFieldValue(xplatui, "RootWindow");
			int screen = (int)GetStaticFieldValue(xplatui, "ScreenNo");

			// get the XVisualInfo for this GraphicsMode
			XVisualInfo info = new XVisualInfo()
			{
				VisualID = graphicsModeHandle,
			};
			int dummy;
			IntPtr infoPtr = XGetVisualInfoInternal(display,
				(IntPtr)1 /* VisualInfoMask.ID */, ref info, out dummy);
			info = (XVisualInfo)Marshal.PtrToStructure(infoPtr, typeof(XVisualInfo));

			// set the X11 colormap.
			SetStaticFieldValue(xplatui, "CustomVisual", info.Visual);
			SetStaticFieldValue(xplatui, "CustomColormap", XCreateColormap(display, rootWindow, info.Visual, 0));

			return Utilities.CreateX11WindowInfo(display, screen, windowHandle,
				rootWindow, infoPtr);
		}
		#endregion

		#region ResizeWindow
		public static void ResizeWindow(IntPtr windowHandle, int backBufferWidth,
			int backBufferHeight)
		{
			XResizeWindow(IntPtr.Zero, windowHandle, backBufferWidth, backBufferHeight);
		}
		#endregion
	}
}
