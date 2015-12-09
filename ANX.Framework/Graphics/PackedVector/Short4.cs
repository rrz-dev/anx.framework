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
    public struct Short4 : IPackedVector<ulong>, IEquatable<Short4>, IPackedVector
    {
        private ulong packedValue;

        [CLSCompliant(false)]
        public ulong PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

        private const float max = 65535 >> 1;
        private const float min = -max - 1f;

        public Short4(float x, float y, float z, float w)
        {
            ulong b1 = (ulong)(((long)MathHelper.Clamp(x, min, max) & 65535) <<  0);
            ulong b2 = (ulong)(((long)MathHelper.Clamp(y, min, max) & 65535) << 16);
            ulong b3 = (ulong)(((long)MathHelper.Clamp(z, min, max) & 65535) << 32);
            ulong b4 = (ulong)(((long)MathHelper.Clamp(w, min, max) & 65535) << 48);

            this.packedValue = b1 | b2 | b3 | b4;
        }

        public Short4(Vector4 vector)
        {
            ulong b1 = (ulong)(((long)MathHelper.Clamp(vector.X, min, max) & 65535) <<  0);
            ulong b2 = (ulong)(((long)MathHelper.Clamp(vector.Y, min, max) & 65535) << 16);
            ulong b3 = (ulong)(((long)MathHelper.Clamp(vector.Z, min, max) & 65535) << 32);
            ulong b4 = (ulong)(((long)MathHelper.Clamp(vector.W, min, max) & 65535) << 48);

            this.packedValue = b1 | b2 | b3 | b4;
        }

        public Vector4 ToVector4()
        {
            Vector4 vector;
            vector.X = (short)this.packedValue;
            vector.Y = (short)(this.packedValue >> 16);
            vector.Z = (short)(this.packedValue >> 32);
            vector.W = (short)(this.packedValue >> 48);
            return vector;
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            ulong b1 = (ulong)(((long)MathHelper.Clamp(vector.X, -max, max) & 65535) << 0);
            ulong b2 = (ulong)(((long)MathHelper.Clamp(vector.Y, -max, max) & 65535) << 16);
            ulong b3 = (ulong)(((long)MathHelper.Clamp(vector.Z, -max, max) & 65535) << 32);
            ulong b4 = (ulong)(((long)MathHelper.Clamp(vector.W, -max, max) & 65535) << 48);

            this.packedValue = b1 | b2 | b3 | b4;
        }

        Vector4 IPackedVector.ToVector4()
        {
            return this.ToVector4();
        }

        public override bool Equals(object obj)
        {
            return obj is Short4 && this == (Short4)obj;
        }

        public bool Equals(Short4 other)
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

        public static bool operator ==(Short4 lhs, Short4 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(Short4 lhs, Short4 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
