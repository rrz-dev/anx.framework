using System;
using System.Globalization;
using System.Reflection;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNAModelMeshPart = Microsoft.Xna.Framework.Graphics.ModelMeshPart;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class ModelMeshPartTest
    {
        [Test]
        public void Constructor()
        {
            XNAModelMeshPart xna;
            ModelMeshPart anx;
            CreateMeshes(out xna, out anx);

            Assert.AreEqual(xna.StartIndex, anx.StartIndex);
            Assert.AreEqual(xna.Tag, anx.Tag);
            Assert.AreEqual(xna.VertexOffset, anx.VertexOffset);
            Assert.AreEqual(xna.PrimitiveCount, anx.PrimitiveCount);
            Assert.AreEqual(xna.NumVertices, anx.NumVertices);
        }

        public static void CreateMeshes(out XNAModelMeshPart xna, out ModelMeshPart anx)
        {
            var parameters = new object[] { 4, 64, 0, 20, 777 };

            xna = (XNAModelMeshPart)Activator.CreateInstance(typeof(XNAModelMeshPart),
                BindingFlags.NonPublic | BindingFlags.Instance, null, parameters, CultureInfo.InvariantCulture);

            anx = (ModelMeshPart)Activator.CreateInstance(typeof(ModelMeshPart),
                BindingFlags.NonPublic | BindingFlags.Instance, null, parameters, CultureInfo.InvariantCulture);
        }
    }
}
