#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class ArrayReader<T> : ContentTypeReader<T[]>
    {
        private ContentTypeReader baseTypeReader;

        protected internal override void Initialize(ContentTypeReaderManager manager)
        {
            this.baseTypeReader = manager.GetTypeReader(typeof(T));
        }

        protected internal override T[] Read(ContentReader input, T[] existingInstance)
        {
            int count = input.ReadInt32();
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = input.ReadObject<T>(baseTypeReader);
            }

            return result;
        }
    }
}
