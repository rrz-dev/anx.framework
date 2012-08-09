#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class DirectionalLight
    {
        private Vector3 diffuseColor;
        private Vector3 direction;
        private bool enabled;
        private Vector3 specularColor;
        private EffectParameter directionParameter;
        private EffectParameter diffuseColorParameter;
        private EffectParameter specularColorParameter;

        public DirectionalLight(EffectParameter directionParameter, EffectParameter diffuseColorParameter, EffectParameter specularColorParameter, DirectionalLight cloneSource)
        {
            this.directionParameter = directionParameter;
            this.diffuseColorParameter = diffuseColorParameter;
            this.specularColorParameter = specularColorParameter;

            if (cloneSource != null)
            {
                this.diffuseColor = cloneSource.diffuseColor;
                this.direction = cloneSource.direction;
                this.enabled = cloneSource.enabled;
                this.specularColor = cloneSource.specularColor;
            }
            else
            {
                this.diffuseColor = Vector3.One;
                this.direction = Vector3.Down;
                this.specularColor = Vector3.Zero;
            }
        }

        public Vector3 DiffuseColor
        {
            get
            {
                return this.diffuseColor;
            }
            set
            {
                this.diffuseColor = value;
                if (this.enabled && this.diffuseColorParameter != null)
                {
                    this.diffuseColorParameter.SetValue(value);
                }
            }
        }

        public Vector3 Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
                if (this.enabled && this.directionParameter != null)
                {
                    this.directionParameter.SetValue(value);
                }
            }
        }

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
            }
        }

        public Vector3 SpecularColor
        {
            get
            {
                return this.diffuseColor;
            }
            set
            {
                this.specularColor = value;
                if (this.enabled && this.specularColorParameter != null)
                {
                    this.specularColorParameter.SetValue(value);
                }
            }
        }
    }
}
