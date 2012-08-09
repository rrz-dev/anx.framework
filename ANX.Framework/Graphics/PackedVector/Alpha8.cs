#region Using Statements
using System;
using System.Globalization;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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
