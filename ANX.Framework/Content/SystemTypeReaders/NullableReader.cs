#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class NullableReader<T> : ContentTypeReader<T?> where T : struct
    {
        private ContentTypeReader baseTypeReader;

        protected internal override void Initialize(ContentTypeReaderManager manager)
        {
            this.baseTypeReader = manager.GetTypeReader(typeof(T));
        }

        protected internal override T? Read(ContentReader input, T? existingInstance)
        {
            bool hasValue = input.ReadBoolean();

            return hasValue ?
							new T?(input.ReadRawObject<T>(this.baseTypeReader)) :
							null;
        }
    }
}
