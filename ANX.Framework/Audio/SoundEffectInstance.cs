using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.SoundSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.InProgress)]
	[Developer("AstrorEnales")]
	public class SoundEffectInstance : IDisposable
	{
		#region Private
        internal ISoundEffectInstance NativeInstance { get; private set; }
	    internal bool IsFireAndForget { get; private set; }
	    #endregion

		#region Public
	    public bool IsDisposed { get; private set; }

	    public virtual bool IsLooped
	    {
	        get { return NativeInstance.IsLooped; }
	        set { NativeInstance.IsLooped = value; }
	    }

	    public float Pan
	    {
	        get { return NativeInstance.Pan; }
	        set { NativeInstance.Pan = value; }
	    }

	    public float Pitch
	    {
	        get { return NativeInstance.Pitch; }
	        set { NativeInstance.Pitch = value; }
	    }

	    public SoundState State
	    {
	        get { return NativeInstance.State; }
	    }

	    public float Volume
	    {
	        get { return NativeInstance.Volume; }
	        set { NativeInstance.Volume = value; }
	    }
		#endregion

		#region Constructor
        protected SoundEffectInstance()
        {
        }

	    internal SoundEffectInstance(SoundEffect setParent, bool setIsFireAndForget)
		{
			IsFireAndForget = setIsFireAndForget;
			NativeInstance = GetCreator().CreateSoundEffectInstance(setParent.NativeSoundEffect);
		}

		~SoundEffectInstance()
		{
			Dispose();
		}
		#endregion

        internal void SetNativeInstance(IDynamicSoundEffectInstance dynamicInstance)
        {
            NativeInstance = dynamicInstance;
        }

		#region GetCreator
		private static ISoundSystemCreator GetCreator()
		{
			return AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
		}
		#endregion

		#region Apply3D
		public void Apply3D(AudioListener listener, AudioEmitter emitter)
		{
			Apply3D(new[] { listener }, emitter);
		}

		public void Apply3D(AudioListener[] listeners, AudioEmitter emitter)
		{
			NativeInstance.Apply3D(listeners, emitter);
		}
		#endregion

		#region Pause
		public void Pause()
		{
			NativeInstance.Pause();
		}
		#endregion

		#region Play
		public virtual void Play()
		{
			NativeInstance.Play();
		}
		#endregion

		#region Resume
		public void Resume()
		{
			NativeInstance.Resume();
		}
		#endregion

		#region Stop
		public void Stop()
		{
			Stop(true);
		}

		public void Stop(bool immediate)
		{
			NativeInstance.Stop(immediate);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (NativeInstance != null)
			{
				NativeInstance.Dispose();
				NativeInstance = null;
			}

			IsDisposed = true;
		}
		#endregion
	}
}
