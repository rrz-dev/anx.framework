#region Using Statements
using System;
using System.Globalization;

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
            if (value is T)
            {
                this.Write(output, (T)value);
            }
            else
            {
                throw new FormatException("The type of the value-object does not match the generic T-parameter");
            }
        }

        protected internal abstract void Write(ContentWriter output, T value);
    }
}
