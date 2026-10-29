#region Using Statements

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [PercentageComplete(95)]
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [TestState(TestStateAttribute.TestState.InProgress)] 
    [ContentProcessor(DisplayName = "FontDescription Processor - ANX Framework")]
    public class FontDescriptionProcessor : ContentProcessor<FontDescription, SpriteFontContent>
    {
        public override SpriteFontContent Process(FontDescription input, ContentProcessorContext context)
        {
            context.Logger.LogMessage("Processing of FontDescription started.");
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            var list = new List<char>(input.Characters);
            if (input.DefaultCharacter.HasValue && !input.Characters.Contains(input.DefaultCharacter.Value))
            {
                list.Add(input.DefaultCharacter.Value);
            }
            list.Sort();
            var spriteFontContent = new SpriteFontContent();

            // Build up a list of all the glyphs to be output.
            var bitmaps = new List<Bitmap>();
            var xPositions = new List<int>();
            var yPositions = new List<int>();

            var globalBitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            System.Drawing.Graphics globalGraphics = System.Drawing.Graphics.FromImage(globalBitmap);
            var font = new Font(input.FontName, input.Size, XnaFontStyleToGdiFontStyle(input.Style));
            try
            {
                const int padding = 4;

                int width = padding;
                int height = padding;
                int lineWidth = padding;
                int lineHeight = padding;
                int count = 0;

                // Rasterize each character in turn,
                // and add it to the output list.
                foreach (char ch in input.Characters)
                {
                    Bitmap bitmap = RasterizeCharacter(globalGraphics, ch, true, font);

                    bitmaps.Add(bitmap);

                    xPositions.Add(lineWidth);
                    yPositions.Add(height);

                    lineWidth += bitmap.Width + padding;
                    lineHeight = Math.Max(lineHeight, bitmap.Height + padding);

                    // Output 16 glyphs per line, then wrap to the next line.
                    if ((++count == 16) || (ch == input.Characters.Count - 1))
                    {
                        width = Math.Max(width, lineWidth);
                        height += lineHeight;
                        lineWidth = padding;
                        lineHeight = padding;
                        count = 0;
                    }
                }

                using (var bitmap = new Bitmap(width, height,
                                               PixelFormat.Format32bppArgb))
                {
                    // Arrange all the glyphs onto a single larger bitmap.
                    using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
                    {
                        graphics.Clear(System.Drawing.Color.Magenta);
                        graphics.CompositingMode = CompositingMode.SourceCopy;

                        for (int i = 0; i < bitmaps.Count; i++)
                        {
                            //cropping defines the dimensions of a single character
                            spriteFontContent.Cropping.Add(new Rectangle(0, 0, bitmaps[i].Width - 1, //we need to subtract one unit of height and width to suppress nasty rendering artifacts. 
                                                                             bitmaps[i].Height - 1));
                            //Glyphs defines the section and dimensions where the character is located on the texture
                            spriteFontContent.Glyphs.Add(new Rectangle(xPositions[i] + 1, yPositions[i] + 1, bitmaps[i].Width -1, bitmaps[i].Height-1));
                            graphics.DrawImage(bitmaps[i], xPositions[i], yPositions[i]);
                        }

                        graphics.Flush();
                    }
                    spriteFontContent.Texture = ConvertBitmap(bitmap);
                    spriteFontContent.CharacterMap = input.Characters.ToList();
                    spriteFontContent.DefaultCharacter = input.DefaultCharacter;
                    spriteFontContent.Kerning = new List<Vector3>();
                    for (var i = 0; i < input.Characters.Count; i++)
                    {
                        var value = Vector3.Zero;
                        if (input.UseKerning)
                        {
                            throw new NotImplementedException("Kerning is not implemented!");
                            //TODO: Implement!
                        }
                        else
                        {
                            value.Y = spriteFontContent.Cropping[i].Width;
                            value.X = 0f;
                            value.Z = 0f;
                        }
                        spriteFontContent.Kerning.Add(value);
                    }
                    spriteFontContent.LineSpacing = (int) Math.Ceiling(font.GetHeight());
                    spriteFontContent.Spacing = input.Spacing;
                }
            }
            finally
            {
                // Clean up temporary objects.
                foreach (Bitmap bitmap in bitmaps)
                    bitmap.Dispose();
            }
            context.Logger.LogMessage("Processing of FontDescription finished.");
            return spriteFontContent;
        }

        #region RasterizeCharacter

        /// <summary>
        /// Helper for rendering out a single font character
        /// into a System.Drawing bitmap.
        /// </summary>
        private static Bitmap RasterizeCharacter(System.Drawing.Graphics globalGraphics, char ch, bool antiAliasing,
                                                 Font font)
        {
            string text = ch.ToString(CultureInfo.InvariantCulture);

            SizeF size = globalGraphics.MeasureString(text, font);

            var width = (int) Math.Ceiling(size.Width);
            var height = (int) Math.Ceiling(size.Height);

            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = antiAliasing
                                                 ? TextRenderingHint.AntiAliasGridFit
                                                 : TextRenderingHint.SingleBitPerPixelGridFit;

                graphics.Clear(System.Drawing.Color.Transparent);

                using (Brush brush = new SolidBrush(System.Drawing.Color.White))
                using (var format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;

                    graphics.DrawString(text, font, brush, 0, 0, format);
                }

                graphics.Flush();
            }

            return CropCharacter(bitmap);
        }

        #endregion

        #region CropCharacter

        /// <summary>
        /// Helper for cropping ununsed space from the sides of a bitmap.
        /// </summary>
        private static Bitmap CropCharacter(Bitmap bitmap)
        {
            int cropLeft = 0;
            int cropRight = bitmap.Width - 1;

            // Remove unused space from the left.
            while ((cropLeft < cropRight) && (BitmapEmpty(bitmap, cropLeft)))
                cropLeft++;

            // Remove unused space from the right.
            while ((cropRight > cropLeft) && (BitmapEmpty(bitmap, cropRight)))
                cropRight--;

            // Don't crop if that would reduce the glyph down to nothing at all!
            if (cropLeft == cropRight)
                return bitmap;

            // Add some padding back in.
            cropLeft = Math.Max(cropLeft - 1, 0);
            cropRight = Math.Min(cropRight + 1, bitmap.Width - 1);

            int width = cropRight - cropLeft + 1;

            // Crop the glyph.
            var croppedBitmap = new Bitmap(width, bitmap.Height, bitmap.PixelFormat);

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(croppedBitmap))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;

                graphics.DrawImage(bitmap, 0, 0,
                                   new System.Drawing.Rectangle(cropLeft, 0, width, bitmap.Height),
                                   GraphicsUnit.Pixel);

                graphics.Flush();
            }

            bitmap.Dispose();

            return croppedBitmap;
        }

        #endregion

        #region CreateOutputGlyphs

        private static List<Rectangle> CreateOutputGlyphs()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ConvertBitmap

        private Texture2DContent ConvertBitmap(Bitmap bitmap)
        {
            var bitmapContent = new PixelBitmapContent<Color>(bitmap.Width, bitmap.Height);
            var destColor = new Color();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    System.Drawing.Color sourceColor = bitmap.GetPixel(x, y);

                    destColor.R = sourceColor.R;
                    destColor.G = sourceColor.G;
                    destColor.B = sourceColor.B;
                    destColor.A = sourceColor.A;

                    bitmapContent.SetPixel(x, y, destColor);
                }
            }

            var textureContent = new Texture2DContent();
            textureContent.Faces[0] = new MipmapChain(bitmapContent);

            return textureContent;
        }

        #endregion

        #region FontDescriptionStyleConversion

        private static FontStyle XnaFontStyleToGdiFontStyle(FontDescriptionStyle fontStyle)
        {
            var fontStyle2 = FontStyle.Regular;
            if ((fontStyle & FontDescriptionStyle.Bold) == FontDescriptionStyle.Bold)
            {
                fontStyle2 |= FontStyle.Bold;
            }
            if ((fontStyle & FontDescriptionStyle.Italic) == FontDescriptionStyle.Italic)
            {
                fontStyle2 |= FontStyle.Italic;
            }
            return fontStyle2;
        }

        #endregion

        #region BitmapEmpty

        /// <summary>
        /// Helper for testing whether a column of a bitmap is entirely empty.
        /// </summary>
        private static bool BitmapEmpty(Bitmap bitmap, int x)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                if (bitmap.GetPixel(x, y).A != 0)
                    return false;
            }

            return true;
        }

        #endregion
    }
}