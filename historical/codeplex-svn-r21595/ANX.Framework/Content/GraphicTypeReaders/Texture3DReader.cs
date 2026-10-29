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
    internal class Texture3DReader : ContentTypeReader<Texture3D>
    {
        protected internal override Texture3D Read(ContentReader input, Texture3D existingInstance)
        {
            GraphicsDevice graphics = input.ResolveGraphicsDevice();
            SurfaceFormat surfaceFormat = (SurfaceFormat)input.ReadInt32();
            int width = input.ReadInt32();
            int height = input.ReadInt32();
            int depth = input.ReadInt32();
            int mipCount = input.ReadInt32();

            var texture3D = new Texture3D(graphics, width, height, depth, mipCount > 1, surfaceFormat);
            for (int index = 0; index < mipCount; index++)
            {
                int size = input.ReadInt32();
                byte[] data = input.ReadBytes(size);
                texture3D.SetData(index, 0, 0, width, height, 0, depth, data, 0, size);
                width = Math.Max(width >> 1, 1);
                height = Math.Max(height >> 1, 1);
                depth = Math.Max(depth >> 1, 1);
            }

            return texture3D;
        }
    }
}
