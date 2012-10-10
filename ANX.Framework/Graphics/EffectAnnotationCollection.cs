#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(10)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class EffectAnnotationCollection : IEnumerable<EffectAnnotation>
    {
        public EffectAnnotation this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public EffectAnnotation this[string name]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IEnumerator<EffectAnnotation> IEnumerable<EffectAnnotation>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public List<EffectAnnotation>.Enumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
