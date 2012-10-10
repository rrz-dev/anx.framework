#region Using Statements
using System;
using System.Runtime.Serialization;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class GamerServicesNotAvailableException : Exception
    {
        public GamerServicesNotAvailableException()
            : base()
        {
        }

#if !WINDOWSMETRO      //TODO: search replacement for Win8
        protected GamerServicesNotAvailableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
        
        public GamerServicesNotAvailableException(string message)
            : base(message)
        {
        }

        public GamerServicesNotAvailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
