using System;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.SoundSystem;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	[PercentageComplete(100)]
	public class SoundEffectInstance : IDisposable
	{
		#region Private
		private SoundEffect parent;

		private ISoundEffectInstance nativeInstance;

		internal bool IsFireAndForget
		{
			get;
			private set;
		}
		#endregion

		#region Public
		#region IsDisposed
		public bool IsDisposed
		{
			get;
			private set;
		}
		#endregion

		#region IsLooped
		public virtual bool IsLooped
		{
			get
			{
				return nativeInstance.IsLooped;
			}
			set
			{
				nativeInstance.IsLooped = value;
			}
		}
		#endregion

		#region Pan
		public float Pan
		{
			get
			{
				return nativeInstance.Pan;
			}
			set
			{
				nativeInstance.Pan = value;
			}
		}
		#endregion

		#region Pitch
		public float Pitch
		{
			get
			{
				return nativeInstance.Pitch;
			}
			set
			{
				nativeInstance.Pitch = value;
			}
		}
		#endregion

		#region State
		public SoundState State
		{
			get
			{
				return nativeInstance.State;
			}
		}
		#endregion

		#region Volume
		public float Volume
		{
			get
			{
				return nativeInstance.Volume;
			}
			set
			{
				nativeInstance.Volume = value;
			}
		}
		#endregion
		#endregion

		#region Constructor
		protected SoundEffectInstance()
		{
		}

		internal SoundEffectInstance(SoundEffect setParent, bool setIsFireAndForget)
		{
			parent = setParent;
			IsFireAndForget = setIsFireAndForget;

			nativeInstance = GetCreator().CreateSoundEffectInstance(
				setParent.nativeSoundEffect);
		}

		~SoundEffectInstance()
		{
			Dispose();
		}
		#endregion

		#region GetCreator
		private static ISoundSystemCreator GetCreator()
		{
			return AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
		}
		#endregion

		#region Apply3D
		public void Apply3D(AudioListener listener, AudioEmitter emitter)
		{
			Apply3D(new AudioListener[] { listener }, emitter);
		}

		public void Apply3D(AudioListener[] listeners, AudioEmitter emitter)
		{
			nativeInstance.Apply3D(listeners, emitter);
		}
		#endregion

		#region Pause
		public void Pause()
		{
			nativeInstance.Pause();
		}
		#endregion

		#region Play
		public virtual void Play()
		{
			nativeInstance.Play();
		}
		#endregion

		#region Resume
		public void Resume()
		{
			nativeInstance.Resume();
		}
		#endregion

		#region Stop
		public void Stop()
		{
			Stop(true);
		}

		public void Stop(bool immediate)
		{
			nativeInstance.Stop(immediate);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (nativeInstance != null)
			{
				nativeInstance.Dispose();
				nativeInstance = null;
			}

			IsDisposed = true;
		}
		#endregion
	}
}
