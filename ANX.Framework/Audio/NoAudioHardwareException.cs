using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
#if !WINDOWSMETRO      //TODO: search replacement for Win8
    [Serializable]
#endif
    [PercentageComplete(99)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public sealed class NoAudioHardwareException : ExternalException
	{
		public NoAudioHardwareException()
		{
		}

		public NoAudioHardwareException(string message)
			: base(message)
		{
		}

		public NoAudioHardwareException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
