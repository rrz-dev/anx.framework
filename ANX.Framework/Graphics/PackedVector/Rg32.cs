#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics.PackedVector
{
    public struct Rg32 : IPackedVector<uint>, IEquatable<Rg32>, IPackedVector
    {
        private uint packedValue;

        public Rg32(float x, float y)
        {
            uint r = (uint)(MathHelper.Clamp(x, 0f, 1f) * 65535.0f) <<  0;
            uint g = (uint)(MathHelper.Clamp(y, 0f, 1f) * 65535.0f) << 16;

            this.packedValue = (uint)r | g;
        }

        public Rg32(Vector2 vector)
        {
            uint r = (uint)(MathHelper.Clamp(vector.X, 0f, 1f) * 65535.0f) << 0;
            uint g = (uint)(MathHelper.Clamp(vector.Y, 0f, 1f) * 65535.0f) << 16;

            this.packedValue = (uint)r | g;
        }

        public uint PackedValue
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

        public Vector2 ToVector2()
        {
            return new Vector2(((packedValue >>  0) & 65535) / 65535.0f,
                               ((packedValue >> 16) & 65535) / 65535.0f);
        }

        Vector4 IPackedVector.ToVector4()
        {
            Vector2 val = this.ToVector2();
            return new Vector4(val.X, val.Y, 0f, 1f);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            packedValue = HalfTypeHelper.convert(vector.X) | (uint)HalfTypeHelper.convert(vector.Y) << 16;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                return this == (Rg32)obj;
            }

            return false;
        }

        public bool Equals(Rg32 other)
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

        public static bool operator ==(Rg32 lhs, Rg32 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(Rg32 lhs, Rg32 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
