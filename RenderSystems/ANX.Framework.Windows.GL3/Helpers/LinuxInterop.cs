using System;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenTK.Platform;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.GL3.Helpers
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
