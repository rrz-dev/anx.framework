#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx11 = SharpDX.Direct3D11;

namespace ANX.RenderSystem.Windows.DX11
{
	public class EffectTechnique_DX11 : INativeEffectTechnique
	{
		private readonly Effect parentEffect;

		public EffectTechnique_DX11(Effect parentEffect, Dx11.EffectTechnique nativeTechnique)
		{
            if (parentEffect == null)
            {
                throw new ArgumentNullException("parentEffect");
            }

			this.parentEffect = parentEffect;
			NativeTechnique = nativeTechnique;
		}

        public Dx11.EffectTechnique NativeTechnique { get; protected set; }

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
					var passDx11 = new EffectPass_DX11(NativeTechnique.GetPassByIndex(i));
					// TODO: wire up native pass and managed pass?
					yield return new EffectPass(this.parentEffect);
				}
			}
		}

        public EffectAnnotationCollection Annotations
        {
            get { throw new NotImplementedException(); }
        }
    }
}
