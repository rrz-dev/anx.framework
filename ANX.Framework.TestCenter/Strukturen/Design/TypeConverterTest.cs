#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
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

using XnaVector3 = Microsoft.Xna.Framework.Vector3;
using AnxVector3 = ANX.Framework.Vector3;

namespace ANX.Framework.TestCenter.Strukturen.Design
{
    [TestFixture]
    public class TypeConverterTest
    {
        [Test]
        public void ConvertVector2Test()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.Vector2Converter();
            var anxConverter = new ANX.Framework.Design.Vector2Converter();

            var xnaObject = new Microsoft.Xna.Framework.Vector2(1, 2);
            var anxObject = new ANX.Framework.Vector2(1, 2);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            Assert.AreEqual(anxObject, anxConverter.ConvertFrom(xnaConverter.ConvertToString(xnaObject)));
        }

        [Test]
        public void ConvertVector3Test()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.Vector3Converter();
            var anxConverter = new ANX.Framework.Design.Vector3Converter();

            var xnaObject = new Microsoft.Xna.Framework.Vector3(1, 2, 3);
            var anxObject = new ANX.Framework.Vector3(1, 2, 3);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            Assert.AreEqual(anxObject, anxConverter.ConvertFrom(xnaConverter.ConvertToString(xnaObject)));
        }

        [Test]
        public void ConvertVector4Test()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.Vector4Converter();
            var anxConverter = new ANX.Framework.Design.Vector4Converter();

            var xnaObject = new Microsoft.Xna.Framework.Vector4(1, 2, 3, 4);
            var anxObject = new ANX.Framework.Vector4(1, 2, 3, 4);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            Assert.AreEqual(anxObject, anxConverter.ConvertFrom(xnaConverter.ConvertToString(xnaObject)));
        }

        [Test]
        public void ConvertQuaternionTest()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.QuaternionConverter();
            var anxConverter = new ANX.Framework.Design.QuaternionConverter();

            var xnaObject = new Microsoft.Xna.Framework.Quaternion(1, 2, 3, 4);
            var anxObject = new ANX.Framework.Quaternion(1, 2, 3, 4);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            Assert.AreEqual(anxObject, anxConverter.ConvertFrom(xnaConverter.ConvertToString(xnaObject)));
        }

        [Test]
        public void ConvertRectangleTest()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.RectangleConverter();
            var anxConverter = new ANX.Framework.Design.RectangleConverter();

            var xnaObject = new Microsoft.Xna.Framework.Rectangle(1, 2, 3, 4);
            var anxObject = new ANX.Framework.Rectangle(1, 2, 3, 4);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // rectangle cannot convert from string
        }

        [Test]
        public void ConvertRayTest()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.RayConverter();
            var anxConverter = new ANX.Framework.Design.RayConverter();

            var xnaObject = new Microsoft.Xna.Framework.Ray(new XnaVector3(1, 2, 3), new XnaVector3(4, 5, 6));
            var anxObject = new ANX.Framework.Ray(new AnxVector3(1, 2, 3), new AnxVector3(4, 5, 6));

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // ray cannot convert from string
        }

        [Test]
        public void ConvertPointTest()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.PointConverter();
            var anxConverter = new ANX.Framework.Design.PointConverter();

            var xnaObject = new Microsoft.Xna.Framework.Point(1, 2);
            var anxObject = new ANX.Framework.Point(1, 2);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            Assert.AreEqual(anxObject, anxConverter.ConvertFrom(xnaConverter.ConvertToString(xnaObject)));
        }

        [Test]
        public void ConvertPlaneTest()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.PlaneConverter();
            var anxConverter = new ANX.Framework.Design.PlaneConverter();

            var xnaObject = new Microsoft.Xna.Framework.Plane(new XnaVector3(1, 2, 3), 4);
            var anxObject = new ANX.Framework.Plane(new AnxVector3(1, 2, 3), 4);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // plane cannot convert from string
        }

        [Test]
        public void ConvertBoundingBoxTest()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.BoundingBoxConverter();
            var anxConverter = new ANX.Framework.Design.BoundingBoxConverter();

            var xnaObject = new Microsoft.Xna.Framework.BoundingBox(new XnaVector3(1, 2, 3), new XnaVector3(4, 5, 6));
            var anxObject = new ANX.Framework.BoundingBox(new AnxVector3(1, 2, 3), new AnxVector3(4, 5, 6));

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // plane cannot convert from string
        }

        [Test]
        public void ConvertBoundingSphereTest()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.BoundingSphereConverter();
            var anxConverter = new ANX.Framework.Design.BoundingSphereConverter();

            var xnaObject = new Microsoft.Xna.Framework.BoundingSphere(new XnaVector3(1, 2, 3), 4);
            var anxObject = new ANX.Framework.BoundingSphere(new AnxVector3(1, 2, 3), 4);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // plane cannot convert from string
        }

        [Test]
        public void ConvertMatrixTest()
        {
            var xnaConverter = new Microsoft.Xna.Framework.Design.MatrixConverter();
            var anxConverter = new ANX.Framework.Design.MatrixConverter();

            var xnaObject = new Microsoft.Xna.Framework.Matrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            var anxObject = new ANX.Framework.Matrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Assert.AreEqual(xnaConverter.ConvertToString(xnaObject), anxConverter.ConvertToString(anxObject));
            // matrix cannot convert from string
        }

        [Test]
        public void ConvertBoundingFrustumTest()
        {
            var xnaObject = new Microsoft.Xna.Framework.BoundingFrustum(new Microsoft.Xna.Framework.Matrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16));
            var anxObject = new ANX.Framework.BoundingFrustum(new ANX.Framework.Matrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16));

            AssertHelper.ConvertEquals(xnaObject, anxObject, "ConvertBoundingFrustumTest");
        }
    }
}
