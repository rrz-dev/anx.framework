#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("???, AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public sealed class ModelBoneCollection : ReadOnlyCollection<ModelBone>
    {
        private readonly ModelBone[] modelBones;

        public ModelBone this[string boneName]
        {
            get
            {
                ModelBone result;
                if (TryGetValue(boneName, out result) == false)
                    throw new KeyNotFoundException();

                return result;
            }
        }

        internal ModelBoneCollection(ModelBone[] modelBones)
            : base(modelBones)
        {
            this.modelBones = modelBones;
        }

        public bool TryGetValue(string boneName, out ModelBone value)
        {
            if (String.IsNullOrEmpty(boneName))
                throw new ArgumentNullException("boneName");

            for (int index = 0; index < Items.Count; index++)
            {
                ModelBone modelBone = Items[index];
                if (String.Compare(modelBone.Name, boneName, StringComparison.Ordinal) == 0)
                {
                    value = modelBone;
                    return true;
                }
            }

            value = null;
            return false;
        }

        public new Enumerator GetEnumerator()
        {
            return new Enumerator(this.modelBones);
        }

        public struct Enumerator : IEnumerator<ModelBone>, IDisposable, IEnumerator
        {
            private readonly ModelBone[] wrappedArray;
            private int position;

            public ModelBone Current
            {
                get { return this.wrappedArray[this.position]; }
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            internal Enumerator(ModelBone[] wrappedArray)
            {
                this.wrappedArray = wrappedArray;
                this.position = -1;
            }

            public bool MoveNext()
            {
                this.position++;
                if (this.position >= this.wrappedArray.Length)
                {
                    this.position = this.wrappedArray.Length;
                    return false;
                }
                return true;
            }

            void IEnumerator.Reset()
            {
                this.position = -1;
            }

            public void Dispose()
            {
            }
        }
    }
}
