#region Using Statements
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class ModelMeshCollection : ReadOnlyCollection<ModelMesh>
    {
        private ModelMesh[] modelMeshes;

        internal ModelMeshCollection(ModelMesh[] modelMeshes)
            : base(modelMeshes)
        {
            this.modelMeshes = modelMeshes;
        }

        public new Enumerator GetEnumerator()
        {
            return new Enumerator(this.modelMeshes);
        }

        public struct Enumerator : IEnumerator<ModelMesh>, IDisposable, IEnumerator
        {
            private ModelMesh[] wrappedArray;
            private int position;

            internal Enumerator(ModelMesh[] wrappedArray)
            {
                this.wrappedArray = wrappedArray;
                this.position = -1;
            }

            public ModelMesh Current
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

        public bool TryGetValue(string meshName, out ModelMesh value)
        {
            throw new NotImplementedException();
        }

        public ModelMesh this[string meshName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    }
}
