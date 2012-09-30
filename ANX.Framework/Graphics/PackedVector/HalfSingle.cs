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
    public struct HalfSingle : IPackedVector<UInt16>, IEquatable<HalfSingle>, IPackedVector
    {
        UInt16 packedValue;

        public ushort PackedValue
        {
            get { return packedValue; }
            set { packedValue = value; }
        }

        public HalfSingle(float single)
        {
            packedValue = HalfTypeHelper.convert(single);
        }

        public float ToSingle()
        {
            return HalfTypeHelper.convert(this.packedValue);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            this.packedValue = HalfTypeHelper.convert(vector.X);
        }

        Vector4 IPackedVector.ToVector4()
        {
            return new Vector4(this.ToSingle(), 0f, 0f, 1f);
        }

        public override bool Equals(object obj)
        {
            return obj is HalfSingle && this == (HalfSingle)obj;
        }

        public bool Equals(HalfSingle other)
        {
            return this.packedValue == other.packedValue;
        }

        public override string ToString()
        {
            return this.ToSingle().ToString();
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

        public static bool operator ==(HalfSingle lhs, HalfSingle rhs)
        {
            return lhs.packedValue == rhs.packedValue;
        }

        public static bool operator !=(HalfSingle lhs, HalfSingle rhs)
        {
            return lhs.packedValue != rhs.packedValue;
        }
    }
}
