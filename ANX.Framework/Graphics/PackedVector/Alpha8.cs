#region Using Statements
using System;
using System.Globalization;

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
    public struct Alpha8 : IPackedVector<byte>, IEquatable<Alpha8>, IPackedVector
    {
        #region Private Members
        private byte packedValue;

        #endregion // Private Members

        public Alpha8(float alpha)
        {
            alpha *= 255f;
            alpha = MathHelper.Clamp(alpha, 0f, 255f);
            this.packedValue = (byte)(alpha < 0f ? 0f : (alpha > 255f ? 255f : alpha));
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            float v = vector.W * 255f;
            this.packedValue = (byte)(v < 0f ? 0f : (v > 255f ? 255f : v));
        }

        public float ToAlpha()
        {
            float value = (float)(packedValue & 255);
            return value / 255f;
        }

        Vector4 IPackedVector.ToVector4()
        {
            return new Vector4(0f, 0f, 0f, this.ToAlpha());
        }

        public byte PackedValue
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

        public override string ToString()
        {
            return this.packedValue.ToString("X2", CultureInfo.InvariantCulture);
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ((obj is Alpha8) && this.Equals((Alpha8)obj));
        }

        public bool Equals(Alpha8 other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        public static bool operator ==(Alpha8 a, Alpha8 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Alpha8 a, Alpha8 b)
        {
            return !a.Equals(b);
        }
    }
}
