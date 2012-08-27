#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.Content;
using System.IO;
using System.Drawing;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Importer
{
    [ContentImporter(new string[] { ".bmp", ".jpg", ".jpeg", ".png", ".wdp", ".gif", ".tif" }, DefaultProcessor = "SpriteTextureProcessor")]
    public class TextureImporter : ContentImporter<TextureContent>
    {
        public override TextureContent Import(string filename, ContentImporterContext context)
        {
            string fileExtension = Path.GetExtension(filename).ToLowerInvariant();

            Image image = Bitmap.FromFile(filename);
            Bitmap bitmap = new Bitmap(image);
            PixelBitmapContent<Color> bitmapContent = new PixelBitmapContent<Color>(image.Width, image.Height);
            System.Drawing.Color sourceColor;
            ANX.Framework.Color destColor = new Color();

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    sourceColor = bitmap.GetPixel(x, y);

                    destColor.R = sourceColor.R;
                    destColor.G = sourceColor.G;
                    destColor.B = sourceColor.B;
                    destColor.A = sourceColor.A;

                    bitmapContent.SetPixel(x, y, destColor);
                }
            }

            TextureContent textureContent = new Texture2DContent();
            textureContent.Faces.Add(new MipmapChain(bitmapContent));

            return textureContent;
        }
    }
}
