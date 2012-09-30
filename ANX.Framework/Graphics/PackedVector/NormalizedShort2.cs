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
    [Developer("???")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public struct NormalizedShort2 : IPackedVector<uint>, IEquatable<NormalizedShort2>, IPackedVector
    {
        private uint packedValue;

        public uint PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

        private const float max = 65535 >> 1;
        private const float oneOverMax = 1f / max;
        private const uint mask = 65536 >> 1;

        public NormalizedShort2(float x, float y)
        {
            uint b1 = (uint)(((int)MathHelper.Clamp(x * max, -max, max) & 65535) <<  0);
            uint b2 = (uint)(((int)MathHelper.Clamp(y * max, -max, max) & 65535) << 16);

            this.packedValue = (uint)(b1 | b2);
        }

        public NormalizedShort2(Vector2 vector)
        {
            uint b1 = (uint)(((int)MathHelper.Clamp(vector.X * max, -max, max) & 65535) <<  0);
            uint b2 = (uint)(((int)MathHelper.Clamp(vector.Y * max, -max, max) & 65535) << 16);

            this.packedValue = (uint)(b1 | b2);
        }

        public Vector2 ToVector2()
        {
            Vector2 vector;
            vector.X = convert(0xffff, (uint)this.packedValue);
            vector.Y = convert(0xffff, (uint)(this.packedValue >> 16));
            return vector;
        }

        private static float convert(uint bitmask, uint value)
        {
            if ((value & mask) != 0)
            {
                if ((value & 65535) >= mask)
                {
                    return -1f;
                }
                value |= ~bitmask;
            }
            else
            {
                value &= 65535;
            }

            return (((float)value) * oneOverMax);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            uint b1 = (uint)(((int)MathHelper.Clamp(vector.X * max, -max, max) & 65535) <<  0);
            uint b2 = (uint)(((int)MathHelper.Clamp(vector.Y * max, -max, max) & 65535) << 16);

            this.packedValue = (uint)(b1 | b2);
        }

        Vector4 IPackedVector.ToVector4()
        {
            Vector2 val = this.ToVector2();
            return new Vector4(val.X, val.Y, 0f, 1f);
        }

        public override bool Equals(object obj)
        {
            return obj is NormalizedShort2 && this == (NormalizedShort2)obj;
        }

        public bool Equals(NormalizedShort2 other)
        {
            return this.packedValue == other.packedValue;
        }

        public override string ToString()
        {
            return this.ToVector2().ToString();
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public static bool operator ==(NormalizedShort2 lhs, NormalizedShort2 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(NormalizedShort2 lhs, NormalizedShort2 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
