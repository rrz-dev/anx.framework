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
    public class GuideAlreadyVisibleException : Exception
    {
        public GuideAlreadyVisibleException()
            : base()
        {
        }

#if !WINDOWSMETRO      //TODO: search replacement for Win8
        protected GuideAlreadyVisibleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
        
        public GuideAlreadyVisibleException(string message)
            : base(message)
        {
        }

        public GuideAlreadyVisibleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
