#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public class AlphaTestEffect : Effect, IEffectMatrices, IEffectFog, IGraphicsResource
    {
        public AlphaTestEffect(GraphicsDevice device)
            : base(device, AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().GetShaderByteCode(NonXNA.PreDefinedShader.AlphaTestEffect))
        {
            throw new NotImplementedException();
        }

        protected AlphaTestEffect(AlphaTestEffect cloneSource)
            : base(cloneSource)
        {
            throw new NotImplementedException();
        }

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
