using System;
using System.Globalization;
using System.Reflection;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNAModelMesh = Microsoft.Xna.Framework.Graphics.ModelMesh;
using XNAModelMeshPart = Microsoft.Xna.Framework.Graphics.ModelMeshPart;
using XNABoundingSphere = Microsoft.Xna.Framework.BoundingSphere;
using XNAVector3 = Microsoft.Xna.Framework.Vector3;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class ModelMeshTest
    {
        [Test]
        public void Constructor()
        {
            XNAModelMesh xna;
            ModelMesh anx;
            CreateMeshes(out xna, out anx);

            Assert.AreEqual(xna.Name, anx.Name);
            Assert.AreEqual(xna.Tag, anx.Tag);
            Assert.AreEqual(xna.ParentBone, anx.ParentBone);
            Assert.AreEqual(xna.MeshParts.Count, anx.MeshParts.Count);
            Assert.AreEqual(xna.Effects.Count, anx.Effects.Count);
            AssertHelper.ConvertEquals(xna.BoundingSphere, anx.BoundingSphere, "Constructor");
        }

        public static void CreateMeshes(out XNAModelMesh xna, out ModelMesh anx)
        {
            var xnaParameters = new object[]
            {
                "mesh1",
                null,
                new XNABoundingSphere(XNAVector3.UnitY, 2f),
                new XNAModelMeshPart[0],
                17
            };
            xna = (XNAModelMesh)Activator.CreateInstance(typeof(XNAModelMesh),
                BindingFlags.NonPublic | BindingFlags.Instance, null, xnaParameters, CultureInfo.InvariantCulture);

            var anxParameters = new object[]
            {
                "mesh1",
                null,
                new BoundingSphere(Vector3.UnitY, 2f),
                new ModelMeshPart[0],
                17
            };
            anx = (ModelMesh)Activator.CreateInstance(typeof(ModelMesh),
                BindingFlags.NonPublic | BindingFlags.Instance, null, anxParameters, CultureInfo.InvariantCulture);
        }
    }
}
