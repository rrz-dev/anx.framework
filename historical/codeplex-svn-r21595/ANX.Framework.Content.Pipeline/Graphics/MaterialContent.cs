#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class MaterialContent : ContentItem
    {
        public MaterialContent()
        {
        }

        public TextureReferenceDictionary Textures
        {
            get;
            private set;
        }

        protected T GetReferenceTypeProperty<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        protected ExternalReference<TextureContent> GetTexture(String key)
        {
            throw new NotImplementedException();
        }

        protected Nullable<T> GetValueTypeProperty<T>(string key) where T : struct
        {
            throw new NotImplementedException();
        }

        protected void SetProperty<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        protected void SetTexture(String key, ExternalReference<TextureContent> value)
        {
            throw new NotImplementedException();
        }
    }
}
