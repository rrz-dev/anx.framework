#region Using Statements
using System;
using System.IO;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class TextureReader : ContentTypeReader<Texture>
    {
        protected internal override Texture Read(ContentReader input, Texture existingInstance)
        {
            return existingInstance;
        }
    }
}
