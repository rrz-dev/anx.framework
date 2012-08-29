using System;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.SoundSystem;
using SharpDX.XAudio2;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Windows.XAudio
{
	public class XAudioSoundEffectInstance : ISoundEffectInstance
	{
		#region Private
		private XAudioSoundEffect parent;

		private SourceVoice source;
		#endregion

		#region Public
		public bool IsLooped
		{
			get
			{
				return false;
				//throw new NotImplementedException();
			}
			set
			{
				//throw new NotImplementedException();
			}
		}

		public float Pan
		{
			get
			{
				return 0f;
				//throw new NotImplementedException();
			}
			set
			{
				//throw new NotImplementedException();
			}
		}

		public float Pitch
		{
			get
			{
				return 1f;
				//throw new NotImplementedException();
			}
			set
			{
				//throw new NotImplementedException();
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
				return source.Volume;
			}
			set
			{
				source.SetVolume(value, 0);
			}
		}
		#endregion

		#region Constructor
		internal XAudioSoundEffectInstance(XAudio2 device, XAudioSoundEffect setParent)
		{
			parent = setParent;

			State = SoundState.Stopped;

			var sourceVoice = new SourceVoice(device, setParent.waveFormat);
			sourceVoice.SubmitSourceBuffer(setParent.audioBuffer, setParent.DecodedPacketsInfo);
			sourceVoice.Start();
		}
		#endregion

		#region Play
		public void Play()
		{
			if (State != SoundState.Playing)
			{
				State = SoundState.Playing;
				source.Start();
			}
		}
		#endregion

		#region Pause
		public void Pause()
		{
			if (State != SoundState.Paused)
			{
				State = SoundState.Paused;
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
				source.Stop();
			}
		}
		#endregion

		#region Resume
		public void Resume()
		{
			if (State != SoundState.Playing)
			{
				State = SoundState.Playing;
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
			if (source != null)
			{
				source.Dispose();
				source = null;
			}
		}
		#endregion
	}
}
