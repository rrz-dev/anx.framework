#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics.PackedVector
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public struct Rgba1010102 : IPackedVector<uint>, IEquatable<Rgba1010102>, IPackedVector
    {
        private uint packedValue;

        [CLSCompliant(false)]
        public uint PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

        public Rgba1010102(float x, float y, float z, float w)
        {
            uint r = (uint)(MathHelper.Clamp(x, 0f, 1f) * 1023f);
            uint g = (uint)(MathHelper.Clamp(y, 0f, 1f) * 1023f) << 10;
            uint b = (uint)(MathHelper.Clamp(z, 0f, 1f) * 1023f) << 20;
            uint a = (uint)(MathHelper.Clamp(w, 0f, 1f) *    3f) << 30;

            this.packedValue = r | g | b | a;
        }

        public Rgba1010102(Vector4 vector)
        {
            uint r = (uint)(MathHelper.Clamp(vector.X, 0f, 1f) * 1023f);
            uint g = (uint)(MathHelper.Clamp(vector.Y, 0f, 1f) * 1023f) << 10;
            uint b = (uint)(MathHelper.Clamp(vector.Z, 0f, 1f) * 1023f) << 20;
            uint a = (uint)(MathHelper.Clamp(vector.W, 0f, 1f) *    3f) << 30;

            this.packedValue = r | g | b | a;
        }

        public Vector4 ToVector4()
        {
            return new Vector4(((packedValue >> 0)  & 1023) / 1023.0f,
                               ((packedValue >> 10) & 1023) / 1023.0f,
                               ((packedValue >> 20) & 1023) / 1023.0f,
                               ((packedValue >> 30) &    3) /    3.0f);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            uint r = (uint)(MathHelper.Clamp(vector.X, 0f, 1f) * 1023f);
            uint g = (uint)(MathHelper.Clamp(vector.Y, 0f, 1f) * 1023f) << 10;
            uint b = (uint)(MathHelper.Clamp(vector.Z, 0f, 1f) * 1023f) << 20;
            uint a = (uint)(MathHelper.Clamp(vector.W, 0f, 1f) * 3f) << 30;

            this.packedValue = r | g | b | a;
        }

        public override bool Equals(object obj)
        {
            return obj is Rgba1010102 && this == (Rgba1010102)obj;
        }

        public bool Equals(Rgba1010102 other)
        {
            return this.packedValue == other.packedValue;
        }

        public override string ToString()
        {
            return this.ToVector4().ToString();
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public static bool operator ==(Rgba1010102 lhs, Rgba1010102 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(Rgba1010102 lhs, Rgba1010102 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
