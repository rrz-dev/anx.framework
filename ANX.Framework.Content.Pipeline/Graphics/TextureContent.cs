#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public abstract class TextureContent : ContentItem
    {
        protected TextureContent(MipmapChainCollection faces)
        {
            Faces = faces;
        }

        public MipmapChainCollection Faces
        {
            get;
            private set;
        }

        public void ConvertBitmapType(Type newBitmapType)
        {
            throw new NotImplementedException();
        }

        public virtual void GenerateMipmaps(bool overwriteExistingMipmaps)
        {
            throw new NotImplementedException();
        }

        public abstract void Validate(Nullable<GraphicsProfile> targetProfile);
    }
}
