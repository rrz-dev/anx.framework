#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using System.ComponentModel;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor]
    public class TextureProcessor : ContentProcessor<TextureContent, TextureContent>
    {
        [DefaultValue(typeof(Color), "255; 0; 255; 255")]
        public virtual Color ColorKeyColor { get; set; }

        [DefaultValue(true)]
        public virtual bool ColorKeyEnabled { get; set; }

        [DefaultValue(false)]
        public virtual bool GenerateMipmaps { get; set; }

        [DefaultValue(true)]
        public virtual bool PremultiplyAlpha { get; set; }

        [DefaultValue(false)]
        public virtual bool ResizeToPowerOfTwo { get; set; }

        [DefaultValue(TextureProcessorOutputFormat.Color)]
        public virtual TextureProcessorOutputFormat TextureFormat { get; set; }

        public TextureProcessor()
        {
            ColorKeyColor = Color.FromNonPremultiplied(255, 0, 255, 255);
            ColorKeyEnabled = true;
            GenerateMipmaps = false;
            PremultiplyAlpha = true;
            ResizeToPowerOfTwo = false;
            TextureFormat = TextureProcessorOutputFormat.Color;
        }

        public override TextureContent Process(TextureContent input, ContentProcessorContext context)
        {
            if (ColorKeyEnabled)
            {
                foreach (MipmapChain face in input.Faces)
                {
                    foreach (BitmapContent bitmap in face)
                    {
                        PixelBitmapContent<Color> pixelBitmapContent = bitmap as PixelBitmapContent<Color>;
                        if (pixelBitmapContent != null)
                        {
                            pixelBitmapContent.ReplaceColor(ColorKeyColor, Color.Transparent);
                        }
                    }
                }
            }

            if (PremultiplyAlpha)
            {
                foreach (MipmapChain face in input.Faces)
                {
                    foreach (BitmapContent bitmap in face)
                    {
                        PixelBitmapContent<Color> pixelBitmapContent = bitmap as PixelBitmapContent<Color>;
                        if (pixelBitmapContent != null)
                        {
                            for (int x = 0, width = bitmap.Width; x < width; x++)
                                for (int y = 0, height = bitmap.Height; y < height; y++)
                                {
                                    Color color = pixelBitmapContent.GetPixel(x, y);
                                    if (color.A < 255)
                                    {
                                        //premultiplies the colors.
                                        pixelBitmapContent.SetPixel(x, y, Color.FromNonPremultiplied((int)color.R, (int)color.G, (int)color.B, (int)color.A));
                                    }
                                }
                        }
                    }
                }
            }

            if (ResizeToPowerOfTwo)
            {
                foreach (MipmapChain face in input.Faces)
                {
                    for (int i = 0; i < face.Count; i++)
                    {
                        BitmapContent bitmapContent = face[i];
                        int width = RoundUpToPowerOfTwo(bitmapContent.Width);
                        int height = RoundUpToPowerOfTwo(bitmapContent.Height);
                        if (width != bitmapContent.Width || height != bitmapContent.Height)
                        {
                            face[i] = BitmapContent.Convert(bitmapContent, bitmapContent.GetType(), width, height);
                        }
                    }
                }
            }

            if (GenerateMipmaps)
            {
                input.GenerateMipmaps(false);
            }

            switch (TextureFormat)
            {
                case TextureProcessorOutputFormat.Color:
                    input.ConvertBitmapType(typeof(PixelBitmapContent<Color>));
                    break;
                case TextureProcessorOutputFormat.DxtCompressed:
                    if (this.HasFractionalAlpha(input))
                    {
                        input.ConvertBitmapType(typeof(Dxt5BitmapContent));
                    }
                    else
                    {
                        input.ConvertBitmapType(typeof(Dxt1BitmapContent));
                    }
                    break;
            }

            //TODO: test

            return input;
        }

        /// <summary>
        /// Tests if the given <see cref="TextureContent"/> has alpha values that are neither 0 nor 255.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool HasFractionalAlpha(TextureContent content)
        {
            foreach (var face in content.Faces)
            {
                foreach (var bitmap in face)
                {
                    PixelBitmapContent<Color> pixelBitmapContent = BitmapContent.Convert<PixelBitmapContent<Color>>(bitmap);

                    for (int x = 0; x < pixelBitmapContent.Width; x++)
                        for (int y = 0; y < pixelBitmapContent.Height; y++)
                        {
                            var alpha = pixelBitmapContent.GetPixel(x, y).A;
                            if (alpha > 0 && alpha < 255)
                                return true;
                        }
                }
            }

            return false;
        }

        private static int RoundUpToPowerOfTwo(int value)
        {
            return (int) Math.Pow(2, Math.Ceiling(Math.Log(value, 2)));
        }
    }
}
