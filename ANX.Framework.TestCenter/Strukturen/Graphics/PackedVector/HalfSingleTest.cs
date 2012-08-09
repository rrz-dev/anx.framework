#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using XNAHalfSingle = Microsoft.Xna.Framework.Graphics.PackedVector.HalfSingle;
using ANXHalfSingle = ANX.Framework.Graphics.PackedVector.HalfSingle;

using XNAVector4 = Microsoft.Xna.Framework.Vector4;
using ANXVector4 = ANX.Framework.Vector4;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics.PackedVector
{
    [TestFixture]
    class HalfSingleTest
    {
        #region Testdata

        static object[] floats =
        {
           new object[] { DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue },
           new object[] { DataFactory.RandomValue }
        };

        #endregion

        [Test, TestCaseSource("floats")]
        public void contructor1(float single)
        {
            XNAHalfSingle xnaVal = new XNAHalfSingle(single);
            ANXHalfSingle anxVal = new ANXHalfSingle(single);

            AssertHelper.ConvertEquals(xnaVal, anxVal, "Constructor1");
        }

        [Test, TestCaseSource("floats")]
        public void unpack1(float single)
        {
            XNAHalfSingle xnaVal = new XNAHalfSingle(single);
            ANXHalfSingle anxVal = new ANXHalfSingle(single);

            AssertHelper.ConvertEquals(xnaVal.ToSingle(), anxVal.ToSingle(), "unpack1");
        }
    }
}
