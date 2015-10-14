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
    [Developer("Glatzemann, AstrorEnales")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public sealed class EffectTechniqueCollection : IEnumerable<EffectTechnique>, IEnumerable
    {
        #region Private Members
        private List<EffectTechnique> techniques;

        #endregion // Private Members

        public EffectTechnique this[int index]
        {
            get { return index >= 0 && index < techniques.Count ? techniques[index] : null; }
        }

        public EffectTechnique this[string name]
        {
            get { return techniques.FirstOrDefault(t => t.Name == name); }
        }

        public int Count
        {
            get { return techniques.Count; }
        }

        internal EffectTechniqueCollection(IEnumerable<EffectTechnique> techniques)
        {
            this.techniques = new List<EffectTechnique>(techniques);
        }

		IEnumerator<EffectTechnique> IEnumerable<EffectTechnique>.GetEnumerator()
        {
            return techniques.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
        {
            return techniques.GetEnumerator();
		}

		public List<EffectTechnique>.Enumerator GetEnumerator()
        {
            return techniques.GetEnumerator();
		}
    }
}
