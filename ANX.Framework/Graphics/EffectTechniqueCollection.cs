#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using System.Collections;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class EffectTechniqueCollection : IEnumerable<EffectTechnique>, IEnumerable
    {
        #region Private Members
        private Effect parentEffect;
        private INativeEffect nativeEffect;
        private List<EffectTechnique> techniques;

        #endregion // Private Members

        internal EffectTechniqueCollection(Effect parentEffect, INativeEffect nativeEffect)
        {
            this.parentEffect = parentEffect;
            this.nativeEffect = nativeEffect;
            this.techniques = new List<EffectTechnique>();

            foreach (EffectTechnique teq in nativeEffect.Techniques)
            {
                this.techniques.Add(teq);
            }
        }

        public EffectTechnique this[int index]
        {
            get
            {
                if (index >= techniques.Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                return techniques[index];
            }
        }

        public EffectTechnique this[string name]
        {
            get
            {
                foreach (EffectTechnique teq in techniques)
                {
                    if (teq.Name.Equals(name))
                    {
                        return teq;
                    }
                }

                throw new ArgumentException("No technique with name '" + name + "' found.");
            }
		}

		IEnumerator<EffectTechnique> IEnumerable<EffectTechnique>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public List<EffectTechnique>.Enumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

        public int Count
        {
            get
            {
                return this.techniques.Count;
            }
        }


    }
}
