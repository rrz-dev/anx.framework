using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public class AlphaTestEffect : Effect, IEffectMatrices, IEffectFog, IGraphicsResource
    {
		public AlphaTestEffect(GraphicsDevice device)
			: base(device, GetByteCode(), GetSourceLanguage())
        {
            throw new NotImplementedException();
        }

        protected AlphaTestEffect(AlphaTestEffect cloneSource)
            : base(cloneSource)
        {
            throw new NotImplementedException();
		}

		#region GetByteCode
		private static byte[] GetByteCode()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			return creator.GetShaderByteCode(PreDefinedShader.AlphaTestEffect);
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
            throw new NotImplementedException();
        }

        protected void OnApply()
        {
            throw new NotImplementedException();
        }

        public float Alpha
        {
            get;
            set;
        }

        public CompareFunction AlphaFunction
        {
            get;
            set;
        }

        public Vector3 DiffuseColor
        {
            get;
            set;
        }

        public Vector3 FogColor
        {
            get;
            set;
        }

        public bool FogEnabled
        {
            get;
            set;
        }

        public float FogEnd
        {
            get;
            set;
        }

        public float FogStart
        {
            get;
            set;
        }

        public Matrix Projection
        {
            get;
            set;
        }

        public int ReferenceAlpha
        {
            get;
            set;
        }

        public Texture2D Texture
        {
            get;
            set;
        }

        public bool VertexColorEnabled
        {
            get;
            set;
        }

        public Matrix View
        {
            get;
            set;
        }

        public Matrix World
        {
            get;
            set;
        }

    }
}
