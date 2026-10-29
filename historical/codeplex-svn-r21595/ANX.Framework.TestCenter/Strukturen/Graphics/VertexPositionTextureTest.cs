using System;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNAVertexPositionTexture = Microsoft.Xna.Framework.Graphics.VertexPositionTexture;
using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using XNAVector2 = Microsoft.Xna.Framework.Vector2;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class VertexPositionTextureTest
    {
        [Test]
        public void VertexDeclaration()
        {
            var xna = XNAVertexPositionTexture.VertexDeclaration;
            var anx = VertexPositionTexture.VertexDeclaration;

            Assert.AreEqual(xna.VertexStride, anx.VertexStride);
            Assert.AreEqual(xna.Name, anx.Name);

            var xnaElements = xna.GetVertexElements();
            var anxElements = anx.GetVertexElements();
            Assert.AreEqual(xnaElements.Length, anxElements.Length);
            for (int index = 0; index < xnaElements.Length; index++)
                AssertHelper.Compare(xnaElements[index], anxElements[index]);
        }

        [Test]
        public void GetHashCodeTest()
        {
            var xna = new XNAVertexPositionTexture(new XNAVector3(1f, 2f, 3f), new XNAVector2(0.5f, 0.5f));
            var anx = new VertexPositionTexture(new Vector3(1f, 2f, 3f), new Vector2(0.5f, 0.5f));

            AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        }

        [Test]
        public void ToStringTest()
        {
            var xna = new XNAVertexPositionTexture(new XNAVector3(1f, 2f, 3f), new XNAVector2(0.5f, 0.5f));
            var anx = new VertexPositionTexture(new Vector3(1f, 2f, 3f), new Vector2(0.5f, 0.5f));

            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString");
        }

        [Test]
        public void Equals0()
        {
            var xna = new XNAVertexPositionTexture(new XNAVector3(1f, 2f, 3f), new XNAVector2(0.5f, 0.5f));
            var anx = new VertexPositionTexture(new Vector3(1f, 2f, 3f), new Vector2(0.5f, 0.5f));

            AssertHelper.ConvertEquals(xna.Equals(null), anx.Equals(null), "Equals0");
        }

        [Test]
        public void Equals1()
        {
            var xna = new XNAVertexPositionTexture(new XNAVector3(1f, 2f, 3f), new XNAVector2(0.5f, 0.5f));
            var anx = new VertexPositionTexture(new Vector3(1f, 2f, 3f), new Vector2(0.5f, 0.5f));

            AssertHelper.ConvertEquals(xna.Equals(xna), anx.Equals(anx), "Equals1");
        }

        [Test]
        public void Equals2()
        {
            var xna = new XNAVertexPositionTexture(new XNAVector3(1f, 2f, 3f), new XNAVector2(0.5f, 0.5f));
            var xna2 = new XNAVertexPositionTexture(new XNAVector3(1f, 2f, 3f), new XNAVector2(0.5f, 0.5f));
            var anx = new VertexPositionTexture(new Vector3(1f, 2f, 3f), new Vector2(0.5f, 0.5f));
            var anx2 = new VertexPositionTexture(new Vector3(1f, 2f, 3f), new Vector2(0.5f, 0.5f));

            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "Equals2");
        }

        [Test]
        public void Equals3()
        {
            var xna = new XNAVertexPositionTexture(new XNAVector3(1f, 2f, 3f), new XNAVector2(0.5f, 0.5f));
            var xna2 = new XNAVertexPositionTexture(new XNAVector3(1f, 2f, 3f), XNAVector2.One);
            var anx = new VertexPositionTexture(new Vector3(1f, 2f, 3f), new Vector2(0.5f, 0.5f));
            var anx2 = new VertexPositionTexture(new Vector3(1f, 2f, 3f), Vector2.One);

            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "Equals3");
        }

        [Test]
        public void Equals4()
        {
            var xna = new XNAVertexPositionTexture(new XNAVector3(1f, 2f, 3f), new XNAVector2(0.5f, 0.5f));
            var xna2 = new XNAVertexPositionTexture(new XNAVector3(1f, 2f, 3f), XNAVector2.One);
            var anx = new VertexPositionTexture(new Vector3(1f, 2f, 3f), new Vector2(0.5f, 0.5f));
            var anx2 = new VertexPositionTexture(new Vector3(1f, 2f, 3f), Vector2.One);

            AssertHelper.ConvertEquals(xna.Equals(xna2), anx.Equals(anx2), "Equals4");
        }
    }
}
