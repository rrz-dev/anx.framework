#region Using Statements
using System;
using System.Collections.Generic;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class ListReader<T> : ContentTypeReader<List<T>>
    {
        private ContentTypeReader baseTypeReader;

        protected internal override void Initialize(ContentTypeReaderManager manager)
        {
            this.baseTypeReader = manager.GetTypeReader(typeof(T));
        }

        protected internal override List<T> Read(ContentReader input, List<T> existingInstance)
        {
            int count = input.ReadInt32();
            List<T> result = existingInstance ?? new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                T item = input.ReadObject<T>(baseTypeReader);
                result.Add(item);
            }

            return result;
        }
    }
}
