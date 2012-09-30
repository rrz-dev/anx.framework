#region Using Statements
using System;
using System.Globalization;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics.PackedVector
{
    [PercentageComplete(100)]
    [Developer("???")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public struct Alpha8 : IPackedVector<byte>, IEquatable<Alpha8>, IPackedVector
    {
        private byte packedValue;

        public byte PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

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
            float value = packedValue & 255;
            return value / 255f;
        }

        Vector4 IPackedVector.ToVector4()
        {
            return new Vector4(0f, 0f, 0f, ToAlpha());
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
            return obj is Alpha8 && this == (Alpha8)obj;
        }

        public bool Equals(Alpha8 other)
        {
            return this.packedValue == other.packedValue;
        }

        public static bool operator ==(Alpha8 lhs, Alpha8 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(Alpha8 lhs, Alpha8 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
