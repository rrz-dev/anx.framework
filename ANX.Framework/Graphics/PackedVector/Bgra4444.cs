#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics.PackedVector
{
    public struct Bgra4444 : IPackedVector<UInt16>, IEquatable<Bgra4444>, IPackedVector
    {
        private UInt16 packedValue;

        public Bgra4444(float x, float y, float z, float w)
        {
            uint r = (uint)(MathHelper.Clamp(x, 0f, 1f) * 15.0f) << 8;
            uint g = (uint)(MathHelper.Clamp(y, 0f, 1f) * 15.0f) << 4;
            uint b = (uint)(MathHelper.Clamp(z, 0f, 1f) * 15.0f);
            uint a = (uint)(MathHelper.Clamp(w, 0f, 1f) * 15.0f) << 12;

            this.packedValue = (ushort)(r | g | b | a);
        }

        public Bgra4444(Vector4 vector)
        {
            uint r = (uint)(MathHelper.Clamp(vector.X, 0f, 1f) * 15.0f) << 8;
            uint g = (uint)(MathHelper.Clamp(vector.Y, 0f, 1f) * 15.0f) << 4;
            uint b = (uint)(MathHelper.Clamp(vector.Z, 0f, 1f) * 15.0f);
            uint a = (uint)(MathHelper.Clamp(vector.W, 0f, 1f) * 15.0f) << 12;

            this.packedValue = (ushort)(r | g | b | a);
        }

        public ushort PackedValue
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
            return new Vector4(((packedValue >> 8) & 15) / 15.0f,
                                ((packedValue >> 4) & 15) / 15.0f,
                                ((packedValue) & 15) / 15.0f,
                                ((packedValue >> 12) & 15) / 15.0f);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                return this == (Bgra4444)obj;
            }

            return false;
        }

        public bool Equals(Bgra4444 other)
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

        public static bool operator ==(Bgra4444 lhs, Bgra4444 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(Bgra4444 lhs, Bgra4444 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
