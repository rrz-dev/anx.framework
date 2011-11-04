#region UsingStatements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

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


        public void ConvertEquals(XNAPoint xna, ANXPoint anx, String test)
        {
            if (xna.X == anx.X &&
                xna.Y == anx.Y)
                Assert.Pass(String.Format("{0} passed", test));
            else
                Assert.Fail(String.Format("{0} failed: xna({1}) anx({2})", test, xna.ToString(), anx.ToString()));
        }
        #endregion

        #region Constructors
        [Test]
        public void constructor0()
        {
            XNAPoint xna = new XNAPoint();

            ANXPoint anx = new ANXPoint();

            ConvertEquals(xna, anx, "constructor0");
        }

        [Test, TestCaseSource("twoint")]
        public void constructor1(int x, int y)
        {
            XNAPoint xna = new XNAPoint(x, y);

            ANXPoint anx = new ANXPoint(x, y);

            ConvertEquals(xna, anx, "constructor1");
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
        #endregion
    }
}
