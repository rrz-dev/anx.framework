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

using XNABoundingBox = Microsoft.Xna.Framework.BoundingBox;
using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXBoundingBox = ANX.Framework.BoundingBox;
using ANXVector3 = ANX.Framework.Vector3;

using NUnit.Framework;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class BoundingBoxTest
    {
        #region Helper
        static object[] sixfloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
        };

        static object[] ninefloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
        };

        static object[] tenfloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
        };

        static object[] twelvefloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
        };

        static object[] twentytwofloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),
                 DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),
                 DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),
                 DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),
                 DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
            new object[] {  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),
                 DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f),  DataFactory.RandomValueMinMax(0f, 100f) },
        };

        #endregion

        #region Constructors
        [Test]
        public void constructor0()
        {
            XNABoundingBox xna = new XNABoundingBox();

            ANXBoundingBox anx = new ANXBoundingBox();

            AssertHelper.ConvertEquals(xna, anx, "constructor0");
        }

        [Test, TestCaseSource("sixfloats")]
        public void constructor1(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
        {
            XNABoundingBox xna = new XNABoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));

            ANXBoundingBox anx = new ANXBoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));

            AssertHelper.ConvertEquals(xna, anx, "constructor0");
        }
        #endregion

        #region Methods
        [Test, TestCaseSource("twelvefloats")]
        public void ContainsBoundingBox(
            float xMin1, float yMin1, float zMin1, float xMax1, float yMax1, float zMax1,
            float xMin2, float yMin2, float zMin2, float xMax2, float yMax2, float zMax2)
        {
            XNABoundingBox xnaBox1 = new XNABoundingBox(new XNAVector3(xMin1, yMin1, zMin1), new XNAVector3(xMax1, yMax1, zMax1));
            XNABoundingBox xnaBox2 = new XNABoundingBox(new XNAVector3(xMin2, yMin2, zMin2), new XNAVector3(xMax2, yMax2, zMax2));

            ANXBoundingBox anxBox1 = new ANXBoundingBox(new ANXVector3(xMin1, yMin1, zMin1), new ANXVector3(xMax1, yMax1, zMax1));
            ANXBoundingBox anxBox2 = new ANXBoundingBox(new ANXVector3(xMin2, yMin2, zMin2), new ANXVector3(xMax2, yMax2, zMax2));

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaBox1.Contains(xnaBox2);
            ANX.Framework.ContainmentType containsANX = anxBox1.Contains(anxBox2);

            if ((int)containsXNA == (int)containsANX)
                Assert.Pass("ContainsBoundingBox passed");
            else
                Assert.Fail(String.Format("ContainsBoundingBox failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("twentytwofloats")]
        public void ContainsBoundingFrustum(
            float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
        {
            XNABoundingBox xnaBox = new XNABoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));
            Microsoft.Xna.Framework.Matrix xnaMatrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMatrix);

            ANXBoundingBox anxBox = new ANXBoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));
            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix); 
            
            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaBox.Contains(xnaFrustum);
            ANX.Framework.ContainmentType containsANX = anxBox.Contains(anxFrustum);

            if ((int)containsXNA == (int)containsANX)
                Assert.Pass("ContainsBoundingFrustum passed");
            else
                Assert.Fail(String.Format("ContainsBoundingFrustum failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("tenfloats")]
        public void ContainsBoundingSphere(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax, float xS, float yS, float zS, float rS)
        {
            XNABoundingBox xnaBox = new XNABoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));
            Microsoft.Xna.Framework.BoundingSphere xnaSphere = new Microsoft.Xna.Framework.BoundingSphere(new XNAVector3(xS, yS, zS), rS);

            ANXBoundingBox anxBox = new ANXBoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));
            ANX.Framework.BoundingSphere anxSphere = new ANX.Framework.BoundingSphere(new ANXVector3(xS, yS, zS), rS);

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaBox.Contains(xnaSphere);
            ANX.Framework.ContainmentType containsANX = anxBox.Contains(anxSphere);

            if ((int)containsXNA == (int)containsANX)
                Assert.Pass("ContainsBoundingSphere passed");
            else
                Assert.Fail(String.Format("ContainsBoundingSphere failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("ninefloats")]
        public void ContainsPoint(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax, float xP, float yP, float zP)
        {
            XNABoundingBox xnaBox = new XNABoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));
            XNAVector3 xnaPoint = new XNAVector3(xP, yP, zP);

            ANXBoundingBox anxBox = new ANXBoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));
            ANXVector3 anxPoint = new ANXVector3(xP, yP, zP);

            Microsoft.Xna.Framework.ContainmentType containsXNA = xnaBox.Contains(xnaPoint);
            ANX.Framework.ContainmentType containsANX = anxBox.Contains(anxPoint);

            if ((int)containsXNA == (int)containsANX)
                Assert.Pass("ContainsPoint passed");
            else
                Assert.Fail(String.Format("ContainsPoint failed: xna({0}) anx({1})", containsXNA.ToString(), containsANX.ToString()));
        }

        [Test, TestCaseSource("ninefloats")]
        public void CreateFromPointsStatic(
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float x3, float y3, float z3)
        {
            List<XNAVector3> pointsXNA = new List<XNAVector3>();
            pointsXNA.Add(new XNAVector3(x1, y1, z1));
            pointsXNA.Add(new XNAVector3(x2, y2, z2));
            pointsXNA.Add(new XNAVector3(x3, y3, z3));

            List<ANXVector3> pointsANX = new List<ANXVector3>();
            pointsANX.Add(new ANXVector3(x1, y1, z1));
            pointsANX.Add(new ANXVector3(x2, y2, z2));
            pointsANX.Add(new ANXVector3(x3, y3, z3));

            XNABoundingBox xna = XNABoundingBox.CreateFromPoints(pointsXNA);
            ANXBoundingBox anx = ANXBoundingBox.CreateFromPoints(pointsANX);

            AssertHelper.ConvertEquals(xna, anx, "CreateFromPointsStatic");
        }

        [Test, TestCaseSource("ninefloats")]
        public void CreateFromSphereStatic(
            float x, float y, float z, float radius,
            float a, float b, float c, float d, float e)
        {
            Microsoft.Xna.Framework.BoundingSphere xnaSphere = new Microsoft.Xna.Framework.BoundingSphere(
                new XNAVector3(x, y, z), radius);

            ANX.Framework.BoundingSphere anxSphere = new ANX.Framework.BoundingSphere(
                new ANXVector3(x, y, z), radius);

            XNABoundingBox xna = XNABoundingBox.CreateFromSphere(xnaSphere);
            ANXBoundingBox anx = ANXBoundingBox.CreateFromSphere(anxSphere);

            AssertHelper.ConvertEquals(xna, anx, "CreateFromSphereStatic");
        }

        [Test, TestCaseSource("twelvefloats")]
        public void CreateMergedStatic(
            float xMin1, float yMin1, float zMin1, float xMax1, float yMax1, float zMax1,
            float xMin2, float yMin2, float zMin2, float xMax2, float yMax2, float zMax2)
        {
            XNABoundingBox xnaBox1 = new XNABoundingBox(new XNAVector3(xMin1, yMin1, zMin1), new XNAVector3(xMax1, yMax1, zMax1));
            XNABoundingBox xnaBox2 = new XNABoundingBox(new XNAVector3(xMin2, yMin2, zMin2), new XNAVector3(xMax2, yMax2, zMax2));

            ANXBoundingBox anxBox1 = new ANXBoundingBox(new ANXVector3(xMin1, yMin1, zMin1), new ANXVector3(xMax1, yMax1, zMax1));
            ANXBoundingBox anxBox2 = new ANXBoundingBox(new ANXVector3(xMin2, yMin2, zMin2), new ANXVector3(xMax2, yMax2, zMax2));

            XNABoundingBox xna = XNABoundingBox.CreateMerged(xnaBox1, xnaBox2);
            ANXBoundingBox anx = ANXBoundingBox.CreateMerged(anxBox1, anxBox2);

            AssertHelper.ConvertEquals(xna, anx, "CreateMergedStatic");
        }

        [Test, TestCaseSource("sixfloats")]
        public void GetCorners(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
        {
            XNABoundingBox xnaBox = new XNABoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));

            ANXBoundingBox anxBox = new ANXBoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));

            XNAVector3[] xna = xnaBox.GetCorners();
            ANXVector3[] anx = anxBox.GetCorners();

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
                Assert.Fail(String.Format("GetCorners failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("twelvefloats")]
        public void IntersectsBoundingBox(
            float xMin1, float yMin1, float zMin1, float xMax1, float yMax1, float zMax1,
            float xMin2, float yMin2, float zMin2, float xMax2, float yMax2, float zMax2)
        {
            XNABoundingBox xnaBox1 = new XNABoundingBox(new XNAVector3(xMin1, yMin1, zMin1), new XNAVector3(xMax1, yMax1, zMax1));
            XNABoundingBox xnaBox2 = new XNABoundingBox(new XNAVector3(xMin2, yMin2, zMin2), new XNAVector3(xMax2, yMax2, zMax2));

            ANXBoundingBox anxBox1 = new ANXBoundingBox(new ANXVector3(xMin1, yMin1, zMin1), new ANXVector3(xMax1, yMax1, zMax1));
            ANXBoundingBox anxBox2 = new ANXBoundingBox(new ANXVector3(xMin2, yMin2, zMin2), new ANXVector3(xMax2, yMax2, zMax2));

            bool xna = xnaBox1.Intersects(xnaBox2);
            bool anx = anxBox1.Intersects(anxBox2);

            if (xna.Equals(anx))
                Assert.Pass("IntersectsBoundingBox passed");
            else
                Assert.Fail(String.Format("IntersectsBoundingBox failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("twentytwofloats")]
        public void IntersectsFrustum(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44,
            float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
        {
            XNABoundingBox xnaBox = new XNABoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));
            Microsoft.Xna.Framework.Matrix xnaMtrix = new Microsoft.Xna.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            Microsoft.Xna.Framework.BoundingFrustum xnaFrustum = new Microsoft.Xna.Framework.BoundingFrustum(xnaMtrix);

            ANXBoundingBox anxBox = new ANXBoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));
            ANX.Framework.Matrix anxMatrix = new ANX.Framework.Matrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANX.Framework.BoundingFrustum anxFrustum = new ANX.Framework.BoundingFrustum(anxMatrix);

            bool xna = xnaBox.Intersects(xnaFrustum);
            bool anx = anxBox.Intersects(anxFrustum);

            if (xna.Equals(anx))
                Assert.Pass("IntersectsFrustum passed");
            else
                Assert.Fail(String.Format("IntersectsFrustum failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("tenfloats")]
        public void IntersectsBoundingSphere(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax, float xS, float yS, float zS, float rS)
        {
            XNABoundingBox xnaBox = new XNABoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));
            Microsoft.Xna.Framework.BoundingSphere xnaSphere = new Microsoft.Xna.Framework.BoundingSphere(new XNAVector3(xS, yS, zS), rS);

            ANXBoundingBox anxBox = new ANXBoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));
            ANX.Framework.BoundingSphere anxSphere = new ANX.Framework.BoundingSphere(new ANXVector3(xS, yS, zS), rS);

            bool xna = xnaBox.Intersects(xnaSphere);
            bool anx = anxBox.Intersects(anxSphere);

            if (xna.Equals(anx))
                Assert.Pass("IntersectsBoundingSphere passed");
            else
                Assert.Fail(String.Format("IntersectsBoundingSphere failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("tenfloats")]
        public void IntersectsPlane(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax, float xP, float yP, float zP, float dP)
        {
            XNABoundingBox xnaBox = new XNABoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));
            Microsoft.Xna.Framework.Plane xnaPlane = new Microsoft.Xna.Framework.Plane(xP, yP, zP, dP);

            ANXBoundingBox anxBox = new ANXBoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));
            ANX.Framework.Plane anxPlane = new ANX.Framework.Plane(xP, yP, zP, dP);

            Microsoft.Xna.Framework.PlaneIntersectionType xna = xnaBox.Intersects(xnaPlane);
            ANX.Framework.PlaneIntersectionType anx = anxBox.Intersects(anxPlane);

            if ((int)xna == (int)anx)
                Assert.Pass("IntersectsPlane passed");
            else
                Assert.Fail(String.Format("IntersectsPlane failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("twelvefloats")]
        public void IntersectsRay(
            float xMin1, float yMin1, float zMin1, float xMax1, float yMax1, float zMax1,
            float xRay, float yRay, float zRay, float xDir, float yDir, float zDir)
        {
            XNABoundingBox xnaBox = new XNABoundingBox(new XNAVector3(xMin1, yMin1, zMin1), new XNAVector3(xMax1, yMax1, zMax1));
            Microsoft.Xna.Framework.Ray xnaRay = new Microsoft.Xna.Framework.Ray(new XNAVector3(xRay, yRay, zRay), new XNAVector3(xDir, yDir, zDir));

            ANXBoundingBox anxBox = new ANXBoundingBox(new ANXVector3(xMin1, yMin1, zMin1), new ANXVector3(xMax1, yMax1, zMax1));
            ANX.Framework.Ray anxRay = new ANX.Framework.Ray(new ANXVector3(xRay, yRay, zRay), new ANXVector3(xDir, yDir, zDir));

            float? xna = xnaBox.Intersects(xnaRay);
            float? anx = anxBox.Intersects(anxRay);

            if (xna.Equals(anx))
                Assert.Pass("IntersectsRay passed");
            else
                Assert.Fail(String.Format("IntersectsRay failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }
        #endregion

        #region Properties
        [Test, TestCaseSource("sixfloats")]
        public void Min(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
        {
            XNABoundingBox xnaBox = new XNABoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));

            ANXBoundingBox anxBox = new ANXBoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));

            XNAVector3 xna = xnaBox.Min;
            XNAVector3 anx = xnaBox.Min;

            if (xna.X == anx.X &&
                xna.Y == anx.Y &&
                xna.Z == anx.Z)
                Assert.Pass("Min passed");
            else
                Assert.Fail(String.Format("Min failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("sixfloats")]
        public void Max(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
        {
            XNABoundingBox xnaBox = new XNABoundingBox(new XNAVector3(xMin, yMin, zMin), new XNAVector3(xMax, yMax, zMax));

            ANXBoundingBox anxBox = new ANXBoundingBox(new ANXVector3(xMin, yMin, zMin), new ANXVector3(xMax, yMax, zMax));

            XNAVector3 xna = xnaBox.Max;
            XNAVector3 anx = xnaBox.Max;

            if (xna.X == anx.X &&
                xna.Y == anx.Y &&
                xna.Z == anx.Z)
                Assert.Pass("Max passed");
            else
                Assert.Fail(String.Format("Max failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }
        #endregion

        #region Operators
        [Test, TestCaseSource("twelvefloats")]
        public void EqualsOperator(
            float xMin1, float yMin1, float zMin1, float xMax1, float yMax1, float zMax1,
            float xMin2, float yMin2, float zMin2, float xMax2, float yMax2, float zMax2)
        {
            XNABoundingBox xnaBox1 = new XNABoundingBox(new XNAVector3(xMin1, yMin1, zMin1), new XNAVector3(xMax1, yMax1, zMax1));
            XNABoundingBox xnaBox2 = new XNABoundingBox(new XNAVector3(xMin2, yMin2, zMin2), new XNAVector3(xMax2, yMax2, zMax2));

            ANXBoundingBox anxBox1 = new ANXBoundingBox(new ANXVector3(xMin1, yMin1, zMin1), new ANXVector3(xMax1, yMax1, zMax1));
            ANXBoundingBox anxBox2 = new ANXBoundingBox(new ANXVector3(xMin2, yMin2, zMin2), new ANXVector3(xMax2, yMax2, zMax2));

            bool xna = xnaBox1 == xnaBox2;
            bool anx = anxBox1 == anxBox2;

            if (xna.Equals(anx))
                Assert.Pass("EqualsOperator passed");
            else
                Assert.Fail(String.Format("EqualsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }

        [Test, TestCaseSource("twelvefloats")]
        public void UnequalsOperator(
            float xMin1, float yMin1, float zMin1, float xMax1, float yMax1, float zMax1,
            float xMin2, float yMin2, float zMin2, float xMax2, float yMax2, float zMax2)
        {
            XNABoundingBox xnaBox1 = new XNABoundingBox(new XNAVector3(xMin1, yMin1, zMin1), new XNAVector3(xMax1, yMax1, zMax1));
            XNABoundingBox xnaBox2 = new XNABoundingBox(new XNAVector3(xMin2, yMin2, zMin2), new XNAVector3(xMax2, yMax2, zMax2));

            ANXBoundingBox anxBox1 = new ANXBoundingBox(new ANXVector3(xMin1, yMin1, zMin1), new ANXVector3(xMax1, yMax1, zMax1));
            ANXBoundingBox anxBox2 = new ANXBoundingBox(new ANXVector3(xMin2, yMin2, zMin2), new ANXVector3(xMax2, yMax2, zMax2));

            bool xna = xnaBox1 != xnaBox2;
            bool anx = anxBox1 != anxBox2;

            if (xna.Equals(anx))
                Assert.Pass("UnequalsOperator passed");
            else
                Assert.Fail(String.Format("UnequalsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
        }
        #endregion
    }
}
