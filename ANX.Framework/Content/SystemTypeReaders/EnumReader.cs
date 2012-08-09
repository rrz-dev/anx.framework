#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class EnumReader<T> : ContentTypeReader<T>
    {
        private ContentTypeReader baseTypeReader;

        protected internal override void Initialize(ContentTypeReaderManager manager)
        {
            Type baseType = Enum.GetUnderlyingType(typeof(T));
            this.baseTypeReader = manager.GetTypeReader(baseType);
        }

        protected internal override T Read(ContentReader input, T existingInstance)
        {
            return (T)input.ReadRawObject<object>(this.baseTypeReader);
        }
    }
}
