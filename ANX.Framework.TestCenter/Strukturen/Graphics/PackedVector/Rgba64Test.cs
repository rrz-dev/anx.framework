#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using XNARgba64 = Microsoft.Xna.Framework.Graphics.PackedVector.Rgba64;
using ANXRgba64 = ANX.Framework.Graphics.PackedVector.Rgba64;

using XNAVector4 = Microsoft.Xna.Framework.Vector4;
using ANXVector4 = ANX.Framework.Vector4;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics.PackedVector
{
    [TestFixture]
    class Rgba64Test
    {
        #region Testdata

        static object[] fourfloats =
        {
           new object[] { DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
        };

        #endregion

        [Test, TestCaseSource("fourfloats")]
        public void contructor1(float x, float y, float z, float w)
        {
            XNARgba64 xnaVal = new XNARgba64(x, y, z, w);
            ANXRgba64 anxVal = new ANXRgba64(x, y, z, w);

            AssertHelper.ConvertEquals(xnaVal, anxVal, "Constructor1");
        }

        [Test, TestCaseSource("fourfloats")]
        public void contructor2(float x, float y, float z, float w)
        {

            XNARgba64 xnaVal = new XNARgba64(new XNAVector4(x, y, z, w));
            ANXRgba64 anxVal = new ANXRgba64(new ANXVector4(x, y, z, w));

            AssertHelper.ConvertEquals(xnaVal, anxVal, "Constructor2");
        }

        [Test, TestCaseSource("fourfloats")]
        public void ToVector4(float x, float y, float z, float w)
        {
            XNARgba64 xnaVal = new XNARgba64(x, y, z, w);
            ANXRgba64 anxVal = new ANXRgba64(x, y, z, w);

            AssertHelper.ConvertEquals(xnaVal.ToVector4(), anxVal.ToVector4(), "ToVector4");
        }

    }
}
