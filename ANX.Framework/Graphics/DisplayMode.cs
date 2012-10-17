using System;
using System.Globalization;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public class DisplayMode
    {
        public SurfaceFormat Format { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public float AspectRatio { get; private set; }
        public Rectangle TitleSafeArea { get; private set; }

        internal DisplayMode(int width, int height, SurfaceFormat format)
        {
            this.Width = width;
            this.Height = height;
            this.Format = format;
            AspectRatio = (height == 0 || width == 0) ? 0f : (float)width / height;
            TitleSafeArea = new Rectangle(0, 0, width, height);
        }

        public override string ToString()
		{
			return String.Format(CultureInfo.CurrentCulture, "{{Width:{0} Height:{1} Format:{2} AspectRatio:{3}}}",
				Width, Height, Format, AspectRatio);
        }
    }
}
