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
        public const string AlphaKey = "";
        public const string DiffuseColorKey = "";
        public const string Texture2Key = "";
        public const string TextureKey = "";
        public const string VertexColorEnabledKey = "";

        public DualTextureMaterialContent()
        {
        }

        public Nullable<float> Alpha
        {
            get;
            set;
        }

        public Nullable<Vector3> DiffuseColor
        {
            get;
            set;
        }

        public ExternalReference<TextureContent> Texture
        {
            get;
            set;
        }

        public ExternalReference<TextureContent> Texture2 
        { 
            get; 
            set; 
        }

        public Nullable<bool> VertexColorEnabled
        {
            get;
            set;
        }
    }
}
