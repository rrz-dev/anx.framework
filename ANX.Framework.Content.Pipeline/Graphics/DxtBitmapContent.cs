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
    public abstract class DxtBitmapContent : BitmapContent
    {
        private int _blockSize;
        private byte[] _pixelData;

        protected DxtBitmapContent(int blockSize)
        {
            //TODO: set _pixelData
            _blockSize = blockSize;
        }

        protected DxtBitmapContent(int blockSize, int width, int height)
            : base(width, height)
        {
            _blockSize = blockSize;

            //http://www.gamedev.net/topic/615440-calculating-pitch-of-a-dxt-compressed-texture/#post_id_4886508
            //Data in a DXT texture is compressed in blocks of 4x4 pixels, the block size is the resolution for this blocks.
            //The block size is also the reason why width and height must be a multiple of four.
            width = (width + 3) / 4;
			height = (height + 3) / 4;
			_pixelData = new byte[width * height * blockSize];
        }

        public override byte[] GetPixelData()
        {
            return (byte[])_pixelData.Clone();
        }

        public override void SetPixelData(byte[] sourceData)
        {
            if (sourceData == null)
            {
                throw new ArgumentNullException("sourceData");
            }

            if (sourceData.Length != _pixelData.Length)
            {
                throw new ArgumentException(string.Format("The length of sourceData (Length: {0}) must be equal to the size of the contained data within the {1} (Length: {2}).", sourceData.Length, this.GetType().FullName, _pixelData.Length));
            }

            _pixelData = (byte[])sourceData.Clone();
        }

        protected override bool TryCopyFrom(BitmapContent sourceBitmap, Rectangle sourceRegion, Rectangle destinationRegion)
        {
            ValidateCopyArguments(sourceBitmap, sourceRegion, this, destinationRegion);

            throw new NotImplementedException();
        }

        protected override bool TryCopyTo(BitmapContent destinationBitmap, Rectangle sourceRegion, Rectangle destinationRegion)
        {
            ValidateCopyArguments(this, sourceRegion, destinationBitmap, destinationRegion);

            throw new NotImplementedException();
        }
    }
}
