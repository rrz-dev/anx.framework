#region Using Statements
using System;

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

namespace ANX.Framework.Input.Touch
{
    public struct TouchLocation : IEquatable<TouchLocation>
    {
        #region Private members
        private int id;
        private TouchLocationState prevState;
        private Vector2 prevPos;
        private TouchLocationState state;
        private Vector2 pos;

        #endregion // Private members

        public TouchLocation(int id, TouchLocationState state, Vector2 position)
        {
            this.id = id;
            this.state = state;
            this.pos = position;
            this.prevState = TouchLocationState.Invalid;
            this.prevPos = Vector2.Zero;
        }

        public TouchLocation(int id, TouchLocationState state, Vector2 position, TouchLocationState previousState, Vector2 previousPosition)
        {
            this.id = id;
            this.state = state;
            this.pos = position;
            this.prevState = previousState;
            this.prevPos = previousPosition;
        }

        public bool TryGetPreviousLocation(out TouchLocation previousLocation)
        {
            if (this.prevState == TouchLocationState.Invalid)
            {
                previousLocation.id = -1;
                previousLocation.state = TouchLocationState.Invalid;
                previousLocation.pos = Vector2.Zero;
                previousLocation.prevState = TouchLocationState.Invalid;
                previousLocation.prevPos = Vector2.Zero;
            
                return false;
            }

            previousLocation.id = this.id;
            previousLocation.state = this.prevState;
            previousLocation.pos = this.pos;
            previousLocation.prevState = TouchLocationState.Invalid;
            previousLocation.prevPos = this.prevPos;
            
            return true;
        }

        public override string ToString()
        {
            return string.Format("{{Position:{0}}}", this.pos);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode() + this.pos.X.GetHashCode() + this.pos.Y.GetHashCode();
        }

        public override bool Equals(Object other)
        {
            if (other != null && other.GetType() == this.GetType())
            {
                return this == (TouchLocation)other;
            }

            return false;
        }

        public bool Equals(TouchLocation other)
        {
            return this.id == other.id && this.pos == other.pos && this.prevPos == other.prevPos;
        }

        public static bool operator ==(TouchLocation lhs, TouchLocation rhs)
        {
            return lhs.id == rhs.id && lhs.pos == rhs.pos && lhs.prevPos == rhs.prevPos;
        }

        public static bool operator !=(TouchLocation lhs, TouchLocation rhs)
        {
            return lhs.id != rhs.id || lhs.pos != rhs.pos || lhs.prevPos != rhs.prevPos;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.pos;
            }
        }

        public TouchLocationState State
        {
            get
            {
                return this.state;
            }
        }
    }
}
