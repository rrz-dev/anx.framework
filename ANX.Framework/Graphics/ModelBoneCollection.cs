#region Using Statements
using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class ModelBoneCollection : ReadOnlyCollection<ModelBone>
    {
        private ModelBone[] modelBones;

        internal ModelBoneCollection(ModelBone[] modelBones)
            : base(modelBones)
        {
            this.modelBones = modelBones;
        }

        public new Enumerator GetEnumerator()
        {
            return new Enumerator(this.modelBones);
        }

        public struct Enumerator : IEnumerator<ModelBone>, IDisposable, IEnumerator
        {
            private ModelBone[] wrappedArray;
            private int position;

            internal Enumerator(ModelBone[] wrappedArray)
            {
                this.wrappedArray = wrappedArray;
                this.position = -1;
            }

            public ModelBone Current
            {
                get
                {
                    return this.wrappedArray[this.position];
                }
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

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
        }

        public bool TryGetValue (string boneName, out ModelBone value)
        {
            throw new NotImplementedException();
        }

        public ModelBone this[string boneName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    }
}
