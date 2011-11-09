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
    public struct NormalizedByte4 : IPackedVector<uint>, IEquatable<NormalizedByte4>, IPackedVector
    {
        private uint packedValue;

        private const float max = (float)(255 >> 1);
        private const float oneOverMax = 1f / max;
        private const uint mask = (uint)(256 >> 1);

        public NormalizedByte4(float x, float y, float z, float w)
        {
            uint b1 = (uint)(((int)MathHelper.Clamp(x * max, -max, max) & 255) << 0);
            uint b2 = (uint)(((int)MathHelper.Clamp(y * max, -max, max) & 255) << 8);
            uint b3 = (uint)(((int)MathHelper.Clamp(z * max, -max, max) & 255) << 16);
            uint b4 = (uint)(((int)MathHelper.Clamp(w * max, -max, max) & 255) << 24);

            this.packedValue = (uint)(b1 | b2 | b3 | b4);
        }

        public NormalizedByte4(Vector4 vector)
        {
            uint b1 = (uint)(((int)MathHelper.Clamp(vector.X * max, -max, max) & 255) << 0);
            uint b2 = (uint)(((int)MathHelper.Clamp(vector.Y * max, -max, max) & 255) << 8);
            uint b3 = (uint)(((int)MathHelper.Clamp(vector.Z * max, -max, max) & 255) << 16);
            uint b4 = (uint)(((int)MathHelper.Clamp(vector.W * max, -max, max) & 255) << 24);

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

        public Vector4 ToVector4()
        {
            Vector4 vector;
            vector.X = convert(0xff, (uint)this.packedValue);
            vector.Y = convert(0xff, (uint)(this.packedValue >> 8));
            vector.Z = convert(0xff, (uint)(this.packedValue >> 16));
            vector.W = convert(0xff, (uint)(this.packedValue >> 24));
            return vector;
        }

        private static float convert(uint bitmask, uint value)
        {
            if ((value & mask) != 0)
            {
                if ((value & 255) >= mask)
                {
                    return -1f;
                }
                value |= ~bitmask;
            }
            else
            {
                value &= 255;
            }

            return (((float)value) * oneOverMax);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            uint b1 = (uint)(((int)MathHelper.Clamp(vector.X * max, -max, max) & 255) << 0);
            uint b2 = (uint)(((int)MathHelper.Clamp(vector.Y * max, -max, max) & 255) << 8);
            uint b3 = (uint)(((int)MathHelper.Clamp(vector.Z * max, -max, max) & 255) << 16);
            uint b4 = (uint)(((int)MathHelper.Clamp(vector.W * max, -max, max) & 255) << 24);

            this.packedValue = (uint)(b1 | b2 | b3 | b4);
        }

        Vector4 IPackedVector.ToVector4()
        {
            return this.ToVector4();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                return this == (NormalizedByte4)obj;
            }

            return false;
        }

        public bool Equals(NormalizedByte4 other)
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

        public static bool operator ==(NormalizedByte4 lhs, NormalizedByte4 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(NormalizedByte4 lhs, NormalizedByte4 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
