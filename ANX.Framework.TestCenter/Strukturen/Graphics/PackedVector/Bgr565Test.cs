#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using XNABgr565 = Microsoft.Xna.Framework.Graphics.PackedVector.Bgr565;
using ANXBgr565 = ANX.Framework.Graphics.PackedVector.Bgr565;

using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXVector3 = ANX.Framework.Vector3;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics.PackedVector
{
    [TestFixture]
    class Bgr565Test
    {
        #region Testdata

        static object[] threefloats =
        {
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue, DataFactory.RandomValue, DataFactory.RandomValue }
        };

        #endregion

        [Test, TestCaseSource("threefloats")]
        public void contructor1(float r, float g, float b)
        {
            XNABgr565 xnaVal = new XNABgr565(r, g, b);
            ANXBgr565 anxVal = new ANXBgr565(r, g, b);

            AssertHelper.ConvertEquals(xnaVal, anxVal, "Constructor1");
        }

        [Test, TestCaseSource("threefloats")]
        public void contructor2(float r, float g, float b)
        {

            XNABgr565 xnaVal = new XNABgr565(new XNAVector3(r, g, b));
            ANXBgr565 anxVal = new ANXBgr565(new ANXVector3(r, g, b));

            AssertHelper.ConvertEquals(xnaVal, anxVal, "Constructor2");
        }

        [Test, TestCaseSource("threefloats")]
        public void ToVector3(float r, float g, float b)
        {
            XNABgr565 xnaVal = new XNABgr565(r, g, b);
            ANXBgr565 anxVal = new ANXBgr565(r, g, b);

            AssertHelper.ConvertEquals(xnaVal.ToVector3(), anxVal.ToVector3(), "ToVector3");
        }

    }
}
