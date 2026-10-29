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
    public abstract class BitmapContent : ContentItem
    {
        public BitmapContent()
        {

        }

        protected BitmapContent(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Height
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public static void Copy(BitmapContent sourceBitmap, BitmapContent destinationBitmap)
        {
            throw new NotImplementedException();
        }

        public static void Copy(BitmapContent sourceBitmap, Rectangle sourceRegion, BitmapContent destinationBitmap, Rectangle destinationRegion)
        {
            throw new NotImplementedException();
        }

        public abstract byte[] GetPixelData();

        public abstract void SetPixelData(byte[] sourceData);

        public abstract bool TryGetFormat(out SurfaceFormat format);

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        protected abstract bool TryCopyFrom(BitmapContent sourceBitmap, Rectangle sourceRegion, Rectangle destinationRegion);

        protected abstract bool TryCopyTo(BitmapContent destinationBitmap, Rectangle sourceRegion, Rectangle destinationRegion);

        protected static void ValidateCopyArguments(BitmapContent sourceBitmap, Rectangle sourceRegion, BitmapContent destinationBitmap, Rectangle destinationRegion)
        {
            throw new NotImplementedException();
        }
    }
}
