#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using ANX.Framework.Content.Pipeline.Helpers;
using System.Diagnostics;

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
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width must be bigger than 0.");

            if (height <= 0)
                throw new ArgumentOutOfRangeException("height must be bigger than 0.");

            Width = width;
            Height = height;
        }

        /// <summary>
        /// Returns the height of the bitmap.
        /// </summary>
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the width of the bitmap.
        /// </summary>
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// Copies the content from <paramref name="sourceBitmap"/> onto <paramref name="destinationBitmap"/> by using <see cref="TryCopyTo"/> and <see cref="TryCopyFrom"/>.
        /// </summary>
        /// <param name="sourceBitmap">The source bitmap of the copy operation.</param>
        /// <param name="destinationBitmap">The destination bitmap of the copy operation.</param>
        public static void Copy(BitmapContent sourceBitmap, BitmapContent destinationBitmap)
        {
            if (sourceBitmap == null)
                throw new ArgumentNullException("sourceBitmap");

            if (destinationBitmap == null)
                throw new ArgumentNullException("destinationBitmap");

            Copy(sourceBitmap, new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), destinationBitmap, new Rectangle(0, 0, destinationBitmap.Width, destinationBitmap.Height));
        }

        /// <summary>
        /// Copies a region of <paramref name="sourceBitmap"/> onto a region of <paramref name="destinationBitmap"/>.
        /// </summary>
        /// <param name="sourceBitmap"The source bitmap of the copy operation.></param>
        /// <param name="sourceRegion"></param>
        /// <param name="destinationBitmap">The destination bitmap of the copy operation.</param>
        /// <param name="destinationRegion"></param>
        public static void Copy(BitmapContent sourceBitmap, Rectangle sourceRegion, BitmapContent destinationBitmap, Rectangle destinationRegion)
        {
            ValidateCopyArguments(sourceBitmap, sourceRegion, destinationBitmap, destinationRegion);

            if (sourceBitmap.TryCopyTo(destinationBitmap, sourceRegion, destinationRegion))
            {
                return;
            }

            if (destinationBitmap.TryCopyFrom(sourceBitmap, sourceRegion, destinationRegion))
            {
                return;
            }
        }

        private static void ValidateConvertArguments(BitmapContent source, Type newType)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (newType == null)
                throw new ArgumentNullException("newBitmapType");

            if (!newType.IsSubclassOf(typeof(BitmapContent)))
            {
                throw new ArgumentException(string.Format("The wanted bitmap type {0} does not inherit from {1}.", newType.FullName, typeof(BitmapContent).FullName));
            }

            if (newType.IsAbstract)
            {
                throw new ArgumentException(string.Format("Can't construct a new instance of {0}. It must not be abstract.", newType.FullName));
            }

            if (newType.ContainsGenericParameters)
            {
                throw new ArgumentException(string.Format("Can't construct a new instance of {0}. It must not contain unset generic parameters.", newType.FullName));
            }

            if (newType.GetConstructor(new Type[] { typeof(int), typeof(int) }) == null)
            {
                throw new ArgumentException(string.Format("Can't construct a new instance of {0}. It must contain a public constructor that accepts width and height.", newType.FullName));
            }
        }

        public static BitmapContent Convert(BitmapContent source, Type newType)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return Convert(source, newType, source.Width, source.Height);
        }

        public static T Convert<T>(BitmapContent source) where T : BitmapContent
        {
            return (T)Convert(source, typeof(T));
        }

        public static BitmapContent Convert(BitmapContent source, Type newType, int newWidth, int newHeight)
        {
            ValidateConvertArguments(source, newType);

            if (source.GetType() != newType)
            {
                BitmapContent newBitmap = (BitmapContent)Activator.CreateInstance(newType, newWidth, newHeight);
                BitmapContent.Copy(source, newBitmap);

                return newBitmap;
            }
            return source;
        }

        public static T Convert<T>(BitmapContent source, int newWidth, int newHeight) where T : BitmapContent
        {
            return (T)Convert(source, typeof(T), newWidth, newHeight);
        }

        public abstract byte[] GetPixelData();

        public abstract void SetPixelData(byte[] sourceData);

        public abstract bool TryGetFormat(out SurfaceFormat format);

        public override string ToString()
        {
            return string.Format("{0}, {1}x{2}", GetType().Name, this.Width, this.Height);
        }

        protected abstract bool TryCopyFrom(BitmapContent sourceBitmap, Rectangle sourceRegion, Rectangle destinationRegion);

        protected abstract bool TryCopyTo(BitmapContent destinationBitmap, Rectangle sourceRegion, Rectangle destinationRegion);

        protected static void ValidateCopyArguments(BitmapContent sourceBitmap, Rectangle sourceRegion, BitmapContent destinationBitmap, Rectangle destinationRegion)
        {
            if (sourceBitmap == null)
                throw new ArgumentNullException("sourceBitmap");

            if (destinationBitmap == null)
                throw new ArgumentNullException("destinationBitmap");

            // Make sure regions are within the bounds of the bitmaps
            if (sourceRegion.X < 0 || sourceRegion.Y < 0 || sourceRegion.Width <= 0 || sourceRegion.Height <= 0 ||
                sourceRegion.Right > sourceBitmap.Width || sourceRegion.Bottom > sourceBitmap.Height)
                throw new ArgumentOutOfRangeException("sourceRegion");

            if (destinationRegion.X < 0 || destinationRegion.Y < 0 || destinationRegion.Width <= 0 || destinationRegion.Height <= 0 || 
                destinationRegion.Right > destinationBitmap.Width || destinationRegion.Bottom > destinationBitmap.Height)
                throw new ArgumentOutOfRangeException("destinationRegion");
        }

        private void ValidateTextureSize(SurfaceFormat format, ref int width, ref int height)
        {
            switch (format)
            {
                case SurfaceFormat.Dxt1:
                case SurfaceFormat.Dxt3:
                case SurfaceFormat.Dxt5:
                    width = MathHelper.Multiple(width, 4);
                    height = MathHelper.Multiple(height, 4);
                    break;
            }
        }

        protected static bool Draw(BitmapContent sourceBitmap, Rectangle sourceRegion, BitmapContent destinationBitmap, Rectangle destinationRegion, TextureFilter filter)
        {
            var pixelSource = BitmapContent.Convert<PixelBitmapContent<Color>>(sourceBitmap);
            var pixelDestination = BitmapContent.Convert<PixelBitmapContent<Color>>(destinationBitmap);
            if (pixelSource == null || pixelDestination == null)
                return false;

            using (Texture2D sourceTexture = new Texture2D(GraphicsHelper.ReferenceDevice, pixelSource.Width, pixelSource.Height, false, SurfaceFormat.Color))
            using (RenderTarget2D destinationTexture = new RenderTarget2D(GraphicsHelper.ReferenceDevice, pixelDestination.Width, pixelDestination.Height, false, SurfaceFormat.Color, DepthFormat.None))
            {
                byte[] sourceData = pixelSource.GetPixelData();
                byte[] destinationData = pixelDestination.GetPixelData();

                sourceTexture.SetData(sourceData);
                destinationTexture.SetData(destinationData);

                GraphicsHelper.DrawQuad(GraphicsHelper.ReferenceDevice, sourceTexture, sourceRegion, destinationTexture, destinationRegion, filter);

                sourceTexture.GetData(sourceData);
                destinationTexture.GetData(destinationData);

                pixelSource.SetPixelData(sourceData);
                pixelDestination.SetPixelData(destinationData);

                BitmapContent.Copy(pixelSource, sourceBitmap);
                BitmapContent.Copy(pixelDestination, destinationBitmap);
            }

            return true;
        }
    }
}
