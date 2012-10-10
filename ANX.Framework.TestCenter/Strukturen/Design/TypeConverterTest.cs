#region Using Statements
using System;
using NUnit.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#region Datatype Usings
using XnaVector2 = Microsoft.Xna.Framework.Vector2;
using AnxVector2 = ANX.Framework.Vector2;

using XnaVector3 = Microsoft.Xna.Framework.Vector3;
using AnxVector3 = ANX.Framework.Vector3;

using XnaVector4 = Microsoft.Xna.Framework.Vector4;
using AnxVector4 = ANX.Framework.Vector4;

using XnaQuaternion = Microsoft.Xna.Framework.Quaternion;
using AnxQuaternion = ANX.Framework.Quaternion;

using XnaMatrix = Microsoft.Xna.Framework.Matrix;
using AnxMatrix = ANX.Framework.Matrix;

using XnaRectangle = Microsoft.Xna.Framework.Rectangle;
using AnxRectangle = ANX.Framework.Rectangle;

using XnaRay = Microsoft.Xna.Framework.Ray;
using AnxRay = ANX.Framework.Ray;

using XnaPoint = Microsoft.Xna.Framework.Point;
using AnxPoint = ANX.Framework.Point;

using XnaPlane = Microsoft.Xna.Framework.Plane;
using AnxPlane = ANX.Framework.Plane;

using XnaBoundingBox = Microsoft.Xna.Framework.BoundingBox;
using AnxBoundingBox = ANX.Framework.BoundingBox;

using XnaBoundingSphere = Microsoft.Xna.Framework.BoundingSphere;
using AnxBoundingSphere = ANX.Framework.BoundingSphere;

using XnaBoundingFrustum = Microsoft.Xna.Framework.BoundingFrustum;
using AnxBoundingFrustum = ANX.Framework.BoundingFrustum;

using XNADesign = Microsoft.Xna.Framework.Design;
using ANXDesign = ANX.Framework.Design;
#endregion

namespace ANX.Framework.TestCenter.Strukturen.Design
{
    [TestFixture]
    public class TypeConverterTest
    {
        [Test]
        public void ConvertVector2Test()
        {
            var xnaConverter = new XNADesign.Vector2Converter();
            var anxConverter = new ANXDesign.Vector2Converter();
            var xnaObject = new XnaVector2(1, 2);
            var anxObject = new AnxVector2(1, 2);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            Assert.AreEqual(anxObject, anxConverter.ConvertFrom(xnaConverter.ConvertToString(xnaObject)));
        }

        [Test]
        public void ConvertVector3Test()
        {
            var xnaConverter = new XNADesign.Vector3Converter();
            var anxConverter = new ANXDesign.Vector3Converter();
            var xnaObject = new XnaVector3(1, 2, 3);
            var anxObject = new AnxVector3(1, 2, 3);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            Assert.AreEqual(anxObject, anxConverter.ConvertFrom(xnaConverter.ConvertToString(xnaObject)));
        }

        [Test]
        public void ConvertVector4Test()
        {
            var xnaConverter = new XNADesign.Vector4Converter();
            var anxConverter = new ANXDesign.Vector4Converter();
            var xnaObject = new XnaVector4(1, 2, 3, 4);
            var anxObject = new AnxVector4(1, 2, 3, 4);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            Assert.AreEqual(anxObject, anxConverter.ConvertFrom(xnaConverter.ConvertToString(xnaObject)));
        }

        [Test]
        public void ConvertQuaternionTest()
        {
            var xnaConverter = new XNADesign.QuaternionConverter();
            var anxConverter = new ANXDesign.QuaternionConverter();
            var xnaObject = new XnaQuaternion(1, 2, 3, 4);
            var anxObject = new AnxQuaternion(1, 2, 3, 4);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            Assert.AreEqual(anxObject, anxConverter.ConvertFrom(xnaConverter.ConvertToString(xnaObject)));
        }

        [Test]
        public void ConvertRectangleTest()
        {
            var xnaConverter = new XNADesign.RectangleConverter();
            var anxConverter = new ANXDesign.RectangleConverter();
            var xnaObject = new XnaRectangle(1, 2, 3, 4);
            var anxObject = new AnxRectangle(1, 2, 3, 4);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // rectangle cannot convert from string
        }

        [Test]
        public void ConvertRayTest()
        {
            var xnaConverter = new XNADesign.RayConverter();
            var anxConverter = new ANXDesign.RayConverter();
            var xnaObject = new XnaRay(new XnaVector3(1, 2, 3), new XnaVector3(4, 5, 6));
            var anxObject = new AnxRay(new AnxVector3(1, 2, 3), new AnxVector3(4, 5, 6));

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // ray cannot convert from string
        }

        [Test]
        public void ConvertPointTest()
        {
            var xnaConverter = new XNADesign.PointConverter();
            var anxConverter = new ANXDesign.PointConverter();
            var xnaObject = new XnaPoint(1, 2);
            var anxObject = new AnxPoint(1, 2);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            Assert.AreEqual(anxObject, anxConverter.ConvertFrom(xnaConverter.ConvertToString(xnaObject)));
        }

        [Test]
        public void ConvertPlaneTest()
        {
            var xnaConverter = new XNADesign.PlaneConverter();
            var anxConverter = new ANXDesign.PlaneConverter();
            var xnaObject = new XnaPlane(new XnaVector3(1, 2, 3), 4);
            var anxObject = new AnxPlane(new AnxVector3(1, 2, 3), 4);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // plane cannot convert from string
        }

        [Test]
        public void ConvertBoundingBoxTest()
        {
            var xnaConverter = new XNADesign.BoundingBoxConverter();
            var anxConverter = new ANXDesign.BoundingBoxConverter();
            var xnaObject = new XnaBoundingBox(new XnaVector3(1, 2, 3), new XnaVector3(4, 5, 6));
            var anxObject = new AnxBoundingBox(new AnxVector3(1, 2, 3), new AnxVector3(4, 5, 6));

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // plane cannot convert from string
        }

        [Test]
        public void ConvertBoundingSphereTest()
        {
            var xnaConverter = new XNADesign.BoundingSphereConverter();
            var anxConverter = new ANXDesign.BoundingSphereConverter();
            var xnaObject = new XnaBoundingSphere(new XnaVector3(1, 2, 3), 4);
            var anxObject = new AnxBoundingSphere(new AnxVector3(1, 2, 3), 4);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // plane cannot convert from string
        }

        [Test]
        public void ConvertMatrixTest()
        {
            var xnaConverter = new XNADesign.MatrixConverter();
            var anxConverter = new ANXDesign.MatrixConverter();
            var xnaObject = new XnaMatrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            var anxObject = new AnxMatrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // matrix cannot convert from string
        }

        [Test]
        public void ConvertBoundingFrustumTest()
        {
            var xnaMatrix = new XnaMatrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            var anxMatrix = new AnxMatrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            var xnaObject = new XnaBoundingFrustum(xnaMatrix);
            var anxObject = new AnxBoundingFrustum(anxMatrix);

            AssertHelper.ConvertEquals(xnaObject, anxObject, "ConvertBoundingFrustumTest");
        }
    }
}
