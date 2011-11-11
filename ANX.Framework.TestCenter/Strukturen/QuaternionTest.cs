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

using XNAQuaternion = Microsoft.Xna.Framework.Quaternion;
using ANXQuaternion = ANX.Framework.Quaternion;

using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXVector3 = ANX.Framework.Vector3;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class QuaternionTest
    {
        #region Helper
        static object[] eightfloats =
        {
            new object[] {  1f,  2f,  3f,  4f,  5f,  6f,  7f,  8f },
            new object[] { 11f, 12f, 13f, 14f, 15f, 16f, 17f, 18f },
            new object[] { 21f, 22f, 23f, 24f, 25f, 26f, 27f, 28f },
            new object[] { 31f, 32f, 33f, 34f, 35f, 36f, 37f, 38f },
            new object[] { 41f, 42f, 43f, 44f, 45f, 46f, 47f, 48f }
        };

        static object[] sixteenfloats =
        {
            new object[] {  1f,  2f,  3f,  4f,  5f,  6f,  7f,  8f, 9, 10, 11, 12, 13, 14, 15, 16 },
            new object[] { 11f, 12f, 13f, 14f, 15f, 16f, 17f, 18f, 19, 110, 111, 112, 113, 114, 115, 116 },
            new object[] { 21f, 22f, 23f, 24f, 25f, 26f, 27f, 28f, 29, 210, 211, 212, 213, 214, 215, 216 },
            new object[] { 31f, 32f, 33f, 34f, 35f, 36f, 37f, 38f, 39, 310, 311, 312, 313, 314, 315, 316 },
            new object[] { 41f, 42f, 43f, 44f, 45f, 46f, 47f, 48f, 49, 410, 411, 412, 413, 414, 415, 416 }
        };
        #endregion

        #region Constructors
        [Test]
        public void constructor0()
        {
            XNAQuaternion xna = new XNAQuaternion();
            ANXQuaternion anx = new ANXQuaternion();

            AssertHelper.ConvertEquals(xna, anx, "constructor0");
        }

        [Test, TestCaseSource("eightfloats")]
        public void constructor1(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(new XNAVector3(x, y, z), w);
            ANXQuaternion anx = new ANXQuaternion(new ANXVector3(x, y, z), w);

            AssertHelper.ConvertEquals(xna, anx, "constructor1");
        }

        [Test, TestCaseSource("eightfloats")]
        public void constructor2(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);
            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            AssertHelper.ConvertEquals(xna, anx, "constructor2");
        }
        #endregion

        #region Methods
        [Test, TestCaseSource("eightfloats")]
        public void AddStatic(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            XNAQuaternion xna = XNAQuaternion.Add(xnaQuaternion1, xnaQuaternion2);
            ANXQuaternion anx = ANXQuaternion.Add(anxQuaternion1, anxQuaternion2);

            AssertHelper.ConvertEquals(xna, anx, "AddStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void ConcatenateStatic(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            XNAQuaternion xna = XNAQuaternion.Concatenate(xnaQuaternion1, xnaQuaternion2);
            ANXQuaternion anx = ANXQuaternion.Concatenate(anxQuaternion1, anxQuaternion2);

            AssertHelper.ConvertEquals(xna, anx, "Concatenate");
        }

        [Test, TestCaseSource("eightfloats")]
        public void Conjugate(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);

            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            xna.Conjugate();
            anx.Conjugate();

            AssertHelper.ConvertEquals(xna, anx, "Conjugate");
        }

        [Test, TestCaseSource("eightfloats")]
        public void ConjugateStatic(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);
            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            xna = XNAQuaternion.Conjugate(xna);
            anx = ANXQuaternion.Conjugate(anx);

            AssertHelper.ConvertEquals(xna, anx, "ConjugateStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void CreateFromAxisAngleStatic(
            float x, float y, float z, float angle,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = XNAQuaternion.CreateFromAxisAngle(new XNAVector3(x, y, z), angle);
            ANXQuaternion anx = ANXQuaternion.CreateFromAxisAngle(new ANXVector3(x, y, z), angle);

            AssertHelper.ConvertEquals(xna, anx, "CreateFromAxisAngleStatic");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateFromRotationMatrixStatic(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAQuaternion xna = XNAQuaternion.CreateFromRotationMatrix(new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44));
            ANXQuaternion anx = ANXQuaternion.CreateFromRotationMatrix(new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44));

            xna.Normalize();
            anx.Normalize();

            AssertHelper.ConvertEquals(xna, anx, "CreateFromRotationMatrixStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void CreateFromYawPitchRollStatic(
            float yaw, float pitch, float roll,
            float nop0, float nop1, float nop2, float nop3, float nop4)
        {
            XNAQuaternion xna = XNAQuaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
            ANXQuaternion anx = ANXQuaternion.CreateFromYawPitchRoll(yaw, pitch, roll);

            AssertHelper.ConvertEquals(xna, anx, "CreateFromYawPitchRollStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void DivideStatic(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            XNAQuaternion xna = XNAQuaternion.Divide(xnaQuaternion1, xnaQuaternion2);
            ANXQuaternion anx = ANXQuaternion.Divide(anxQuaternion1, anxQuaternion2);

            AssertHelper.ConvertEquals(xna, anx, "DivideStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void DotStatic(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            float xna = XNAQuaternion.Dot(xnaQuaternion1, xnaQuaternion2);
            float anx = ANXQuaternion.Dot(anxQuaternion1, anxQuaternion2);

            AssertHelper.ConvertEquals(xna, anx, "DotStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void GetHashCode(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);

            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        }

        [Test, TestCaseSource("eightfloats")]
        public void InverseStatic(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xnaQuaternion = new XNAQuaternion(x, y, z, w);

            ANXQuaternion anxQuaternion = new ANXQuaternion(x, y, z, w);

            XNAQuaternion xna = XNAQuaternion.Inverse(xnaQuaternion);
            ANXQuaternion anx = ANXQuaternion.Inverse(anxQuaternion);

            AssertHelper.ConvertEquals(xna, anx, "InverseStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void Length(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xnaQuaternion = new XNAQuaternion(x, y, z, w);

            ANXQuaternion anxQuaternion = new ANXQuaternion(x, y, z, w);

            float xna = xnaQuaternion.Length();
            float anx = anxQuaternion.Length();

            AssertHelper.ConvertEquals(xna, anx, "Length");
        }

        [Test, TestCaseSource("eightfloats")]
        public void LengthSquared(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xnaQuaternion = new XNAQuaternion(x, y, z, w);

            ANXQuaternion anxQuaternion = new ANXQuaternion(x, y, z, w);

            float xna = xnaQuaternion.LengthSquared();
            float anx = anxQuaternion.LengthSquared();

            AssertHelper.ConvertEquals(xna, anx, "LengthSquared");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void LerpStatic(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2,
            float amount,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);
            xnaQuaternion1.Normalize();
            xnaQuaternion2.Normalize();

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);
            anxQuaternion1.Normalize();
            anxQuaternion2.Normalize();

            XNAQuaternion xna = XNAQuaternion.Lerp(xnaQuaternion1, xnaQuaternion2, amount);
            ANXQuaternion anx = ANXQuaternion.Lerp(anxQuaternion1, anxQuaternion2, amount);

            AssertHelper.ConvertEquals(xna, anx, "LerpStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void MultiplyQuaternionStatic(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            XNAQuaternion xna = XNAQuaternion.Multiply(xnaQuaternion1, xnaQuaternion2);
            ANXQuaternion anx = ANXQuaternion.Multiply(anxQuaternion1, anxQuaternion2);

            AssertHelper.ConvertEquals(xna, anx, "MultiplyQuaternionStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void MultiplyFloatStatic(
            float x, float y, float z, float w, float amount,
            float nop0, float nop1, float nop2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x, y, z, w);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x, y, z, w);

            XNAQuaternion xna = XNAQuaternion.Multiply(xnaQuaternion1, amount);
            ANXQuaternion anx = ANXQuaternion.Multiply(anxQuaternion1, amount);

            AssertHelper.ConvertEquals(xna, anx, "MultiplyFloatStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void NegateStatic(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xnaQuaternion = new XNAQuaternion(x, y, z, w);

            ANXQuaternion anxQuaternion = new ANXQuaternion(x, y, z, w);

            XNAQuaternion xna = XNAQuaternion.Negate(xnaQuaternion);
            ANXQuaternion anx = ANXQuaternion.Negate(anxQuaternion);

            AssertHelper.ConvertEquals(xna, anx, "NegateStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void Normalize(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);

            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            xna.Normalize();
            anx.Normalize();

            AssertHelper.ConvertEquals(xna, anx, "Normalize");
        }

        [Test, TestCaseSource("eightfloats")]
        public void NormalizeStatic(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xnaQuaternion = new XNAQuaternion(x, y, z, w);

            ANXQuaternion anxQuaternion = new ANXQuaternion(x, y, z, w);

            XNAQuaternion xna = XNAQuaternion.Normalize(xnaQuaternion);
            ANXQuaternion anx = ANXQuaternion.Normalize(anxQuaternion);

            AssertHelper.ConvertEquals(xna, anx, "NormalizeStatic");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void SlerpStatic(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2,
            float amount,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);
            xnaQuaternion1.Normalize();
            xnaQuaternion2.Normalize();

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);
            anxQuaternion1.Normalize();
            anxQuaternion2.Normalize();

            XNAQuaternion xna = XNAQuaternion.Slerp(xnaQuaternion1, xnaQuaternion2, amount);
            ANXQuaternion anx = ANXQuaternion.Slerp(anxQuaternion1, anxQuaternion2, amount);

            AssertHelper.ConvertEquals(xna, anx, "SlerpStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void SubtractStatic(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            XNAQuaternion xna = XNAQuaternion.Subtract(xnaQuaternion1, xnaQuaternion2);
            ANXQuaternion anx = ANXQuaternion.Subtract(anxQuaternion1, anxQuaternion2);

            AssertHelper.ConvertEquals(xna, anx, "SubtractStatic");
        }
        #endregion
        
        #region Properties
        [Test, TestCaseSource("eightfloats")]
        public void X(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);
            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            AssertHelper.ConvertEquals(xna.X, anx.X, "X");
        }

        [Test, TestCaseSource("eightfloats")]
        public void Y(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);
            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            AssertHelper.ConvertEquals(xna.Y, anx.Y, "Y");
        }

        [Test, TestCaseSource("eightfloats")]
        public void Z(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);
            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            AssertHelper.ConvertEquals(xna.Z, anx.Z, "Z");
        }

        [Test, TestCaseSource("eightfloats")]
        public void W(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);
            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            AssertHelper.ConvertEquals(xna.W, anx.W, "W");
        }

        [Test]
        public void IdentityStatic()
        {
            AssertHelper.ConvertEquals(XNAQuaternion.Identity, ANXQuaternion.Identity, "IdentityStatic");
        }
        #endregion

        #region Operators
        [Test, TestCaseSource("eightfloats")]
        public void AddOperator(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            XNAQuaternion xna = xnaQuaternion1 + xnaQuaternion2;
            ANXQuaternion anx = anxQuaternion1 + anxQuaternion2;

            AssertHelper.ConvertEquals(xna, anx, "AddOperator");
        }

        [Test, TestCaseSource("eightfloats")]
        public void DivideOperator(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            XNAQuaternion xna = xnaQuaternion1 / xnaQuaternion2;
            ANXQuaternion anx = anxQuaternion1 / anxQuaternion2;

            AssertHelper.ConvertEquals(xna, anx, "DivideOperator");
        }

        [Test, TestCaseSource("eightfloats")]
        public void EqualsOperator(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            bool xna = xnaQuaternion1 == xnaQuaternion2;
            bool anx = anxQuaternion1 == anxQuaternion2;

            if (xna.Equals(anx))
                Assert.Pass("EqualsOperator passed");
            else
                Assert.Fail(String.Format("EqualsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("eightfloats")]
        public void UnequalsOperator(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            bool xna = xnaQuaternion1 != xnaQuaternion2;
            bool anx = anxQuaternion1 != anxQuaternion2;

            if (xna.Equals(anx))
                Assert.Pass("UnequalsOperator passed");
            else
                Assert.Fail(String.Format("UnequalsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("eightfloats")]
        public void MultiplyOperatorQuaternion(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            XNAQuaternion xna = xnaQuaternion1 * xnaQuaternion2;
            ANXQuaternion anx = anxQuaternion1 * anxQuaternion2;

            AssertHelper.ConvertEquals(xna, anx, "MultiplyOperatorQuaternion");
        }

        [Test, TestCaseSource("eightfloats")]
        public void MultiplyOperatorFloat(
            float x, float y, float z, float w, float scaleFactor,
            float nop0, float nop1, float nop2)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);
            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            AssertHelper.ConvertEquals(xna * scaleFactor, anx * scaleFactor, "MultiplyOperatorFloat");
        }

        [Test, TestCaseSource("eightfloats")]
        public void SubtractOperator(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAQuaternion xnaQuaternion1 = new XNAQuaternion(x1, y1, z1, w1);
            XNAQuaternion xnaQuaternion2 = new XNAQuaternion(x2, y2, z2, w2);

            ANXQuaternion anxQuaternion1 = new ANXQuaternion(x1, y1, z1, w1);
            ANXQuaternion anxQuaternion2 = new ANXQuaternion(x2, y2, z2, w2);

            XNAQuaternion xna = xnaQuaternion1 - xnaQuaternion2;
            ANXQuaternion anx = anxQuaternion1 - anxQuaternion2;

            AssertHelper.ConvertEquals(xna, anx, "SubtractOperator");
        }

        [Test, TestCaseSource("eightfloats")]
        public void NegateOperator(
            float x, float y, float z, float w,
            float nop0, float nop1, float nop2, float nop3)
        {
            XNAQuaternion xna = new XNAQuaternion(x, y, z, w);
            ANXQuaternion anx = new ANXQuaternion(x, y, z, w);

            AssertHelper.ConvertEquals(-xna, -anx, "NegateOperator");
        }
        #endregion
    }
}
