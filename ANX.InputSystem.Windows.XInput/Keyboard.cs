#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.InputSystem;
using ANX.Framework.NonXNA;
using SharpDX.DirectInput;
using DXKeyboard=SharpDX.DirectInput.Keyboard;
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

namespace ANX.InputSystem.Windows.XInput
{
    public class Keyboard : IKeyboard
    {
        #region Private Members
        private DirectInput directInput;
        private DXKeyboard nativeKeyboard;
        private KeyboardState nativeState;

        #endregion // Private Members

        public IntPtr WindowHandle
        {
            get;
            set;
        }

        public Keyboard()
        {
            this.nativeState = new KeyboardState();
        }

        public Framework.Input.KeyboardState GetState()
        {
            return GetState(Framework.PlayerIndex.One);
        }

        public Framework.Input.KeyboardState GetState(Framework.PlayerIndex playerIndex)
        {
            if (this.nativeKeyboard == null && this.WindowHandle != null && this.WindowHandle != IntPtr.Zero)
            {
                this.directInput = new DirectInput();
                this.nativeKeyboard = new DXKeyboard(this.directInput);
                this.nativeKeyboard.SetCooperativeLevel(this.WindowHandle, CooperativeLevel.NonExclusive | CooperativeLevel.Background);
                this.nativeKeyboard.Acquire();
            }

            if (this.nativeKeyboard != null)
            {
                nativeKeyboard.GetCurrentState(ref this.nativeState);
                if (this.nativeState.PressedKeys.Count > 0)
                {
                    return FormatConverter.Translate(this.nativeState);
                }
            }

            return new Framework.Input.KeyboardState();
        }

        public void Dispose()
        {
            if (this.nativeKeyboard != null)
            {
                this.nativeKeyboard.Unacquire();
                this.nativeKeyboard.Dispose();
                this.nativeKeyboard = null;
            }

            if (this.directInput != null)
            {
                this.directInput.Dispose();
                this.directInput = null;
            }
        }
    }
}
