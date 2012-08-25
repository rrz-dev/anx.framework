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
    internal class Texture3DReader : ContentTypeReader<Texture3D>
    {
        protected internal override Texture3D Read(ContentReader input, Texture3D existingInstance)
        {
            IServiceProvider service = input.ContentManager.ServiceProvider;

            var rfc = service.GetService(typeof(IRenderSystemCreator)) as IRenderSystemCreator;
            if (rfc == null)
            {
                throw new ContentLoadException("Service not found IRenderFrameworkCreator");
            }

            GraphicsDevice graphics = input.ResolveGraphicsDevice();
            SurfaceFormat surfaceFormat = (SurfaceFormat)input.ReadInt32();
            int width = input.ReadInt32();
            int height = input.ReadInt32();
            int depth = input.ReadInt32();
            int mipCount = input.ReadInt32();

            List<byte> colorData = new List<byte>();

            for (int i = 0; i < mipCount; i++)
            {
                int size = input.ReadInt32();
                colorData.AddRange(input.ReadBytes(size));
            }

            throw new NotImplementedException();
            //return rfc.CreateTexture(graphics, surfaceFormat, width, height, mipCount, depth, colorData.ToArray();
        }
    }
}
