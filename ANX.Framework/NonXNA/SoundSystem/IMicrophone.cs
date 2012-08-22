using System;
using ANX.Framework.Audio;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.SoundSystem
{
	public interface IMicrophone : IDisposable
	{
		MicrophoneState State { get; }
		int SampleRate { get; }
		bool IsHeadset { get; }
		TimeSpan BufferDuration { get; set; }

		void Stop();
		void Start();

		int GetSampleSizeInBytes(ref TimeSpan duration);
		TimeSpan GetSampleDuration(int sizeInBytes);

		int GetData(byte[] buffer);
		int GetData(byte[] buffer, int offset, int count);
	}
}
