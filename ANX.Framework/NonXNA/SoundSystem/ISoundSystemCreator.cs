using System;
using System.IO;
using ANX.Framework.Audio;
using System.Collections.ObjectModel;
using ANX.Framework.Media;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.SoundSystem
{
	public interface ISoundSystemCreator : ICreator
	{
	    float DistanceScale { get; set; }
	    float DopplerScale { get; set; }
	    float MasterVolume { get; set; }
	    float SpeedOfSound { get; set; }

	    IAudioListener CreateAudioListener();

		IAudioEmitter CreateAudioEmitter();

		ISoundEffect CreateSoundEffect(SoundEffect parent, Stream stream);

		ISoundEffect CreateSoundEffect(SoundEffect parent, byte[] buffer, int offset,
			int count, int sampleRate, AudioChannels channels, int loopStart,
			int loopLength);

		ISoundEffectInstance CreateSoundEffectInstance(ISoundEffect nativeSoundEffect);

		IMicrophone CreateMicrophone(Microphone managedMicrophone);

		ReadOnlyCollection<Microphone> GetAllMicrophones();

		int GetDefaultMicrophone(ReadOnlyCollection<Microphone> allMicrophones);

        ISong CreateSong(Song parentSong, Uri uri);
	}
}
