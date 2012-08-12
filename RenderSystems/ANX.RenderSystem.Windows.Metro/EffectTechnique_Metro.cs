using System;
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
		//private EffectTechnique nativeTechnique;
		private ANX.Framework.Graphics.Effect parentEffect;

		//internal EffectTechnique_DX10(ANX.Framework.Graphics.Effect parentEffect)
		//{
		//    if (parentEffect == null)
		//    {
		//        throw new ArgumentNullException("parentEffect");
		//    }

		//    this.parentEffect = parentEffect;
		//}

		//public EffectTechnique NativeTechnique
		//{
		//    get
		//    {
		//        return this.nativeTechnique;
		//    }
		//    internal set
		//    {
		//        this.nativeTechnique = value;
		//    }
		//}

		public string Name
		{
			get
			{
				//return nativeTechnique.Description.Name;
				throw new NotImplementedException();
			}
		}


		public IEnumerable<EffectPass> Passes
		{
			get
			{
				//TODO: implement
				System.Diagnostics.Debugger.Break();
				return null;

				//for (int i = 0; i < nativeTechnique.Description.PassCount; i++)
				//{
				//    EffectPass_DX10 passDx10 = new EffectPass_DX10();
				//    passDx10.NativePass = nativeTechnique.GetPassByIndex(i);

				//    Graphics.EffectPass pass = new Graphics.EffectPass(this.parentEffect);

				//    yield return pass;
				//}
			}
		}
	}
}
