using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(90)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class EffectTechnique
    {
        internal INativeEffectTechnique NativeTechnique { get; private set; }

        public string Name
        {
            get { return NativeTechnique.Name; }
        }

        public EffectAnnotationCollection Annotations
		{
			get
			{
				throw new NotImplementedException();
			}
		}

        public EffectPassCollection Passes { get; private set; }

        internal EffectTechnique(Effect parentEffect, INativeEffectTechnique nativeTechnique)
        {
            this.NativeTechnique = nativeTechnique;
            this.Passes = new EffectPassCollection(parentEffect, parentEffect.NativeEffect, nativeTechnique);
        }
    }
}
