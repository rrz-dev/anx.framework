#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx10 = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
	public class EffectTechnique_DX10 : INativeEffectTechnique
	{
		private Effect parentEffect;

		public EffectTechnique_DX10(Effect parentEffect, Dx10.EffectTechnique nativeTechnique)
		{
            if (parentEffect == null)
            {
                throw new ArgumentNullException("parentEffect");
            }

			this.parentEffect = parentEffect;
			NativeTechnique = nativeTechnique;
		}

        public Dx10.EffectTechnique NativeTechnique { get; protected set; }
        
        public string Name
		{
			get
			{
				return NativeTechnique.Description.Name;
			}
		}

		public IEnumerable<EffectPass> Passes
		{
			get
			{
				for (int i = 0; i < NativeTechnique.Description.PassCount; i++)
				{
					var passDx10 = new EffectPass_DX10(NativeTechnique.GetPassByIndex(i));
					// TODO: wire up native pass and managed pass?
					yield return new EffectPass(this.parentEffect);
				}
			}
		}
	}
}