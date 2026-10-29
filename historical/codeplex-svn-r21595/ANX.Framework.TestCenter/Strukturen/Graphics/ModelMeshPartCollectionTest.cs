using System;
using System.Globalization;
using System.Reflection;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNAModelMeshPart = Microsoft.Xna.Framework.Graphics.ModelMeshPart;
using XNAModelMeshPartCollection = Microsoft.Xna.Framework.Graphics.ModelMeshPartCollection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class ModelMeshPartCollectionTest
    {
        [Test]
        public void Constructor()
        {
            XNAModelMeshPartCollection xna;
            ModelMeshPartCollection anx;
            CreateCollections(out xna, out anx);

            Assert.AreEqual(xna.Count, anx.Count);
        }

        private void CreateCollections(out XNAModelMeshPartCollection xna, out ModelMeshPartCollection anx)
        {
            XNAModelMeshPart xnaMesh;
            ModelMeshPart anxMesh;
            ModelMeshPartTest.CreateMeshes(out xnaMesh, out anxMesh);
            var xnaParameters = new object[] { new[] { xnaMesh } };
            xna = (XNAModelMeshPartCollection)Activator.CreateInstance(typeof(XNAModelMeshPartCollection),
                BindingFlags.NonPublic | BindingFlags.Instance, null, xnaParameters, CultureInfo.InvariantCulture);

            var anxParameters = new object[] { new[] { anxMesh } };
            anx = (ModelMeshPartCollection)Activator.CreateInstance(typeof(ModelMeshPartCollection),
                BindingFlags.NonPublic | BindingFlags.Instance, null, anxParameters, CultureInfo.InvariantCulture);
        }
    }
}
