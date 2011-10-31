#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

using XNARect = Microsoft.Xna.Framework.Rectangle;
using ANXRect = ANX.Framework.Rectangle;

namespace ANX.Framework.TestCenter.Strukturen
{

    [TestFixture]
    class RectangleTest
    {
        #region Helper
        public void ConvertEquals(XNARect xna, ANXRect anx, String test)
        {
            //comparing string to catch "not defined" and "infinity" (which seems not to be equal)
            if (xna.ToString().Equals(anx.ToString()))
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(test + " failed: xna({" + xna.X + "}{" + xna.Y + "}{" + xna.Width + "}{" + xna.Height + "}) anx({" + anx.X + "}{" + anx.Y + "}{" + anx.Width + "}{" + anx.Height + "})");
            }
        }

        #endregion


        static object[] ninefloats =
{

   
     new object[] {0,0,0,0},
     new object[] {1,2,3,4},
     new object[] {-1,-2,3,4},
     new object[] {1,-2,0,0}
   

};

        static object[] ninefloats6 =
{

   
     new object[] {0,0,0,0,0,0,0,0},
     new object[] {1,2,3,4,0,0,2,5},
     new object[] {-1,-2,3,4,1,10,1,5},
     new object[] {1,-2,0,0,3,1,2,1}
   

};
        #region properties
        [Test, TestCaseSource("ninefloats")]
        public void Bottom(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            int xnaR = xna.Bottom;
            int anxR = anx.Bottom;
            Assert.AreEqual(xnaR, anxR);
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
            ConvertEquals(XNARect.Empty, ANXRect.Empty, "Empty");


        }
        [Test, TestCaseSource("ninefloats")]
        public void ToString(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            String xnaR = xna.ToString();
            String anxR = anx.ToString();
            Assert.AreEqual(xnaR, anxR);
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

            int xnaR = xna.Left;
            int anxR = anx.Left;
            Assert.AreEqual(xnaR, anxR);
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

            int xnaR = xna.Right;
            int anxR = anx.Right;
            Assert.AreEqual(xnaR, anxR);
        }
        [Test, TestCaseSource("ninefloats")]
        public void Top(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);

            int xnaR = xna.Top;
            int anxR = anx.Top;
            Assert.AreEqual(xnaR, anxR);
        }
        #endregion


        #region constructors
        [Test, TestCaseSource("ninefloats")]
        public void Constructor(int x1, int y1, int w1, int h1)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            ConvertEquals(xna, anx, "Constructor");
        }
        #endregion


        #region public methods
        [Test, TestCaseSource("ninefloats6")]
        public void ContainsPoint(int x1, int y1, int w1, int h1, int u, int v, int nop1, int nop2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            bool anxR = anx.Contains(u, v);
            bool xnaR = xna.Contains(u, v);
            Assert.AreEqual(xnaR, anxR);
        }


        [Test, TestCaseSource("ninefloats6")]
        public void ContainsRect(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            XNARect xna2 = new XNARect(x2, y2, w2, h2);
            ANXRect anx2 = new ANXRect(x2, y2, w2, h2);

            bool xnaR = xna.Contains(xna2);
            bool anxR = anx.Contains(anx2);

            Assert.AreEqual(xnaR, anxR);
        }



        [Test, TestCaseSource("ninefloats")]
        public void getHashCode(int x1, int y1, int w1, int h1)
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

            ConvertEquals(xna, anx, "Inflate");

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

            ConvertEquals(xna, anx, "Offset");

        }


        [Test, TestCaseSource("ninefloats6")]
        public void IntersectStatic(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            XNARect xna2 = new XNARect(x2, y2, w2, h2);
            ANXRect anx2 = new ANXRect(x2, y2, w2, h2);

            ConvertEquals(XNARect.Intersect(xna, xna2), ANXRect.Intersect(anx, anx2), "Intersection");
        }


        [Test, TestCaseSource("ninefloats6")]
        public void Union(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            XNARect xna = new XNARect(x1, y1, w1, h1);
            ANXRect anx = new ANXRect(x1, y1, w1, h1);
            XNARect xna2 = new XNARect(x2, y2, w2, h2);
            ANXRect anx2 = new ANXRect(x2, y2, w2, h2);

            ConvertEquals(XNARect.Union(xna, xna2), ANXRect.Union(anx, anx2), "Union");
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
