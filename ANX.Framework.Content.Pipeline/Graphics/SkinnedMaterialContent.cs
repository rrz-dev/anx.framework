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
        public const string AlphaKey = "Alpha";
        public const string DiffuseColorKey = "DiffuseColor";
        public const string EmissiveColorKey = "EmissiveColor";
        public const string SpecularColorKey = "SpecularColor";
        public const string SpecularPowerKey = "SpecularPower";
        public const string TextureKey = "Texture";
        public const string WeightsPerVertexKey = "WeightsPerVertex";

        public SkinnedMaterialContent()
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

        public Nullable<Vector3> SpecularColor
        {
            get { return this.GetValueTypeProperty<Vector3>(SpecularColorKey); }
            set { this.SetProperty(SpecularColorKey, value); }
        }

        public Nullable<float> SpecularPower
        {
            get { return this.GetValueTypeProperty<float>(SpecularPowerKey); }
            set { this.SetProperty(SpecularPowerKey, value); }
        }

        public ExternalReference<TextureContent> Texture
        {
            get { return this.GetTexture(TextureKey); }
            set { this.SetTexture(TextureKey, value); }
        }

        public Nullable<int> WeightsPerVertex
        {
            get { return this.GetValueTypeProperty<int>(WeightsPerVertexKey); }
            set { this.SetProperty(WeightsPerVertexKey, value); }
        }
    }
}
