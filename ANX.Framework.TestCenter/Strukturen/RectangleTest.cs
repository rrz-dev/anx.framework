#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using XNARect = Microsoft.Xna.Framework.Rectangle;
using ANXRect = ANX.Framework.Rectangle;

namespace ANX.Framework.TestCenter.Strukturen
{

    [TestFixture]
    class RectangleTest
    {
        private static object[] ninefloats =
        {
            new object[] {0, 0, 0, 0},
            new object[] {1, 2, 3, 4},
            new object[] {-1, -2, 3, 4},
            new object[] {1, -2, 0, 0}
        };

        private static object[] ninefloats6 =
        {
            new object[] {0, 0, 0, 0, 0, 0, 0, 0},
            new object[] {1, 2, 3, 4, 0, 0, 2, 5},
            new object[] {-1, -2, 3, 4, 1, 10, 1, 5},
            new object[] {1, -2, 0, 0, 3, 1, 2, 1}
        };

        #region properties
        [Test, TestCaseSource("ninefloats")]
        public void Bottom(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            AssertHelper.ConvertEquals(xna.Bottom, anx.Bottom, "Bottom");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Center(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            int xnaRX = xna.Center.X;
            int anxRX = anx.Center.X;

            int xnaRY = xna.Center.Y;
            int anxRY = anx.Center.Y;

            Assert.AreEqual(xnaRX, anxRX);
            Assert.AreEqual(xnaRY, anxRY);
        }

        [Test]
        public void Empty()
        {
            AssertHelper.ConvertEquals(XNARect.Empty, ANXRect.Empty, "Empty");
        }

        [Test, TestCaseSource("ninefloats")]
        public void ToString(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString");
        }

        [Test, TestCaseSource("ninefloats")]
        public void IsEmpty(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            Assert.AreEqual(xna.IsEmpty, anx.IsEmpty);
            Assert.IsTrue(XNARect.Empty.IsEmpty);
            Assert.IsTrue(ANXRect.Empty.IsEmpty);
        }

        [Test, TestCaseSource("ninefloats")]
        public void Left(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            AssertHelper.ConvertEquals(xna.Left, anx.Left, "Left");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Location(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            int xnaRX = xna.Location.X;
            int anxRX = anx.Location.X;

            int xnaRY = xna.Location.Y;
            int anxRY = anx.Location.Y;

            Assert.AreEqual(xnaRX, anxRX);
            Assert.AreEqual(xnaRY, anxRY);
        }

        [Test, TestCaseSource("ninefloats")]
        public void Right(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            AssertHelper.ConvertEquals(xna.Right, anx.Right, "Right");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Top(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            AssertHelper.ConvertEquals(xna.Top, anx.Top, "Top");
        }
        #endregion

        #region constructors
        [Test, TestCaseSource("ninefloats")]
        public void Constructor(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            AssertHelper.ConvertEquals(xna, anx, "Constructor");
        }
        #endregion

        #region public methods
        [Test, TestCaseSource("ninefloats6")]
        public void ContainsPoint(int x1, int y1, int w1, int h1, int u, int v, int nop1, int nop2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            AssertHelper.ConvertEquals(xna.Contains(u, v), anx.Contains(u, v), "ContainsPoint");
        }

        [Test, TestCaseSource("ninefloats6")]
        public void ContainsRect(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            XNARect xna2 = new XNARect(x2, y2, w2, h2);
            ANXRect anx2 = new ANXRect(x2, y2, w2, h2);

            AssertHelper.ConvertEquals(xna.Contains(xna2), anx.Contains(anx2), "ContainsRect");
        }

        [Test, TestCaseSource("ninefloats")]
        public void GetHashCode(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            Assert.AreEqual(xna.GetHashCode(), anx.GetHashCode());
        }

        [Test, TestCaseSource("ninefloats6")]
        public void Inflate(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            xna.Inflate(x2, y2);
            anx.Inflate(x2, y2);

            AssertHelper.ConvertEquals(xna, anx, "Inflate");
        }

        [Test, TestCaseSource("ninefloats6")]
        public void Intersects(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            XNARect xna2 = new XNARect(x2, y2, w2, h2);
            ANXRect anx2 = new ANXRect(x2, y2, w2, h2);

            Assert.AreEqual(xna.Intersects(xna2), anx.Intersects(anx2));
        }

        [Test, TestCaseSource("ninefloats6")]
        public void Offset(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            xna.Offset(x2, y2);
            anx.Offset(x2, y2);

            AssertHelper.ConvertEquals(xna, anx, "Offset");
        }

        [Test, TestCaseSource("ninefloats6")]
        public void IntersectStatic(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            XNARect xna2 = new XNARect(x2, y2, w2, h2);
            ANXRect anx2 = new ANXRect(x2, y2, w2, h2);

            AssertHelper.ConvertEquals(XNARect.Intersect(xna, xna2), ANXRect.Intersect(anx, anx2), "Intersection");
        }

        [Test, TestCaseSource("ninefloats6")]
        public void Union(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            XNARect xna2 = new XNARect(x2, y2, w2, h2);
            ANXRect anx2 = new ANXRect(x2, y2, w2, h2);

            AssertHelper.ConvertEquals(XNARect.Union(xna, xna2), ANXRect.Union(anx, anx2), "Union");
        }

        [Test, TestCaseSource("ninefloats6")]
        public void Equals(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            XNARect xna1 = new XNARect(x1, y1, w1, h1);
            ANXRect anx1 = new ANXRect(x1, y1, w1, h1);
            XNARect xna2 = new XNARect(x1 + 1, y1 + 1, w1 + 1, h1 + 1);
            ANXRect anx2 = new ANXRect(x1 + 1, y1 + 1, w1 + 1, h1 + 1);

            Assert.IsTrue(xna.Equals(xna1));
            Assert.IsTrue(anx.Equals(anx1));

            Assert.IsFalse(xna.Equals(xna2));
            Assert.IsFalse(anx.Equals(anx2));
        }
        #endregion
    }
}
