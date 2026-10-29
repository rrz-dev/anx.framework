using System;
using ANX.Framework.Graphics;
using NUnit.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using XNAVertexElement = Microsoft.Xna.Framework.Graphics.VertexElement;
using ANXVertexElement = ANX.Framework.Graphics.VertexElement;

using XNAVertexElementFormat = Microsoft.Xna.Framework.Graphics.VertexElementFormat;
using XNAVertexElementUsage = Microsoft.Xna.Framework.Graphics.VertexElementUsage;

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class VertexElementTest
    {
        [Test]
        public void ToString0()
        {
            var xna = new XNAVertexElement();
            var anx = new ANXVertexElement();

            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString0");
        }

        [Test]
        public void ToString1()
        {
            var xna = new XNAVertexElement(15, XNAVertexElementFormat.Vector2, XNAVertexElementUsage.TextureCoordinate, 0);
            var anx = new ANXVertexElement(15, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0);

            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString1");
        }

        [Test]
        public void GetHashCodeTest()
        {
            var xna = new XNAVertexElement(15, XNAVertexElementFormat.Vector2, XNAVertexElementUsage.TextureCoordinate, 0);
            var anx = new ANXVertexElement(15, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0);

            AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        }

        [Test]
        public void Equals0()
        {
            var xna = new XNAVertexElement(15, XNAVertexElementFormat.Vector2, XNAVertexElementUsage.TextureCoordinate, 0);
            var anx = new ANXVertexElement(15, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0);

            AssertHelper.ConvertEquals(xna.Equals(null), anx.Equals(null), "Equals0");
        }

        [Test]
        public void Equals1()
        {
            var xna = new XNAVertexElement(15, XNAVertexElementFormat.Vector2, XNAVertexElementUsage.TextureCoordinate, 0);
            var anx = new ANXVertexElement(15, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0);

            AssertHelper.ConvertEquals(xna.Equals(xna), anx.Equals(anx), "Equals1");
        }

        [Test]
        public void Equals2()
        {
            var xna = new XNAVertexElement(15, XNAVertexElementFormat.Vector2, XNAVertexElementUsage.TextureCoordinate, 0);
            var xna2 = xna;
            var anx = new ANXVertexElement(15, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0);
            var anx2 = anx;

            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "Equals2");
        }

        [Test]
        public void Equals3()
        {
            var xna = new XNAVertexElement(15, XNAVertexElementFormat.Vector2, XNAVertexElementUsage.TextureCoordinate, 0);
            var xna2 = new XNAVertexElement(3, XNAVertexElementFormat.Color, XNAVertexElementUsage.Color, 0);
            var anx = new ANXVertexElement(15, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0);
            var anx2 = new ANXVertexElement(3, VertexElementFormat.Color, VertexElementUsage.Color, 0);

            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "Equals3");
        }

        [Test]
        public void Equals4()
        {
            var xna = new XNAVertexElement(15, XNAVertexElementFormat.Vector2, XNAVertexElementUsage.TextureCoordinate, 0);
            var xna2 = new XNAVertexElement(3, XNAVertexElementFormat.Color, XNAVertexElementUsage.Color, 0);
            var anx = new ANXVertexElement(15, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0);
            var anx2 = new ANXVertexElement(3, VertexElementFormat.Color, VertexElementUsage.Color, 0);

            AssertHelper.ConvertEquals(xna.Equals(xna2), anx.Equals(anx2), "Equals4");
        }
    }
}
