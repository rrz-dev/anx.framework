#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class Dxt3BitmapContent : DxtBitmapContent
    {
        public Dxt3BitmapContent(int width, int height)
            : base(16, width, height)
        {
            
        }

        public override bool TryGetFormat(out SurfaceFormat format)
        {
            format = SurfaceFormat.Dxt3;
            return true;
        }
    }
}
