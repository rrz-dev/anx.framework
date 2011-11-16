#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Drawing;

#endregion // Using Statements

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

namespace ANX.Framework.Windows.DX10
{
    internal sealed class NativeMethods
    {
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        // Nested Types
        [StructLayout(LayoutKind.Sequential)]
        public struct Message
        {
            public IntPtr hWnd;
            public NativeMethods.WindowMessage msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public Point p;
        }

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
