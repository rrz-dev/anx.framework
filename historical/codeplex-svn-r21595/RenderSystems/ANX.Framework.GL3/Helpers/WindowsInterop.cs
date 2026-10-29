using System;
using System.Runtime.InteropServices;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.GL3.Helpers
{
	internal static class WindowsInterop
	{
		#region RECT (Helper struct)
		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			/// <summary>
			/// X position of upper-left corner.
			/// </summary>
			public int Left;
			/// <summary>
			/// Y position of upper-left corner.
			/// </summary>
			public int Top;
			/// <summary>
			/// X position of lower-right corner.
			/// </summary>
			public int Right;
			/// <summary>
			/// Y position of lower-right corner.
			/// </summary>
			public int Bottom;
		}
		#endregion

		#region Invokes
		[DllImport("user32.dll")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
			int x, int y, int width, int height, uint uFlags);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
		#endregion

		#region ResizeWindow
		public static void ResizeWindow(IntPtr windowHandle, int backBufferWidth,
			int backBufferHeight)
		{
			RECT windowRect;
			RECT clientRect;
			if (GetWindowRect(windowHandle, out windowRect) &&
				GetClientRect(windowHandle, out clientRect))
			{
				int width = backBufferWidth + (windowRect.Right - windowRect.Left) -
					clientRect.Right;
				int height = backBufferHeight + (windowRect.Bottom - windowRect.Top) -
					clientRect.Bottom;

				SetWindowPos(windowHandle, IntPtr.Zero, windowRect.Left, windowRect.Top,
					width, height, 0);
			}
		}
		#endregion
	}
}
