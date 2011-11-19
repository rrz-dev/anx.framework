#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using XNAVector2 = Microsoft.Xna.Framework.Vector2;
using ANXVector2 = ANX.Framework.Vector2;

using XNAMatrix = Microsoft.Xna.Framework.Matrix;
using ANXMatrix = ANX.Framework.Matrix;

using XNAQuaternion = Microsoft.Xna.Framework.Quaternion;
using ANXQuaternion = ANX.Framework.Quaternion;

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

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class Vector2Test
    {
        #region Testdata

        static object[] ninefloats =
        {
           // new object[] {Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat ,Vector2Test.RandomFloat},
           // new object[] {Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat ,Vector2Test.RandomFloat},
           // new object[] {Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat },
           // new object[] {Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat ,Vector2Test.RandomFloat},
           // new object[] {Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat },
   
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue },
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue }
        };

        static object[] twentytwoFloats =
        {
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
        };

        #endregion

        #region Test

        #region Properties

        [Test]
        public void One()
        {
            AssertHelper.ConvertEquals(XNAVector2.One, ANXVector2.One, "One");
        }

        [Test]
        public void UnitX()
        {
            AssertHelper.ConvertEquals(XNAVector2.UnitX, ANXVector2.UnitX, "UnitX");
        }

        [Test]
        public void UnitY()
        {
            AssertHelper.ConvertEquals(XNAVector2.UnitY, ANXVector2.UnitY, "UnitY");
        }

        [Test]
        public void Zero()
        {
            AssertHelper.ConvertEquals(XNAVector2.Zero, ANXVector2.Zero, "Zero");
        }

        #endregion

        #region Constructors

        [Test, TestCaseSource("ninefloats")]
        public void contructor1(float x, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xnaR = new XNAVector2(x);

            ANXVector2 anxR = new ANXVector2(x);

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor1");
        }

        [Test, TestCaseSource("ninefloats")]
        public void contructor2(float x, float y, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xnaR = new XNAVector2(x, y);

            ANXVector2 anxR = new ANXVector2(x, y);

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor2");
        }

        #endregion

        #region Public Methods

        [Test, TestCaseSource("ninefloats")]
        public void Add(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = XNAVector2.Add(xna1, xna2);
            ANXVector2 anxR = ANXVector2.Add(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Add");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Barycentric(float x1, float y1, float x2, float y2, float x3, float y3, float amount1, float amount2, float nop)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);
            XNAVector2 xna3 = new XNAVector2(x3, y3);


            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);
            ANXVector2 anx3 = new ANXVector2(x3, y3);


            XNAVector2 xnaR = XNAVector2.Barycentric(xna1, xna2, xna3, amount1, amount2);
            ANXVector2 anxR = ANXVector2.Barycentric(anx1, anx2, anx3, amount1, amount2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Barycentric");
        }

        [Test, TestCaseSource("ninefloats")]
        public void CatmullRom(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, float amount)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);
            XNAVector2 xna3 = new XNAVector2(x3, y3);
            XNAVector2 xna4 = new XNAVector2(x4, y4);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);
            ANXVector2 anx3 = new ANXVector2(x3, y3);
            ANXVector2 anx4 = new ANXVector2(x4, y4);

            XNAVector2 xnaR = XNAVector2.CatmullRom(xna1, xna2, xna3, xna4, amount);
            ANXVector2 anxR = ANXVector2.CatmullRom(anx1, anx2, anx3, anx4, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "CatmullRom");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Clamp(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, float amount)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);
            XNAVector2 xna3 = new XNAVector2(x3, y3);
            XNAVector2 xna4 = new XNAVector2(x4, y4);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);
            ANXVector2 anx3 = new ANXVector2(x3, y3);
            ANXVector2 anx4 = new ANXVector2(x4, y4);

            XNAVector2 xnaR = XNAVector2.Clamp(xna1, xna2, xna3);
            ANXVector2 anxR = ANXVector2.Clamp(anx1, anx2, anx3);

            AssertHelper.ConvertEquals(xnaR, anxR, "Clamp");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Distance(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            float xnaR = XNAVector2.Distance(xna1, xna2);
            float anxR = ANXVector2.Distance(anx1, anx2);

            Assert.AreEqual(xnaR, anxR);
        }

        [Test, TestCaseSource("ninefloats")]
        public void DistanceSquared(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            float xnaR = XNAVector2.DistanceSquared(xna1, xna2);
            float anxR = ANXVector2.DistanceSquared(anx1, anx2);

            Assert.AreEqual(xnaR, anxR);
        }

        [Test, TestCaseSource("ninefloats")]
        public void DivideVectorDivider(float x1, float y1, float divider, float nop1, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);


            ANXVector2 anx1 = new ANXVector2(x1, y1);


            XNAVector2 xnaR = XNAVector2.Divide(xna1, divider);
            ANXVector2 anxR = ANXVector2.Divide(anx1, divider);

            AssertHelper.ConvertEquals(xnaR, anxR, "DivideVectorDivider");
        }

        [Test, TestCaseSource("ninefloats")]
        public void DivideVectorVector(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = XNAVector2.Divide(xna1, xna2);
            ANXVector2 anxR = ANXVector2.Divide(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "DivideVectorVector");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Dot(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            float xnaR = XNAVector2.Dot(xna1, xna2);
            float anxR = ANXVector2.Dot(anx1, anx2);

            Assert.AreEqual(xnaR, anxR);
        }

        [Test, TestCaseSource("ninefloats")]
        public void GetHashCode(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);


            ANXVector2 anx1 = new ANXVector2(x1, y1);


            float xnaR = xna1.GetHashCode();
            float anxR = anx1.GetHashCode();

            Assert.AreEqual(xnaR, anxR);
        }

        [Test, TestCaseSource("ninefloats")]
        public void Hermite(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, float amount)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);
            XNAVector2 xna3 = new XNAVector2(x3, y3);
            XNAVector2 xna4 = new XNAVector2(x4, y4);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);
            ANXVector2 anx3 = new ANXVector2(x3, y3);
            ANXVector2 anx4 = new ANXVector2(x4, y4);

            XNAVector2 xnaR = XNAVector2.Hermite(xna1, xna2, xna3, xna4, amount);
            ANXVector2 anxR = ANXVector2.Hermite(anx1, anx2, anx3, anx4, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "Hermite");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Length(float x1, float y1, float nop1, float nop2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            ANXVector2 anx1 = new ANXVector2(x1, y1);
            Assert.AreEqual(anx1.Length(), xna1.Length());
        }

        [Test, TestCaseSource("ninefloats")]
        public void LengthSquared(float x1, float y1, float nop1, float nop2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            ANXVector2 anx1 = new ANXVector2(x1, y1);
            Assert.AreEqual(anx1.LengthSquared(), xna1.LengthSquared());
        }

        [Test, TestCaseSource("ninefloats")]
        public void Lerp(float x1, float y1, float x2, float y2, float amount, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = XNAVector2.Lerp(xna1, xna2, amount);
            ANXVector2 anxR = ANXVector2.Lerp(anx1, anx2, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "Lerp");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Max(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = XNAVector2.Max(xna1, xna2);
            ANXVector2 anxR = ANXVector2.Max(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Max");
        }


        [Test, TestCaseSource("ninefloats")]
        public void Min(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = XNAVector2.Min(xna1, xna2);
            ANXVector2 anxR = ANXVector2.Min(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Min");
        }

        [Test, TestCaseSource("ninefloats")]
        public void MultiplyVectorFloat(float x1, float y1, float scale, float nop, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);

            ANXVector2 anx1 = new ANXVector2(x1, y1);

            XNAVector2 xnaR = XNAVector2.Multiply(xna1, scale);
            ANXVector2 anxR = ANXVector2.Multiply(anx1, scale);

            AssertHelper.ConvertEquals(xnaR, anxR, "MultiplyVectorFloat");
        }

        [Test, TestCaseSource("ninefloats")]
        public void MultiplyVectorVector(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = XNAVector2.Multiply(xna1, xna2);
            ANXVector2 anxR = ANXVector2.Multiply(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "MultiplyVectorVector");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Negate(float x1, float y1, float nop1, float nop2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);

            ANXVector2 anx1 = new ANXVector2(x1, y1);

            XNAVector2 xnaR = XNAVector2.Negate(xna1);
            ANXVector2 anxR = ANXVector2.Negate(anx1);

            AssertHelper.ConvertEquals(xnaR, anxR, "Negate");
        }

        [Test, TestCaseSource("ninefloats")]
        public void NormalizeInstanz(float x1, float y1, float nop1, float nop2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xnaR = new XNAVector2(x1, y1);

            ANXVector2 anxR = new ANXVector2(x1, y1);

            xnaR.Normalize();
            anxR.Normalize();

            AssertHelper.ConvertEquals(xnaR, anxR, "NormalizeInstanz");
        }

        [Test, TestCaseSource("ninefloats")]
        public void NormalizeStatic(float x1, float y1, float nop1, float nop2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);

            ANXVector2 anx1 = new ANXVector2(x1, y1);

            XNAVector2 xnaR = XNAVector2.Normalize(xna1);
            ANXVector2 anxR = ANXVector2.Normalize(anx1);

            AssertHelper.ConvertEquals(xnaR, anxR, "NormalizeStatic");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Reflect(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = XNAVector2.Reflect(xna1, xna2);
            ANXVector2 anxR = ANXVector2.Reflect(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Reflect");
        }

        [Test, TestCaseSource("ninefloats")]
        public void SmoothStep(float x1, float y1, float x2, float y2, float amount, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = XNAVector2.SmoothStep(xna1, xna2, amount);
            ANXVector2 anxR = ANXVector2.SmoothStep(anx1, anx2, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "SmoothStep");
        }

        [Test, TestCaseSource("ninefloats")]
        public void Subtract(float x1, float y1, float x2, float y2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = XNAVector2.Subtract(xna1, xna2);
            ANXVector2 anxR = ANXVector2.Subtract(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Subtract");
        }

        [Test, TestCaseSource("ninefloats")]
        public void ToString(float x1, float y1, float nop1, float nop2, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);

            ANXVector2 anx1 = new ANXVector2(x1, y1);

            String xnaR = xna1.ToString();
            String anxR = anx1.ToString();

            Assert.AreEqual(xnaR, anxR);
        }

        [Test, TestCaseSource("ninefloats")]
        public void StaticTransform(float x1, float y1, float x2, float y2, float nop1, float nop2, float nop3, float nop4, float nop5)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xnaResult;
            XNAMatrix xnaMatrix = XNAMatrix.CreateRotationX(nop1) * XNAMatrix.CreateRotationY(nop2) * XNAMatrix.CreateRotationZ(nop3) * XNAMatrix.CreateTranslation(nop4, nop5, nop1);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anxResult;
            ANXMatrix anxMatrix = ANXMatrix.CreateRotationX(nop1) * ANXMatrix.CreateRotationY(nop2) * ANXMatrix.CreateRotationZ(nop3) * ANXMatrix.CreateTranslation(nop4, nop5, nop1);

            XNAVector2.Transform(ref xna1, ref xnaMatrix, out xnaResult);
            ANXVector2.Transform(ref anx1, ref anxMatrix, out anxResult);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "StaticTransform");
        }

        [Test, TestCaseSource("ninefloats")]
        public void StaticTransform2(float x1, float y1, float x2, float y2, float nop1, float nop2, float nop3, float nop4, float nop5)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAMatrix xnaMatrix = XNAMatrix.CreateRotationX(nop1) * XNAMatrix.CreateRotationY(nop2) * XNAMatrix.CreateRotationZ(nop3) * XNAMatrix.CreateTranslation(nop4, nop5, nop1);
            XNAVector2 xnaResult = XNAVector2.Transform(xna1, xnaMatrix);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXMatrix anxMatrix = ANXMatrix.CreateRotationX(nop1) * ANXMatrix.CreateRotationY(nop2) * ANXMatrix.CreateRotationZ(nop3) * ANXMatrix.CreateTranslation(nop4, nop5, nop1);
            ANXVector2 anxResult = ANXVector2.Transform(anx1, anxMatrix);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "StaticTransform2");
        }

        [Test, TestCaseSource("ninefloats")]
        public void StaticTransform3_ANXonly(float x1, float y1, float x2, float y2, float nop1, float nop2, float nop3, float nop4, float nop5)
        {
            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXMatrix anxMatrix = ANXMatrix.CreateRotationX(nop1) * ANXMatrix.CreateRotationY(nop2) * ANXMatrix.CreateRotationZ(nop3) * ANXMatrix.CreateTranslation(nop4, nop5, nop1);

            ANXVector2 anxResult1 = ANXVector2.Transform(anx1, anxMatrix);
            ANXVector2 anxResult2;

            Vector2.Transform(ref anx1, ref anxMatrix, out anxResult2);

            AssertHelper.ConvertEquals(anxResult1, anxResult2, "StaticTransform3_ANXonly");
        }

/*
        public static Vector2 Transform(Vector2 position, Matrix matrix)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector2 result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static Vector2 Transform(Vector2 value, Quaternion rotation)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector2 result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void Transform(Vector2[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2[] destinationArray, int destinationIndex, int length)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void Transform(Vector2[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector2[] destinationArray, int destinationIndex, int length)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void Transform(Vector2[] sourceArray, ref Matrix matrix, Vector2[] destinationArray)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void Transform(Vector2[] sourceArray, ref Quaternion rotation, Vector2[] destinationArray)
        {
            throw new Exception("method has not yet been implemented");
        }
        #endregion

*/

        #endregion

        #region Operators
        [Test, TestCaseSource("ninefloats")]
        public void AddOperator(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = xna1 + xna2;
            ANXVector2 anxR = anx1 + anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "AddOperator");
        }

        [Test, TestCaseSource("ninefloats")]
        public void SubtractOperator(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = xna1 - xna2;
            ANXVector2 anxR = anx1 - anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "SubtractOperator");
        }

        [Test, TestCaseSource("ninefloats")]
        public void EqualsOperator(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            bool xnaR = xna1 == xna2;
            bool anxR = anx1 == anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "EqualsOperator");
        }

        [Test, TestCaseSource("ninefloats")]
        public void UnequalsOperator(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            bool xnaR = xna1 != xna2;
            bool anxR = anx1 != anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "UnequalsOperator");
        }

        [Test, TestCaseSource("ninefloats")]
        public void MultiplyOperatorFloat(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float value)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);

            ANXVector2 anx1 = new ANXVector2(x1, y1);

            XNAVector2 xnaR = xna1 * value;
            ANXVector2 anxR = anx1 * value;

            AssertHelper.ConvertEquals(xnaR, anxR, "MultiplyOperatorFloat");
        }

        [Test, TestCaseSource("ninefloats")]
        public void MultiplyOperatorVector2(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = xna1 * xna2;
            ANXVector2 anxR = anx1 * anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "MultiplyOperatorVector2");
        }

        [Test, TestCaseSource("ninefloats")]
        public void DivideOperatorFloat(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float value)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);

            ANXVector2 anx1 = new ANXVector2(x1, y1);

            XNAVector2 xnaR = xna1 / value;
            ANXVector2 anxR = anx1 / value;

            AssertHelper.ConvertEquals(xnaR, anxR, "DivideOperatorFloat");
        }

        [Test, TestCaseSource("ninefloats")]
        public void DivideOperatorVector2(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1)
        {
            XNAVector2 xna1 = new XNAVector2(x1, y1);
            XNAVector2 xna2 = new XNAVector2(x2, y2);

            ANXVector2 anx1 = new ANXVector2(x1, y1);
            ANXVector2 anx2 = new ANXVector2(x2, y2);

            XNAVector2 xnaR = xna1 / xna2;
            ANXVector2 anxR = anx1 / anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "DivideOperatorVector2");
        }
        #endregion
        
        #region Transformations
        [Test, TestCaseSource("twentytwoFloats")]
        public void TransformStaticMatrix(
            float x, float y, float z,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2)
        {
            XNAVector2 xnaVector = new XNAVector2(x, y);
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector2 anxVector = new ANXVector2(x, y);
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector2 xna = XNAVector2.Transform(xnaVector, xnaMatrix);
            ANXVector2 anx = ANXVector2.Transform(anxVector, anxMatrix);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticMatrix");
        }

        [Test, TestCaseSource("twentytwoFloats")]
        public void TransformStaticQuaternion(
            float x, float y, float z,
            float xQ, float yQ, float zQ, float wQ,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14)
        {
            XNAVector2 xnaVector = new XNAVector2(x, y);
            XNAQuaternion xnaQuaternion = new XNAQuaternion(xQ, yQ, zQ, wQ);

            ANXVector2 anxVector = new ANXVector2(x, y);
            ANXQuaternion anxQuaternion = new ANXQuaternion(xQ, yQ, zQ, wQ);

            XNAVector2 xna = XNAVector2.Transform(xnaVector, xnaQuaternion);
            ANXVector2 anx = ANXVector2.Transform(anxVector, anxQuaternion);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticQuaternion");
        }

        [Test, TestCaseSource("twentytwoFloats")]
        public static void TransformStaticMatrixToDestination(
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector2[] xnaVector = new XNAVector2[] { new XNAVector2(x1, y1), new XNAVector2(x2, y2) };
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector2[] anxVector = new ANXVector2[] { new ANXVector2(x1, y1), new ANXVector2(x2, y2) };
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector2[] xna = new XNAVector2[2];
            XNAVector2.Transform(xnaVector, ref xnaMatrix, xna);
            ANXVector2[] anx = new ANXVector2[2];
            ANXVector2.Transform(anxVector, ref anxMatrix, anx);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticMatrixToDestination");
        }

        [Test, TestCaseSource("twentytwoFloats")]
        public static void TransformStaticQuaternionToDestination(
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float xQ, float yQ, float zQ, float wQ,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11)
        {
            XNAVector2[] xnaVector = new XNAVector2[] { new XNAVector2(x1, y1), new XNAVector2(x2, y2) };
            XNAQuaternion xnaQuaternion = new XNAQuaternion(xQ, yQ, zQ, wQ);

            ANXVector2[] anxVector = new ANXVector2[] { new ANXVector2(x1, y1), new ANXVector2(x2, y2) };
            ANXQuaternion anxQuaternion = new ANXQuaternion(xQ, yQ, zQ, wQ);

            XNAVector2[] xna = new XNAVector2[2];
            XNAVector2.Transform(xnaVector, ref xnaQuaternion, xna);
            ANXVector2[] anx = new ANXVector2[2];
            ANXVector2.Transform(anxVector, ref anxQuaternion, anx);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticQuaternionToDestination");
        }

        [Test, TestCaseSource("twentytwoFloats")]
        public static void TransformStaticMatrixToDestinationWithIndex(
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector2[] xnaVector = new XNAVector2[] { new XNAVector2(x1, y1), new XNAVector2(x2, y2) };
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector2[] anxVector = new ANXVector2[] { new ANXVector2(x1, y1), new ANXVector2(x2, y2) };
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector2[] xna = new XNAVector2[2];
            XNAVector2.Transform(xnaVector, 1, ref xnaMatrix, xna, 1, 1);
            ANXVector2[] anx = new ANXVector2[2];
            ANXVector2.Transform(anxVector, 1, ref anxMatrix, anx, 1, 1);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticMatrixToDestinationWithIndex");
        }

        [Test, TestCaseSource("twentytwoFloats")]
        public static void TransformStaticQuaternionToDestinationWithIndex(
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float xQ, float yQ, float zQ, float wQ,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11)
        {
            XNAVector2[] xnaVector = new XNAVector2[] { new XNAVector2(x1, y1), new XNAVector2(x2, y2) };
            XNAQuaternion xnaQuaternion = new XNAQuaternion(xQ, yQ, zQ, wQ);

            ANXVector2[] anxVector = new ANXVector2[] { new ANXVector2(x1, y1), new ANXVector2(x2, y2) };
            ANXQuaternion anxQuaternion = new ANXQuaternion(xQ, yQ, zQ, wQ);

            XNAVector2[] xna = new XNAVector2[2];
            XNAVector2.Transform(xnaVector, 1, ref xnaQuaternion, xna, 1, 1);
            ANXVector2[] anx = new ANXVector2[2];
            ANXVector2.Transform(anxVector, 1, ref anxQuaternion, anx, 1, 1);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticQuaternionToDestinationWithIndex");
        }

        [Test, TestCaseSource("twentytwoFloats")]
        public void TransformNormalStatic(
            float x, float y, float z,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2)
        {
            XNAVector2 xnaVector = new XNAVector2(x, y);
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector2 anxVector = new ANXVector2(x, y);
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector2 xna = XNAVector2.TransformNormal(xnaVector, xnaMatrix);
            ANXVector2 anx = ANXVector2.TransformNormal(anxVector, anxMatrix);

            AssertHelper.ConvertEquals(xna, anx, "TransformNormalStatic");
        }

        [Test, TestCaseSource("twentytwoFloats")]
        public void TransformNormalStaticToDestination(
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector2[] xnaVector = new XNAVector2[] { new XNAVector2(x1, y1), new XNAVector2(x2, y2) };
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector2[] anxVector = new ANXVector2[] { new ANXVector2(x1, y1), new ANXVector2(x2, y2) };
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector2[] xna = new XNAVector2[2];
            XNAVector2.TransformNormal(xnaVector, ref xnaMatrix, xna);
            ANXVector2[] anx = new ANXVector2[2];
            ANXVector2.TransformNormal(anxVector, ref anxMatrix, anx);

            AssertHelper.ConvertEquals(xna, anx, "TransformNormalStaticToDestination");
        }

        [Test, TestCaseSource("twentytwoFloats")]
        public void TransformNormalStaticToDestinationWithIndex(
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector2[] xnaVector = new XNAVector2[] { new XNAVector2(x1, y1), new XNAVector2(x2, y2) };
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector2[] anxVector = new ANXVector2[] { new ANXVector2(x1, y1), new ANXVector2(x2, y2) };
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector2[] xna = new XNAVector2[2];
            XNAVector2.TransformNormal(xnaVector, 1, ref xnaMatrix, xna, 1, 1);
            ANXVector2[] anx = new ANXVector2[2];
            ANXVector2.TransformNormal(anxVector, 1, ref anxMatrix, anx, 1, 1);

            AssertHelper.ConvertEquals(xna, anx, "TransformNormalStaticToDestinationWithIndex");
        }
        #endregion

        #endregion

        //TODO: transform, transform normal operations

    }
}
