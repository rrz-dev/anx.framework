using System;
using System.Runtime.InteropServices;
using System.Security;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    internal sealed class NativeMethods
    {
			// Not needed anymore, is in platforms now!
				//[SuppressUnmanagedCodeSecurity, DllImport("user32.dll", CharSet = CharSet.Auto)]
				//internal static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

				//// Nested Types
				//[StructLayout(LayoutKind.Sequential)]
				//public struct Message
				//{
				//    public IntPtr hWnd;
				//    public NativeMethods.WindowMessage msg;
				//    public IntPtr wParam;
				//    public IntPtr lParam;
				//    public uint time;
				//    public Point p;
				//}

        internal enum WindowMessage : uint
        {
            ActivateApplication = 0x1c,
            Character = 0x102,
            Close = 0x10,
            Destroy = 2,
            EnterMenuLoop = 0x211,
            EnterSizeMove = 0x231,
            ExitMenuLoop = 530,
            ExitSizeMove = 0x232,
            GetMinMax = 0x24,
            KeyDown = 0x100,
            KeyUp = 0x101,
            LeftButtonDoubleClick = 0x203,
            LeftButtonDown = 0x201,
            LeftButtonUp = 0x202,
            MiddleButtonDoubleClick = 0x209,
            MiddleButtonDown = 0x207,
            MiddleButtonUp = 520,
            MouseFirst = 0x201,
            MouseLast = 0x20d,
            MouseMove = 0x200,
            MouseWheel = 0x20a,
            NonClientHitTest = 0x84,
            Paint = 15,
            PowerBroadcast = 0x218,
            Quit = 0x12,
            RightButtonDoubleClick = 0x206,
            RightButtonDown = 0x204,
            RightButtonUp = 0x205,
            SetCursor = 0x20,
            Size = 5,
            SystemCharacter = 0x106,
            SystemCommand = 0x112,
            SystemKeyDown = 260,
            SystemKeyUp = 0x105,
            XButtonDoubleClick = 0x20d,
            XButtonDown = 0x20b,
            XButtonUp = 0x20c
        }

    }
}
