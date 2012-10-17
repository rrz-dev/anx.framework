using System;
using ANX.Framework.Input.Touch;
using NUnit.Framework;

using XNATouchLocation = Microsoft.Xna.Framework.Input.Touch.TouchLocation;
using ANXTouchLocation = ANX.Framework.Input.Touch.TouchLocation;

using XNATouchLocationState = Microsoft.Xna.Framework.Input.Touch.TouchLocationState;
using XNAVector2 = Microsoft.Xna.Framework.Vector2;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Input.Touch
{
    class TouchLocationTest
    {
        [Test]
        public void ToString()
        {
            var xna = new XNATouchLocation(15, XNATouchLocationState.Moved, new XNAVector2(14f, 3.5f));
            var anx = new ANXTouchLocation(15, TouchLocationState.Moved, new Vector2(14f, 3.5f));

            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString");
        }

        [Test]
        public void GetHashCode()
        {
            var xna = new XNATouchLocation(15, XNATouchLocationState.Moved, new XNAVector2(14f, 3.5f));
            var anx = new ANXTouchLocation(15, TouchLocationState.Moved, new Vector2(14f, 3.5f));

            AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        }

        [Test]
        public void Equals0()
        {
            var xna = new XNATouchLocation(15, XNATouchLocationState.Moved, new XNAVector2(14f, 3.5f));
            var anx = new ANXTouchLocation(15, TouchLocationState.Moved, new Vector2(14f, 3.5f));

            AssertHelper.ConvertEquals(xna.Equals(null), anx.Equals(null), "Equals0");
        }

        [Test]
        public void Equals1()
        {
            var xna = new XNATouchLocation(15, XNATouchLocationState.Moved, new XNAVector2(14f, 3.5f));
            var anx = new ANXTouchLocation(15, TouchLocationState.Moved, new Vector2(14f, 3.5f));

            AssertHelper.ConvertEquals(xna.Equals(xna), anx.Equals(anx), "Equals1");
        }

        [Test]
        public void Equals2()
        {
            var xna = new XNATouchLocation(15, XNATouchLocationState.Moved, new XNAVector2(14f, 3.5f));
            var xna2 = new XNATouchLocation(13, XNATouchLocationState.Pressed, new XNAVector2(14f, 3.5f));
            var anx = new ANXTouchLocation(15, TouchLocationState.Moved, new Vector2(14f, 3.5f));
            var anx2 = new ANXTouchLocation(13, TouchLocationState.Pressed, new Vector2(14f, 3.5f));

            AssertHelper.ConvertEquals(xna.Equals(xna2), anx.Equals(anx2), "Equals2");
        }

        [Test]
        public void Equals3()
        {
            var xna = new XNATouchLocation(15, XNATouchLocationState.Moved, new XNAVector2(14f, 3.5f));
            var anx = new ANXTouchLocation(15, TouchLocationState.Moved, new Vector2(14f, 3.5f));

            AssertHelper.ConvertEquals(xna == xna, anx == anx, "Equals3");
        }

        [Test]
        public void Equals4()
        {
            var xna = new XNATouchLocation(15, XNATouchLocationState.Moved, new XNAVector2(14f, 3.5f));
            var xna2 = new XNATouchLocation(13, XNATouchLocationState.Pressed, new XNAVector2(14f, 3.5f));
            var anx = new ANXTouchLocation(15, TouchLocationState.Moved, new Vector2(14f, 3.5f));
            var anx2 = new ANXTouchLocation(13, TouchLocationState.Pressed, new Vector2(14f, 3.5f));

            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "Equals4");
        }

        [Test]
        public void TryGetPreviousLocation0()
        {
            var xna = new XNATouchLocation(15, XNATouchLocationState.Moved, new XNAVector2(14f, 3.5f),
                XNATouchLocationState.Pressed, XNAVector2.Zero);
            var anx = new ANXTouchLocation(15, TouchLocationState.Moved, new Vector2(14f, 3.5f),
                TouchLocationState.Pressed, Vector2.Zero);

            XNATouchLocation xnaPrevLocation;
            ANXTouchLocation anxPrevLocation;
            Assert.AreEqual(xna.TryGetPreviousLocation(out xnaPrevLocation), anx.TryGetPreviousLocation(out anxPrevLocation));
            AssertHelper.ConvertEquals((int)xnaPrevLocation.State, (int)anxPrevLocation.State, "TryGetPreviousLocation0");
        }

        [Test]
        public void TryGetPreviousLocation1()
        {
            var xna = new XNATouchLocation(15, XNATouchLocationState.Moved, new XNAVector2(14f, 3.5f));
            var anx = new ANXTouchLocation(15, TouchLocationState.Moved, new Vector2(14f, 3.5f));

            XNATouchLocation xnaPrevLocation;
            ANXTouchLocation anxPrevLocation;
            Assert.AreEqual(xna.TryGetPreviousLocation(out xnaPrevLocation), anx.TryGetPreviousLocation(out anxPrevLocation));
            AssertHelper.ConvertEquals((int)xnaPrevLocation.State, (int)anxPrevLocation.State, "TryGetPreviousLocation1");
        }
    }
}
