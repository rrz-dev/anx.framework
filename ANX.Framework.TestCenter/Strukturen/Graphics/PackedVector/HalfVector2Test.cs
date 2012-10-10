#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using XNAHalfVector2 = Microsoft.Xna.Framework.Graphics.PackedVector.HalfVector2;
using ANXHalfVector2 = ANX.Framework.Graphics.PackedVector.HalfVector2;

using XNAVector2 = Microsoft.Xna.Framework.Vector2;
using ANXVector2 = ANX.Framework.Vector2;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics.PackedVector
{
    [TestFixture]
    class HalfVector2Test
    {
        #region Testdata

        static object[] twofloats =
        {
           new object[] { DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue, DataFactory.RandomValue }
        };

        #endregion

        [Test, TestCaseSource("twofloats")]
        public void contructor1(float x, float y)
        {
            XNAHalfVector2 xnaVal = new XNAHalfVector2(x, y);
            ANXHalfVector2 anxVal = new ANXHalfVector2(x, y);

            AssertHelper.ConvertEqualsPackedVector(xnaVal, anxVal, "Constructor1");
        }

        [Test, TestCaseSource("twofloats")]
        public void contructor2(float x, float y)
        {
            XNAHalfVector2 xnaVal = new XNAHalfVector2(new XNAVector2(x, y));
            ANXHalfVector2 anxVal = new ANXHalfVector2(new ANXVector2(x, y));

            AssertHelper.ConvertEqualsPackedVector(xnaVal, anxVal, "Constructor2");
        }

        [Test, TestCaseSource("twofloats")]
        public void unpack1(float x, float y)
        {
            XNAHalfVector2 xnaVal = new XNAHalfVector2(x, y);
            ANXHalfVector2 anxVal = new ANXHalfVector2(x, y);

            AssertHelper.ConvertEquals(xnaVal.ToVector2(), anxVal.ToVector2(), "unpack1");
        }
    }
}
