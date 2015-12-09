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
        public const string AlphaKey = "Alpha";
        public const string DiffuseColorKey = "DiffuseColor";
        public const string EmissiveColorKey = "EmissiveColor";
        public const string EnvironmentMapAmountKey = "EnivornmentMapAmount";
        public const string EnvironmentMapKey = "EnvironmentMap";
        public const string EnvironmentMapSpecularKey = "EnvironmentMapSpecular";
        public const string FresnelFactorKey = "FresnelFactor";
        public const string TextureKey = "Texture";

        public EnvironmentMapMaterialContent()
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

        public Nullable<Vector3> EmissiveColor
        {
            get { return this.GetValueTypeProperty<Vector3>(EmissiveColorKey); }
            set { this.SetProperty(EmissiveColorKey, value); }
        }

        public ExternalReference<TextureContent> EnvironmentMap
        {
            get { return this.GetTexture(EnvironmentMapKey); }
            set { this.SetTexture(EnvironmentMapKey, value); }
        }

        public Nullable<float> EnvironmentMapAmount
        {
            get { return this.GetValueTypeProperty<float>(EnvironmentMapAmountKey); }
            set { this.SetProperty(EnvironmentMapAmountKey, value); }
        }

        public Nullable<Vector3> EnvironmentMapSpecular
        {
            get { return this.GetValueTypeProperty<Vector3>(EnvironmentMapSpecularKey); }
            set { this.SetProperty(EnvironmentMapSpecularKey, value); }
        }

        public Nullable<float> FresnelFactor
        {
            get { return this.GetValueTypeProperty<float>(FresnelFactorKey); }
            set { this.SetProperty(FresnelFactorKey, value); }
        }

        public ExternalReference<TextureContent> Texture
        {
            get { return this.GetTexture(TextureKey); }
            set { this.SetTexture(TextureKey, value); }
        }
    }
}
