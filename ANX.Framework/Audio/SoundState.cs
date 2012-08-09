using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	[Flags]
	public enum SoundState
	{
		Playing = 0,
		Paused = 1,
		Stopped = 2,
	}
}
