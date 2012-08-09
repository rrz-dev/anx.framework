#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class EffectPassCollection : IEnumerable<EffectPass>
    {
        #region Private Members
        private Effect parentEffect;
        private INativeEffect nativeEffect;
        private INativeEffectTechnique parentTechnique;
        private List<EffectPass> passes;

        #endregion // Private Members

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

        public EffectPass this[int index]
        {
            get
            {
                if (index >= passes.Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                return passes[index];
            }
        }

        public EffectPass this[string name]
        {
            get
            {
                throw new NotImplementedException();
            }
		}

		IEnumerator<EffectPass> IEnumerable<EffectPass>.GetEnumerator()
		{
            return (IEnumerator<EffectPass>)this.passes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
            return (IEnumerator)this.passes.GetEnumerator();
		}

		public List<EffectPass>.Enumerator GetEnumerator()
		{
            return this.passes.GetEnumerator();
		}

        public int Count
        {
            get
            {
                return this.passes.Count;
            }
        }
    }
}
