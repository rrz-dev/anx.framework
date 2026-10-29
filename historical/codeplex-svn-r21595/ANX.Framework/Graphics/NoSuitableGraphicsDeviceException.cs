#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class NoSuitableGraphicsDeviceException : Exception
    {
        public NoSuitableGraphicsDeviceException()
            : base()
        {
        }

        public NoSuitableGraphicsDeviceException(string message)
            : base(message)
        {
        }

        public NoSuitableGraphicsDeviceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
