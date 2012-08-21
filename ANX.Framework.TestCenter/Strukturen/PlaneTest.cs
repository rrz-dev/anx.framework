#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXVector3 = ANX.Framework.Vector3;

using XNAVector4 = Microsoft.Xna.Framework.Vector4;
using ANXVector4 = ANX.Framework.Vector4;

using XNAMatrix = Microsoft.Xna.Framework.Matrix;
using ANXMatrix = ANX.Framework.Matrix;

using XNAQuaternion = Microsoft.Xna.Framework.Quaternion;
using ANXQuaternion = ANX.Framework.Quaternion;

using XNABoundingBox = Microsoft.Xna.Framework.BoundingBox;
using ANXBoundingBox = ANX.Framework.BoundingBox;

using XNABoundingSphere = Microsoft.Xna.Framework.BoundingSphere;
using ANXBoundingSphere = ANX.Framework.BoundingSphere;

using XNABoundingFrustum = Microsoft.Xna.Framework.BoundingFrustum;
using ANXBoundingFrustum = ANX.Framework.BoundingFrustum;

using XNAPlane = Microsoft.Xna.Framework.Plane;
using ANXPlane = ANX.Framework.Plane;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class PlaneTest
    {
        static object[] eightfloats =
        {
            new object[] {  1, 2, 3, 4, 5, 6, 7, 8 },
            new object[] {  8, 7, 6, 5, 4, 3, 2, 1 }
        };

        [Test]
        public void IntersectsSphere()
        {
            XNAPlane xna = new XNAPlane(new XNAVector3(-10, -10, -10), new XNAVector3(0, -10, -10), new XNAVector3(-10, 0, -10));
            ANXPlane anx = new ANXPlane(new ANXVector3(-10, -10, -10), new ANXVector3(0, -10, -10), new ANXVector3(-10, 0, -10));
            XNABoundingSphere xSphere = new XNABoundingSphere(new XNAVector3(-20, -20, -20), 5);
            ANXBoundingSphere aSphere = new ANXBoundingSphere(new ANXVector3(-20, -20, -20), 5);

            Assert.AreEqual(xna.Intersects(xSphere).ToString(), anx.Intersects(aSphere).ToString());
        }

        #region Dot
        [Test, TestCaseSource("eightfloats")]
        public static void DotVector4(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            XNAVector4 xnaVector = new XNAVector4(x1, y1, z1, w1);
            float xnaResult = xnaPlane.Dot(xnaVector);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            ANXVector4 anxVector = new ANXVector4(x1, y1, z1, w1);
            float anxResult = anxPlane.Dot(anxVector);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "DotVector4");
        }

        [Test, TestCaseSource("eightfloats")]
        public static void DotVector4Ref(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            XNAVector4 xnaVector = new XNAVector4(x1, y1, z1, w1);
            float xnaResult;
            xnaPlane.Dot(ref xnaVector, out xnaResult);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            ANXVector4 anxVector = new ANXVector4(x1, y1, z1, w1);
            float anxResult;
            anxPlane.Dot(ref anxVector, out anxResult);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "DotVector4Ref");
        }

        [Test, TestCaseSource("eightfloats")]
        public static void DotCoordinateVector3(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            XNAVector3 xnaVector = new XNAVector3(x1, y1, z1);
            float xnaResult = xnaPlane.DotCoordinate(xnaVector);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            ANXVector3 anxVector = new ANXVector3(x1, y1, z1);
            float anxResult = anxPlane.DotCoordinate(anxVector);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "DotCoordinateVector3");
        }

        [Test, TestCaseSource("eightfloats")]
        public static void DotCoordinateVector3Ref(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            XNAVector3 xnaVector = new XNAVector3(x1, y1, z1);
            float xnaResult;
            xnaPlane.DotCoordinate(ref xnaVector, out xnaResult);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            ANXVector3 anxVector = new ANXVector3(x1, y1, z1);
            float anxResult;
            anxPlane.DotCoordinate(ref anxVector, out anxResult);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "DotCoordinateVector3Ref");
        }


        [Test, TestCaseSource("eightfloats")]
        public static void DotNormalVector3(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            XNAVector3 xnaVector = new XNAVector3(x1, y1, z1);
            float xnaResult = xnaPlane.DotNormal(xnaVector);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            ANXVector3 anxVector = new ANXVector3(x1, y1, z1);
            float anxResult = anxPlane.DotNormal(anxVector);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "DotNormalVector3");
        }

        [Test, TestCaseSource("eightfloats")]
        public static void DotNormalVector3Ref(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            XNAVector3 xnaVector = new XNAVector3(x1, y1, z1);
            float xnaResult;
            xnaPlane.DotNormal(ref xnaVector, out xnaResult);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            ANXVector3 anxVector = new ANXVector3(x1, y1, z1);
            float anxResult;
            anxPlane.DotNormal(ref anxVector, out anxResult);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "DotNormalVector3Ref");
        }
        #endregion

        [Test, TestCaseSource("eightfloats")]
        public static void GetHashCode(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            int xnaResult = xnaPlane.GetHashCode();

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            int anxResult = anxPlane.GetHashCode();

            AssertHelper.ConvertEquals(xnaResult, anxResult, "GetHashCode");
        }

        [Test, TestCaseSource("eightfloats")]
        public static void ToString(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);

            AssertHelper.ConvertEquals(xnaPlane.ToString(), anxPlane.ToString(), "ToString");
        }

        #region Normalize
        [Test, TestCaseSource("eightfloats")]
        public static void Normalize(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            xnaPlane.Normalize();

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            anxPlane.Normalize();

            AssertHelper.ConvertEquals(xnaPlane, anxPlane, "Normalize");
        }

        [Test, TestCaseSource("eightfloats")]
        public static void NormalizePlane(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            xnaPlane = XNAPlane.Normalize(xnaPlane);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            anxPlane = ANXPlane.Normalize(anxPlane);

            AssertHelper.ConvertEquals(xnaPlane, anxPlane, "NormalizePlane");
        }

        [Test, TestCaseSource("eightfloats")]
        public static void NormalizePlaneRef(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xna = new XNAPlane(x1, y1, z1, w1);
            XNAPlane xnaPlane;
            XNAPlane.Normalize(ref xna, out xnaPlane);

            ANXPlane anx = new ANXPlane(x1, y1, z1, w1);
            ANXPlane anxPlane;
            ANXPlane.Normalize(ref anx, out anxPlane);

            AssertHelper.ConvertEquals(xnaPlane, anxPlane, "NormalizePlaneRef");
        }
        #endregion

        #region Transform
        [Test, TestCaseSource("eightfloats")]
        public static void TransformMatrix(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            XNAMatrix xnaMatrix = new XNAMatrix(x1, y1, z1, w1, x2, y2, z2, w2, x1, y1, z1, w1, x2, y2, z2, w2);
            XNAPlane xnaResult = XNAPlane.Transform(xnaPlane, xnaMatrix);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            ANXMatrix anxMatrix = new ANXMatrix(x1, y1, z1, w1, x2, y2, z2, w2, x1, y1, z1, w1, x2, y2, z2, w2);
            ANXPlane anxResult = ANXPlane.Transform(anxPlane, anxMatrix);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "TransformMatrix");
        }

        [Test, TestCaseSource("eightfloats")]
        public static void TransformMatrixRef(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            XNAMatrix xnaMatrix = new XNAMatrix(x1, y1, z1, w1, x2, y2, z2, w2, x1, y1, z1, w1, x2, y2, z2, w2);
            XNAPlane xnaResult;
            XNAPlane.Transform(ref xnaPlane, ref xnaMatrix, out xnaResult);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            ANXMatrix anxMatrix = new ANXMatrix(x1, y1, z1, w1, x2, y2, z2, w2, x1, y1, z1, w1, x2, y2, z2, w2);
            ANXPlane anxResult; 
            ANXPlane.Transform(ref anxPlane, ref anxMatrix, out anxResult);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "TransformMatrixRef");
        }

        [Test, TestCaseSource("eightfloats")]
        public static void TransformQuaternion(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            XNAQuaternion xnaQuat = new XNAQuaternion(x2, y2, z2, w2);
            XNAPlane xnaResult = XNAPlane.Transform(xnaPlane, xnaQuat);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            ANXQuaternion anxQuat = new ANXQuaternion(x2, y2, z2, w2);
            ANXPlane anxResult = ANXPlane.Transform(anxPlane, anxQuat);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "TransformQuaternion");
        }

        [Test, TestCaseSource("eightfloats")]
        public static void TransformQuaternionRef(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2)
        {
            XNAPlane xnaPlane = new XNAPlane(x1, y1, z1, w1);
            XNAQuaternion xnaQuat = new XNAQuaternion(x2, y2, z2, w2);
            XNAPlane xnaResult;
            XNAPlane.Transform(ref xnaPlane, ref xnaQuat, out xnaResult);

            ANXPlane anxPlane = new ANXPlane(x1, y1, z1, w1);
            ANXQuaternion anxQuat = new ANXQuaternion(x2, y2, z2, w2);
            ANXPlane anxResult;
            ANXPlane.Transform(ref anxPlane, ref anxQuat, out anxResult);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "TransformQuaternionRef");
        }
        #endregion
    }
}
