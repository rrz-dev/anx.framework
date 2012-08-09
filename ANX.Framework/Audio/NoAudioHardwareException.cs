using System;
using System.Runtime.InteropServices;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
#if !WINDOWSMETRO      //TODO: search replacement for Win8
    [SerializableAttribute]
#endif
    public sealed class NoAudioHardwareException : ExternalException
	{
		public NoAudioHardwareException()
			: base()
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
