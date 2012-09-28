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
    public class PixelBitmapContent<T> : BitmapContent where T : struct, IEquatable<T>
    {
        private T[,] pixels;

        protected PixelBitmapContent()
        {

        }

        public PixelBitmapContent(int width, int height)
            : base(width, height)
        {
            pixels = new T[width, height];
        }

        public T GetPixel(int x, int y)
        {
            return pixels[x, y];
        }

        public override byte[] GetPixelData()
        {
            throw new NotImplementedException();
        }

        public T[] GetRow(int y)
        {
            throw new NotImplementedException();
        }

        public void SetPixel(int x, int y, T value)
        {
            pixels[x, y] = value;
        }

        public override void SetPixelData(byte[] sourceData)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
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
