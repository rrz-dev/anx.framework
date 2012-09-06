using System;
using System.Runtime.InteropServices;
using ANX.Framework.Graphics;

namespace ANX.RenderSystem.Windows.DX10.Helpers
{
	internal static class WindowHelper
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;        // x position of upper-left corner 
			public int Top;         // y position of upper-left corner 
			public int Right;       // x position of lower-right corner 
			public int Bottom;      // y position of lower-right corner 
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
