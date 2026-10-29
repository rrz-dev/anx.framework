using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.SoundSystem
{
	public interface IAudioEmitter
	{
		float DopplerScale
		{
			get;
			set;
		}

		Vector3 Forward
		{
			get;
			set;
		}

		Vector3 Position
		{
			get;
			set;
		}

		Vector3 Up
		{
			get;
			set;
		}

		Vector3 Velocity
		{
			get;
			set;
		}
	}
}
