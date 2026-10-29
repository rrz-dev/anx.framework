#region Using Statements
using System;
using System.Runtime.Serialization;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public class AddInLoadingException : Exception
    {
        public AddInLoadingException()
            : base()
        {
        }

#if !WINDOWSMETRO      //TODO: search replacement for Win8
        protected AddInLoadingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
        
        public AddInLoadingException(string message)
            : base(message)
        {
        }

        public AddInLoadingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
