#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics.PackedVector
{
    public struct NormalizedShort4 : IPackedVector<ulong>, IEquatable<NormalizedShort4>, IPackedVector
    {
        private ulong packedValue;

        private const float max = (float)(65535 >> 1);
        private const float oneOverMax = 1f / max;
        private const uint mask = (uint)(65536 >> 1);

        public NormalizedShort4(float x, float y, float z, float w)
        {
            ulong b1 = (ulong)(((long)MathHelper.Clamp(x * max, -max, max) & 65535) <<  0);
            ulong b2 = (ulong)(((long)MathHelper.Clamp(y * max, -max, max) & 65535) << 16);
            ulong b3 = (ulong)(((long)MathHelper.Clamp(z * max, -max, max) & 65535) << 32);
            ulong b4 = (ulong)(((long)MathHelper.Clamp(w * max, -max, max) & 65535) << 48);

            this.packedValue = (ulong)(b1 | b2 | b3 | b4);
        }

        public NormalizedShort4(Vector4 vector)
        {
            ulong b1 = (ulong)(((long)MathHelper.Clamp(vector.X * max, -max, max) & 65535) <<  0);
            ulong b2 = (ulong)(((long)MathHelper.Clamp(vector.Y * max, -max, max) & 65535) << 16);
            ulong b3 = (ulong)(((long)MathHelper.Clamp(vector.Z * max, -max, max) & 65535) << 32);
            ulong b4 = (ulong)(((long)MathHelper.Clamp(vector.W * max, -max, max) & 65535) << 48);

            this.packedValue = (ulong)(b1 | b2 | b3 | b4);
        }

        public ulong PackedValue
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
            vector.X = convert(0xffff, (ulong)this.packedValue);
            vector.Y = convert(0xffff, (ulong)(this.packedValue >> 16));
            vector.Z = convert(0xffff, (ulong)(this.packedValue >> 32));
            vector.W = convert(0xffff, (ulong)(this.packedValue >> 48));
            return vector;
        }

        private static float convert(uint bitmask, ulong value)
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
            ulong b1 = (ulong)(((long)MathHelper.Clamp(vector.X * max, -max, max) & 65535) << 0);
            ulong b2 = (ulong)(((long)MathHelper.Clamp(vector.Y * max, -max, max) & 65535) << 16);
            ulong b3 = (ulong)(((long)MathHelper.Clamp(vector.Z * max, -max, max) & 65535) << 32);
            ulong b4 = (ulong)(((long)MathHelper.Clamp(vector.W * max, -max, max) & 65535) << 48);

            this.packedValue = (ulong)(b1 | b2 | b3 | b4);
        }

        Vector4 IPackedVector.ToVector4()
        {
            return this.ToVector4();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                return this == (NormalizedShort4)obj;
            }

            return false;
        }

        public bool Equals(NormalizedShort4 other)
        {
            return this.packedValue == other.packedValue;
        }

        public override string ToString()
        {
            return this.packedValue.ToString("X16");
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public static bool operator ==(NormalizedShort4 lhs, NormalizedShort4 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(NormalizedShort4 lhs, NormalizedShort4 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
