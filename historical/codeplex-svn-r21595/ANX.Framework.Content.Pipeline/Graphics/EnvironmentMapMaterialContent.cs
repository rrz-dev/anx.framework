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
    public class EnvironmentMapMaterialContent : MaterialContent 
    {
        public const string AlphaKey = "";
        public const string DiffuseColorKey = "";
        public const string EmissiveColorKey = "";
        public const string EnvironmentMapAmountKey = "";
        public const string EnvironmentMapKey = "";
        public const string EnvironmentMapSpecularKey = "";
        public const string FresnelFactorKey = "";
        public const string TextureKey = "";

        public EnvironmentMapMaterialContent()
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

        public ExternalReference<TextureContent> EnvironmentMap
        {
            get;
            set;
        }

        public Nullable<float> EnvironmentMapAmount
        {
            get;
            set;
        }

        public Nullable<Vector3> EnvironmentMapSpecular
        {
            get;
            set;
        }

        public Nullable<float> FresnelFactor
        {
            get;
            set;
        }

        public ExternalReference<TextureContent> Texture
        {
            get;
            set;
        }
    }
}
