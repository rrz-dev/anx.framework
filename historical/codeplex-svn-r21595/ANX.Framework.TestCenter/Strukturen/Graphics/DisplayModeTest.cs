using System;
using System.Globalization;
using System.Reflection;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNADisplayMode = Microsoft.Xna.Framework.Graphics.DisplayMode;
using XNASurfaceFormat = Microsoft.Xna.Framework.Graphics.SurfaceFormat;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class DisplayModeTest
    {
        [Test]
        public void Values()
        {
            var xnaParameters = new object[] { 800, 600, XNASurfaceFormat.Color };
            var xna = (XNADisplayMode)Activator.CreateInstance(typeof(XNADisplayMode),
                BindingFlags.NonPublic | BindingFlags.Instance, null, xnaParameters, CultureInfo.InvariantCulture);
            var anxParameters = new object[] { 800, 600, SurfaceFormat.Color };
            var anx = (DisplayMode)Activator.CreateInstance(typeof(DisplayMode),
                BindingFlags.NonPublic | BindingFlags.Instance, null, anxParameters, CultureInfo.InvariantCulture);

            Assert.AreEqual(xna.AspectRatio, anx.AspectRatio);
            AssertHelper.ConvertEquals(xna.TitleSafeArea, anx.TitleSafeArea, "Values");
        }

        [Test]
        public void ToStringTest()
        {
            var xnaParameters = new object[] { 800, 600, XNASurfaceFormat.Color };
            var xna = (XNADisplayMode)Activator.CreateInstance(typeof(XNADisplayMode),
                BindingFlags.NonPublic | BindingFlags.Instance, null, xnaParameters, CultureInfo.InvariantCulture);
            var anxParameters = new object[] { 800, 600, SurfaceFormat.Color };
            var anx = (DisplayMode)Activator.CreateInstance(typeof(DisplayMode),
                BindingFlags.NonPublic | BindingFlags.Instance, null, anxParameters, CultureInfo.InvariantCulture);

            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString");
        }
    }
}
