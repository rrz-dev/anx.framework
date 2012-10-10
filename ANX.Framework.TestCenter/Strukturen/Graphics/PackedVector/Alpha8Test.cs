using NUnit.Framework;
using XNAAlpha8 = Microsoft.Xna.Framework.Graphics.PackedVector.Alpha8;
using ANXAlpha8 = ANX.Framework.Graphics.PackedVector.Alpha8;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics.PackedVector
{
    [TestFixture]
    class Alpha8Test
    {
        #region Testdata

        static object[] floats =
        {
           new object[] {DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue }
        };

        #endregion

        [Test, TestCaseSource("floats")]
        public void contructor(float alpha)
        {
            XNAAlpha8 xnaVal = new XNAAlpha8(alpha);
            ANXAlpha8 anxVal = new ANXAlpha8(alpha);

            AssertHelper.ConvertEqualsPackedVector(xnaVal, anxVal, "Constructor");
        }

        [Test, TestCaseSource("floats")]
        public void ToAlpha(float alpha)
        {
            XNAAlpha8 xnaVal = new XNAAlpha8(alpha);
            ANXAlpha8 anxVal = new ANXAlpha8(alpha);

            AssertHelper.ConvertEquals(xnaVal.ToAlpha(), anxVal.ToAlpha(), "ToAlpha");
        }

    }
}
