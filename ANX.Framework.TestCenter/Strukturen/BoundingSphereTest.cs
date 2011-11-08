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

using XNABoundingSphere = Microsoft.Xna.Framework.BoundingSphere;
using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXBoundingSphere = ANX.Framework.BoundingSphere;
using ANXVector3 = ANX.Framework.Vector3;

using NUnit.Framework;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class BoundingSphereTest
    {
        #region Helper
        static object[] fourfloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
        };

        static object[] eightfloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
        };

        static object[] tenfloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
        };
         
        static object[] twentyfloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),
                 DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),
                 DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),
                 DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),
                 DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),
                 DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f),  DataFactory.RandomValueMinMax(0f, 1000f) },
        };

        #endregion

        #region Constructors
        [Test]
        public void constructor0()
        {
            XNABoundingSphere xna = new XNABoundingSphere();

            ANXBoundingSphere anx = new ANXBoundingSphere();

            AssertHelper.ConvertEquals(xna, anx, "constructor0");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor1(float x, float y, float z, float r)
        {
            XNABoundingSphere xna = new XNABoundingSphere(new XNAVector3(x, y, z), r);

            ANXBoundingSphere anx = new ANXBoundingSphere(new ANXVector3(x, y, z), r);

            AssertHelper.ConvertEquals(xna, anx, "constructor0");
        }
        #endregion

        #region Methods
        [Test, TestCaseSource("tenfloats")]
        public void ContainsBoundingBox(float xS, float yS, float zS, float rS, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
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

            XNABoundingSphere xnaSphere = new XNABoundingSphere(new XNAVector3(xS, yS, zS), rS);
            Microsoft.Xna.Framework.BoundingBox xnaBox = new Microsoft.Xna.Framework.BoundingBox(
                new XNAVector3(xMin, yMin, zMin),
                new XNAVector3(xMax, yMax, zMax));

            ANXBoundingSphere anxSphere = new ANXBoundingSphere(new ANXVector3(xS, yS, zS), rS);
            ANX.Framework.BoundingBox anxBox = new ANX.Framework.BoundingBox(
                new ANXVector3(xMin, yMin, zMin),
                new ANXVector3(xMax, yMax, zMax));

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaSphere.Contains(xnaBox);
            ANX.Framework.ContainmentType containsANX = anxSphere.Contains(anxBox);

            if (containsANX.Equals(containsANX))
                Assert.Pass("ContainsBoundingBox passed");
            else
                Assert.Fail(String.Format("ContainsBoundingBox failed: xna({0}) anx({1})", containsANX.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("twentyfloats")]
        public void ContainsBoundingFrustum(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xS, float yS, float zS, float r)
        {
            XNABoundingSphere xnaSphere = new XNABoundingSphere(new XNAVector3(xS, yS, zS), r);
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANXBoundingSphere anxSphere = new ANXBoundingSphere(new ANXVector3(xS, yS, zS), r);
            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaSphere.Contains(xnaFrustum);
            ANX.Framework.ContainmentType containsANX = anxSphere.Contains(anxFrustum);

            if ((int)containsXNA == (int)containsANX)
                Assert.Pass("ContainsBoundingFrustum passed");
            else
                Assert.Fail(String.Format("ContainsBoundingFrustum failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("eightfloats")]
        public void ContainsBoundingSphere(float x1, float y1, float z1, float r1, float x2, float y2, float z2, float r2)
        {
            XNABoundingSphere xnaSphere1 = new XNABoundingSphere(new XNAVector3(x1, y1, z1), r1);
            XNABoundingSphere xnaSphere2 = new XNABoundingSphere(new XNAVector3(x2, y2, z2), r2);

            ANXBoundingSphere anxSphere1 = new ANXBoundingSphere(new ANXVector3(x1, y1, z1), r1);
            ANXBoundingSphere anxSphere2 = new ANXBoundingSphere(new ANXVector3(x2, y2, z2), r2);

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaSphere1.Contains(xnaSphere2);
            ANX.Framework.ContainmentType containsANX = anxSphere1.Contains(anxSphere2);

            if (containsANX.Equals(containsANX))
                Assert.Pass("ContainsBoundingSphere passed");
            else
                Assert.Fail(String.Format("ContainsBoundingSphere failed: xna({0}) anx({1})", containsANX.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("eightfloats")]
        public void ContainsPoint(float xS, float yS, float zS, float rS, float xP, float yP, float zP, float a)
        {
            XNABoundingSphere xnaSphere = new XNABoundingSphere(new XNAVector3(xS, yS, zS), rS);
            XNAVector3 xnaPoint = new XNAVector3(xP, yP, zP);

            ANXBoundingSphere anxSphere = new ANXBoundingSphere(new ANXVector3(xS, yS, zS), rS);
            ANXVector3 anxPoint = new ANXVector3(xP, yP, zP);

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaSphere.Contains(xnaPoint);
            ANX.Framework.ContainmentType containsANX = anxSphere.Contains(anxPoint);

            if (containsANX.Equals(containsANX))
                Assert.Pass("ContainsPoint passed");
            else
                Assert.Fail(String.Format("ContainsPoint failed: xna({0}) anx({1})", containsANX.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("eightfloats")]
        public void CreateFromBoundingBox(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax, float a, float b)
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

            Microsoft.Xna.Framework.BoundingBox xnaBox = new Microsoft.Xna.Framework.BoundingBox(
                new XNAVector3(xMin, yMin, zMin),
                new XNAVector3(xMax, yMax, zMax));

            ANX.Framework.BoundingBox anxBox = new ANX.Framework.BoundingBox(
                new ANXVector3(xMin, yMin, zMin),
                new ANXVector3(xMax, yMax, zMax));

            XNABoundingSphere xna = XNABoundingSphere.CreateFromBoundingBox(xnaBox);
            ANXBoundingSphere anx = ANXBoundingSphere.CreateFromBoundingBox(anxBox);

            AssertHelper.ConvertEquals(xna, anx, "CreateFromBoundingBox");
        }

        [Test, TestCaseSource("twentyfloats")]
        public void CreateFromFrustumStatic(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float a, float b, float c, float d)
        {
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            XNABoundingSphere xna = XNABoundingSphere.CreateFromFrustum(xnaFrustum);
            ANXBoundingSphere anx = ANXBoundingSphere.CreateFromFrustum(anxFrustum);

            AssertHelper.ConvertEquals(xna, anx, "CreateFromFrustumStatic");
        }

        [Test, TestCaseSource("tenfloats")]
        public void CreateFromPointsStatic(
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float x3, float y3, float z3,
            float a)
        {
            List<XNAVector3> pointsXNA = new List<XNAVector3>();
            pointsXNA.Add(new XNAVector3(x1, y1, z1));
            pointsXNA.Add(new XNAVector3(x2, y2, z2));
            pointsXNA.Add(new XNAVector3(x3, y3, z3));

            List<ANXVector3> pointsANX = new List<ANXVector3>();
            pointsANX.Add(new ANXVector3(x1, y1, z1));
            pointsANX.Add(new ANXVector3(x2, y2, z2));
            pointsANX.Add(new ANXVector3(x3, y3, z3));

            XNABoundingSphere xna = XNABoundingSphere.CreateFromPoints(pointsXNA);
            ANXBoundingSphere anx = ANXBoundingSphere.CreateFromPoints(pointsANX);

            AssertHelper.ConvertEquals(xna, anx, "CreateFromPointsStatic");
        }

        [Test, TestCaseSource("tenfloats")]
        public void CreateMergedStatic(
            float x1, float y1, float z1, float r1,
            float x2, float y2, float z2, float r2,
            float a, float b)
        {
            XNABoundingSphere xnaSphere1 = new XNABoundingSphere(new XNAVector3(x1, y1, z1), r1);
            XNABoundingSphere xnaSphere2 = new XNABoundingSphere(new XNAVector3(x2, y2, z2), r2);

            ANXBoundingSphere anxSphere1 = new ANXBoundingSphere(new ANXVector3(x1, y1, z1), r1);
            ANXBoundingSphere anxSphere2 = new ANXBoundingSphere(new ANXVector3(x2, y2, z2), r2);

            XNABoundingSphere xna = XNABoundingSphere.CreateMerged(xnaSphere1, xnaSphere2);
            ANXBoundingSphere anx = ANXBoundingSphere.CreateMerged(anxSphere1, anxSphere2);

            AssertHelper.ConvertEquals(xna, anx, "CreateMergedStatic");
        }

        [Test, TestCaseSource("tenfloats")]
        public void IntersectsBoundingBox(float xS, float yS, float zS, float rS, float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
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

            XNABoundingSphere xnaSphere = new XNABoundingSphere(new XNAVector3(xS, yS, zS), rS);
            Microsoft.Xna.Framework.BoundingBox xnaBox = new Microsoft.Xna.Framework.BoundingBox(
                new XNAVector3(xMin, yMin, zMin),
                new XNAVector3(xMax, yMax, zMax));

            ANXBoundingSphere anxSphere = new ANXBoundingSphere(new ANXVector3(xS, yS, zS), rS);
            ANX.Framework.BoundingBox anxBox = new ANX.Framework.BoundingBox(
                new ANXVector3(xMin, yMin, zMin),
                new ANXVector3(xMax, yMax, zMax));

            bool containsXNA = xnaSphere.Intersects(xnaBox);
            bool containsANX = anxSphere.Intersects(anxBox);

            if (containsANX.Equals(containsANX))
                Assert.Pass("IntersectsBoundingBox passed");
            else
                Assert.Fail(String.Format("IntersectsBoundingBox failed: xna({0}) anx({1})", containsANX.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("twentyfloats")]
        public void IntersectsBoundingFrustum(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xS, float yS, float zS, float r)
        {
            XNABoundingSphere xnaSphere = new XNABoundingSphere(new XNAVector3(xS, yS, zS), r);
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANXBoundingSphere anxSphere = new ANXBoundingSphere(new ANXVector3(xS, yS, zS), r);
            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            bool containsXNA = xnaSphere.Intersects(xnaFrustum);
            bool containsANX = anxSphere.Intersects(anxFrustum);

            if (containsXNA.Equals(containsANX))
                Assert.Pass("IntersectsBoundingFrustum passed");
            else
                Assert.Fail(String.Format("IntersectsBoundingFrustum failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("eightfloats")]
        public void IntersectsBoundingSphere(float x1, float y1, float z1, float r1, float x2, float y2, float z2, float r2)
        {
            XNABoundingSphere xnaSphere1 = new XNABoundingSphere(new XNAVector3(x1, y1, z1), r1);
            XNABoundingSphere xnaSphere2 = new XNABoundingSphere(new XNAVector3(x2, y2, z2), r2);

            ANXBoundingSphere anxSphere1 = new ANXBoundingSphere(new ANXVector3(x1, y1, z1), r1);
            ANXBoundingSphere anxSphere2 = new ANXBoundingSphere(new ANXVector3(x2, y2, z2), r2);

            bool containsXNA = xnaSphere1.Intersects(xnaSphere2);
            bool containsANX = anxSphere1.Intersects(anxSphere2);

            if (containsANX.Equals(containsANX))
                Assert.Pass("IntersectsBoundingSphere passed");
            else
                Assert.Fail(String.Format("IntersectsBoundingSphere failed: xna({0}) anx({1})", containsANX.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("eightfloats")]
        public void IntersectsPlane(float xS, float yS, float zS, float r, float xP, float yP, float zP, float dP)
        {
            XNABoundingSphere xnaSphere = new XNABoundingSphere(new XNAVector3(xS, yS, zS), r);
            Microsoft.Xna.Framework.Plane xnaPlane = new Microsoft.Xna.Framework.Plane(xP, yP, zP, dP);

            ANXBoundingSphere anxSphere = new ANXBoundingSphere(new ANXVector3(xS, yS, zS), r);
            ANX.Framework.Plane anxPlane = new ANX.Framework.Plane(xP, yP, zP, dP);

            Microsoft.Xna.Framework.PlaneIntersectionType xna = xnaSphere.Intersects(xnaPlane);
            ANX.Framework.PlaneIntersectionType anx = anxSphere.Intersects(anxPlane);

            if ((int)xna == (int)anx)
                Assert.Pass("IntersectsPlane passed");
            else
                Assert.Fail(String.Format("IntersectsPlane failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("tenfloats")]
        public void IntersectsRay(
            float xS, float yS, float zS, float r,
            float xRay, float yRay, float zRay, float xDir, float yDir, float zDir)
        {
            XNABoundingSphere xnaSphere = new XNABoundingSphere(new XNAVector3(xS, yS, zS), r);
            Microsoft.Xna.Framework.Ray xnaRay = new Microsoft.Xna.Framework.Ray(new XNAVector3(xRay, yRay, zRay), new XNAVector3(xDir, yDir, zDir));
            xnaRay.Direction.Normalize();

            ANXBoundingSphere anxSphere = new ANXBoundingSphere(new ANXVector3(xS, yS, zS), r);
            ANX.Framework.Ray anxRay = new ANX.Framework.Ray(new ANXVector3(xRay, yRay, zRay), new ANXVector3(xDir, yDir, zDir));
            anxRay.Direction.Normalize();

            float? xna = xnaSphere.Intersects(xnaRay);
            float? anx = anxSphere.Intersects(anxRay);

            if (xna.Equals(anx))
                Assert.Pass("IntersectsRay passed");
            else
                Assert.Fail(String.Format("IntersectsRay failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("twentyfloats")]
        public void Transform(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xS, float yS, float zS, float r)
        {
            XNABoundingSphere xnaSphere = new XNABoundingSphere(new XNAVector3(xS, yS, zS), r);
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXBoundingSphere anxSphere = new ANXBoundingSphere(new ANXVector3(xS, yS, zS), r);
            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            xnaSphere.Transform(xnaMatrix);
            anxSphere.Transform(anxMatrix);

            AssertHelper.ConvertEquals(xnaSphere, anxSphere, "Transform");
        }
        #endregion

        #region Properties
        [Test, TestCaseSource("fourfloats")]
        public void Center(float x, float y, float z, float r)
        {
            XNABoundingSphere xnaSphere = new XNABoundingSphere(new XNAVector3(x, y, z), r);

            ANXBoundingSphere anxSphere = new ANXBoundingSphere(new ANXVector3(x, y, z), r);

            XNAVector3 xna = xnaSphere.Center;
            XNAVector3 anx = xnaSphere.Center;

            if (xna.X == anx.X &&
                xna.Y == anx.Y &&
                xna.Z == anx.Z)
                Assert.Pass("Center passed");
            else
                Assert.Fail(String.Format("Center failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("fourfloats")]
        public void Radius(float x, float y, float z, float r)
        {
            XNABoundingSphere xnaSphere = new XNABoundingSphere(new XNAVector3(x, y, z), r);

            ANXBoundingSphere anxSphere = new ANXBoundingSphere(new ANXVector3(x, y, z), r);

            float xna = xnaSphere.Radius;
            float anx = xnaSphere.Radius;

            if (xna.Equals(anx))
                Assert.Pass("Radius passed");
            else
                Assert.Fail(String.Format("Radius failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }
        #endregion

        #region Operators
        [Test, TestCaseSource("eightfloats")]
        public void EqualsOperator(float x1, float y1, float z1, float r1, float x2, float y2, float z2, float r2)
        {
            XNABoundingSphere xnaSphere1 = new XNABoundingSphere(new XNAVector3(x1, y1, z1), r1);
            XNABoundingSphere xnaSphere2 = new XNABoundingSphere(new XNAVector3(x2, y2, z2), r2);

            ANXBoundingSphere anxSphere1 = new ANXBoundingSphere(new ANXVector3(x1, y1, z1), r1);
            ANXBoundingSphere anxSphere2 = new ANXBoundingSphere(new ANXVector3(x2, y2, z2), r2);

            bool xna = xnaSphere1 == xnaSphere2;
            bool anx = anxSphere1 == anxSphere2;

            if (xna.Equals(anx))
                Assert.Pass("EqualsOperator passed");
            else
                Assert.Fail(String.Format("EqualsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("eightfloats")]
        public void UnequalsOperator(float x1, float y1, float z1, float r1, float x2, float y2, float z2, float r2)
        {
            XNABoundingSphere xnaSphere1 = new XNABoundingSphere(new XNAVector3(x1, y1, z1), r1);
            XNABoundingSphere xnaSphere2 = new XNABoundingSphere(new XNAVector3(x2, y2, z2), r2);

            ANXBoundingSphere anxSphere1 = new ANXBoundingSphere(new ANXVector3(x1, y1, z1), r1);
            ANXBoundingSphere anxSphere2 = new ANXBoundingSphere(new ANXVector3(x2, y2, z2), r2);

            bool xna = xnaSphere1 != xnaSphere2;
            bool anx = anxSphere1 != anxSphere2;

            if (xna.Equals(anx))
                Assert.Pass("UnequalsOperator passed");
            else
                Assert.Fail(String.Format("UnequalsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }
        #endregion
    }
}
