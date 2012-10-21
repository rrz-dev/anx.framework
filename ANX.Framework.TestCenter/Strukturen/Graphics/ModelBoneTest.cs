using System;
using System.Globalization;
using System.Reflection;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNAModelBone = Microsoft.Xna.Framework.Graphics.ModelBone;
using XNAMatrix = Microsoft.Xna.Framework.Matrix;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class ModelBoneTest
    {
        [Test]
        public void Constructor()
        {
            XNAModelBone xna;
            ModelBone anx;
            CreateBones(out xna, out anx);

            Assert.AreEqual(xna.Name, anx.Name);
            Assert.AreEqual(xna.Index, anx.Index);
            Assert.AreEqual(xna.Parent, anx.Parent);
            Assert.AreEqual(xna.Children, anx.Children);
            AssertHelper.ConvertEquals(xna.Transform, anx.Transform, "Constructor");
        }

        public static void CreateBones(out XNAModelBone xna, out ModelBone anx)
        {
            var xnaParameters = new object[] { "bone1", XNAMatrix.Identity, 15 };
            xna = (XNAModelBone)Activator.CreateInstance(typeof(XNAModelBone),
                BindingFlags.NonPublic | BindingFlags.Instance, null, xnaParameters, CultureInfo.InvariantCulture);

            var anxParameters = new object[] { "bone1", Matrix.Identity, 15 };
            anx = (ModelBone)Activator.CreateInstance(typeof(ModelBone),
                BindingFlags.NonPublic | BindingFlags.Instance, null, anxParameters, CultureInfo.InvariantCulture);
        }
    }
}
