#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using XNARg32 = Microsoft.Xna.Framework.Graphics.PackedVector.Rg32;
using ANXRg32 = ANX.Framework.Graphics.PackedVector.Rg32;

using XNAVector2 = Microsoft.Xna.Framework.Vector2;
using ANXVector2 = ANX.Framework.Vector2;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics.PackedVector
{
    [TestFixture]
    class Rg32Test
    {
        #region Testdata

        static object[] twofloats =
        {
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue },
        };

        #endregion

        [Test, TestCaseSource("twofloats")]
        public void contructor1(float x, float y)
        {
            XNARg32 xnaVal = new XNARg32(x, y);
            ANXRg32 anxVal = new ANXRg32(x, y);

            AssertHelper.ConvertEquals(xnaVal, anxVal, "Constructor1");
        }

        [Test, TestCaseSource("twofloats")]
        public void contructor2(float x, float y)
        {

            XNARg32 xnaVal = new XNARg32(new XNAVector2(x, y));
            ANXRg32 anxVal = new ANXRg32(new ANXVector2(x, y));

            AssertHelper.ConvertEquals(xnaVal, anxVal, "Constructor2");
        }

        [Test, TestCaseSource("twofloats")]
        public void ToVector2(float x, float y)
        {
            XNARg32 xnaVal = new XNARg32(x, y);
            ANXRg32 anxVal = new ANXRg32(x, y);

            AssertHelper.ConvertEquals(xnaVal.ToVector2(), anxVal.ToVector2(), "ToVector2");
        }

    }
}
