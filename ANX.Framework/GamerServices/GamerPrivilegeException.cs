#region Using Statements
using System;
using System.Runtime.Serialization;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    public class GamerPrivilegeException : Exception
    {
        public GamerPrivilegeException()
            : base()
        {
        }

#if !WINDOWSMETRO      //TODO: search replacement for Win8
        protected GamerPrivilegeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
        
        public GamerPrivilegeException(string message)
            : base(message)
        {
        }

        public GamerPrivilegeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
