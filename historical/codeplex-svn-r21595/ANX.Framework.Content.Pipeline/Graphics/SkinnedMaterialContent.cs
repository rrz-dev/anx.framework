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
    public class SkinnedMaterialContent : MaterialContent 
    {
        public const string AlphaKey = "";
        public const string DiffuseColorKey = "";
        public const string EmissiveColorKey = "";
        public const string SpecularColorKey = "";
        public const string SpecularPowerKey = "";
        public const string TextureKey = "";
        public const string WeightsPerVertexKey = "";

        public SkinnedMaterialContent()
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

        public Nullable<Vector3> EmissiveColor
        {
            get;
            set;
        }

        public Nullable<Vector3> SpecularColor
        {
            get;
            set;
        }

        public Nullable<float> SpecularPower
        {
            get;
            set;
        }

        public ExternalReference<TextureContent> Texture
        {
            get;
            set;
        }

        public Nullable<int> WeightsPerVertex
        {
            get;
            set;
        }
    }
}
