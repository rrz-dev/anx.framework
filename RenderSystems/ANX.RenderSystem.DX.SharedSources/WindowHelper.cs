using System;
using System.Runtime.InteropServices;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if DX10
namespace ANX.RenderSystem.Windows.DX10
#endif
#if DX11
namespace ANX.RenderSystem.Windows.DX11
#endif
{
	public static class WindowHelper
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		[DllImport("user32.dll")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height,
			uint uFlags);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

		#region ResizeRenderWindow
		public static void ResizeRenderWindow(PresentationParameters presentationParameters)
		{
			RECT windowRect;
			RECT clientRect;
			if (GetWindowRect(presentationParameters.DeviceWindowHandle, out windowRect) &&
				GetClientRect(presentationParameters.DeviceWindowHandle, out clientRect))
			{
				int width = presentationParameters.BackBufferWidth + ((windowRect.Right - windowRect.Left) - clientRect.Right);
				int height = presentationParameters.BackBufferHeight + ((windowRect.Bottom - windowRect.Top) - clientRect.Bottom);

				SetWindowPos(presentationParameters.DeviceWindowHandle, IntPtr.Zero, windowRect.Left, windowRect.Top, width,
					height, 0);
			}
		}
		#endregion
	}
}
