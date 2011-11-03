#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

using XNAColor = Microsoft.Xna.Framework.Color;
using ANXColor = ANX.Framework.Color;

using NUnit.Framework;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class ColorTest
    {
        private static Random r = new Random();

        public static float RandomFloat
        {
            get { return (float)(r.NextDouble()); }
        }

        static object[] fourfloats =
        {
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat }
        };

        static object[] eightfloats =
        {
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat }
        };

        static object[] ninefloats =
        {
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat },
            new object[] { RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat, RandomFloat }
        };

        private void ConvertEquals(XNAColor xna, ANXColor anx, String test)
        {
            if (xna.R == anx.R &&
                xna.G == anx.G &&
                xna.B == anx.B &&
                xna.A == anx.A)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0} failed: xna({1}) anx({2})", test, xna.ToString(), anx.ToString()));
            }
        }

        #region Constructors
        [Test]
        public void constructor0()
        {
            XNAColor xna = new XNAColor();
            ANXColor anx = new ANXColor();

            ConvertEquals(xna, anx, "constructor0");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor1(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor(new Microsoft.Xna.Framework.Vector3(r, g, b));
            ANXColor anx = new ANXColor(new ANX.Framework.Vector3(r, g, b));

            ConvertEquals(xna, anx, "constructor1");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor2(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor(new Microsoft.Xna.Framework.Vector4(r, g, b, a));
            ANXColor anx = new ANXColor(new ANX.Framework.Vector4(r, g, b, a));

            ConvertEquals(xna, anx, "constructor2");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor3(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor(r, g, b);
            ANXColor anx = new ANXColor(r, g, b);

            ConvertEquals(xna, anx, "constructor3");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor4(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor((int)(r * 255), (int)(g * 255), (int)(b * 255));
            ANXColor anx = new ANXColor((int)(r * 255), (int)(g * 255), (int)(b * 255));

            ConvertEquals(xna, anx, "constructor4");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor5(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor(r, g, b) * a;
            ANXColor anx = new ANXColor(r, g, b) * a;

            ConvertEquals(xna, anx, "constructor5");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor6(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(a * 255));
            ANXColor anx = new ANXColor((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(a * 255));

            ConvertEquals(xna, anx, "constructor6");
        }
        #endregion

        #region Methods
        [Test, TestCaseSource("eightfloats")]
        public void EqualOperator(
            float r1, float g1, float b1, float a1,
            float r2, float g2, float b2, float a2)
        {
            XNAColor xna1 = new XNAColor(r1, g1, b1) * a1;
            XNAColor xna2 = new XNAColor(r2, g2, b2) * a2;

            ANXColor anx1 = new ANXColor(r1, g1, b1) * a1;
            ANXColor anx2 = new ANXColor(r2, g2, b2) * a2;

            bool xna = xna1 == xna2;
            bool anx = anx1 == anx2;

            if (xna == anx)
            {
                Assert.Pass("EqualsOperator passed");
            }
            else
            {
                Assert.Fail(String.Format("EqualsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("eightfloats")]
        public void UnequalOperator(
            float r1, float g1, float b1, float a1,
            float r2, float g2, float b2, float a2)
        {
            XNAColor xna1 = new XNAColor(r1, g1, b1) * a1;
            XNAColor xna2 = new XNAColor(r2, g2, b2) * a2;

            ANXColor anx1 = new ANXColor(r1, g1, b1) * a1;
            ANXColor anx2 = new ANXColor(r2, g2, b2) * a2;

            bool xna = xna1 != xna2;
            bool anx = anx1 != anx2;

            if (xna == anx)
            {
                Assert.Pass("UnequalsOperator passed");
            }
            else
            {
                Assert.Fail(String.Format("EqualsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("fourfloats")]
        public void FromNonPremultipliedIntStatic(float r, float g, float b, float a)
        {
            XNAColor xna = XNAColor.FromNonPremultiplied((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(a * 255));

            ANXColor anx = ANXColor.FromNonPremultiplied((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(a * 255));

            ConvertEquals(xna, anx, "FromNonPremultipliedIntStatic");
        }

        [Test, TestCaseSource("fourfloats")]
        public void FromNonPremultipliedVector4Static(float r, float g, float b, float a)
        {
            XNAColor xna = XNAColor.FromNonPremultiplied(new Microsoft.Xna.Framework.Vector4(r, g, b, a));

            ANXColor anx = ANXColor.FromNonPremultiplied(new ANX.Framework.Vector4(r, g, b, a));

            ConvertEquals(xna, anx, "FromNonPremultipliedVector4Static");
        }

        [Test, TestCaseSource("ninefloats")]
        public void LerpStatic(
            float r1, float g1, float b1, float a1,
            float r2, float g2, float b2, float a2, float amount)
        {
            XNAColor xna1 = new XNAColor(r1, g1, b1) * a1;
            XNAColor xna2 = new XNAColor(r2, g2, b2) * a2;

            ANXColor anx1 = new ANXColor(r1, g1, b1) * a1;
            ANXColor anx2 = new ANXColor(r2, g2, b2) * a2;

            XNAColor xna = XNAColor.Lerp(xna1, xna2, amount);
            ANXColor anx = ANXColor.Lerp(anx1, anx2, amount);

            ConvertEquals(xna, anx, "LerpStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void MultiplyStatic(float r, float g, float b, float a, float scale, float x, float y, float z)
        {
            XNAColor xna = new XNAColor(r, g, b) * a;

            ANXColor anx = new ANXColor(r, g, b) * a;

            XNAColor.Multiply(xna, scale);
            ANXColor.Multiply(anx, scale);

            ConvertEquals(xna, anx, "MultiplyStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void MultiplyOperator(float r, float g, float b, float a, float scale, float x, float y, float z)
        {
            XNAColor xna = new XNAColor(r, g, b) * a;

            ANXColor anx = new ANXColor(r, g, b) * a;

            xna *= scale;
            anx *= scale;

            ConvertEquals(xna, anx, "MultiplyOperator");
        }

        [Test, TestCaseSource("fourfloats")]
        public void ToVector3(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            Microsoft.Xna.Framework.Vector3 xna = xnaColor.ToVector3();
            ANX.Framework.Vector3 anx = anxColor.ToVector3();

            if (xna.Equals(anx))
            {
                Assert.Pass("ToVector3 passed");
            }
            else
            {
                Assert.Fail(String.Format("ToVector3 failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("fourfloats")]
        public void ToVector4(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            Microsoft.Xna.Framework.Vector4 xna = xnaColor.ToVector4();
            ANX.Framework.Vector4 anx = anxColor.ToVector4();

            if (xna.Equals(anx))
            {
                Assert.Pass("ToVector4 passed");
            }
            else
            {
                Assert.Fail(String.Format("ToVector4 failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        #endregion // Methods

        #region Properties

        [Test, TestCaseSource("fourfloats")]
        public void PackedValue(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            uint xna = xnaColor.PackedValue;
            uint anx = anxColor.PackedValue;

            if (xna.Equals(anx))
            {
                Assert.Pass("PackedValue passed");
            }
            else
            {
                Assert.Fail(String.Format("PackedValue failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("fourfloats")]
        public void R(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            byte xna = xnaColor.R;
            byte anx = anxColor.R;

            if (xna.Equals(anx))
            {
                Assert.Pass("R passed");
            }
            else
            {
                Assert.Fail(String.Format("R failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("fourfloats")]
        public void G(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            byte xna = xnaColor.G;
            byte anx = anxColor.G;

            if (xna.Equals(anx))
            {
                Assert.Pass("G passed");
            }
            else
            {
                Assert.Fail(String.Format("G failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("fourfloats")]
        public void B(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            byte xna = xnaColor.B;
            byte anx = anxColor.B;

            if (xna.Equals(anx))
            {
                Assert.Pass("B passed");
            }
            else
            {
                Assert.Fail(String.Format("B failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("fourfloats")]
        public void A(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            byte xna = xnaColor.A;
            byte anx = anxColor.A;

            if (xna.Equals(anx))
            {
                Assert.Pass("A passed");
            }
            else
            {
                Assert.Fail(String.Format("A failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        #endregion
        }
    }
}
