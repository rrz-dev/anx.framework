#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    internal class TextureCubeReader : ContentTypeReader<TextureCube>
    {
        protected internal override TextureCube Read(ContentReader input, TextureCube existingInstance)
        {
            GraphicsDevice graphics = input.ResolveGraphicsDevice();
            SurfaceFormat surfaceFormat = (SurfaceFormat)input.ReadInt32();
            int size = input.ReadInt32();
            int mipCount = input.ReadInt32();

            var textureCube = new TextureCube(graphics, size, mipCount > 1, surfaceFormat);
            for (int face = 0; face < 6; face++)
            {
                for (int index = 0; index < mipCount; index++)
                {
                    int dataSize = input.ReadInt32();
                    byte[] data = input.ReadBytes(dataSize);
                    textureCube.SetData((CubeMapFace)face, index, null, data, 0, dataSize);
                }
            }

            return textureCube;
        }
    }
}
