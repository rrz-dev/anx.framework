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
    public struct HalfVector2 : IPackedVector<uint>, IEquatable<HalfVector2>, IPackedVector
    {
        private uint packedValue;

        public uint PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

        public HalfVector2(float x, float y)
        {
            packedValue = HalfTypeHelper.convert(x) | (uint)HalfTypeHelper.convert(y) << 16;
        }

        public HalfVector2(Vector2 vector)
        {
            packedValue = HalfTypeHelper.convert(vector.X) | (uint)HalfTypeHelper.convert(vector.Y) << 16;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(HalfTypeHelper.convert((ushort)this.packedValue), HalfTypeHelper.convert((ushort)(this.packedValue >> 16)));
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            packedValue = HalfTypeHelper.convert(vector.X) | (uint)HalfTypeHelper.convert(vector.Y) << 16;
        }

        Vector4 IPackedVector.ToVector4()
        {
            Vector2 val = this.ToVector2();
            return new Vector4(val.X, val.Y, 0f, 1f);
        }

        public override bool Equals(object obj)
        {
            return obj is HalfVector2 && this == (HalfVector2)obj;
        }

        public bool Equals(HalfVector2 other)
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

        public static bool operator ==(HalfVector2 lhs, HalfVector2 rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(HalfVector2 lhs, HalfVector2 rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
