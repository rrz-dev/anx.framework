#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
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
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann")]
    public sealed class EffectParameterCollection : IEnumerable<EffectParameter>
    {
        #region Private Members
        private Effect parentEffect;
        private INativeEffect nativeEffect;
        private List<EffectParameter> parameters;
        #endregion

        public EffectParameter this[int index]
        {
            get { return index >= 0 && index < parameters.Count ? parameters[index] : null; }
        }

        public EffectParameter this[string name]
        {
            get { return parameters.FirstOrDefault(t => t.Name == name); }
        }

        public int Count
        {
            get { return parameters.Count; }
        }

        internal EffectParameterCollection(Effect parentEffect, INativeEffect nativeEffect)
        {
            this.parentEffect = parentEffect;
            this.nativeEffect = nativeEffect;
            this.parameters = new List<EffectParameter>();

            foreach (EffectParameter p in nativeEffect.Parameters)
            {
                this.parameters.Add(p);
            }
        }

        public EffectParameter GetParameterBySemantic(string semantic)
        {
            foreach (EffectParameter parameter in parameters)
            {
                if (string.Equals(parameter.Semantic.ToLowerInvariant(), semantic.ToLowerInvariant()))
                {
                    return parameter;
                }
            }

            return null;
        }

		IEnumerator<EffectParameter> IEnumerable<EffectParameter>.GetEnumerator()
		{
			return parameters.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return parameters.GetEnumerator();
		}

		public List<EffectParameter>.Enumerator GetEnumerator()
		{
			return parameters.GetEnumerator();
		}
    }
}
