using System;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.SoundSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Windows.XAudio
{
	public class Creator : ISoundSystemCreator
	{
		#region Public
		#region Name
		public string Name
		{
			get { return "XAudio"; }
		}
		#endregion

		#region Priority
		public int Priority
		{
			get { return 10; }
		}
		#endregion

		#region IsSupported
		public bool IsSupported
		{
			get
			{
				//TODO: this is just a very basic version of test for support
				return OSInformation.IsWindows;
			}
		}
		#endregion
		#endregion

		#region RegisterCreator
		public void RegisterCreator(AddInSystemFactory factory)
		{
			factory.AddCreator(this);
		}
		#endregion

		#region ISoundSystemCreator Member

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

		public IAudioListener CreateAudioListener()
		{
			throw new NotImplementedException();
		}

		public IAudioEmitter CreateAudioEmitter()
		{
			throw new NotImplementedException();
		}

		public ISoundEffect CreateSoundEffect(SoundEffect parent, Stream stream)
		{
			throw new NotImplementedException();
		}

		public ISoundEffect CreateSoundEffect(SoundEffect parent, byte[] buffer, int offset, int count, int sampleRate, AudioChannels channels, int loopStart, int loopLength)
		{
			throw new NotImplementedException();
		}

		public ISoundEffectInstance CreateSoundEffectInstance(ISoundEffect nativeSoundEffect)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region ISoundSystemCreator Member


		public IMicrophone CreateMicrophone(Microphone managedMicrophone)
		{
			throw new NotImplementedException();
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Microphone> GetAllMicrophones()
		{
			throw new NotImplementedException();
		}

		public int GetDefaultMicrophone(System.Collections.ObjectModel.ReadOnlyCollection<Microphone> allMicrophones)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
