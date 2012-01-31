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

using XNABoundingFrustum = Microsoft.Xna.Framework.BoundingFrustum;
using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXBoundingFrustum = ANX.Framework.BoundingFrustum;
using ANXVector3 = ANX.Framework.Vector3;

using NUnit.Framework;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class BoundingFrustumTest
    {
        #region Helper
        static object[] thirtytwofloats =
        {
            new object[] {  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,
                 DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,
                 DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue },
            new object[] {  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,
                 DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,
                 DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue },
            new object[] {  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,
                 DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,
                 DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue },
           new object[] {  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,
                 DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,
                 DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue },
            new object[] {  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,
                 DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,
                 DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue,  DataFactory.RandomValue },
        };
        #endregion

        #region Constructors
        [Test, TestCaseSource("thirtytwofloats")]
        public void constructor0(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            AssertHelper.ConvertEquals(xnaFrustum, anxFrustum, "constructor0");
        }
        #endregion

        #region Methods
        [Test, TestCaseSource("thirtytwofloats")]
        public void ContainsBoundingBox(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xMin, float yMin, float zMin, float xMax, float yMax, float zMax,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            if (xMin > xMax)
            {
                float x = xMin;
                xMin = xMax;
                xMax = x;
            }
            if (yMin > yMax)
            {
                float y = yMin;
                yMin = yMax;
                yMax = y;
            }
            if (zMin > zMax)
            {
                float z = zMin;
                zMin = zMax;
                zMax = z;
            }

            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);
            Microsoft.Xna.Framework.BoundingBox xnaBox = new Microsoft.Xna.Framework.BoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix); 
            ANX.Framework.BoundingBox anxBox = new ANX.Framework.BoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaFrustum.Contains(xnaBox);
            ANX.Framework.ContainmentType containsANX = anxFrustum.Contains(anxBox);

            if ((int)containsXNA == (int)containsANX)
                Assert.Pass("ContainsBoundingBox passed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString());
            else
                Assert.Fail(String.Format("ContainsBoundingBox failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void ContainsBoundingFrustum(
            float m11_1, float m12_1, float m13_1, float m14_1, float m21_1, float m22_1, float m23_1, float m24_1, float m31_1, float m32_1, float m33_1, float m34_1, float m41_1, float m42_1, float m43_1, float m44_1,
            float m11_2, float m12_2, float m13_2, float m14_2, float m21_2, float m22_2, float m23_2, float m24_2, float m31_2, float m32_2, float m33_2, float m34_2, float m41_2, float m42_2, float m43_2, float m44_2)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix1 = new Microsoft.Xna.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum1 = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix1);
            Microsoft.Xna.Framework.Matrix xnaMatrix2 = new Microsoft.Xna.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum2 = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix2);

            ANX.Framework.Matrix anxMatrix1 = new ANX.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            ANX.Framework.BoundingFrustum anxFrustum1 = new ANX.Framework.BoundingFrustum(anxMatrix1);
            ANX.Framework.Matrix anxMatrix2 = new ANX.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            ANX.Framework.BoundingFrustum anxFrustum2 = new ANX.Framework.BoundingFrustum(anxMatrix2);

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaFrustum1.Contains(xnaFrustum2);
            ANX.Framework.ContainmentType containsANX = anxFrustum1.Contains(anxFrustum2);

            if ((int)containsXNA == (int)containsANX)
                Assert.Pass("ContainsBoundingFrustum passed");
            else
                Assert.Fail(String.Format("ContainsBoundingFrustum failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void ContainsBoundingSphere(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xS, float yS, float zS, float rS,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11)
        {
            rS = Math.Max(rS, -rS);

            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);
            Microsoft.Xna.Framework.BoundingSphere xnaSphere = new Microsoft.Xna.Framework.BoundingSphere(new XNAVector3(xS, yS, zS), rS);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix); 
            ANX.Framework.BoundingSphere anxSphere = new ANX.Framework.BoundingSphere(new ANXVector3(xS, yS, zS), rS);

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaFrustum.Contains(xnaSphere);
            ANX.Framework.ContainmentType containsANX = anxFrustum.Contains(anxSphere);

            if ((int)containsXNA == (int)containsANX)
                Assert.Pass("ContainsBoundingSphere passed");
            else
                Assert.Fail(String.Format("ContainsBoundingSphere failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void ContainsPoint(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xP, float yP, float zP,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);
            XNAVector3 xnaPoint = new XNAVector3(xP, yP, zP);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix); 
            ANXVector3 anxPoint = new ANXVector3(xP, yP, zP);

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaFrustum.Contains(xnaPoint);
            ANX.Framework.ContainmentType containsANX = anxFrustum.Contains(anxPoint);

            if ((int)containsXNA == (int)containsANX)
                Assert.Pass("ContainsPoint passed");
            else
                Assert.Fail(String.Format("ContainsPoint failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void GetCornerArray(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            XNAVector3[] xna = new XNAVector3[8];
            ANXVector3[] anx = new ANXVector3[8];

            xnaFrustum.GetCorners(xna);
            anxFrustum.GetCorners(anx);

            AssertHelper.ConvertEquals(xna, anx, "GetCornerArray");
        }

        [Test, TestCaseSource("thirtytwofloats"), ExpectedException(typeof(ArgumentNullException))]
        public void GetCornerArray02(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            anxFrustum.GetCorners(null);
        }

        [Test, TestCaseSource("thirtytwofloats"), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetCornerArray03(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            Vector3[] corners = new Vector3[2];

            anxFrustum.GetCorners(corners);
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void GetCorners(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            XNAVector3[] xna = xnaFrustum.GetCorners();
            ANXVector3[] anx = anxFrustum.GetCorners();

            if (xna[0].X == anx[0].X &&
                xna[0].Y == anx[0].Y &&
                xna[0].Z == anx[0].Z &&
                xna[1].X == anx[1].X &&
                xna[1].Y == anx[1].Y &&
                xna[1].Z == anx[1].Z &&
                xna[2].X == anx[2].X &&
                xna[2].Y == anx[2].Y &&
                xna[2].Z == anx[2].Z &&
                xna[3].X == anx[3].X &&
                xna[3].Y == anx[3].Y &&
                xna[3].Z == anx[3].Z &&
                xna[4].X == anx[4].X &&
                xna[4].Y == anx[4].Y &&
                xna[4].Z == anx[4].Z &&
                xna[5].X == anx[5].X &&
                xna[5].Y == anx[5].Y &&
                xna[5].Z == anx[5].Z &&
                xna[6].X == anx[6].X &&
                xna[6].Y == anx[6].Y &&
                xna[6].Z == anx[6].Z &&
                xna[7].X == anx[7].X &&
                xna[7].Y == anx[7].Y &&
                xna[7].Z == anx[7].Z)
                Assert.Pass("GetCorners passed");
            else
                Assert.Fail("GetCorners failed: xna(" + 
                    xna[0].ToString() + " " +
                    xna[1].ToString() + " " +
                    xna[2].ToString() + " " +
                    xna[3].ToString() + " " +
                    xna[4].ToString() + " " +
                    xna[5].ToString() + " " +
                    xna[6].ToString() +  " " +
                    xna[7].ToString() + ") anx(" +
                    anx[0].ToString() + " " +
                    anx[1].ToString() + " " +
                    anx[2].ToString() + " " +
                    anx[3].ToString() + " " +
                    anx[4].ToString() + " " +
                    anx[5].ToString() + " " +
                    anx[6].ToString() + " " +
                    anx[7].ToString() + ")");
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void IntersectsBoundingBox(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xMin, float yMin, float zMin, float xMax, float yMax, float zMax,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            if (xMin > xMax)
            {
                float x = xMin;
                xMin = xMax;
                xMax = x;
            }
            if (yMin > yMax)
            {
                float y = yMin;
                yMin = yMax;
                yMax = y;
            }
            if (zMin > zMax)
            {
                float z = zMin;
                zMin = zMax;
                zMax = z;
            }

            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);
            Microsoft.Xna.Framework.BoundingBox xnaBox = new Microsoft.Xna.Framework.BoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);
            ANX.Framework.BoundingBox anxBox = new ANX.Framework.BoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));

            bool containsXNA = xnaFrustum.Intersects(xnaBox);
            bool containsANX = anxFrustum.Intersects(anxBox);

            if (containsXNA.Equals(containsANX))
                Assert.Pass("IntersectsBoundingBox passed");
            else
                Assert.Fail(String.Format("IntersectsBoundingBox failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void IntersectsBoundingFrustum(
            float m11_1, float m12_1, float m13_1, float m14_1, float m21_1, float m22_1, float m23_1, float m24_1, float m31_1, float m32_1, float m33_1, float m34_1, float m41_1, float m42_1, float m43_1, float m44_1,
            float m11_2, float m12_2, float m13_2, float m14_2, float m21_2, float m22_2, float m23_2, float m24_2, float m31_2, float m32_2, float m33_2, float m34_2, float m41_2, float m42_2, float m43_2, float m44_2)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix1 = new Microsoft.Xna.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum1 = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix1);
            Microsoft.Xna.Framework.Matrix xnaMatrix2 = new Microsoft.Xna.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum2 = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix2);

            ANX.Framework.Matrix anxMatrix1 = new ANX.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            ANX.Framework.BoundingFrustum anxFrustum1 = new ANX.Framework.BoundingFrustum(anxMatrix1);
            ANX.Framework.Matrix anxMatrix2 = new ANX.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            ANX.Framework.BoundingFrustum anxFrustum2 = new ANX.Framework.BoundingFrustum(anxMatrix2);

            bool xna = xnaFrustum1.Intersects(xnaFrustum2);
            bool anx = anxFrustum1.Intersects(anxFrustum2);

            if (xna.Equals(anx))
                Assert.Pass("IntersectsBoundingFrustum passed");
            else
                Assert.Fail(String.Format("IntersectsBoundingFrustum failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void IntersectsBoundingSphere(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xS, float yS, float zS, float rS,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11)
        {
            rS = Math.Max(rS, -rS);

            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);
            Microsoft.Xna.Framework.BoundingSphere xnaSphere = new Microsoft.Xna.Framework.BoundingSphere(new XNAVector3(xS, yS, zS), rS);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);
            ANX.Framework.BoundingSphere anxSphere = new ANX.Framework.BoundingSphere(new ANXVector3(xS, yS, zS), rS);

            bool xna = xnaFrustum.Intersects(xnaSphere);
            bool anx = anxFrustum.Intersects(anxSphere);

            if (xna.Equals(anx))
                Assert.Pass("IntersectsBoundingSphere passed");
            else
                Assert.Fail(String.Format("IntersectsBoundingSphere failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void IntersectsPlane(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xP, float yP, float zP, float dP,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);
            Microsoft.Xna.Framework.Plane xnaPlane = new Microsoft.Xna.Framework.Plane(xP, yP, zP, dP);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);
            ANX.Framework.Plane anxPlane = new ANX.Framework.Plane(xP, yP, zP, dP);

            Microsoft.Xna.Framework.PlaneIntersectionType xna = xnaFrustum.Intersects(xnaPlane);
            ANX.Framework.PlaneIntersectionType anx = anxFrustum.Intersects(anxPlane);

            if ((int)xna == (int)anx)
                Assert.Pass("IntersectsPlane passed");
            else
                Assert.Fail(String.Format("IntersectsPlane failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void IntersectsRay(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xRay, float yRay, float zRay, float xDir, float yDir, float zDir,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);
            Microsoft.Xna.Framework.Ray xnaRay = new Microsoft.Xna.Framework.Ray(new XNAVector3(xRay, yRay, zRay), new XNAVector3(xDir, yDir, zDir));

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);
            ANX.Framework.Ray anxRay = new ANX.Framework.Ray(new ANXVector3(xRay, yRay, zRay), new ANXVector3(xDir, yDir, zDir));

            float? xna = xnaFrustum.Intersects(xnaRay);
            float? anx = anxFrustum.Intersects(anxRay);

            if (xna.Equals(anx))
                Assert.Pass("IntersectsRay passed");
            else
                Assert.Fail(String.Format("IntersectsRay failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }
        #endregion

        #region Properties
        [Test, TestCaseSource("thirtytwofloats")]
        public void Bottom(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);
            
            Microsoft.Xna.Framework.Plane xna = xnaFrustum.Bottom;
            ANX.Framework.Plane anx = anxFrustum.Bottom;

            AssertHelper.ConvertEquals(xnaFrustum, anxFrustum, "Bottom");
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void Left(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            Microsoft.Xna.Framework.Plane xna = xnaFrustum.Left;
            ANX.Framework.Plane anx = anxFrustum.Left;

            AssertHelper.ConvertEquals(xnaFrustum, anxFrustum, "Left");
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void Right(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            Microsoft.Xna.Framework.Plane xna = xnaFrustum.Right;
            ANX.Framework.Plane anx = anxFrustum.Right;

            AssertHelper.ConvertEquals(xnaFrustum, anxFrustum, "Right");
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void Top(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            Microsoft.Xna.Framework.Plane xna = xnaFrustum.Top;
            ANX.Framework.Plane anx = anxFrustum.Top;

            AssertHelper.ConvertEquals(xnaFrustum, anxFrustum, "Top");
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void Near(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            Microsoft.Xna.Framework.Plane xna = xnaFrustum.Near;
            ANX.Framework.Plane anx = anxFrustum.Near;

            AssertHelper.ConvertEquals(xnaFrustum, anxFrustum, "Near");
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void Far(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float nop0, float nop1, float nop2, float nop3, float nop4, float nop5, float nop6, float nop7, float nop8, float nop9, float nop10, float nop11, float nop12, float nop13, float nop14, float nop15)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            Microsoft.Xna.Framework.Plane xna = xnaFrustum.Far;
            ANX.Framework.Plane anx = anxFrustum.Far;

            AssertHelper.ConvertEquals(xnaFrustum, anxFrustum, "Far");
        }
        #endregion

        #region Operators
        [Test, TestCaseSource("thirtytwofloats")]
        public void EqualsOperator(
            float m11_1, float m12_1, float m13_1, float m14_1, float m21_1, float m22_1, float m23_1, float m24_1, float m31_1, float m32_1, float m33_1, float m34_1, float m41_1, float m42_1, float m43_1, float m44_1,
            float m11_2, float m12_2, float m13_2, float m14_2, float m21_2, float m22_2, float m23_2, float m24_2, float m31_2, float m32_2, float m33_2, float m34_2, float m41_2, float m42_2, float m43_2, float m44_2)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix1 = new Microsoft.Xna.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum1 = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix1);
            Microsoft.Xna.Framework.Matrix xnaMatrix2 = new Microsoft.Xna.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum2 = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix2);

            ANX.Framework.Matrix anxMatrix1 = new ANX.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            ANX.Framework.BoundingFrustum anxFrustum1 = new ANX.Framework.BoundingFrustum(anxMatrix1);
            ANX.Framework.Matrix anxMatrix2 = new ANX.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            ANX.Framework.BoundingFrustum anxFrustum2 = new ANX.Framework.BoundingFrustum(anxMatrix2);

            bool xna = xnaFrustum1 == xnaFrustum2;
            bool anx = anxFrustum1 == anxFrustum2;

            if (xna.Equals(anx))
            {
                Assert.Pass("EqualsOperator passed");
            }
            else
            {
                Assert.Fail(String.Format("EqualsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("thirtytwofloats")]
        public void UnequalsOperator(
            float m11_1, float m12_1, float m13_1, float m14_1, float m21_1, float m22_1, float m23_1, float m24_1, float m31_1, float m32_1, float m33_1, float m34_1, float m41_1, float m42_1, float m43_1, float m44_1,
            float m11_2, float m12_2, float m13_2, float m14_2, float m21_2, float m22_2, float m23_2, float m24_2, float m31_2, float m32_2, float m33_2, float m34_2, float m41_2, float m42_2, float m43_2, float m44_2)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix1 = new Microsoft.Xna.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum1 = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix1);
            Microsoft.Xna.Framework.Matrix xnaMatrix2 = new Microsoft.Xna.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum2 = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix2);

            ANX.Framework.Matrix anxMatrix1 = new ANX.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            ANX.Framework.BoundingFrustum anxFrustum1 = new ANX.Framework.BoundingFrustum(anxMatrix1);
            ANX.Framework.Matrix anxMatrix2 = new ANX.Framework.Matrix(m11_1, m12_1, m13_1, m14_1, m21_1, m22_1, m23_1, m24_1, m31_1, m32_1, m33_1, m34_1, m41_1, m42_1, m43_1, m44_1);
            ANX.Framework.BoundingFrustum anxFrustum2 = new ANX.Framework.BoundingFrustum(anxMatrix2);
            
            bool xna = xnaFrustum1 != xnaFrustum2;
            bool anx = anxFrustum1 != anxFrustum2;

            if (xna.Equals(anx))
            {
                Assert.Pass("UnequalsOperator passed");
            }
            else
            {
                Assert.Fail(String.Format("UnequalsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }
        #endregion
    }
}
