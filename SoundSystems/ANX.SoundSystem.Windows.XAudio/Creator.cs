using System;
using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework.Audio;
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
		private MasteringVoice masteringVoice;

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

		public float DistanceScale
		{
			get
			{
				return 1f;
				//throw new NotImplementedException();
			}
			set
			{
				//throw new NotImplementedException();
			}
		}

		public float DopplerScale
		{
			get
			{
				return 1f;
				//throw new NotImplementedException();
			}
			set
			{
				//throw new NotImplementedException();
			}
		}

		public float MasterVolume
		{
			get
			{
				return masteringVoice.Volume;
				//throw new NotImplementedException();
			}
			set
			{
				masteringVoice.SetVolume(value, 0);
				//throw new NotImplementedException();
			}
		}

		public float SpeedOfSound
		{
			get
			{
				return 1f;
				//throw new NotImplementedException();
			}
			set
			{
				//throw new NotImplementedException();
			}
		}
		#endregion

		#region Constructor
		public Creator()
		{
			device = new XAudio2();
			masteringVoice = new MasteringVoice(device);
		}

		~Creator()
		{
			if (masteringVoice != null)
			{
				masteringVoice.Dispose();
				masteringVoice = null;
			}

			if (device != null)
			{
				device.Dispose();
				device = null;
			}
		}
		#endregion

		public IAudioListener CreateAudioListener()
		{
			throw new NotImplementedException();
		}

		public IAudioEmitter CreateAudioEmitter()
		{
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
			throw new NotImplementedException();
		}

		public ReadOnlyCollection<Microphone> GetAllMicrophones()
		{
			throw new NotImplementedException();
		}

		public int GetDefaultMicrophone(ReadOnlyCollection<Microphone> allMicrophones)
		{
			throw new NotImplementedException();
		}

		private void PreventSystemChange()
		{
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.SoundSystem);
		}
	}
}
