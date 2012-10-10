#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    internal class TextureCubeReader : ContentTypeReader<TextureCube>
    {
        protected internal override TextureCube Read(ContentReader input, TextureCube existingInstance)
        {
            IServiceProvider service = input.ContentManager.ServiceProvider;

            var rfc = service.GetService(typeof(IRenderSystemCreator)) as IRenderSystemCreator;
            if (rfc == null)
            {
                throw new ContentLoadException("Service not found IRenderFrameworkCreator");
            }

            GraphicsDevice graphics = input.ResolveGraphicsDevice();
            SurfaceFormat surfaceFormat = (SurfaceFormat)input.ReadInt32();
            int size = input.ReadInt32();
            int mipCount = input.ReadInt32();

            List<byte> colorData = new List<byte>();

            // for each cube face: +x, -x, +y, -y, +z, -z
            for (int face = 0; face < 6; face++)
            {
                for (int i = 0; i < mipCount; i++)
                {
                    int dataSize = input.ReadInt32();
                    colorData.AddRange(input.ReadBytes(dataSize));
                }
            }

            throw new NotImplementedException();
            //return rfc.CreateTexture(graphics, surfaceFormat, size, mipCount, colorData.ToArray());
        }
    }
}
