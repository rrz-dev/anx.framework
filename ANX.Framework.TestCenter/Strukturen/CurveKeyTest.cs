#region Using Statements
using System;
using NUnit.Framework;
#endregion // Using Statements

using XNACurve = Microsoft.Xna.Framework.Curve;
using ANXCurve = ANX.Framework.Curve;

using XNACurveKey = Microsoft.Xna.Framework.CurveKey;
using ANXCurveKey = ANX.Framework.CurveKey;

using XNACurveLoopType = Microsoft.Xna.Framework.CurveLoopType;
using ANXCurveLoopType = ANX.Framework.CurveLoopType;

using XNACurveKeyCollection = Microsoft.Xna.Framework.CurveKeyCollection;
using ANXCurveKeyCollection = ANX.Framework.CurveKeyCollection;

using XNACurveTangent = Microsoft.Xna.Framework.CurveTangent;
using ANXCurveTangent = ANX.Framework.CurveTangent;

using XNACurveContinuity = Microsoft.Xna.Framework.CurveContinuity;
using ANXCurveContinuity = ANX.Framework.CurveContinuity;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    public class CurveKeyTest
    {
        #region Testdata
        static object[] sixteenfloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
 			new object[] {1f,  2f, 3f,  4f,  5f,  6f, 7f,  8f,  9f,  10f,  11f,  12f,  13f,  14f,  15f,  16f,},
       };
        static object[] sixteenfloats2 =
        {
 			new object[] {1f,  1f, 3f,  4f,  5f,  6f, 7f,  8f,  9f,  10f,  11f,  12f,  13f,  14f,  15f,  16f,},
 			new object[] {2f,  1f, 3f,  4f,  5f,  6f, 7f,  8f,  9f,  10f,  11f,  12f,  13f,  14f,  15f,  16f,},
 			new object[] {1f,  2f, 3f,  4f,  5f,  6f, 7f,  8f,  9f,  10f,  11f,  12f,  13f,  14f,  15f,  16f,},
       };
        static object[] sixteenfloatsCurve =
		{
            new object[] {XNACurveContinuity.Smooth,ANXCurveContinuity.Smooth,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] { XNACurveContinuity.Smooth,ANXCurveContinuity.Smooth, DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {XNACurveContinuity.Smooth,ANXCurveContinuity.Smooth,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] { XNACurveContinuity.Step,ANXCurveContinuity.Step, DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {XNACurveContinuity.Step,ANXCurveContinuity.Step,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
 			new object[] {XNACurveContinuity.Smooth,ANXCurveContinuity.Smooth,  1f,  2f, 3f,  4f,  5f,  6f, 7f,  8f,  9f,  10f,  11f,  12f,  13f,  14f,  15f,  16f,},
 			new object[] {XNACurveContinuity.Step,ANXCurveContinuity.Step,  1f,  2f, 3f,  4f,  5f,  6f, 7f,  8f,  9f,  10f,  11f,  12f,  13f,  14f,  15f,  16f,},
		};

        #endregion

        [TestCaseSource("sixteenfloats")]
        public void Constructor(float a1, float a2, float a3, float a4, float a5, float a6, float a7, float a8, float a9,
            float a10, float a11, float a12, float a13, float a14, float a15, float a16)
        {
            XNACurveKey xna = new XNACurveKey(a1, a2);
            ANXCurveKey anx = new ANXCurveKey(a1, a2);

            AssertHelper.ConvertEquals(xna, anx, "Constructor");
        }

        [TestCaseSource("sixteenfloats")]
        public void Constructor2(float a1, float a2, float a3, float a4, float a5, float a6, float a7, float a8, float a9,
            float a10, float a11, float a12, float a13, float a14, float a15, float a16)
        {
            XNACurveKey xna = new XNACurveKey(a1, a2, a3, a4);
            ANXCurveKey anx = new ANXCurveKey(a1, a2, a3, a4);

            AssertHelper.ConvertEquals(xna, anx, "Constructor2");
        }

        [TestCaseSource("sixteenfloatsCurve")]
        public void Constructor3(XNACurveContinuity xnacurve, ANXCurveContinuity anxcurve, float a1, float a2, float a3,
            float a4, float a5, float a6, float a7, float a8, float a9, float a10, float a11, float a12, float a13, float a14,
            float a15, float a16)
        {
            XNACurveKey xna = new XNACurveKey(a1, a2, a3, a4, xnacurve);
            ANXCurveKey anx = new ANXCurveKey(a1, a2, a3, a4, anxcurve);

            AssertHelper.ConvertEquals(xna, anx, "Constructor3");
        }

        [TestCaseSource("sixteenfloats")]
        public void Clone(float a1, float a2, float a3, float a4, float a5, float a6, float a7, float a8, float a9, float a10,
            float a11, float a12, float a13, float a14, float a15, float a16)
        {
            XNACurveKey xna = new XNACurveKey(a1, a2, a3, a4);
            XNACurveKey xna2 = xna.Clone();
            ANXCurveKey anx = new ANXCurveKey(a1, a2, a3, a4);
            ANXCurveKey anx2 = anx.Clone();

            AssertHelper.ConvertEquals(xna, anx, xna2, anx2, "Clone");
        }

        [TestCaseSource("sixteenfloats2")]
        public void CompareTo(float a1, float a2, float a3, float a4, float a5, float a6, float a7, float a8, float a9,
            float a10, float a11, float a12, float a13, float a14, float a15, float a16)
        {
            XNACurveKey xna = new XNACurveKey(a1, a2, a3, a4);
            XNACurveKey xna2 = new XNACurveKey(a2, a1, a3, a4);
            ANXCurveKey anx = new ANXCurveKey(a1, a2, a3, a4);
            ANXCurveKey anx2 = new ANXCurveKey(a2, a1, a3, a4);

            AssertHelper.ConvertEquals(xna.CompareTo(xna2), anx.CompareTo(anx2), "CompareTo");
        }

        [TestCaseSource("sixteenfloats")]
        public void op_Equality(float a1, float a2, float a3, float a4, float a5, float a6, float a7, float a8, float a9,
            float a10, float a11, float a12, float a13, float a14, float a15, float a16)
        {
            XNACurveKey xna = new XNACurveKey(a1, a2, a3, a4);
            XNACurveKey xna2 = new XNACurveKey(a2, a1, a3, a4);
            ANXCurveKey anx = new ANXCurveKey(a1, a2, a3, a4);
            ANXCurveKey anx2 = new ANXCurveKey(a2, a1, a3, a4);

            AssertHelper.ConvertEquals(xna==xna2, anx==anx2, "op_Equality");
        }
  
        [TestCaseSource("sixteenfloats")]
        public void op_Equality2(float a1, float a2, float a3, float a4, float a5, float a6, float a7, float a8, float a9,
            float a10, float a11, float a12, float a13, float a14, float a15, float a16)
        {
            XNACurveKey xna = new XNACurveKey(a1, a2, a3, a4);
            XNACurveKey xna2 = new XNACurveKey(a2, a1, a3, a4);
            ANXCurveKey anx = new ANXCurveKey(a1, a2, a3, a4);
            ANXCurveKey anx2 = new ANXCurveKey(a2, a1, a3, a4);

            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "op_Equality2");
        }

        [TestCaseSource("sixteenfloats")]
        public void op_Unequality(float a1, float a2, float a3, float a4, float a5, float a6, float a7, float a8, float a9,
            float a10, float a11, float a12, float a13, float a14, float a15, float a16)
        {
            XNACurveKey xna = new XNACurveKey(a1, a2, a3, a4);
            XNACurveKey xna2 = new XNACurveKey(a2, a1, a3, a4);
            ANXCurveKey anx = new ANXCurveKey(a1, a2, a3, a4);
            ANXCurveKey anx2 = new ANXCurveKey(a2, a1, a3, a4);

            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "op_Unequality");
        }

        [TestCaseSource("sixteenfloats")]
        public void op_Unequality2(float a1, float a2, float a3, float a4, float a5, float a6, float a7, float a8, float a9,
            float a10, float a11, float a12, float a13, float a14, float a15, float a16)
        {
            XNACurveKey xna = new XNACurveKey(a1, a2, a3, a4);
            XNACurveKey xna2 = new XNACurveKey(a2, a1, a3, a4);
            ANXCurveKey anx = new ANXCurveKey(a1, a2, a3, a4);
            ANXCurveKey anx2 = new ANXCurveKey(a2, a1, a3, a4);

            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "op_Unequality2");
        }

        [TestCaseSource("sixteenfloats")]
        public void GetHashCode(float a1, float a2, float a3, float a4, float a5, float a6, float a7, float a8, float a9,
            float a10, float a11, float a12, float a13, float a14, float a15, float a16)
        {
            XNACurveKey xna = new XNACurveKey(a1, a2, a3, a4);
            XNACurveKey xna2 = new XNACurveKey(a2, a1, a3, a4);
            ANXCurveKey anx = new ANXCurveKey(a1, a2, a3, a4);
            ANXCurveKey anx2 = new ANXCurveKey(a2, a1, a3, a4);

            AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        }
    }
}
