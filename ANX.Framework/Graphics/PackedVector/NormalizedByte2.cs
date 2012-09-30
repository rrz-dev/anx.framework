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
    public struct NormalizedByte2 : IPackedVector<ushort>, IEquatable<NormalizedByte2>, IPackedVector
    {
        private ushort packedValue;

        public ushort PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

        private const float max = 255 >> 1;
        private const float oneOverMax = 1f / max;
        private const uint mask = 256 >> 1;

        public NormalizedByte2(float x, float y)
        {
            ushort b1 = (ushort)(((int)MathHelper.Clamp(x * max, -max, max) & 255) << 0);
            ushort b2 = (ushort)(((int)MathHelper.Clamp(y * max, -max, max) & 255) << 8);

            this.packedValue = (UInt16)(b1 | b2);
        }

        public NormalizedByte2(Vector2 vector)
        {
            ushort b1 = (ushort)(((int)MathHelper.Clamp(vector.X * max, -max, max) & 255) << 0);
            ushort b2 = (ushort)(((int)MathHelper.Clamp(vector.Y * max, -max, max) & 255) << 8);

            this.packedValue = (UInt16)(b1 | b2);
        }

        public Vector2 ToVector2()
        {
            Vector2 vector;
            vector.X = convert(0xff, (uint)this.packedValue);
            vector.Y = convert(0xff, (uint)(this.packedValue >> 8));
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
            ushort b1 = (ushort)(((int)MathHelper.Clamp(vector.X * max, -max, max) & 255) << 0);
            ushort b2 = (ushort)(((int)MathHelper.Clamp(vector.Y * max, -max, max) & 255) << 8);

            this.packedValue = (UInt16)(b1 | b2);
        }

        Vector4 IPackedVector.ToVector4()
        {
            Vector2 val = this.ToVector2();
            return new Vector4(val.X, val.Y, 0f, 1f);
        }

        public override bool Equals(object obj)
        {
            return obj is NormalizedByte2 && this == (NormalizedByte2)obj;
        }

        public bool Equals(NormalizedByte2 other)
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

        public static bool operator ==(NormalizedByte2 lhs, NormalizedByte2 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(NormalizedByte2 lhs, NormalizedByte2 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
