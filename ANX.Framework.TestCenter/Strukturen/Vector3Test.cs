#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XNAVector2 = Microsoft.Xna.Framework.Vector2;
using ANXVector2 = ANX.Framework.Vector2;
using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXVector3 = ANX.Framework.Vector3;

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

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class Vector3Test
    {
        #region Testdata

        static object[] thirteenFloats =
        {
           // new object[] {Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat ,Vector2Test.RandomFloat},
           // new object[] {Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat ,Vector2Test.RandomFloat},
           // new object[] {Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat },
           // new object[] {Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat ,Vector2Test.RandomFloat},
           // new object[] {Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat,Vector2Test.RandomFloat },
   
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue}
        };

        #endregion

        #region Tests

        #region properties

        [Test]
        public void One()
        {
            AssertHelper.ConvertEquals(XNAVector3.One, ANXVector3.One, "One");
        }

        [Test]
        public void Zero()
        {
            AssertHelper.ConvertEquals(XNAVector3.Zero, ANXVector3.Zero, "Zero");
        }

        [Test]
        public void Up()
        {
            AssertHelper.ConvertEquals(XNAVector3.Up, ANXVector3.Up, "Up");
        }

        [Test]
        public void Down()
        {
            AssertHelper.ConvertEquals(XNAVector3.Down, ANXVector3.Down, "Down");
        }

        [Test]
        public void Backward()
        {
            AssertHelper.ConvertEquals(XNAVector3.Backward, ANXVector3.Backward, "Backward");
        }

        [Test]
        public void Forward()
        {
            AssertHelper.ConvertEquals(XNAVector3.Forward, ANXVector3.Forward, "Forward");
        }

        [Test]
        public void Left()
        {
            AssertHelper.ConvertEquals(XNAVector3.Left, ANXVector3.Left, "Left");
        }

        [Test]
        public void Right()
        {
            AssertHelper.ConvertEquals(XNAVector3.Right, ANXVector3.Right, "Right");
        }

        [Test]
        public void UnitX()
        {
            AssertHelper.ConvertEquals(XNAVector3.UnitX, ANXVector3.UnitX, "UnitX");
        }

        [Test]
        public void UnitY()
        {
            AssertHelper.ConvertEquals(XNAVector3.UnitY, ANXVector3.UnitY, "UnitY");
        }

        [Test]
        public void UnitZ()
        {
            AssertHelper.ConvertEquals(XNAVector3.UnitZ, ANXVector3.UnitZ, "UnitZ");
        }

        #endregion

        #region constructors

        [Test]
        public void constructor1()
        {
            XNAVector3 xnaR = new XNAVector3();

            ANXVector3 anxR = new ANXVector3();

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor1");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void constructor2(float x, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12)
        {
            XNAVector3 xnaR = new XNAVector3(x);

            ANXVector3 anxR = new ANXVector3(x);

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor2");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void constructor3(float x, float y, float z, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10)
        {
            XNAVector2 xnaV2 = new XNAVector2(x, y);
            XNAVector3 xnaR = new XNAVector3(xnaV2, z);

            ANXVector2 anxV2 = new ANXVector2(x, y);
            ANXVector3 anxR = new ANXVector3(anxV2, z);

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor3");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void constructor4(float x, float y, float z, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10)
        {
            XNAVector3 xnaR = new XNAVector3(x, y, z);

            ANXVector3 anxR = new ANXVector3(x, y, z);

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor4");
        }

        #endregion

        #region public methods

        [Test, TestCaseSource("thirteenFloats")]
        public void Add(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            XNAVector3 xnaR = XNAVector3.Add(xna1, xna2);
            ANXVector3 anxR = ANXVector3.Add(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Add");
        }


        [Test, TestCaseSource("thirteenFloats")]
        public void Barycentric(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3, float amount1, float amount2, float nop1, float nop2)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);
            XNAVector3 xna3 = new XNAVector3(x3, y3, z3);


            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);
            ANXVector3 anx3 = new ANXVector3(x3, y3, z3);


            XNAVector3 xnaR = XNAVector3.Barycentric(xna1, xna2, xna3, amount1, amount2);
            ANXVector3 anxR = ANXVector3.Barycentric(anx1, anx2, anx3, amount1, amount2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Barycentric");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void CatmullRom(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3, float x4, float y4, float z4, float amount)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);
            XNAVector3 xna3 = new XNAVector3(x3, y3, z3);
            XNAVector3 xna4 = new XNAVector3(x4, y4, z4);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);
            ANXVector3 anx3 = new ANXVector3(x3, y3, z3);
            ANXVector3 anx4 = new ANXVector3(x4, y4, z4);

            XNAVector3 xnaR = XNAVector3.CatmullRom(xna1, xna2, xna3, xna4, amount);
            ANXVector3 anxR = ANXVector3.CatmullRom(anx1, anx2, anx3, anx4, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "CatmullRom");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Clamp(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3, float amount, float nop1, float nop2, float nop3)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);
            XNAVector3 xna3 = new XNAVector3(x3, y3, z3);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);
            ANXVector3 anx3 = new ANXVector3(x3, y3, z3);

            XNAVector3 xnaR = XNAVector3.Clamp(xna1, xna2, xna3);
            ANXVector3 anxR = ANXVector3.Clamp(anx1, anx2, anx3);

            AssertHelper.ConvertEquals(xnaR, anxR, "Clamp");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Cross(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            XNAVector3 xnaR = XNAVector3.Cross(xna1, xna2);
            ANXVector3 anxR = ANXVector3.Cross(anx2, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Cross");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Distance(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            float xnaR = XNAVector3.Distance(xna1, xna2);
            float anxR = ANXVector3.Distance(anx1, anx2);

            Assert.AreEqual(xnaR, anxR);
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void DistanceSquared(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            float xnaR = XNAVector3.DistanceSquared(xna1, xna2);
            float anxR = ANXVector3.DistanceSquared(anx1, anx2);

            Assert.AreEqual(xnaR, anxR);
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void DivideVectorDivider(float x1, float y1, float z1, float divider, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);

            XNAVector3 xnaR = XNAVector3.Divide(xna1, divider);
            ANXVector3 anxR = ANXVector3.Divide(anx1, divider);

            AssertHelper.ConvertEquals(xnaR, anxR, "DivideVectorDivider");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void DivideVectorVector(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            XNAVector3 xnaR = XNAVector3.Divide(xna1, xna2);
            ANXVector3 anxR = ANXVector3.Divide(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "DivideVectorVector");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Dot(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            float xnaR = XNAVector3.Dot(xna1, xna2);
            float anxR = ANXVector3.Dot(anx1, anx2);

            Assert.AreEqual(xnaR, anxR);
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void GetHashCode(float x1, float y1, float z1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);

            float xnaR = xna1.GetHashCode();
            float anxR = anx1.GetHashCode();

            Assert.AreEqual(xnaR, anxR);

        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Hermite(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3, float x4, float y4, float z4, float amount)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);
            XNAVector3 xna3 = new XNAVector3(x3, y3, z3);
            XNAVector3 xna4 = new XNAVector3(x4, y4, z4);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);
            ANXVector3 anx3 = new ANXVector3(x3, y3, z3);
            ANXVector3 anx4 = new ANXVector3(x4, y4, z4);

            XNAVector3 xnaR = XNAVector3.Hermite(xna1, xna2, xna3, xna4, amount);
            ANXVector3 anxR = ANXVector3.Hermite(anx1, anx2, anx3, anx4, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "Hermite");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Length(float x1, float y1, float z1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            Assert.AreEqual(anx1.Length(), xna1.Length());
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void LengthSquared(float x1, float y1, float z1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            Assert.AreEqual(anx1.LengthSquared(), xna1.LengthSquared());
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Lerp(float x1, float y1, float z1, float x2, float y2, float z2, float amount, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            XNAVector3 xnaR = XNAVector3.Lerp(xna1, xna2, amount);
            ANXVector3 anxR = ANXVector3.Lerp(anx1, anx2, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "Lerp");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Max(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            XNAVector3 xnaR = XNAVector3.Max(xna1, xna2);
            ANXVector3 anxR = ANXVector3.Max(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Max");
        }


        [Test, TestCaseSource("thirteenFloats")]
        public void Min(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            XNAVector3 xnaR = XNAVector3.Min(xna1, xna2);
            ANXVector3 anxR = ANXVector3.Min(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Min");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void MultiplyVectorFloat(float x1, float y1, float z1, float scale, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);


            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);


            XNAVector3 xnaR = XNAVector3.Multiply(xna1, scale);
            ANXVector3 anxR = ANXVector3.Multiply(anx1, scale);

            AssertHelper.ConvertEquals(xnaR, anxR, "MultiplyVectorFloat");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void MultiplyVectorVector(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            XNAVector3 xnaR = XNAVector3.Multiply(xna1, xna2);
            ANXVector3 anxR = ANXVector3.Multiply(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "MultiplyVectorVector");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Negate(float x1, float y1, float z1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);

            XNAVector3 xnaR = XNAVector3.Negate(xna1);
            ANXVector3 anxR = ANXVector3.Negate(anx1);

            AssertHelper.ConvertEquals(xnaR, anxR, "Negate");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void NormalizeInstanz(float x1, float y1, float z1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10)
        {
            XNAVector3 xnaR = new XNAVector3(x1, y1, z1);

            ANXVector3 anxR = new ANXVector3(x1, y1, z1);

            xnaR.Normalize();
            anxR.Normalize();

            AssertHelper.ConvertEquals(xnaR, anxR, "NormalizeInstanz");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void NormalizeStatic(float x1, float y1, float z1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);

            XNAVector3 xnaR = XNAVector3.Normalize(xna1);
            ANXVector3 anxR = ANXVector3.Normalize(anx1);

            AssertHelper.ConvertEquals(xnaR, anxR, "NormalizeStatic");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Reflect(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            XNAVector3 xnaR = XNAVector3.Reflect(xna1, xna2);
            ANXVector3 anxR = ANXVector3.Reflect(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Reflect");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void SmoothStep(float x1, float y1, float z1, float x2, float y2, float z2, float amount, float nop1, float nop2, float nop3, float nop4, float nop5)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            XNAVector3 xnaR = XNAVector3.SmoothStep(xna1, xna2, amount);
            ANXVector3 anxR = ANXVector3.SmoothStep(anx1, anx2, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "SmoothStep");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void Subtract(float x1, float y1, float z1, float x2, float y2, float z2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);
            XNAVector3 xna2 = new XNAVector3(x2, y2, z2);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);
            ANXVector3 anx2 = new ANXVector3(x2, y2, z2);

            XNAVector3 xnaR = XNAVector3.Subtract(xna1, xna2);
            ANXVector3 anxR = ANXVector3.Subtract(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Subtract");
        }

        [Test, TestCaseSource("thirteenFloats")]
        public void ToString(float x1, float y1, float z1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10)
        {
            XNAVector3 xna1 = new XNAVector3(x1, y1, z1);

            ANXVector3 anx1 = new ANXVector3(x1, y1, z1);

            String xnaR = xna1.ToString();
            String anxR = anx1.ToString();

            Assert.AreEqual(xnaR, anxR);
        }

        #endregion

        #endregion

        //TODO: transform, transform normal operations
    }
}
