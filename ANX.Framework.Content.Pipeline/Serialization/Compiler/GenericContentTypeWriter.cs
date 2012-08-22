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
            this.Write(output, ContentTypeWriter<T>.CastType(value));
        }

        protected internal abstract void Write(ContentWriter output, T value);

        private static T CastType(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (!(value is T))
            {
                System.Diagnostics.Debugger.Break();
                //throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.WrongArgumentType, new object[]
                //{
                //    typeof(T),
                //    value.GetType()
                //}));
            }
            return (T)((object)value);
        }

    }
}
