#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public sealed class EffectAnnotationCollection : IEnumerable<EffectAnnotation>
    {
        private readonly List<EffectAnnotation> annotations;

        public int Count
        {
            get { return annotations.Count; }
        }

        public EffectAnnotation this[int index]
        {
            get { return index >= 0 && index < annotations.Count ? annotations[index] : null; }
        }

        public EffectAnnotation this[string name]
        {
            get { return annotations.FirstOrDefault(annotation => annotation.Name == name); }
        }

        internal EffectAnnotationCollection(IEnumerable<EffectAnnotation> setAnnotations)
        {
            annotations = new List<EffectAnnotation>(setAnnotations);
        }

        IEnumerator<EffectAnnotation> IEnumerable<EffectAnnotation>.GetEnumerator()
        {
            return annotations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return annotations.GetEnumerator();
        }

        public List<EffectAnnotation>.Enumerator GetEnumerator()
        {
            return annotations.GetEnumerator();
        }
    }
}
