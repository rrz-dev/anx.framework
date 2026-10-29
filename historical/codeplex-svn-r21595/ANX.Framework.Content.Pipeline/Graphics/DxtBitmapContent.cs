#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class DxtBitmapContent : BitmapContent
    {
        protected DxtBitmapContent(int blockSize)
        {
            throw new NotImplementedException();
        }

        protected DxtBitmapContent(int blockSize, int width, int height)
            : base(width, height)
        {
            throw new NotImplementedException();
        }

        public override byte[] GetPixelData()
        {
            throw new NotImplementedException();
        }

        public override void SetPixelData(byte[] sourceData)
        {
            throw new NotImplementedException();
        }

        protected override bool TryCopyFrom(BitmapContent sourceBitmap, Rectangle sourceRegion, Rectangle destinationRegion)
        {
            throw new NotImplementedException();
        }

        protected override bool TryCopyTo(BitmapContent destinationBitmap, Rectangle sourceRegion, Rectangle destinationRegion)
        {
            throw new NotImplementedException();
        }

        public override bool TryGetFormat(out Framework.Graphics.SurfaceFormat format)
        {
            throw new NotImplementedException();
        }
    }
}
