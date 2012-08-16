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
    public class Dxt5BitmapContent : DxtBitmapContent
    {
        public Dxt5BitmapContent(int width, int height)
            : base(-1, width, height)   //TODO: blockSize
        {
            throw new NotImplementedException();
        }

        public override bool TryGetFormat(out SurfaceFormat format)
        {
            throw new NotImplementedException();
        }
    }
}
