#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.Content;
using System.IO;
using System.Drawing;
using ANX.Framework.Content.Pipeline.Helpers;
using System.Runtime.InteropServices;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Importer
{
    [ContentImporter(new string[] { ".bmp", ".jpg", ".jpeg", ".png", ".wdp", ".gif", ".tif", ".tga", ".dds" }, DefaultProcessor = "TextureProcessor", Category = "Texture Files", DisplayName = "TextureImporter - ANX Framework")]
    public class TextureImporter : ContentImporter<TextureContent>
    {
        public override TextureContent Import(string filename, ContentImporterContext context)
        {
            string fileExtension = Path.GetExtension(filename).ToLowerInvariant();

            BitmapContent bitmapContent;
            if (fileExtension == ".tga" || fileExtension == ".dds")
                bitmapContent = LoadByRendersystem(filename);
            else
                bitmapContent = LoadByGdi(filename);

            TextureContent textureContent = new Texture2DContent();
            textureContent.Faces[0] = new MipmapChain(bitmapContent);

            return textureContent;
        }

        private BitmapContent LoadByGdi(string fileName)
        {
            Image image = Bitmap.FromFile(fileName);
            Bitmap bitmap = new Bitmap(image);
            var bitmapContent = new PixelBitmapContent<Color>(image.Width, image.Height);
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

            return bitmapContent;
        }

        private BitmapContent LoadByRendersystem(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var texture = ANX.Framework.Graphics.Texture2D.FromStream(GraphicsHelper.ReferenceDevice, stream);

                Type vectorType;
                if (VectorConverter.TryGetVectorType(texture.Format, out vectorType))
                {
                    var content = (BitmapContent)Activator.CreateInstance(typeof(PixelBitmapContent<>).MakeGenericType(vectorType), texture.Width, texture.Height);
                    
                    byte[] data = new byte[texture.Width * texture.Height * Marshal.SizeOf(vectorType)];
                    texture.GetData<byte>(data);
                    content.SetPixelData(data);

                    return content;
                }
                else
                {
                    throw new InvalidContentException("Unable to convert file format.");
                }
            }
        }
    }
}
