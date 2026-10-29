#region Using Statements
using System;
using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.Media;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.SoundSystem;
using System.Collections.Generic;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Null
{
	public class Creator : ISoundSystemCreator
    {
        private IAudioListener audioListener = new NullAudioListener();
        private IAudioEmitter audioEmitter = new NullAudioEmitter();

		#region Public
		public string Name
		{
			get { return "Null"; }
		}

		public int Priority
		{
			get { return 10; }
		}

	    public bool IsSupported
	    {
	        get 
            {
                return true; 
            }
	    }

	    public float DistanceScale
        {
            get;
            set;
		}

		public float DopplerScale
        {
            get;
            set;
		}

	    public float MasterVolume
	    {
            get;
	        set; 
	    }

        public float SpeedOfSound
        {
            get;
            set;
        }
		#endregion

        public Creator()
        {
            DistanceScale = 1f;
            DopplerScale = 1f;
            SpeedOfSound = 343.5f;
        }

		public IAudioListener CreateAudioListener()
        {
            return audioListener;
        }

		public IAudioEmitter CreateAudioEmitter()
        {
            return audioEmitter;
        }

		public ISoundEffect CreateSoundEffect(SoundEffect parent, Stream stream)
		{
            return new NullSoundEffect();
        }

		public ISoundEffect CreateSoundEffect(SoundEffect parent, byte[] buffer, int offset, int count, int sampleRate,
			AudioChannels channels, int loopStart, int loopLength)
		{
            return new NullSoundEffect();
        }

		public ISoundEffectInstance CreateSoundEffectInstance(ISoundEffect nativeSoundEffect)
		{
            return new NullSoundEffectInstance();
        }

        public IDynamicSoundEffectInstance CreateDynamicSoundEffectInstance(int sampleRate, AudioChannels channels)
        {
            return new NullDynamicSoundEffectInstance();
        }

		public IMicrophone CreateMicrophone(Microphone managedMicrophone)
        {
            return new NullMicrophone();
        }

		public ReadOnlyCollection<Microphone> GetAllMicrophones()
        {
            return new ReadOnlyCollection<Microphone>(new List<Microphone>());
        }

		public int GetDefaultMicrophone(ReadOnlyCollection<Microphone> allMicrophones)
        {
            return 0;
        }

        public ISong CreateSong(Song parentSong, Uri uri)
        {
            return new NullSong();
        }

        public ISong CreateSong(Song parentSong, string filepath, int duration)
        {
            return new NullSong();
        }
    }
}
