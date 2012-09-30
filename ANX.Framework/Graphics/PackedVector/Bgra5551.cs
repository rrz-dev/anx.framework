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
    public struct Bgra5551 : IPackedVector<UInt16>, IEquatable<Bgra5551>, IPackedVector
    {
        private UInt16 packedValue;

        public ushort PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

        public Bgra5551(float x, float y, float z, float w)
        {
            uint r = (uint)(MathHelper.Clamp(x, 0f, 1f) * 31.0f) << 10;
            uint g = (uint)(MathHelper.Clamp(y, 0f, 1f) * 31.0f) << 5;
            uint b = (uint)(MathHelper.Clamp(z, 0f, 1f) * 31.0f);
            uint a = (uint)(MathHelper.Clamp(w, 0f, 1f)) << 15;

            this.packedValue = (ushort)(r | g | b | a);
        }

        public Bgra5551(Vector4 vector)
        {
            uint r = (uint)(MathHelper.Clamp(vector.X, 0f, 1f) * 31.0f) << 10;
            uint g = (uint)(MathHelper.Clamp(vector.Y, 0f, 1f) * 31.0f) << 5;
            uint b = (uint)(MathHelper.Clamp(vector.Z, 0f, 1f) * 31.0f);
            uint a = (uint)(MathHelper.Clamp(vector.W, 0f, 1f)) << 15;

            this.packedValue = (ushort)(r | g | b | a);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            uint r = (uint)(MathHelper.Clamp(vector.X, 0f, 1f) * 31.0f) << 10;
            uint g = (uint)(MathHelper.Clamp(vector.Y, 0f, 1f) * 31.0f) << 5;
            uint b = (uint)(MathHelper.Clamp(vector.Z, 0f, 1f) * 31.0f);
            uint a = (uint)(MathHelper.Clamp(vector.W, 0f, 1f)) << 15;

            this.packedValue = (ushort)(r | g | b | a);
        }

        public Vector4 ToVector4()
        {
            return new Vector4(((packedValue >> 10) & 31) / 31.0f,
                                ((packedValue >> 5) & 31) / 31.0f,
                                ((packedValue) & 31) / 31.0f,
                                ((packedValue >> 15) & 1) / 1f);
        }

        public override bool Equals(object obj)
        {
            return obj is Bgra5551 && this == (Bgra5551)obj;
        }

        public bool Equals(Bgra5551 other)
        {
            return this.packedValue == other.packedValue;
        }

        public override string ToString()
        {
            return this.packedValue.ToString("X4");
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public static bool operator ==(Bgra5551 lhs, Bgra5551 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(Bgra5551 lhs, Bgra5551 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
