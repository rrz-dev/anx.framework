using System;
using System.Globalization;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
	[Developer("AstrorEnales")]
    public class DisplayMode
    {
		public float AspectRatio { get; set; }
		public SurfaceFormat Format { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public Rectangle TitleSafeArea { get; set; }

        public override string ToString()
		{
			return String.Format(CultureInfo.CurrentCulture, "{{Width:{0} Height:{1} Format:{2} AspectRatio:{3}}}",
				Width, Height, Format, AspectRatio);
        }
    }
}
