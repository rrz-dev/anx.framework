#region Using Statements
using System;
using System.Runtime.Serialization;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public class NoInputDeviceException : Exception
    {
        public NoInputDeviceException()
            : base()
        {
        }

#if !WINDOWSMETRO      //TODO: search replacement for Win8
        protected NoInputDeviceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
        
        public NoInputDeviceException(string message)
            : base(message)
        {
        }

        public NoInputDeviceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
