#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XNAVector2 = Microsoft.Xna.Framework.Vector2;
using ANXVector2 = ANX.Framework.Vector2;
using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXVector3 = ANX.Framework.Vector3;
using XNAVector4 = Microsoft.Xna.Framework.Vector4;
using ANXVector4 = ANX.Framework.Vector4;
using XNAMatrix = Microsoft.Xna.Framework.Matrix;
using ANXMatrix = ANX.Framework.Matrix;
using XNAQuaternion = Microsoft.Xna.Framework.Quaternion;
using ANXQuaternion = ANX.Framework.Quaternion;

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
    class Vector4Test
    {
        #region Testdata

        static object[] fourFloats =
        {
            new object[] { DataFactory.RandomFloat, DataFactory.RandomFloat, DataFactory.RandomFloat, DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat, DataFactory.RandomFloat, DataFactory.RandomFloat, DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat, DataFactory.RandomFloat, DataFactory.RandomFloat, DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat, DataFactory.RandomFloat, DataFactory.RandomFloat, DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat, DataFactory.RandomFloat, DataFactory.RandomFloat, DataFactory.RandomFloat }
        };

        static object[] seventeenFloats =
        {
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue}
        };

        static object[] twentyfourFloats =
        {
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
           new object[] {DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue,DataFactory.RandomValue},
        };
        #endregion

        #region Tests

        #region Properties

        [Test]
        public void One()
        {
            AssertHelper.ConvertEquals(XNAVector4.One, ANXVector4.One, "One");
        }

        [Test]
        public void Zero()
        {
            AssertHelper.ConvertEquals(XNAVector4.Zero, ANXVector4.Zero, "Zero");
        }

        [Test]
        public void UnitX()
        {
            AssertHelper.ConvertEquals(XNAVector4.UnitX, ANXVector4.UnitX, "UnitX");
        }

        [Test]
        public void UnitY()
        {
            AssertHelper.ConvertEquals(XNAVector4.UnitY, ANXVector4.UnitY, "UnitY");
        }

        [Test]
        public void UnitZ()
        {
            AssertHelper.ConvertEquals(XNAVector4.UnitZ, ANXVector4.UnitZ, "UnitZ");
        }

        [Test]
        public void UnitW()
        {
            AssertHelper.ConvertEquals(XNAVector4.UnitW, ANXVector4.UnitW, "UnitW");
        }

        #endregion

        #region Constructors

        [Test]
        public void constructor1()
        {
            XNAVector4 xnaR = new XNAVector4();

            ANXVector4 anxR = new ANXVector4();

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor1");
        }

        [Test, TestCaseSource("fourFloats")]
        public void constructor2(float x, float nop1, float nop2, float nop3)
        {
            XNAVector4 xnaR = new XNAVector4(x);

            ANXVector4 anxR = new ANXVector4(x);

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor2");
        }

        [Test, TestCaseSource("fourFloats")]
        public void constructor3(float x, float y, float z, float w)
        {
            XNAVector3 xnaV3 = new XNAVector3(x, y, z);
            XNAVector4 xnaR = new XNAVector4(xnaV3, w);

            ANXVector3 anxV3 = new ANXVector3(x, y, z);
            ANXVector4 anxR = new ANXVector4(anxV3, w);

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor3");
        }

        [Test, TestCaseSource("fourFloats")]
        public void constructor4(float x, float y, float z, float w)
        {
            XNAVector2 xnaV2 = new XNAVector2(x, y);
            XNAVector4 xnaR = new XNAVector4(xnaV2, z, w);

            ANXVector2 anxV2 = new ANXVector2(x, y);
            ANXVector4 anxR = new ANXVector4(anxV2, z, w);

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor4");
        }

        [Test, TestCaseSource("fourFloats")]
        public void constructor5(float x, float y, float z, float w)
        {
            XNAVector4 xnaR = new XNAVector4(x, y, z, w);

            ANXVector4 anxR = new ANXVector4(x, y, z, w);

            AssertHelper.ConvertEquals(xnaR, anxR, "Constructor5");
        }

        #endregion

        #region Public Methods

        [Test, TestCaseSource("seventeenFloats")]
        public void Add(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = XNAVector4.Add(xna1, xna2);
            ANXVector4 anxR = ANXVector4.Add(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Add");
        }


        [Test, TestCaseSource("seventeenFloats")]
        public void Barycentric(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float x3, float y3, float z3, float w3, float amount1, float amount2, float nop1, float nop2, float nop3)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);
            XNAVector4 xna3 = new XNAVector4(x3, y3, z3, w3);


            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);
            ANXVector4 anx3 = new ANXVector4(x3, y3, z3, w3);


            XNAVector4 xnaR = XNAVector4.Barycentric(xna1, xna2, xna3, amount1, amount2);
            ANXVector4 anxR = ANXVector4.Barycentric(anx1, anx2, anx3, amount1, amount2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Barycentric");

        }

        [Test, TestCaseSource("seventeenFloats")]
        public void CatmullRom(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float x3, float y3, float z3, float w3, float x4, float y4, float z4, float w4, float amount)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);
            XNAVector4 xna3 = new XNAVector4(x3, y3, z3, w3);
            XNAVector4 xna4 = new XNAVector4(x4, y4, z4, w4);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);
            ANXVector4 anx3 = new ANXVector4(x3, y3, z3, w3);
            ANXVector4 anx4 = new ANXVector4(x4, y4, z4, w4);

            XNAVector4 xnaR = XNAVector4.CatmullRom(xna1, xna2, xna3, xna4, amount);
            ANXVector4 anxR = ANXVector4.CatmullRom(anx1, anx2, anx3, anx4, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "CatmullRom");

        }

        [Test, TestCaseSource("seventeenFloats")]
        public void Clamp(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float x3, float y3, float z3, float w3, float amount, float nop1, float nop2, float nop3, float nop4)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);
            XNAVector4 xna3 = new XNAVector4(x3, y3, z3, w3);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);
            ANXVector4 anx3 = new ANXVector4(x3, y3, z3, w3);

            XNAVector4 xnaR = XNAVector4.Clamp(xna1, xna2, xna3);
            ANXVector4 anxR = ANXVector4.Clamp(anx1, anx2, anx3);

            AssertHelper.ConvertEquals(xnaR, anxR, "Clamp");

        }

        [Test, TestCaseSource("seventeenFloats")]
        public void Distance(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            float xnaR = XNAVector4.Distance(xna1, xna2);
            float anxR = ANXVector4.Distance(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Distance");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void DistanceSquared(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            float xnaR = XNAVector4.DistanceSquared(xna1, xna2);
            float anxR = ANXVector4.DistanceSquared(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "DistanceSquared");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void DivideVectorDivider(float x1, float y1, float z1, float w1, float divider, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);

            XNAVector4 xnaR = XNAVector4.Divide(xna1, divider);
            ANXVector4 anxR = ANXVector4.Divide(anx1, divider);

            AssertHelper.ConvertEquals(xnaR, anxR, "DivideVectorDivider");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void DivideVectorVector(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = XNAVector4.Divide(xna1, xna2);
            ANXVector4 anxR = ANXVector4.Divide(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "DivideVectorVector");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void Dot(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            float xnaR = XNAVector4.Dot(xna1, xna2);
            float anxR = ANXVector4.Dot(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Dot");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void GetHashCode(float x1, float y1, float z1, float w1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);

            float xnaR = xna1.GetHashCode();
            float anxR = anx1.GetHashCode();

            AssertHelper.ConvertEquals(xnaR, anxR, "GetHashCode");

        }

        [Test, TestCaseSource("seventeenFloats")]
        public void Hermite(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float x3, float y3, float z3, float w3, float x4, float y4, float z4, float w4, float amount)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);
            XNAVector4 xna3 = new XNAVector4(x3, y3, z3, w3);
            XNAVector4 xna4 = new XNAVector4(x4, y4, z4, w4);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);
            ANXVector4 anx3 = new ANXVector4(x3, y3, z3, w3);
            ANXVector4 anx4 = new ANXVector4(x4, y4, z4, w4);

            XNAVector4 xnaR = XNAVector4.Hermite(xna1, xna2, xna3, xna4, amount);
            ANXVector4 anxR = ANXVector4.Hermite(anx1, anx2, anx3, anx4, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "Hermite");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void Length(float x1, float y1, float z1, float w1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            AssertHelper.ConvertEquals(anx1.Length(), xna1.Length(), "Length");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void LengthSquared(float x1, float y1, float z1, float w1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            AssertHelper.ConvertEquals(anx1.LengthSquared(), xna1.LengthSquared(), "LengthSquared");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void Lerp(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float amount, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = XNAVector4.Lerp(xna1, xna2, amount);
            ANXVector4 anxR = ANXVector4.Lerp(anx1, anx2, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "Lerp");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void Max(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = XNAVector4.Max(xna1, xna2);
            ANXVector4 anxR = ANXVector4.Max(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Max");
        }


        [Test, TestCaseSource("seventeenFloats")]
        public void Min(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = XNAVector4.Min(xna1, xna2);
            ANXVector4 anxR = ANXVector4.Min(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Min");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void MultiplyVectorFloat(float x1, float y1, float z1, float w1, float scale, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);

            XNAVector4 xnaR = XNAVector4.Multiply(xna1, scale);
            ANXVector4 anxR = ANXVector4.Multiply(anx1, scale);

            AssertHelper.ConvertEquals(xnaR, anxR, "MultiplyVectorFloat");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void MultiplyVectorVector(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = XNAVector4.Multiply(xna1, xna2);
            ANXVector4 anxR = ANXVector4.Multiply(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "MultiplyVectorVector");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void Negate(float x1, float y1, float z1, float w1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);

            XNAVector4 xnaR = XNAVector4.Negate(xna1);
            ANXVector4 anxR = ANXVector4.Negate(anx1);

            AssertHelper.ConvertEquals(xnaR, anxR, "Negate");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void NormalizeInstanz(float x1, float y1, float z1, float w1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13)
        {
            XNAVector4 xnaR = new XNAVector4(x1, y1, z1, w1);

            ANXVector4 anxR = new ANXVector4(x1, y1, z1, w1);

            xnaR.Normalize();
            anxR.Normalize();

            AssertHelper.ConvertEquals(xnaR, anxR, "NormalizeInstanz");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void NormalizeStatic(float x1, float y1, float z1, float w1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);

            XNAVector4 xnaR = XNAVector4.Normalize(xna1);
            ANXVector4 anxR = ANXVector4.Normalize(anx1);

            AssertHelper.ConvertEquals(xnaR, anxR, "NormalizeStatic");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void SmoothStep(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float amount, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = XNAVector4.SmoothStep(xna1, xna2, amount);
            ANXVector4 anxR = ANXVector4.SmoothStep(anx1, anx2, amount);

            AssertHelper.ConvertEquals(xnaR, anxR, "SmoothStep");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void Subtract(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = XNAVector4.Subtract(xna1, xna2);
            ANXVector4 anxR = ANXVector4.Subtract(anx1, anx2);

            AssertHelper.ConvertEquals(xnaR, anxR, "Subtract");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void ToString(float x1, float y1, float z1, float w1, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);

            String xnaR = xna1.ToString();
            String anxR = anx1.ToString();

            Assert.AreEqual(xnaR, anxR);
        }

        #endregion

        #region Operators
        [Test, TestCaseSource("seventeenFloats")]
        public void AddOperator(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = xna1 + xna2;
            ANXVector4 anxR = anx1 + anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "AddOperator");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void SubtractOperator(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = xna1 - xna2;
            ANXVector4 anxR = anx1 - anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "SubtractOperator");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void EqualsOperator(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            bool xnaR = xna1 == xna2;
            bool anxR = anx1 == anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "EqualsOperator");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void UnequalsOperator(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            bool xnaR = xna1 != xna2;
            bool anxR = anx1 != anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "UnequalsOperator");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void MultiplyOperatorFloat(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float value, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);

            XNAVector4 xnaR = xna1 * value;
            ANXVector4 anxR = anx1 * value;

            AssertHelper.ConvertEquals(xnaR, anxR, "MultiplyOperatorFloat");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void MultiplyOperatorVector4(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = xna1 * xna2;
            ANXVector4 anxR = anx1 * anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "MultiplyOperatorVector4");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void DivideOperatorFloat(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float value, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);

            XNAVector4 xnaR = xna1 / value;
            ANXVector4 anxR = anx1 / value;

            AssertHelper.ConvertEquals(xnaR, anxR, "DivideOperatorFloat");
        }

        [Test, TestCaseSource("seventeenFloats")]
        public void DivideOperatorVector4(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            XNAVector4 xna1 = new XNAVector4(x1, y1, z1, w1);
            XNAVector4 xna2 = new XNAVector4(x2, y2, z2, w2);

            ANXVector4 anx1 = new ANXVector4(x1, y1, z1, w1);
            ANXVector4 anx2 = new ANXVector4(x2, y2, z2, w2);

            XNAVector4 xnaR = xna1 / xna2;
            ANXVector4 anxR = anx1 / anx2;

            AssertHelper.ConvertEquals(xnaR, anxR, "DivideOperatorVector4");
        }
        #endregion

        #region Transformations
        [Test, TestCaseSource("twentyfourFloats")]
        public void TransformStaticMatrixVector2(
            float x, float y,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5)
        {
            XNAVector2 xnaVector = new XNAVector2(x, y);
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector2 anxVector = new ANXVector2(x, y);
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector4 xna = XNAVector4.Transform(xnaVector, xnaMatrix);
            ANXVector4 anx = ANXVector4.Transform(anxVector, anxMatrix);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticMatrixVector2");
        }

        [Test, TestCaseSource("twentyfourFloats")]
        public void TransformStaticMatrixVector3(
            float x, float y, float z,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4)
        {
            XNAVector3 xnaVector = new XNAVector3(x, y, z);
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector3 anxVector = new ANXVector3(x, y, z);
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector4 xna = XNAVector4.Transform(xnaVector, xnaMatrix);
            ANXVector4 anx = ANXVector4.Transform(anxVector, anxMatrix);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticMatrixVector3");
        }

        [Test, TestCaseSource("twentyfourFloats")]
        public void TransformStaticMatrixVector4(
            float x, float y, float z, float w,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAVector4 xnaVector = new XNAVector4(x, y, z, w);
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector4 anxVector = new ANXVector4(x, y, z, w);
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector4 xna = XNAVector4.Transform(xnaVector, xnaMatrix);
            ANXVector4 anx = ANXVector4.Transform(anxVector, anxMatrix);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticMatrixVector4");
        }

        [Test, TestCaseSource("twentyfourFloats")]
        public void TransformStaticQuaternion(
            float x, float y, float z, float w,
            float xQ, float yQ, float zQ, float wQ,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            XNAVector4 xnaVector = new XNAVector4(x, y, z, w);
            XNAQuaternion xnaQuaternion = new XNAQuaternion(xQ, yQ, zQ, wQ);

            ANXVector4 anxVector = new ANXVector4(x, y, z, w);
            ANXQuaternion anxQuaternion = new ANXQuaternion(xQ, yQ, zQ, wQ);

            XNAVector4 xna = XNAVector4.Transform(xnaVector, xnaQuaternion);
            ANXVector4 anx = ANXVector4.Transform(anxVector, anxQuaternion);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticQuaternion");
        }

        [Test, TestCaseSource("twentyfourFloats")]
        public static void TransformStaticMatrixToDestination(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector4[] xnaVector = new XNAVector4[] { new XNAVector4(x1, y1, z1, w1), new XNAVector4(x2, y2, z2, w2) };
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector4[] anxVector = new ANXVector4[] { new ANXVector4(x1, y1, z1, w1), new ANXVector4(x2, y2, z2, w2) };
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector4[] xna = new XNAVector4[2];
            XNAVector4.Transform(xnaVector, ref xnaMatrix, xna);
            ANXVector4[] anx = new ANXVector4[2];
            ANXVector4.Transform(anxVector, ref anxMatrix, anx);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticMatrixToDestination");
        }

        [Test, TestCaseSource("twentyfourFloats")]
        public static void TransformStaticQuaternionToDestination(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2,
            float xQ, float yQ, float zQ, float wQ,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11)
        {
            XNAVector4[] xnaVector = new XNAVector4[] { new XNAVector4(x1, y1, z1, w1), new XNAVector4(x2, y2, z2, w2) };
            XNAQuaternion xnaQuaternion = new XNAQuaternion(xQ, yQ, zQ, wQ);

            ANXVector4[] anxVector = new ANXVector4[] { new ANXVector4(x1, y1, z1, w1), new ANXVector4(x2, y2, z2, w2) };
            ANXQuaternion anxQuaternion = new ANXQuaternion(xQ, yQ, zQ, wQ);

            XNAVector4[] xna = new XNAVector4[2];
            XNAVector4.Transform(xnaVector, ref xnaQuaternion, xna);
            ANXVector4[] anx = new ANXVector4[2];
            ANXVector4.Transform(anxVector, ref anxQuaternion, anx);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticQuaternionToDestination");
        }

        [Test, TestCaseSource("twentyfourFloats")]
        public static void TransformStaticMatrixToDestinationWithIndex(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2,
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector4[] xnaVector = new XNAVector4[] { new XNAVector4(x1, y1, z1, w1), new XNAVector4(x2, y2, z2, w2) };
            XNAMatrix xnaMatrix = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXVector4[] anxVector = new ANXVector4[] { new ANXVector4(x1, y1, z1, w1), new ANXVector4(x2, y2, z2, w2) };
            ANXMatrix anxMatrix = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            XNAVector4[] xna = new XNAVector4[2];
            XNAVector4.Transform(xnaVector, 1, ref xnaMatrix, xna, 1, 1);
            ANXVector4[] anx = new ANXVector4[2];
            ANXVector4.Transform(anxVector, 1, ref anxMatrix, anx, 1, 1);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticMatrixToDestinationWithIndex");
        }

        [Test, TestCaseSource("twentyfourFloats")]
        public static void TransformStaticQuaternionToDestinationWithIndex(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2,
            float xQ, float yQ, float zQ, float wQ,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11)
        {
            XNAVector4[] xnaVector = new XNAVector4[] { new XNAVector4(x1, y1, z1, w1), new XNAVector4(x2, y2, z2, w2) };
            XNAQuaternion xnaQuaternion = new XNAQuaternion(xQ, yQ, zQ, wQ);

            ANXVector4[] anxVector = new ANXVector4[] { new ANXVector4(x1, y1, z1, w1), new ANXVector4(x2, y2, z2, w2) };
            ANXQuaternion anxQuaternion = new ANXQuaternion(xQ, yQ, zQ, wQ);

            XNAVector4[] xna = new XNAVector4[2];
            XNAVector4.Transform(xnaVector, 1, ref xnaQuaternion, xna, 1, 1);
            ANXVector4[] anx = new ANXVector4[2];
            ANXVector4.Transform(anxVector, 1, ref anxQuaternion, anx, 1, 1);

            AssertHelper.ConvertEquals(xna, anx, "TransformStaticQuaternionToDestinationWithIndex");
        }
        #endregion

        #endregion

        //TODO: transform operations

    }
}