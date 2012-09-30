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
    public struct Byte4 : IPackedVector<uint>, IEquatable<Byte4>, IPackedVector
    {
        private uint packedValue;

        public uint PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

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
            return obj is Byte4 && this == (Byte4)obj;
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
