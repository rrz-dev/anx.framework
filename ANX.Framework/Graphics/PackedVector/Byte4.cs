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

namespace ANX.Framework.Graphics.PackedVector
{
    public struct Byte4 : IPackedVector<uint>, IEquatable<Byte4>, IPackedVector
    {
        private uint packedValue;

        public Byte4(float x, float y, float z, float w)
        {
            uint b1 = (uint)MathHelper.Clamp(x, 0f, 255f) << 0;
            uint b2 = (uint)MathHelper.Clamp(y, 0f, 255f) << 8;
            uint b3 = (uint)MathHelper.Clamp(z, 0f, 255f) << 16;
            uint b4 = (uint)MathHelper.Clamp(w, 0f, 255f) << 24;

            this.packedValue = (uint)(b1 | b2 | b3 | b4);
        }

        public Byte4(Vector4 vector)
        {
            uint b1 = (uint)MathHelper.Clamp(vector.X, 0f, 255f) << 0;
            uint b2 = (uint)MathHelper.Clamp(vector.Y, 0f, 255f) << 8;
            uint b3 = (uint)MathHelper.Clamp(vector.Z, 0f, 255f) << 16;
            uint b4 = (uint)MathHelper.Clamp(vector.W, 0f, 255f) << 24;

            this.packedValue = (uint)(b1 | b2 | b3 | b4);
        }

        public uint PackedValue
        {
            get
            {
                return this.packedValue;
            }
            set
            {
                this.packedValue = value;
            }
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            uint b1 = (uint)MathHelper.Clamp(vector.X, 0f, 255f) << 0;
            uint b2 = (uint)MathHelper.Clamp(vector.Y, 0f, 255f) << 8;
            uint b3 = (uint)MathHelper.Clamp(vector.Z, 0f, 255f) << 16;
            uint b4 = (uint)MathHelper.Clamp(vector.W, 0f, 255f) << 24;

            this.packedValue = (uint)(b1 | b2 | b3 | b4);
        }

        public Vector4 ToVector4()
        {
            return new Vector4((packedValue >>  0) & 255,
                               (packedValue >>  8) & 255,
                               (packedValue >> 16) & 255,
                               (packedValue >> 24) & 255);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                return this == (Byte4)obj;
            }

            return false;
        }

        public bool Equals(Byte4 other)
        {
            return this.packedValue == other.packedValue;
        }

        public override string ToString()
        {
            return this.packedValue.ToString("X8");
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public static bool operator ==(Byte4 lhs, Byte4 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(Byte4 lhs, Byte4 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
