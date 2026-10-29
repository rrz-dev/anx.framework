#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using XNABgra4444 = Microsoft.Xna.Framework.Graphics.PackedVector.Bgra4444;
using ANXBgra4444 = ANX.Framework.Graphics.PackedVector.Bgra4444;

using XNAVector4 = Microsoft.Xna.Framework.Vector4;
using ANXVector4 = ANX.Framework.Vector4;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics.PackedVector
{
    [TestFixture]
    class Bgra4444Test
    {
        #region Testdata

        static object[] fourfloats =
        {
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue }
        };

        #endregion

        [Test, TestCaseSource("fourfloats")]
        public void contructor1(float r, float g, float b, float a)
        {
            XNABgra4444 xnaVal = new XNABgra4444(r, g, b, a);
            ANXBgra4444 anxVal = new ANXBgra4444(r, g, b, a);

            AssertHelper.ConvertEqualsPackedVector(xnaVal, anxVal, "Constructor1");
        }

        [Test, TestCaseSource("fourfloats")]
        public void contructor2(float r, float g, float b, float a)
        {

            XNABgra4444 xnaVal = new XNABgra4444(new XNAVector4(r, g, b, a));
            ANXBgra4444 anxVal = new ANXBgra4444(new ANXVector4(r, g, b, a));

            AssertHelper.ConvertEqualsPackedVector(xnaVal, anxVal, "Constructor2");
        }

        [Test, TestCaseSource("fourfloats")]
        public void ToVector4(float r, float g, float b, float a)
        {
            XNABgra4444 xnaVal = new XNABgra4444(r, g, b, a);
            ANXBgra4444 anxVal = new ANXBgra4444(r, g, b, a);

            AssertHelper.ConvertEquals(xnaVal.ToVector4(), anxVal.ToVector4(), "ToVector4");
        }

    }
}
