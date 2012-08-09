#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public struct RenderTargetBinding
    {
        #region Private Members
        private Texture renderTarget;
        private CubeMapFace cubeMapFace;
        #endregion // Private Members

        public RenderTargetBinding(RenderTarget2D renderTarget)
        {
            this.renderTarget = renderTarget;
            this.cubeMapFace = Graphics.CubeMapFace.PositiveX;
        }

        public RenderTargetBinding(RenderTargetCube renderTargetCube, CubeMapFace face)
        {
            this.renderTarget = renderTargetCube;
            this.cubeMapFace = face;
        }
        
        public static implicit operator RenderTargetBinding(RenderTarget2D renderTarget)
        {
            return new RenderTargetBinding(renderTarget);
        }

        public Texture RenderTarget
        {
            get
            {
                return this.renderTarget;
            }
        }

        public CubeMapFace CubeMapFace
        {
            get
            {
                return this.cubeMapFace;
            }
        }
    }
}
