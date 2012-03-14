#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License


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
            : base(graphics, AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().GetShaderByteCode(NonXNA.PreDefinedShader.BasicEffect))
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
