using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class EffectTechnique
    {
        private Effect parentEffect;
        private INativeEffectTechnique nativeTechnique;
		private EffectPassCollection effectPassCollection;

		internal INativeEffectTechnique NativeTechnique
		{
			get
			{
				return this.nativeTechnique;
			}
		}

		public string Name
		{
			get
			{
				return nativeTechnique.Name;
			}
		}

		public EffectAnnotationCollection Annotations
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public EffectPassCollection Passes
		{
			get
			{
				return this.effectPassCollection;
			}
		}

        internal EffectTechnique(Effect parentEffect, INativeEffectTechnique nativeTechnique)
        {
            this.parentEffect = parentEffect;
            this.nativeTechnique = nativeTechnique;
            this.effectPassCollection = new EffectPassCollection(parentEffect, parentEffect.NativeEffect, nativeTechnique);
        }
    }
}
