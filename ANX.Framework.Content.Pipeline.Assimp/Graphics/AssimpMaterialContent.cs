using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class AssimpMaterialContent : BasicMaterialContent
    {
        public const string AmbientColorKey = "AmbientColor";
        public const string BumpScalingKey = "BumpScaling";
        public const string ReflectivityKey = "Reflectivity";
        public const string ReflectiveColorKey = "ReflectiveColor";
        public const string IsTwoSidedKey = "TwoSided";
        public const string IsWireframeKey = "Wireframe";
        public const string IsAdditiveKey = "Additive";
        public const string ShadingModeKey = "ShadingMode";

        public Vector3? AmbientColor
        {
            get { return this.GetValueTypeProperty<Vector3>(AmbientColorKey); }
            set { this.SetProperty(AmbientColorKey, value); }
        }

        public float? BumpScaling
        {
            get { return this.GetValueTypeProperty<float>(BumpScalingKey); }
            set { this.SetProperty(BumpScalingKey, value); }
        }

        public float? Reflectivity
        {
            get { return this.GetValueTypeProperty<float>(ReflectivityKey); }
            set { this.SetProperty(ReflectivityKey, value); }
        }

        public Vector3? ReflectiveColor
        {
            get { return this.GetValueTypeProperty<Vector3>(ReflectiveColorKey); }
            set { this.SetProperty(ReflectiveColorKey, value); }
        }

        public bool? IsTwoSided
        {
            get { return this.GetValueTypeProperty<bool>(IsTwoSidedKey); }
            set { this.SetProperty(IsTwoSidedKey, value); }
        }

        public bool? IsWireframe
        {
            get { return this.GetValueTypeProperty<bool>(IsWireframeKey); }
            set { this.SetProperty(IsWireframeKey, value); }
        }

        public bool? IsAdditive
        {
            get { return this.GetValueTypeProperty<bool>(IsAdditiveKey); }
            set { this.SetProperty(IsAdditiveKey, value); }
        }

        public string ShadingMode
        {
            get { return this.GetReferenceTypeProperty<string>(ShadingModeKey); }
            set { this.SetProperty(ShadingModeKey, value); }
        }
    }
}
