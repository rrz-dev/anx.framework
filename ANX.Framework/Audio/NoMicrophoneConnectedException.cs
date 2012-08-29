using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	[PercentageComplete(100)]
	public sealed class NoMicrophoneConnectedException : Exception
	{
		public NoMicrophoneConnectedException()
			: base()
		{
		}

		public NoMicrophoneConnectedException(string message)
			: base(message)
		{
		}

		public NoMicrophoneConnectedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
