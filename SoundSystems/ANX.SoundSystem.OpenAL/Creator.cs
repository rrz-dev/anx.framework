using System;
using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.Media;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.SoundSystem;
using OpenTK;
using OpenTK.Audio.OpenAL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
	public class Creator : ISoundSystemCreator
	{
	    private float currentDistanceScale;
	    private float currentMasterVolume;

		#region Public
	    public string Name
	    {
	        get { return "OpenAL"; }
	    }

	    public int Priority
	    {
	        get { return 100; }
	    }

	    public bool IsSupported
		{
			get
			{
				PlatformName os = OSInformation.GetName();
				return OSInformation.IsWindows ||
					os == PlatformName.Linux ||
					os == PlatformName.MacOSX;
			}
		}

	    public float DistanceScale
	    {
	        get { return currentDistanceScale; }
	        set
	        {
	            currentDistanceScale = value;
	            // TODO: set actual property
	        }
	    }

	    public float DopplerScale
	    {
	        get { return AL.Get(ALGetFloat.DopplerFactor); }
	        set { AL.DopplerFactor(value); }
	    }

	    public float MasterVolume
	    {
	        get { return currentMasterVolume; }
	        set
	        {
	            currentMasterVolume = value;
	            // TODO: set actual property
	        }
	    }

	    public float SpeedOfSound
	    {
	        get { return AL.Get(ALGetFloat.SpeedOfSound); }
	        set { AL.SpeedOfSound(value); }
	    }
	    #endregion

		public Creator()
		{
		    currentDistanceScale = 1f;
		    currentMasterVolume = 1f;
			Init();
		}

        private static void Init()
        {
            ContextHandle context = Alc.GetCurrentContext();
            if (context.Handle == IntPtr.Zero)
            {
                string deviceName = Alc.GetString(IntPtr.Zero, AlcGetString.DefaultDeviceSpecifier);
                IntPtr deviceHandle = Alc.OpenDevice(deviceName);
                context = Alc.CreateContext(deviceHandle, new int[0]);
            }

            Alc.MakeContextCurrent(context);
        }

	    #region CreateSoundEffectInstance
		public ISoundEffectInstance CreateSoundEffectInstance(ISoundEffect nativeSoundEffect)
		{
			PreventSystemChange();
			return new OpenALSoundEffectInstance(nativeSoundEffect as OpenALSoundEffect);
		}
		#endregion

		#region CreateSoundEffect
		public ISoundEffect CreateSoundEffect(SoundEffect parent, Stream stream)
		{
			PreventSystemChange();
			return new OpenALSoundEffect(stream);
		}

		public ISoundEffect CreateSoundEffect(SoundEffect parent, byte[] buffer, int offset, int count, int sampleRate,
			AudioChannels channels, int loopStart, int loopLength)
		{
			PreventSystemChange();
			return new OpenALSoundEffect(buffer, offset, count, sampleRate, channels, loopStart, loopLength);
		}
        #endregion

        #region CreateDynamicSoundEffectInstance
        public IDynamicSoundEffectInstance CreateDynamicSoundEffectInstance(int sampleRate, AudioChannels channels)
        {
            PreventSystemChange();
            return new OpenALDynamicSoundEffectInstance(sampleRate, channels);
        }
        #endregion

		#region CreateAudioListener
		public IAudioListener CreateAudioListener()
		{
			PreventSystemChange();
			return new OpenALAudioListener();
		}
		#endregion

		#region CreateAudioEmitter (TODO)
		public IAudioEmitter CreateAudioEmitter()
		{
			PreventSystemChange();
			throw new NotImplementedException();
		}
		#endregion

		#region CreateMicrophone (TODO)
		public IMicrophone CreateMicrophone(Microphone managedMicrophone)
		{
			PreventSystemChange();
			throw new NotImplementedException();
		}
		#endregion

        #region GetAllMicrophones (TODO)
        public ReadOnlyCollection<Microphone> GetAllMicrophones()
		{
			PreventSystemChange();
			throw new NotImplementedException();
		}
		#endregion

        #region GetDefaultMicrophone (TODO)
        public int GetDefaultMicrophone(ReadOnlyCollection<Microphone> allMicrophones)
		{
			PreventSystemChange();
			throw new NotImplementedException();
		}
        #endregion

        #region CreateSong
        public ISong CreateSong(Song parentSong, Uri uri)
        {
            PreventSystemChange();
            return new OpenALSong(uri);
        }

        public ISong CreateSong(Song parentSong, string filepath, int duration)
        {
            PreventSystemChange();
            return new OpenALSong(filepath, duration);
        }
        #endregion

		private static void PreventSystemChange()
		{
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.SoundSystem);
		}
	}
}
