using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNADisplayMode = Microsoft.Xna.Framework.Graphics.DisplayMode;
using XNADisplayModeCollection = Microsoft.Xna.Framework.Graphics.DisplayModeCollection;
using XNASurfaceFormat = Microsoft.Xna.Framework.Graphics.SurfaceFormat;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class DisplayModeCollectionTest
    {
        [Test]
        public void Accessor()
        {
            var xnaModeParameters = new object[] { 800, 600, XNASurfaceFormat.Color };
            var xnaMode = (XNADisplayMode)Activator.CreateInstance(typeof(XNADisplayMode),
                BindingFlags.NonPublic | BindingFlags.Instance, null, xnaModeParameters, CultureInfo.InvariantCulture);
            var anxModeParameters = new object[] { 800, 600, SurfaceFormat.Color };
            var anxMode = (DisplayMode)Activator.CreateInstance(typeof(DisplayMode),
                BindingFlags.NonPublic | BindingFlags.Instance, null, anxModeParameters, CultureInfo.InvariantCulture);

            var xnaModeList = new List<XNADisplayMode> {xnaMode};
            var anxModeList = new List<DisplayMode> {anxMode};
            var xnaCollectionParameters = new object[] { xnaModeList };
            var xna = (XNADisplayModeCollection)Activator.CreateInstance(typeof(XNADisplayModeCollection),
                BindingFlags.NonPublic | BindingFlags.Instance, null, xnaCollectionParameters, CultureInfo.InvariantCulture);
            var anxCollectionParameters = new object[] { anxModeList };
            var anx = (DisplayModeCollection)Activator.CreateInstance(typeof(DisplayModeCollection),
                BindingFlags.NonPublic | BindingFlags.Instance, null, anxCollectionParameters, CultureInfo.InvariantCulture);

            Assert.AreEqual((xna[XNASurfaceFormat.Color] as List<XNADisplayMode>).Count,
                (anx[SurfaceFormat.Color] as List<DisplayMode>).Count);

            Assert.AreEqual((xna[XNASurfaceFormat.Bgra5551] as List<XNADisplayMode>).Count,
                (anx[SurfaceFormat.Bgra5551] as List<DisplayMode>).Count);
        }
    }
}
