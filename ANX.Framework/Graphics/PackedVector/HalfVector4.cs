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
    public struct HalfVector4 : IPackedVector<ulong>, IEquatable<HalfVector4>, IPackedVector
    {
        private ulong packedValue;

        [CLSCompliant(false)]
        public ulong PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

        public HalfVector4(float x, float y, float z, float w)
        {
            this.packedValue = (ulong)HalfTypeHelper.convert(x) 
                             | (ulong)HalfTypeHelper.convert(y) << 16
                             | (ulong)HalfTypeHelper.convert(z) << 32 
                             | (ulong)HalfTypeHelper.convert(w) << 48;
        }

        public HalfVector4(Vector4 vector)
        {
            this.packedValue = (ulong)HalfTypeHelper.convert(vector.X)
                             | (ulong)HalfTypeHelper.convert(vector.Y) << 16
                             | (ulong)HalfTypeHelper.convert(vector.Z) << 32
                             | (ulong)HalfTypeHelper.convert(vector.W) << 48;
        }

        public Vector4 ToVector4()
        {
            return new Vector4(HalfTypeHelper.convert((ushort)this.packedValue), 
                               HalfTypeHelper.convert((ushort)(this.packedValue >> 16)),
                               HalfTypeHelper.convert((ushort)(this.packedValue >> 32)),
                               HalfTypeHelper.convert((ushort)(this.packedValue >> 48))
                              );
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            packedValue = HalfTypeHelper.convert(vector.X) | (uint)HalfTypeHelper.convert(vector.Y) << 16;
        }

        public override bool Equals(object obj)
        {
            return obj is HalfVector4 && this == (HalfVector4)obj;
        }

        public bool Equals(HalfVector4 other)
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

        public static bool operator ==(HalfVector4 lhs, HalfVector4 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(HalfVector4 lhs, HalfVector4 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
