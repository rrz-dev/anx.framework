using System;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA;

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

        internal INativeEffectPass NativeEffectPass
        {
            get;
            private set;
        }

        public string Name 
        {
            get { return this.NativeEffectPass.Name; }
        }

        public EffectAnnotationCollection Annotations
        {
            get { return this.NativeEffectPass.Annotations; }
        }

        internal EffectPass(INativeEffectPass nativeEffectPass)
        {
            if (nativeEffectPass == null)
                throw new ArgumentNullException("nativeEffectPass");

            this.NativeEffectPass = nativeEffectPass;
        }

        public void Apply()
        {
            this.NativeEffectPass.Apply();
        }
    }
}
