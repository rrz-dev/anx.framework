#region Using Statements
using System;
using ANX.Framework.NonXNA;


#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license


namespace ANX.Framework.Graphics
{
    public class BasicEffect : Effect, IEffectMatrices, IEffectLights, IEffectFog
    {
        private bool vertexColorEnabled = false;
        private bool perPixelLighting = false;
        private bool lightingEnabled = false;
        private bool fogEnabled = false;
        private bool textureEnabled = false;

        private EffectParameter world;
        private EffectParameter view;
        private EffectParameter projection;
        private EffectParameter texture;
        private EffectParameter fogColor;
        private EffectParameter fogVector;

        private DirectionalLight[] directionalLight;
        private Vector3 ambientLightColor;

		public BasicEffect(GraphicsDevice graphics)
			: base(graphics, GetByteCode(), GetSourceLanguage())
        {
            world = base.Parameters["World"];
            view = base.Parameters["View"];
            projection = base.Parameters["Projection"];

            directionalLight = new DirectionalLight[]
            {
                new DirectionalLight(base.Parameters["LightDirection[0]"], base.Parameters["LightDiffuseColor[0]"], base.Parameters["LightSpecularColor[0]"], null),
                new DirectionalLight(base.Parameters["LightDirection[1]"], base.Parameters["LightDiffuseColor[1]"], base.Parameters["LightSpecularColor[1]"], null),
                new DirectionalLight(base.Parameters["LightDirection[2]"], base.Parameters["LightDiffuseColor[2]"], base.Parameters["LightSpecularColor[2]"], null),
            };

            fogColor = base.Parameters["FogColor"];
            fogVector = base.Parameters["FogVector"];

            texture = base.Parameters["Texture"];
        }

        protected BasicEffect(BasicEffect cloneSource)
            : base(cloneSource)
        {
            throw new NotImplementedException();
		}

		#region GetByteCode
		private static byte[] GetByteCode()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			return creator.GetShaderByteCode(PreDefinedShader.BasicEffect);
		}
		#endregion

		#region GetSourceLanguage
		private static EffectSourceLanguage GetSourceLanguage()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			return creator.GetStockShaderSourceLanguage;
		}
		#endregion

        public bool PreferPerPixelLighting
        {
            get
            {
                return perPixelLighting;
            }
            set
            {
                perPixelLighting = value;
            }
        }

        public Matrix Projection
        {
            get
            {
                return this.projection.GetValueMatrix();
            }
            set
            {
                this.projection.SetValue(value);
            }
        }

        public Matrix View
        {
            get
            {
                return this.view.GetValueMatrix();
            }
            set
            {
                this.view.SetValue(value);
            }
        }

        public Matrix World
        {
            get
            {
                return this.world.GetValueMatrix();
            }
            set
            {
                this.world.SetValue(value);
            }
        }

        public void EnableDefaultLighting()
        {
            LightingEnabled = true;
            ambientLightColor = new Vector3(0.05333332f, 0.09882354f, 0.1819608f);

            directionalLight[0].Direction = new Vector3(-0.5265408f, -0.5735765f, -0.6275069f);
            directionalLight[0].DiffuseColor = new Vector3(1.0000000f, 0.9607844f, 0.8078432f);
            directionalLight[0].SpecularColor = new Vector3(1.0000000f, 0.9607844f, 0.8078432f);
            directionalLight[0].Enabled = true;

            directionalLight[1].Direction = new Vector3(0.7198464f, 0.3420201f, 0.6040227f);
            directionalLight[1].DiffuseColor = new Vector3(0.9647059f, 0.7607844f, 0.4078432f);
            directionalLight[1].SpecularColor = new Vector3(0.0000000f, 0.0000000f, 0.0000000f);
            directionalLight[1].Enabled = true;

            directionalLight[2].Direction = new Vector3(0.4545195f, -0.7660444f, 0.4545195f);
            directionalLight[2].DiffuseColor = new Vector3(0.3231373f, 0.3607844f, 0.3937255f);
            directionalLight[2].SpecularColor = new Vector3(0.3231373f, 0.3607844f, 0.3937255f);
            directionalLight[2].Enabled = true;
        }

        public Vector3 AmbientLightColor
        {
            get
            {
                return ambientLightColor;
            }
            set
            {
                ambientLightColor = value;
            }
        }

        public DirectionalLight DirectionalLight0
        {
            get 
            { 
                return directionalLight[0]; 
            }
        }

        public DirectionalLight DirectionalLight1
        {
            get 
            { 
                return directionalLight[1]; 
            }
        }

        public DirectionalLight DirectionalLight2
        {
            get 
            { 
                return directionalLight[2]; 
            }
        }

        public bool LightingEnabled
        {
            get
            {
                return lightingEnabled;
            }
            set
            {
                lightingEnabled = value;
            }
        }

        public Vector3 FogColor
        {
            get
            {
                return this.fogColor.GetValueVector3();
            }
            set
            {
                this.fogVector.SetValue(value);
            }
        }

        public bool FogEnabled
        {
            get
            {
                return fogEnabled;
            }
            set
            {
                FogEnabled = value;
            }
        }

        public float FogEnd
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float FogStart
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Texture2D Texture
        {
            get
            {
                return this.texture.GetValueTexture2D();
            }
            set
            {
                this.texture.SetValue(value);
            }
        }

        public bool TextureEnabled
        {
            get
            {
                return textureEnabled;
            }
            set
            {
                TextureEnabled = value;
            }
        }

        public Vector3 DiffuseColor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Vector3 EmissiveColor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Vector3 SpecularColor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float SpecularPower
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float Alpha
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool VertexColorEnabled
        {
            get
            {
                return this.vertexColorEnabled;
            }
            set
            {
                this.vertexColorEnabled = value;
                SetTechnique();
            }
        }

        public override Effect Clone()
        {
            return new BasicEffect(this);
        }

        private void SetTechnique()
        {
            //TODO: implement completly

            if (vertexColorEnabled)
            {
                this.CurrentTechnique = Techniques["VertexColor"];
                return;
            }

            //this.CurrentTechnique = Techniques["NormalTex"];    //TODO: this is for ModelSample to be work
            throw new InvalidOperationException("Currently ANX's BasicEffect only supports VertexColor technique");
        }
    }
}
