using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class EffectTechnique_Metro : INativeEffectTechnique
	{
		#region Private
		private Effect parentEffect;
		private List<EffectPass_Metro> passes;
		#endregion

		#region Public
		public string Name
		{
			get;
			private set;
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
		#endregion

		#region Constructor
		public EffectTechnique_Metro(string setName, Effect setParentEffect,
			ExtendedShaderPass[] nativePasses)
		{
			Name = setName;
			parentEffect = setParentEffect;
			ParsePasses(nativePasses);
		}
		#endregion

		#region ParsePasses
		private void ParsePasses(ExtendedShaderPass[] nativePasses)
		{
			passes = new List<EffectPass_Metro>();

			foreach (ExtendedShaderPass pass in nativePasses)
			{
				passes.Add(new EffectPass_Metro(parentEffect, pass));
			}
		}
		#endregion
	}
}
