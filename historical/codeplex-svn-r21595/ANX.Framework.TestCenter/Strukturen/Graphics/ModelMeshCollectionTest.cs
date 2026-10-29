using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNAModelMesh = Microsoft.Xna.Framework.Graphics.ModelMesh;
using XNAModelMeshCollection = Microsoft.Xna.Framework.Graphics.ModelMeshCollection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class ModelMeshCollectionTest
    {
        [Test]
        public void Constructor()
        {
            XNAModelMeshCollection xna;
            ModelMeshCollection anx;
            CreateCollections(out xna, out anx);

            Assert.AreEqual(xna.Count, anx.Count);
        }

        [Test]
        public void AccessorArgumentNull()
        {
            XNAModelMeshCollection xna;
            ModelMeshCollection anx;
            CreateCollections(out xna, out anx);

            TestDelegate xnaDeleg = delegate { XNAModelMesh xnaMesh = xna[""]; };
            TestDelegate anxDeleg = delegate { ModelMesh anxMesh = anx[""]; };

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentNullException>(xnaDeleg),
                Assert.Throws<ArgumentNullException>(anxDeleg), "AccessorArgumentNull");
        }

        [Test]
        public void AccessorKeyNotFound()
        {
            XNAModelMeshCollection xna;
            ModelMeshCollection anx;
            CreateCollections(out xna, out anx);

            TestDelegate xnaDeleg = delegate { XNAModelMesh xnaMesh = xna["test"]; };
            TestDelegate anxDeleg = delegate { ModelMesh anxMesh = anx["test"]; };

            AssertHelper.ConvertEquals(Assert.Throws<KeyNotFoundException>(xnaDeleg),
                Assert.Throws<KeyNotFoundException>(anxDeleg), "AccessorKeyNotFound");
        }

        [Test]
        public void Accessor()
        {
            XNAModelMeshCollection xna;
            ModelMeshCollection anx;
            CreateCollections(out xna, out anx);

            XNAModelMesh xnaMesh = xna["mesh1"];
            ModelMesh anxMesh = anx["mesh1"];

            Assert.AreEqual(xnaMesh.Name, anxMesh.Name);
            Assert.AreEqual(xnaMesh.Tag, anxMesh.Tag);
            Assert.AreEqual(xnaMesh.ParentBone, anxMesh.ParentBone);
            Assert.AreEqual(xnaMesh.MeshParts.Count, anxMesh.MeshParts.Count);
            AssertHelper.ConvertEquals(xnaMesh.BoundingSphere, anxMesh.BoundingSphere, "Accessor");
        }

        private void CreateCollections(out XNAModelMeshCollection xna, out ModelMeshCollection anx)
        {
            XNAModelMesh xnaMesh;
            ModelMesh anxMesh;
            ModelMeshTest.CreateMeshes(out xnaMesh, out anxMesh);
            var xnaParameters = new object[] { new[] { xnaMesh } };
            xna = (XNAModelMeshCollection)Activator.CreateInstance(typeof(XNAModelMeshCollection),
                BindingFlags.NonPublic | BindingFlags.Instance, null, xnaParameters, CultureInfo.InvariantCulture);

            var anxParameters = new object[] { new[] { anxMesh } };
            anx = (ModelMeshCollection)Activator.CreateInstance(typeof(ModelMeshCollection),
                BindingFlags.NonPublic | BindingFlags.Instance, null, anxParameters, CultureInfo.InvariantCulture);
        }
    }
}
