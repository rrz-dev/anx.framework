#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class ObjectReader : ContentTypeReader
    {
        public ObjectReader()
            : base(typeof(object))
        {
        }

        protected internal override object Read(ContentReader input, object existingInstance)
        {
            throw new NotSupportedException();
        }
    }
}
