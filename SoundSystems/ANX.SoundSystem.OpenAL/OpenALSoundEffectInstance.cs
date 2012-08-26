using System;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.SoundSystem;
using OpenTK.Audio;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
	public class OpenALSoundEffectInstance : ISoundEffectInstance
	{
		#region Private
		private OpenALSoundEffect parent;

		private int handle;
		#endregion

		#region Public
		public bool IsLooped
		{
			get
			{
				bool result;
				AL.GetSource(handle, ALSourceb.Looping, out result);
				return result;
			}
			set
			{
				AL.Source(handle, ALSourceb.Looping, value);
			}
		}

		public float Pan
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public float Pitch
		{
			get
			{
				float result;
				AL.GetSource(handle, ALSourcef.Pitch, out result);
				return result;
			}
			set
			{
				AL.Source(handle, ALSourcef.Pitch, value);
			}
		}

		public SoundState State
		{
			get;
			private set;
		}

		public float Volume
		{
			get
			{
				float result;
				AL.GetSource(handle, ALSourcef.Gain, out result);
				return result;
			}
			set
			{
				AL.Source(handle, ALSourcef.Gain, value);
			}
		}
		#endregion

		#region Constructor
		internal OpenALSoundEffectInstance(OpenALSoundEffect setParent)
		{
			parent = setParent;

			State = SoundState.Stopped;

			handle = AL.GenSource();
			AL.Source(handle, ALSourcei.Buffer, parent.bufferHandle);
			IsLooped = false;
			Pitch = 1f;
			Volume = 1f;
			// TODO: Pan = 0f;
		}
		#endregion

		#region Play
		public void Play()
		{
			if (State != SoundState.Playing)
			{
				State = SoundState.Playing;
				AL.SourcePlay(handle);
			}
		}
		#endregion

		#region Pause
		public void Pause()
		{
			if (State != SoundState.Paused)
			{
				State = SoundState.Paused;
				AL.SourcePause(handle);
			}
		}
		#endregion

		#region Stop
		public void Stop(bool immediate)
		{
			if (State == SoundState.Stopped)
				return;

			if (immediate)
			{
				State = SoundState.Stopped;
				AL.SourceStop(handle);
			}
		}
		#endregion

		#region Resume
		public void Resume()
		{
			if (State != SoundState.Playing)
			{
				State = SoundState.Playing;
				AL.SourcePlay(handle);
			}
		}
		#endregion

		#region Apply3D (TODO)
		public void Apply3D(AudioListener[] listeners, AudioEmitter emitter)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			AL.DeleteSource(handle);
			handle = -1;
		}
		#endregion
	}
}
