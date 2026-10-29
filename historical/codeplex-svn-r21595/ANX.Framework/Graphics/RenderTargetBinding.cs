#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public struct RenderTargetBinding
    {
        #region Private
        private Texture renderTarget;
        private CubeMapFace cubeMapFace;
		#endregion

		#region Public
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
		#endregion

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
    }
}
