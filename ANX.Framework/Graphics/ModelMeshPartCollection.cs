using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
    public sealed class ModelMeshPartCollection : ReadOnlyCollection<ModelMeshPart>
    {
        private readonly ModelMeshPart[] modelMeshParts;

        internal ModelMeshPartCollection(ModelMeshPart[] modelMeshParts)
            : base(modelMeshParts)
        {
            this.modelMeshParts = modelMeshParts;
        }

        public new Enumerator GetEnumerator()
        {
            return new Enumerator(this.modelMeshParts);
        }

        public struct Enumerator : IEnumerator<ModelMeshPart>, IDisposable, IEnumerator
        {
            private readonly ModelMeshPart[] wrappedArray;
			private int position;

			public ModelMeshPart Current
			{
				get
				{
					return this.wrappedArray[this.position];
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

            internal Enumerator(ModelMeshPart[] wrappedArray)
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
