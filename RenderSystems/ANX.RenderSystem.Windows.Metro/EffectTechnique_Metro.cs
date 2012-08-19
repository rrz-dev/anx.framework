using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.Metro.Shader;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class EffectTechnique_Metro : INativeEffectTechnique
	{
		#region Private
		private EffectPass_Metro[] passes;
		#endregion

		#region Public
		public string Name
		{
			get;
			private set;
		}

        public int PassCount
        {
            get
            {
                return passes.Length;
            }
        }

		public IEnumerable<EffectPass> Passes
		{
			get
			{
				foreach (EffectPass_Metro pass in passes)
				{
					yield return pass.Pass;
				}
			}
		}

        public EffectPass_Metro this[int index]
        {
            get
            {
                return passes[index];
            }
        }
		#endregion

		#region Constructor
		public EffectTechnique_Metro(string setName, Effect parentEffect,
			ExtendedShaderPass[] nativePasses)
		{
            Name = setName;

            passes = new EffectPass_Metro[nativePasses.Length];
            for (int index = 0; index < nativePasses.Length; index++)
            {
                passes[index] = new EffectPass_Metro(parentEffect, nativePasses[index]);
            }
		}
		#endregion
	}
}
