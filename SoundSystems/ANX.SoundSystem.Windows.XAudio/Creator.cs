using System;
using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.Media;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.SoundSystem;
using SharpDX.XAudio2;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Windows.XAudio
{
	public class Creator : ISoundSystemCreator
    {
		private XAudio2 device;
	    private float distanceScale;
	    private float dopplerScale;
        private float speedOfSound;
        internal static MasteringVoice MasteringVoice { get; private set; }

		#region Public
		public string Name
		{
			get { return "XAudio"; }
		}

		public int Priority
		{
			get { return 10; }
		}

	    public bool IsSupported
	    {
	        get { return OSInformation.IsWindows; }
	    }

	    public float DistanceScale
        {
            get { return distanceScale; }
            set
            {
                distanceScale = value;
                // TODO: actually set the parameter to XAudio
            }
		}

		public float DopplerScale
        {
            get { return dopplerScale; }
            set
            {
                dopplerScale = value;
                // TODO: actually set the parameter to XAudio
            }
		}

	    public float MasterVolume
	    {
	        get 
            {
                if (MasteringVoice != null)
                {
                    return MasteringVoice.Volume;
                }

                return 0.0f;
            }
	        set 
            {
                if (MasteringVoice != null)
                {
                    MasteringVoice.SetVolume(value, 0);
                }
            }
	    }

	    public float SpeedOfSound
		{
			get { return speedOfSound; }
            set
            {
                speedOfSound = value;
                // TODO: actually set the parameter to XAudio
            }
		}
		#endregion

		#region Constructor
		public Creator()
        {
		    distanceScale = 1f;
            dopplerScale = 1f;
            speedOfSound = 343.5f;
            try
            {
                device = new XAudio2();
            }
            catch (Exception ex)
            {
                device = null;
                //TODO: error handling
                System.Diagnostics.Debugger.Break();
            }
            if (device != null)
            {
                MasteringVoice = new MasteringVoice(device, XAudio2.DefaultChannels, XAudio2.DefaultSampleRate);
            }
        }

        ~Creator()
        {
            if (MasteringVoice != null)
            {
                MasteringVoice.DestroyVoice();
                MasteringVoice.Dispose();
            }

            if (device != null)
                device.Dispose();

            MasteringVoice = null;
            device = null;
        }
	    #endregion

		public IAudioListener CreateAudioListener()
        {
            PreventSystemChange();
			throw new NotImplementedException();
		}

		public IAudioEmitter CreateAudioEmitter()
        {
            PreventSystemChange();
			throw new NotImplementedException();
		}

		#region CreateSoundEffect
		public ISoundEffect CreateSoundEffect(SoundEffect parent, Stream stream)
		{
			PreventSystemChange();
			return new XAudioSoundEffect(parent, stream);
		}

		public ISoundEffect CreateSoundEffect(SoundEffect parent, byte[] buffer, int offset, int count, int sampleRate,
			AudioChannels channels, int loopStart, int loopLength)
		{
			PreventSystemChange();
			return new XAudioSoundEffect(parent, buffer, offset, count, sampleRate, channels, loopStart, loopLength);
		}
		#endregion

		#region CreateSoundEffectInstance
		public ISoundEffectInstance CreateSoundEffectInstance(ISoundEffect nativeSoundEffect)
		{
			PreventSystemChange();
			return new XAudioSoundEffectInstance(device, nativeSoundEffect as XAudioSoundEffect);
		}
		#endregion

		public IMicrophone CreateMicrophone(Microphone managedMicrophone)
        {
            PreventSystemChange();
			throw new NotImplementedException();
		}

		public ReadOnlyCollection<Microphone> GetAllMicrophones()
        {
            PreventSystemChange();
			throw new NotImplementedException();
		}

		public int GetDefaultMicrophone(ReadOnlyCollection<Microphone> allMicrophones)
        {
            PreventSystemChange();
			throw new NotImplementedException();
		}

        public ISong CreateSong(Song parentSong, Uri uri)
        {
            PreventSystemChange();
            throw new NotImplementedException();
        }

        public IDynamicSoundEffectInstance CreateDynamicSoundEffectInstance()
        {
            PreventSystemChange();
            throw new NotImplementedException();
        }

		private static void PreventSystemChange()
		{
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.SoundSystem);
		}
    }
}
