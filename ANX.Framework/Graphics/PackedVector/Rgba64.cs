#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics.PackedVector
{
    public struct Rgba64 : IPackedVector<ulong>, IEquatable<Rgba64>, IPackedVector
    {
        private ulong packedValue;

        public Rgba64(float x, float y, float z, float w)
        {
            ulong r = (ulong)(MathHelper.Clamp(x, 0f, 1f) * 65535f) << 0;
            ulong g = (ulong)(MathHelper.Clamp(y, 0f, 1f) * 65535f) << 16;
            ulong b = (ulong)(MathHelper.Clamp(z, 0f, 1f) * 65535f) << 32;
            ulong a = (ulong)(MathHelper.Clamp(w, 0f, 1f) * 65535f) << 48;

            this.packedValue = (ulong)(r | g | b | a);
        }

        public Rgba64(Vector4 vector)
        {
            ulong r = (ulong)(MathHelper.Clamp(vector.X, 0f, 1f) * 65535f) << 0;
            ulong g = (ulong)(MathHelper.Clamp(vector.Y, 0f, 1f) * 65535f) << 16;
            ulong b = (ulong)(MathHelper.Clamp(vector.Z, 0f, 1f) * 65535f) << 32;
            ulong a = (ulong)(MathHelper.Clamp(vector.W, 0f, 1f) * 65535f) << 48;

            this.packedValue = (ulong)(r | g | b | a);
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
            return new Vector4(((packedValue >>  0) & 65535) / 65535f,
                               ((packedValue >> 16) & 65535) / 65535f,
                               ((packedValue >> 32) & 65535) / 65535f,
                               ((packedValue >> 48) & 65535) / 65535f);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            ulong r = (ulong)(MathHelper.Clamp(vector.X, 0f, 1f) * 65535f) << 0;
            ulong g = (ulong)(MathHelper.Clamp(vector.Y, 0f, 1f) * 65535f) << 16;
            ulong b = (ulong)(MathHelper.Clamp(vector.Z, 0f, 1f) * 65535f) << 32;
            ulong a = (ulong)(MathHelper.Clamp(vector.W, 0f, 1f) * 65535f) << 48;

            this.packedValue = (ulong)(r | g | b | a);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                return this == (Rgba64)obj;
            }

            return false;
        }

        public bool Equals(Rgba64 other)
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

        public static bool operator ==(Rgba64 lhs, Rgba64 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(Rgba64 lhs, Rgba64 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
