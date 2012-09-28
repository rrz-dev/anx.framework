#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using System.Reflection;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    [ContentTypeWriter]
    internal class Texture2DWriter : BuiltinTypeWriter<Texture2DContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(Texture).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, Texture2DContent value)
        {
            System.Diagnostics.Debugger.Break();

            BitmapContent bitmapContent = value.Faces[0][0];
            SurfaceFormat format;
            if (!bitmapContent.TryGetFormat(out format))
            {
                throw new InvalidContentException("bad texture type");
            }

            // write header
            output.Write((int)format);
            output.Write(bitmapContent.Width);
            output.Write(bitmapContent.Height);
            output.Write(value.Faces[0].Count);

            foreach (MipmapChain current in value.Faces)
            {
                foreach (BitmapContent currentFace in current)
                {
                    byte[] pixelData = currentFace.GetPixelData();
                    output.Write(pixelData.Length);
                    output.Write(pixelData);
                }
            }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(Texture2D), targetPlatform);
        }
    }
}
