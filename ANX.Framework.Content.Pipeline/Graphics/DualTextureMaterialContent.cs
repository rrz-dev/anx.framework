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
    public class DualTextureMaterialContent : MaterialContent 
    {
        public const string AlphaKey = "Alpha";
        public const string DiffuseColorKey = "DiffuseColor";
        public const string Texture2Key = "Texture2";
        public const string TextureKey = "Texture";
        public const string VertexColorEnabledKey = "VertexColorEnabled";

        public DualTextureMaterialContent()
        {
        }

        public Nullable<float> Alpha
        {
            get { return this.GetValueTypeProperty<float>(AlphaKey); }
            set { this.SetProperty(AlphaKey, value); }
        }

        public Nullable<Vector3> DiffuseColor
        {
            get { return this.GetValueTypeProperty<Vector3>(DiffuseColorKey); }
            set { this.SetProperty(DiffuseColorKey, value); }
        }

        public ExternalReference<TextureContent> Texture
        {
            get { return this.GetTexture(TextureKey); }
            set { this.SetTexture(TextureKey, value); }
        }

        public ExternalReference<TextureContent> Texture2 
        {
            get { return this.GetTexture(Texture2Key); }
            set { this.SetTexture(Texture2Key, value); }
        }

        public Nullable<bool> VertexColorEnabled
        {
            get { return this.GetValueTypeProperty<bool>(VertexColorEnabledKey); }
            set { this.SetProperty(VertexColorEnabledKey, value); }
        }
    }
}
