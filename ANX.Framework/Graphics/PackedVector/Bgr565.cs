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
    public struct Bgr565 : IPackedVector<UInt16>, IEquatable<Bgr565>, IPackedVector
    {
        private UInt16 packedValue;

        public ushort PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

        public Bgr565(float x, float y, float z)
        {
            uint r = (uint)(MathHelper.Clamp(x, 0f, 1f) * 31.0f) << 11;
            uint g = (uint)(MathHelper.Clamp(y, 0f, 1f) * 63.0f) << 5;
            uint b = (uint)(MathHelper.Clamp(z, 0f, 1f) * 31.0f);

            this.packedValue = (ushort)((r | g) | b);
        }

        public Bgr565(Vector3 vector)
        {
            uint r = (uint)(MathHelper.Clamp(vector.X, 0f, 1f) * 31.0f) << 11;
            uint g = (uint)(MathHelper.Clamp(vector.Y, 0f, 1f) * 63.0f) << 5;
            uint b = (uint)(MathHelper.Clamp(vector.Z, 0f, 1f) * 31.0f);

            this.packedValue = (ushort)((r | g) | b);
        }

        public Vector3 ToVector3()
        {
            return new Vector3( ((packedValue >> 11) & 31) / 31.0f,
                                ((packedValue >> 5) & 63) / 63.0f,
                                ((packedValue) & 31) / 31.0f);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            uint r = (uint)(MathHelper.Clamp(vector.X, 0f, 1f) * 31.0f) << 11;
            uint g = (uint)(MathHelper.Clamp(vector.Y, 0f, 1f) * 63.0f) << 5;
            uint b = (uint)(MathHelper.Clamp(vector.Z, 0f, 1f) * 31.0f);

            this.packedValue = (ushort)((r | g) | b);
        }

        Vector4 IPackedVector.ToVector4()
        {
            return new Vector4(ToVector3(), 1f);
        }

        public override bool Equals(object obj)
        {
            return obj is Bgr565 && this == (Bgr565)obj;
        }

        public bool Equals(Bgr565 other)
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

        public static bool operator ==(Bgr565 lhs, Bgr565 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(Bgr565 lhs, Bgr565 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
