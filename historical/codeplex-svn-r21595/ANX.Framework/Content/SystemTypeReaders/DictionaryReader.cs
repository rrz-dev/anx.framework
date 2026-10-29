#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    internal class DictionaryReader<TKey, TValue> : ContentTypeReader<Dictionary<TKey, TValue>>
    {
        private ContentTypeReader keyTypeReader;
        private ContentTypeReader valueTypereader;

        protected internal override void Initialize(ContentTypeReaderManager manager)
        {
            this.keyTypeReader = manager.GetTypeReader(typeof(TKey));
            this.valueTypereader = manager.GetTypeReader(typeof(TValue));
        }

        protected internal override Dictionary<TKey, TValue> Read(ContentReader input, Dictionary<TKey, TValue> existingInstance)
        {
            int count = input.ReadInt32();
            Dictionary<TKey, TValue> result = existingInstance ?? new Dictionary<TKey, TValue>(count);
            for (int i = 0; i < count; i++)
            {
                TKey key = input.ReadObject<TKey>(keyTypeReader);
                TValue value = input.ReadObject<TValue>(valueTypereader);
                result.Add(key, value);
            }

            return result;
        }
    }
}
