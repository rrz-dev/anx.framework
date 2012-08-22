using System;
using ANX.Framework;
using ANX.Framework.NonXNA.SoundSystem;
using OpenTK.Audio.OpenAL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
	public class OpenALAudioListener : IAudioListener
	{
		#region Private
		private OpenTK.Vector3 forward;
		private OpenTK.Vector3 up;
		#endregion

		#region Public
		public Vector3 Forward
		{
			get
			{
				AL.GetListener(ALListenerfv.Orientation, out forward, out up);
				return new Vector3(forward.X, forward.Y, forward.Z);
			}
			set
			{
				forward.X = value.X;
				forward.Y = value.Y;
				forward.Z = value.Z;
				AL.Listener(ALListenerfv.Orientation, ref forward, ref up);
			}
		}

		public Vector3 Position
		{
			get
			{
				Vector3 result;
				AL.GetListener(ALListener3f.Position, out result.X, out result.Y, out result.Z);
				return result;
			}
			set
			{
				AL.Listener(ALListener3f.Position, value.X, value.Y, value.Z);
			}
		}

		public Vector3 Up
		{
			get
			{
				AL.GetListener(ALListenerfv.Orientation, out forward, out up);
				return new Vector3(up.X, up.Y, up.Z);
			}
			set
			{
				up.X = value.X;
				up.Y = value.Y;
				up.Z = value.Z;
				AL.Listener(ALListenerfv.Orientation, ref forward, ref up);
			}
		}

		public Vector3 Velocity
		{
			get
			{
				Vector3 result;
				AL.GetListener(ALListener3f.Velocity, out result.X, out result.Y, out result.Z);
				return result;
			}
			set
			{
				AL.Listener(ALListener3f.Velocity, value.X, value.Y, value.Z);
			}
		}
		#endregion
	}
}
