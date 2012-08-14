#region Using Statements
using System;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    public abstract class ContentTypeWriter<T> : ContentTypeWriter
    {
        protected ContentTypeWriter()
            : base(typeof(T))
        {
        }

        protected internal override void Write(ContentWriter output, object value)
        {
            throw new NotImplementedException();
        }

        protected internal abstract void Write(ContentWriter output, T value);
    }
}
