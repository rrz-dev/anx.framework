using ANX.Framework.Audio;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.SoundSystem;
using System;
using System.Collections.ObjectModel;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Windows.ModernUI
{
    class Creator : ISoundSystemCreator
    {
        #region Public
        #region Name
        public string Name
        {
            get
            {
                return "Sound.Windows.ModernUI";
            }
        }
        #endregion

        #region Priority
        public int Priority
        {
            get
            {
                return 100;
            }
        }
        #endregion

        #region IsSupported
        public bool IsSupported
        {
            get
            {
                return OSInformation.GetName() == PlatformName.Windows8;
            }
        }
        #endregion

        #region DistanceScale
        public float DistanceScale
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

        #region DopplerScale
        public float DopplerScale
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

        #region MasterVolume
        public float MasterVolume
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

        #region SpeedOfSound
        public float SpeedOfSound
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
        #endregion

        #region CreateSoundEffectInstance
        public ISoundEffectInstance CreateSoundEffectInstance(
            ISoundEffect nativeSoundEffect)
        {
            AddInSystemFactory.Instance.PreventSystemChange(AddInType.SoundSystem);
            throw new NotImplementedException();
        }
        #endregion

        #region CreateSoundEffect
        public ISoundEffect CreateSoundEffect(SoundEffect parent, Stream stream)
        {
            AddInSystemFactory.Instance.PreventSystemChange(AddInType.SoundSystem);
            throw new NotImplementedException();
        }
        #endregion

        #region CreateSoundEffect (TODO)
        public ISoundEffect CreateSoundEffect(SoundEffect parent, byte[] buffer,
            int offset, int count, int sampleRate, AudioChannels channels,
            int loopStart, int loopLength)
        {
            AddInSystemFactory.Instance.PreventSystemChange(AddInType.SoundSystem);
            throw new NotImplementedException();
        }
        #endregion

        #region CreateAudioListener
        public IAudioListener CreateAudioListener()
        {
            AddInSystemFactory.Instance.PreventSystemChange(AddInType.SoundSystem);
            throw new NotImplementedException();
        }
        #endregion

        #region CreateAudioEmitter (TODO)
        public IAudioEmitter CreateAudioEmitter()
        {
            AddInSystemFactory.Instance.PreventSystemChange(AddInType.SoundSystem);
            throw new NotImplementedException();
        }
        #endregion

        #region CreateMicrophone
        public IMicrophone CreateMicrophone(Microphone managedMicrophone)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetAllMicrophones
        public ReadOnlyCollection<Microphone> GetAllMicrophones()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetDefaultMicrophone
        public int GetDefaultMicrophone(ReadOnlyCollection<Microphone> allMicrophones)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
