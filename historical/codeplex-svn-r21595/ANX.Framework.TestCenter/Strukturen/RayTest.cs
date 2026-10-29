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

using XNABoundingBox = Microsoft.Xna.Framework.BoundingBox;
using ANXBoundingBox = ANX.Framework.BoundingBox;

using XNABoundingSphere = Microsoft.Xna.Framework.BoundingSphere;
using ANXBoundingSphere = ANX.Framework.BoundingSphere;

using XNABoundingFrustum = Microsoft.Xna.Framework.BoundingFrustum;
using ANXBoundingFrustum = ANX.Framework.BoundingFrustum;

using XNAPlane = Microsoft.Xna.Framework.Plane;
using ANXPlane = ANX.Framework.Plane;

using XNARay = Microsoft.Xna.Framework.Ray;
using ANXRay = ANX.Framework.Ray;
namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class RayTest
    {
        [Test]
        public void IntersectsPlane()
        {
            XNAPlane xna = new XNAPlane(new XNAVector3(-10, -10, -10), new XNAVector3(0, -10, -10), new XNAVector3(-10, 0, -10));
            ANXPlane anx = new ANXPlane(new ANXVector3(-10, -10, -10), new ANXVector3(0, -10, -10), new ANXVector3(-10, 0, -10));
            XNARay xray = new XNARay(new XNAVector3(0, 0, 0), new XNAVector3(-1, -1 , 0));
            ANXRay aray = new ANXRay(new ANXVector3(0, 0, 0), new ANXVector3(-1, -1, 0));

            Assert.AreEqual(xray.Intersects(xna), aray.Intersects(anx));

        }
    }
}
