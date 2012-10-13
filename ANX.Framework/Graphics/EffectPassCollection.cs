#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann, AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class EffectPassCollection : IEnumerable<EffectPass>
    {
        #region Private Members
        private Effect parentEffect;
        private INativeEffect nativeEffect;
        private INativeEffectTechnique parentTechnique;
        private readonly List<EffectPass> passes;

        #endregion // Private Members

        public EffectPass this[int index]
        {
            get { return index >= 0 && index < passes.Count ? passes[index] : null; }
        }

        public EffectPass this[string name]
        {
            get { return passes.FirstOrDefault(pass => pass.Name == name); }
        }

        public int Count
        {
            get { return passes.Count; }
        }

        internal EffectPassCollection(Effect parentEffect, INativeEffect nativeEffect, INativeEffectTechnique parentTechnique)
        {
            this.parentEffect = parentEffect;
            this.nativeEffect = nativeEffect;
            this.parentTechnique = parentTechnique;
            this.passes = new List<EffectPass>();

            foreach (EffectPass pass in parentTechnique.Passes)
            {
                this.passes.Add(pass);
            }
        }

		IEnumerator<EffectPass> IEnumerable<EffectPass>.GetEnumerator()
		{
            return passes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
            return passes.GetEnumerator();
		}

		public List<EffectPass>.Enumerator GetEnumerator()
		{
            return passes.GetEnumerator();
		}
    }
}
