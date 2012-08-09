#region UsingStatements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using XNAPoint = Microsoft.Xna.Framework.Point;
using ANXPoint = ANX.Framework.Point;

using NUnit.Framework;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class PointTest
    {
        #region Helper
        private static Random r = new Random();

        public static int RandomInt
        {
            get { return r.Next(); }
        }

        static object[] twoint =
        {
            new object[] { RandomInt, RandomInt },
            new object[] { RandomInt, RandomInt },
            new object[] { RandomInt, RandomInt },
            new object[] { RandomInt, RandomInt },
            new object[] { RandomInt, RandomInt },
        };


        
        #endregion

        #region Constructors
        [Test]
        public void constructor0()
        {
            XNAPoint xna = new XNAPoint();

            ANXPoint anx = new ANXPoint();

            AssertHelper.ConvertEquals(xna, anx, "constructor0");
        }

        [Test, TestCaseSource("twoint")]
        public void constructor1(int x, int y)
        {
            XNAPoint xna = new XNAPoint(x, y);

            ANXPoint anx = new ANXPoint(x, y);

            AssertHelper.ConvertEquals(xna, anx, "constructor1");
        }
        #endregion

        #region Properties
        [Test, TestCaseSource("twoint")]
        public void X(int x, int y)
        {
            XNAPoint xnaPoint = new XNAPoint(x, y);

            ANXPoint anxPoint = new ANXPoint(x, y);

            int xna = xnaPoint.X;
            int anx = anxPoint.X;

            if (xna.Equals(anx))
                Assert.Pass("X passed");
            else
                Assert.Fail(String.Format("X failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("twoint")]
        public void Y(int x, int y)
        {
            XNAPoint xnaPoint = new XNAPoint(x, y);

            ANXPoint anxPoint = new ANXPoint(x, y);

            int xna = xnaPoint.Y;
            int anx = anxPoint.Y;

            if (xna.Equals(anx))
                Assert.Pass("Y passed");
            else
                Assert.Fail(String.Format("Y failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test]
        public void Zero()
        {
            AssertHelper.ConvertEquals(XNAPoint.Zero, ANXPoint.Zero, "Zero");
        }
        
        [TestCaseSource("twoint")]
        public void OpEqual(int x, int y)
        {
            XNAPoint xna = new XNAPoint(x, y);
            ANXPoint anx = new ANXPoint(x, y);
            XNAPoint xna2 = new XNAPoint(x, y);
            ANXPoint anx2 = new ANXPoint(x, y);

            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "OpEqual");
        }

        [TestCaseSource("twoint")]
        public void OpUnEqual(int x, int y)
        {
            XNAPoint xna = new XNAPoint(x, y);
            ANXPoint anx = new ANXPoint(x, y);
            XNAPoint xna2 = new XNAPoint(x, y);
            ANXPoint anx2 = new ANXPoint(x, y);

            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "OpUnEqual");
        }
        [TestCaseSource("twoint")]
        public void Equals(int x, int y)
        {
            XNAPoint xna = new XNAPoint(x, y);
            ANXPoint anx = new ANXPoint(x, y);
            XNAPoint xna2 = new XNAPoint(x, y);
            ANXPoint anx2 = new ANXPoint(x, y);

            AssertHelper.ConvertEquals(xna.Equals(xna2), anx.Equals(anx2), "Equals");
        }
        [TestCaseSource("twoint")]
        public void Equals2(int x, int y)
        {
            XNAPoint xna = new XNAPoint(x, y);
            ANXPoint anx = new ANXPoint(x, y);

            AssertHelper.ConvertEquals(xna.Equals(null), anx.Equals(null), "Equals2");
        }
        [TestCaseSource("twoint")]
        public void ToString(int x, int y)
        {
            XNAPoint xna = new XNAPoint(x, y);
            ANXPoint anx = new ANXPoint(x, y);

            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString");
        }
        [TestCaseSource("twoint")]
        public void GetHashCode(int x, int y)
        {
            XNAPoint xna = new XNAPoint(x, y);
            ANXPoint anx = new ANXPoint(x, y);

            AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        }
        #endregion
    }
}
