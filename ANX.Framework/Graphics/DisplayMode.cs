#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public class DisplayMode
    {
        public float AspectRatio
        {
            get;
            set;
        }

        public SurfaceFormat Format
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public Rectangle TitleSafeArea
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
