using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class EffectPass
    {
        private readonly Effect parentEffect;

        public string Name { get; private set; }
        public EffectAnnotationCollection Annotations { get; private set; }

        internal EffectPass(Effect parentEffect)
        {
            if (parentEffect == null)
                throw new ArgumentNullException("parentEffect");

            this.parentEffect = parentEffect;
        }

        public void Apply()
        {
			parentEffect.PreBindSetParameters();
            parentEffect.NativeEffect.Apply(parentEffect.GraphicsDevice);
        }
    }
}
