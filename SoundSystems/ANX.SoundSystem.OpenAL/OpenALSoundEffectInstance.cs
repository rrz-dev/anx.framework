using System;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.SoundSystem;
using OpenTK.Audio.OpenAL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
    [Developer("AstrorEnales")]
	public class OpenALSoundEffectInstance : ISoundEffectInstance
	{
		#region Private
		private readonly OpenALSoundEffect parent;
	    private float currentPan;
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
	        set { AL.Source(handle, ALSourceb.Looping, value); }
	    }

	    public float Pan
	    {
            get { return currentPan; }
	        set
	        {
	            currentPan = value;
                // TODO: set actual parameter
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
	        set { AL.Source(handle, ALSourcef.Pitch, value); }
	    }

	    public SoundState State { get; private set; }

	    public float Volume
	    {
	        get
	        {
	            float result;
	            AL.GetSource(handle, ALSourcef.Gain, out result);
	            return result;
	        }
	        set { AL.Source(handle, ALSourcef.Gain, value); }
	    }
	    #endregion

		#region Constructor
		internal OpenALSoundEffectInstance(OpenALSoundEffect setParent)
		{
			parent = setParent;
			State = SoundState.Stopped;
			handle = AL.GenSource();
			AL.Source(handle, ALSourcei.Buffer, parent.BufferHandle);
			IsLooped = false;
			Pitch = 1f;
			Volume = 1f;
			Pan = 0f;

			ALError error = AL.GetError();
			if (error != ALError.NoError)
				throw new Exception("OpenAL error " + error + ": " + AL.GetErrorString(error));
		}
		#endregion

		#region Play
		public void Play()
		{
		    if (State == SoundState.Playing)
		        return;

		    State = SoundState.Playing;
		    AL.SourcePlay(handle);
		}
		#endregion

		#region Pause
		public void Pause()
		{
		    if (State == SoundState.Paused)
		        return;

		    State = SoundState.Paused;
		    AL.SourcePause(handle);
		}
		#endregion

		#region Stop
		public void Stop(bool immediate)
		{
            if (State == SoundState.Stopped || immediate == false)
				return;

		    State = SoundState.Stopped;
		    AL.SourceStop(handle);
		}
		#endregion

		#region Resume
		public void Resume()
		{
		    if (State == SoundState.Playing)
		        return;

		    State = SoundState.Playing;
		    AL.SourcePlay(handle);
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
