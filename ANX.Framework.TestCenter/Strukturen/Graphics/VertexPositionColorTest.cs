using System;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNAVertexPositionColor = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using XNAColor = Microsoft.Xna.Framework.Color;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class VertexPositionColorTest
    {
        [Test]
        public void VertexDeclaration()
        {
            var xna = XNAVertexPositionColor.VertexDeclaration;
            var anx = VertexPositionColor.VertexDeclaration;

            Assert.AreEqual(xna.VertexStride, anx.VertexStride);
            Assert.AreEqual(xna.Name, anx.Name);

            var xnaElements = xna.GetVertexElements();
            var anxElements = anx.GetVertexElements();
            Assert.AreEqual(xnaElements.Length, anxElements.Length);
            for (int index = 0; index < xnaElements.Length; index++)
                AssertHelper.Compare(xnaElements[index], anxElements[index]);
        }

        [Test]
        public void GetHashCode()
        {
            var xna = new XNAVertexPositionColor(new XNAVector3(1f, 2f, 3f), XNAColor.CornflowerBlue);
            var anx = new VertexPositionColor(new Vector3(1f, 2f, 3f), Color.CornflowerBlue);

            AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        }

        [Test]
        public void ToString()
        {
            var xna = new XNAVertexPositionColor(new XNAVector3(1f, 2f, 3f), XNAColor.CornflowerBlue);
            var anx = new VertexPositionColor(new Vector3(1f, 2f, 3f), Color.CornflowerBlue);

            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString");
        }

        [Test]
        public void Equals0()
        {
            var xna = new XNAVertexPositionColor(new XNAVector3(1f, 2f, 3f), XNAColor.CornflowerBlue);
            var anx = new VertexPositionColor(new Vector3(1f, 2f, 3f), Color.CornflowerBlue);

            AssertHelper.ConvertEquals(xna.Equals(null), anx.Equals(null), "Equals0");
        }

        [Test]
        public void Equals1()
        {
            var xna = new XNAVertexPositionColor(new XNAVector3(1f, 2f, 3f), XNAColor.CornflowerBlue);
            var anx = new VertexPositionColor(new Vector3(1f, 2f, 3f), Color.CornflowerBlue);

            AssertHelper.ConvertEquals(xna.Equals(xna), anx.Equals(anx), "Equals1");
        }

        [Test]
        public void Equals2()
        {
            var xna = new XNAVertexPositionColor(new XNAVector3(1f, 2f, 3f), XNAColor.CornflowerBlue);
            var anx = new VertexPositionColor(new Vector3(1f, 2f, 3f), Color.CornflowerBlue);

            AssertHelper.ConvertEquals(xna != xna, anx != anx, "Equals2");
        }

        [Test]
        public void Equals3()
        {
            var xna = new XNAVertexPositionColor(new XNAVector3(1f, 2f, 3f), XNAColor.CornflowerBlue);
            var xna2 = new XNAVertexPositionColor(new XNAVector3(1f, 2f, 3f), XNAColor.Red);
            var anx = new VertexPositionColor(new Vector3(1f, 2f, 3f), Color.CornflowerBlue);
            var anx2 = new VertexPositionColor(new Vector3(1f, 2f, 3f), Color.Red);

            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "Equals3");
        }

        [Test]
        public void Equals4()
        {
            var xna = new XNAVertexPositionColor(new XNAVector3(1f, 2f, 3f), XNAColor.CornflowerBlue);
            var xna2 = new XNAVertexPositionColor(new XNAVector3(1f, 2f, 3f), XNAColor.Red);
            var anx = new VertexPositionColor(new Vector3(1f, 2f, 3f), Color.CornflowerBlue);
            var anx2 = new VertexPositionColor(new Vector3(1f, 2f, 3f), Color.Red);

            AssertHelper.ConvertEquals(xna.Equals(xna2), anx.Equals(anx2), "Equals4");
        }
    }
}
