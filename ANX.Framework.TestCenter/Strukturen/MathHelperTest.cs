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

using XNAMath = Microsoft.Xna.Framework.MathHelper;
using ANXMath = ANX.Framework.MathHelper;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class MathHelperTest
    {
        static object[] onefloat =
        {
            float.NegativeInfinity,
            float.MinValue
            -3,2f,
            -1,6f,
            0,
            1,6f,
            3,2f,
            float.MaxValue ,
            float.PositiveInfinity,
            DataFactory.RandomFloat,
            DataFactory.RandomFloat,
            DataFactory.RandomFloat,
        };
        static object[] twofloats =
        {
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat }
        };
        static object[] threefloats =
        {
            new object[] { 0 ,0, 0 },
            new object[] { 0, 0, 1 },
            new object[] { 0, 1, 0 },
            new object[] { 1, 0, 0 },
            new object[] { 0, 1, 1 },
            new object[] { 1, 0, 1 },
            new object[] { 1, 1, 0 },
            new object[] { 3, 2, 1 },
            new object[] { 1, 2, 3 },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat }
        };
        static object[] fivefloats =
        {
            new object[] { 0 ,0, 0,0,0 },
            new object[] { 0, 0, 1,0,0 },
            new object[] { 0, 1, 0,0,0 },
            new object[] { 1, 0, 0,0,0 },
            new object[] { 0, 1, 1,0,0 },
            new object[] { 1, 0, 1,0,1 },
            new object[] { 1, 1, 0,0,1 },
            new object[] { 3, 2, 1,0,1 },
            new object[] { 1, 2, 3,0,1 },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat}
        };

        [TestCaseSource("fivefloats")]
        public void Barycentric(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            float xna = XNAMath.Barycentric(value1, tangent1, value2, tangent2, amount);
            float anx = ANXMath.Barycentric(value1, tangent1, value2, tangent2, amount);
            TestFloat(xna, anx, "Barycentric");
        }

        [TestCaseSource("fivefloats")]
        public void CatmullRom(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            float xna = XNAMath.CatmullRom(value1, tangent1, value2, tangent2, amount);
            float anx = ANXMath.CatmullRom(value1, tangent1, value2, tangent2, amount);
            TestFloat(xna, anx, "CatmullRom");
        }

        [Test, TestCaseSource("threefloats")]
        public void Clamp(float value1, float value2, float amount)
        {
            float xna = XNAMath.Clamp(value1, value2, amount);
            float anx = ANXMath.Clamp(value1, value2, amount);
            TestFloat(xna, anx, "Clamp");
        }

        [Test, TestCaseSource("threefloats")]
        public void SmoothStep(float value1, float value2, float amount)
        {
            float xna = XNAMath.SmoothStep(value1, value2, amount);
            float anx = ANXMath.SmoothStep(value1, value2, amount);
            TestFloat(xna, anx, "SmoothStep");
        }

        [Test, TestCaseSource("threefloats")]
        public void Lerp(float value1, float value2, float amount)
        {
            float xna = XNAMath.Lerp(value1, value2, amount);
            float anx = ANXMath.Lerp(value1, value2, amount);
            TestFloat(xna, anx, "Lerp");
        }

        [TestCaseSource("twofloats")]
        public void Distance(float min, float max)
        {
            float xna = XNAMath.Distance(min, max);
            float anx = ANXMath.Distance(min, max);
            TestFloat(xna, anx, "Distance");
        }

        [TestCaseSource("twofloats")]
        public void Max(float min, float max)
        {
            float xna = XNAMath.Max(min, max);
            float anx = ANXMath.Max(min, max);
            TestFloat(xna, anx, "Max");
        }

        [TestCaseSource("twofloats")]
        public void Min(float min, float max)
        {
            float xna = XNAMath.Min(min, max);
            float anx = ANXMath.Min(min, max);
            TestFloat(xna, anx, "Min");
        }

        [TestCaseSource("onefloat")]
        public void ToDegrees(float one)
        {
            float xna = XNAMath.ToDegrees(one);
            float anx = ANXMath.ToDegrees(one);
            TestFloat(xna, anx, "ToRadians");

        }

        [TestCaseSource("onefloat")]
        public void ToRadians(float one)
        {
            float xna = XNAMath.ToRadians(one);
            float anx = ANXMath.ToRadians(one);
            TestFloat(xna, anx, "ToRadians");

        }

        [TestCaseSource("onefloat")]
        public void WrapAngle(float one)
        {
            float xna = XNAMath.WrapAngle(one);
            float anx = ANXMath.WrapAngle(one);
            TestFloat(xna, anx, "WrapAngle");
        }

        [TestCaseSource("fivefloats")]
        public void Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            float xna = XNAMath.Hermite(value1, tangent1, value2, tangent2, amount);
            float anx = ANXMath.Hermite(value1, tangent1, value2, tangent2, amount);
            TestFloat(xna, anx, "Hermite");
        }

        private static void TestFloat(float xna, float anx, string funktion)
        {
            if (AssertHelper.CompareFloats(xna, anx, 0.000001f))
            {
                Assert.Pass(funktion+" passed");
            }
            else
            {
                if (xna.ToString() == anx.ToString())
                {
                    Assert.Pass(funktion+" passed");
                }

                Assert.Fail(String.Format(funktion + " failed: xna({0}) anx({1})", xna, anx));
            }
        }
    }
}
