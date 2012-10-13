#region Using Statements
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("???, AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class ModelMeshCollection : ReadOnlyCollection<ModelMesh>
    {
        private readonly ModelMesh[] modelMeshes;

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
            private readonly ModelMesh[] wrappedArray;
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
            if (String.IsNullOrEmpty(meshName))
                throw new ArgumentNullException("meshName");

            for (int index = 0; index < Items.Count; index++)
            {
                ModelMesh modelMesh = Items[index];
                if (String.Compare(modelMesh.Name, meshName, StringComparison.Ordinal) == 0)
                {
                    value = modelMesh;
                    return true;
                }
            }

            value = null;
            return false;
        }

        public ModelMesh this[string meshName]
        {
            get
            {
                ModelMesh result;
                if (TryGetValue(meshName, out result) == false)
                {
                    throw new KeyNotFoundException();
                }
                return result;
            }
        }
    }
}
