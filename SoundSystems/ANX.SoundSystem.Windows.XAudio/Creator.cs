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
	public class Creator : ISoundSystemCreator, IDisposable
    {
	    private float distanceScale;
	    private float dopplerScale;
        private float speedOfSound;
	    private XAudio2 device;
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
	        get { return OSInformation.IsWindows || OSInformation.GetName() == PlatformName.Windows8ModernUI; }
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
            get { return MasteringVoice != null ? MasteringVoice.Volume : 0f; }
	        set 
            {
                if (MasteringVoice != null)
                    MasteringVoice.SetVolume(value, 0);
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
        }

        public void Dispose()
        {
            if (MasteringVoice != null)
            {
                MasteringVoice.DestroyVoice();
                MasteringVoice.Dispose();
                MasteringVoice = null;
            }

            if (device != null)
            {
                device.Dispose();
                device = null;
            }
        }
	    #endregion

        internal void InitializeDevice()
        {
            if (device == null)
            {
                try
                {
                    device = new XAudio2(XAudio2Flags.None, ProcessorSpecifier.AnyProcessor);
                }
                catch (SharpDX.SharpDXException ex)
                {
                    if (ex.ResultCode == 0x80040154)
                    {
                        // couldn't initialize XAudio: "class not registered"
                        device = null;
                    }
                    else
                    {
                        throw ex;
                    }
                }

                if (device != null)
                {
                    MasteringVoice = new MasteringVoice(device, XAudio2.DefaultChannels, XAudio2.DefaultSampleRate);
                }

                AddInSystemFactory.Instance.PreventSystemChange(AddInType.SoundSystem);
            }
        }

		public IAudioListener CreateAudioListener()
        {
            InitializeDevice();

			throw new NotImplementedException();
		}

		public IAudioEmitter CreateAudioEmitter()
        {
            InitializeDevice();

			throw new NotImplementedException();
		}

		#region CreateSoundEffect
		public ISoundEffect CreateSoundEffect(SoundEffect parent, Stream stream)
		{
            InitializeDevice();

			return new XAudioSoundEffect(stream);
		}

		public ISoundEffect CreateSoundEffect(SoundEffect parent, byte[] buffer, int offset, int count, int sampleRate,
			AudioChannels channels, int loopStart, int loopLength)
		{
            InitializeDevice();

			return new XAudioSoundEffect(buffer, offset, count, sampleRate, channels, loopStart, loopLength);
		}
		#endregion

		#region CreateSoundEffectInstance
		public ISoundEffectInstance CreateSoundEffectInstance(ISoundEffect nativeSoundEffect)
		{
            InitializeDevice();

			return new XAudioSoundEffectInstance(device, nativeSoundEffect as XAudioSoundEffect);
		}
        #endregion

        #region CreateDynamicSoundEffectInstance
        public IDynamicSoundEffectInstance CreateDynamicSoundEffectInstance(int sampleRate, AudioChannels channels)
        {
            InitializeDevice();

            return new XAudioDynamicSoundEffectInstance(device, sampleRate, channels);
        }
        #endregion

		public IMicrophone CreateMicrophone(Microphone managedMicrophone)
        {
            InitializeDevice();

			throw new NotImplementedException();
		}

		public ReadOnlyCollection<Microphone> GetAllMicrophones()
        {
            InitializeDevice();

			throw new NotImplementedException();
		}

		public int GetDefaultMicrophone(ReadOnlyCollection<Microphone> allMicrophones)
        {
            InitializeDevice();

			throw new NotImplementedException();
		}

        #region CreateSong
        public ISong CreateSong(Song parentSong, Uri uri)
        {
            InitializeDevice();

            return new XAudioSong(device, uri);
        }

        public ISong CreateSong(Song parentSong, string filepath, int duration)
        {
            InitializeDevice();

            return new XAudioSong(device, filepath, duration);
        }
        #endregion
    }
}
