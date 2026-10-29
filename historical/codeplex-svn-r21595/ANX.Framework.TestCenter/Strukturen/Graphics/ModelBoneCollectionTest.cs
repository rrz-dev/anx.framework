using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNAModelBone = Microsoft.Xna.Framework.Graphics.ModelBone;
using XNAModelBoneCollection = Microsoft.Xna.Framework.Graphics.ModelBoneCollection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class ModelBoneCollectionTest
    {
        [Test]
        public void Constructor()
        {
            XNAModelBoneCollection xna;
            ModelBoneCollection anx;
            CreateCollections(out xna, out anx);

            Assert.AreEqual(xna.Count, anx.Count);
        }

        [Test]
        public void AccessorArgumentNull()
        {
            XNAModelBoneCollection xna;
            ModelBoneCollection anx;
            CreateCollections(out xna, out anx);

            TestDelegate xnaDeleg = delegate { XNAModelBone xnaBone = xna[""]; };
            TestDelegate anxDeleg = delegate { ModelBone anxBone = anx[""]; };

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentNullException>(xnaDeleg),
                Assert.Throws<ArgumentNullException>(anxDeleg), "AccessorArgumentNull");
        }

        [Test]
        public void AccessorKeyNotFound()
        {
            XNAModelBoneCollection xna;
            ModelBoneCollection anx;
            CreateCollections(out xna, out anx);

            TestDelegate xnaDeleg = delegate { XNAModelBone xnaBone = xna["test"]; };
            TestDelegate anxDeleg = delegate { ModelBone anxBone = anx["test"]; };

            AssertHelper.ConvertEquals(Assert.Throws<KeyNotFoundException>(xnaDeleg),
                Assert.Throws<KeyNotFoundException>(anxDeleg), "AccessorKeyNotFound");
        }

        [Test]
        public void Accessor()
        {
            XNAModelBoneCollection xna;
            ModelBoneCollection anx;
            CreateCollections(out xna, out anx);

            XNAModelBone xnaBone = xna["bone1"];
            ModelBone anxBone = anx["bone1"];

            Assert.AreEqual(xnaBone.Index, anxBone.Index);
            AssertHelper.ConvertEquals(xnaBone.Transform, anxBone.Transform, "Accessor");
        }

        private void CreateCollections(out XNAModelBoneCollection xna, out ModelBoneCollection anx)
        {
            XNAModelBone xnaBone;
            ModelBone anxBone;
            ModelBoneTest.CreateBones(out xnaBone, out anxBone);
            var xnaParameters = new object[] { new[] { xnaBone } };
            xna = (XNAModelBoneCollection)Activator.CreateInstance(typeof(XNAModelBoneCollection),
                BindingFlags.NonPublic | BindingFlags.Instance, null, xnaParameters, CultureInfo.InvariantCulture);

            var anxParameters = new object[] { new[] { anxBone } };
            anx = (ModelBoneCollection)Activator.CreateInstance(typeof(ModelBoneCollection),
                BindingFlags.NonPublic | BindingFlags.Instance, null, anxParameters, CultureInfo.InvariantCulture);
        }
    }
}
