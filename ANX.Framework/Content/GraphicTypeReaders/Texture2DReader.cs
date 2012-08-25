#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class Texture2DReader : ContentTypeReader<Texture2D>
    {
        protected internal override Texture2D Read(ContentReader input, Texture2D existingInstance)
        {
            IServiceProvider service = input.ContentManager.ServiceProvider;

            var rfc = service.GetService(typeof(IRenderSystemCreator)) as IRenderSystemCreator;
            if (rfc == null)
            {
                throw new ContentLoadException("Service not found IRenderFrameworkCreator");
            }

            GraphicsDevice graphics = input.ResolveGraphicsDevice();
            int surfaceFormat = input.ReadInt32();
            int width = input.ReadInt32();
            int height = input.ReadInt32();
            int mipCount = input.ReadInt32();
            SurfaceFormat sFormat = (SurfaceFormat)surfaceFormat;

            List<byte> colorData = new List<byte>();

            for (int i = 0; i < mipCount; i++)
            {
                int size = input.ReadInt32();
                colorData.AddRange(input.ReadBytes(size));
            }

            Texture2D texture = new Texture2D(graphics, width, height, mipCount > 0, sFormat);
            texture.SetData<byte>(colorData.ToArray());

            return texture;
        }
    }
}
