using System;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.SoundSystem;

namespace ANX.SoundSystem.OpenAL
{
	public class OpenALSoundEffectInstance : ISoundEffectInstance
	{
		#region Private
		private OpenALSoundEffect parent;
		#endregion

		#region Public (TODO)
		public bool IsLooped
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
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public SoundState State
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public float Volume
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
		#endregion

		#region Constructor
		internal OpenALSoundEffectInstance(OpenALSoundEffect setParent)
		{
			parent = setParent;
		}
		#endregion

		public void Play()
		{
			throw new NotImplementedException();
		}

		public void Pause()
		{
			throw new NotImplementedException();
		}

		public void Stop(bool immediate)
		{
			throw new NotImplementedException();
		}

		public void Resume()
		{
			throw new NotImplementedException();
		}

		public void Apply3D(Framework.Audio.AudioListener[] listeners, Framework.Audio.AudioEmitter emitter)
		{
			throw new NotImplementedException();
		}

		#region Dispose (TODO)
		public void Dispose()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
