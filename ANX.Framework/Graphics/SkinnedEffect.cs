#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license



namespace ANX.Framework.Graphics
{
    public class SkinnedEffect : Effect, IEffectMatrices, IEffectLights, IEffectFog
    {
		public SkinnedEffect(GraphicsDevice graphics)
			: base(graphics, GetByteCode(), GetSourceLanguage())
        {
            throw new NotImplementedException();
        }

        protected SkinnedEffect(SkinnedEffect cloneSource)
            : base(cloneSource)
        {
            throw new NotImplementedException();
		}

		#region GetByteCode
		private static byte[] GetByteCode()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			return creator.GetShaderByteCode(PreDefinedShader.SkinnedEffect);
		}
		#endregion

		#region GetSourceLanguage
		private static EffectSourceLanguage GetSourceLanguage()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			return creator.GetStockShaderSourceLanguage;
		}
		#endregion

        public override Effect Clone()
        {
            return new SkinnedEffect(this);
        }

        public const int MaxBones = 72;

        public bool PreferPerPixelLighting
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

        public Matrix Projection
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

        public Matrix View
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

        public Matrix World
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

        public void EnableDefaultLighting()
        {
            throw new NotImplementedException();
        }

        public Vector3 AmbientLightColor
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

        public DirectionalLight DirectionalLight0
        {
            get { throw new NotImplementedException(); }
        }

        public DirectionalLight DirectionalLight1
        {
            get { throw new NotImplementedException(); }
        }

        public DirectionalLight DirectionalLight2
        {
            get { throw new NotImplementedException(); }
        }

        public bool LightingEnabled
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

        public Vector3 FogColor
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

        public bool FogEnabled
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
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int WeightsPerVertex
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

        public Matrix[] GetBoneTransforms(int count)
        {
            throw new NotImplementedException();
        }

        public void SetBoneTransforms(Matrix[] boneTransforms)
        {
            throw new NotImplementedException();
        }
    }
}
