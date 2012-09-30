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
    public struct NormalizedByte4 : IPackedVector<uint>, IEquatable<NormalizedByte4>, IPackedVector
    {
        private uint packedValue;

        public uint PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

        private const float max = 255 >> 1;
        private const float oneOverMax = 1f / max;
        private const uint mask = 256 >> 1;

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
            return obj is NormalizedByte4 && this == (NormalizedByte4)obj;
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
