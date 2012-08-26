using System;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.SoundSystem;
using OpenTK.Audio;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
	public class OpenALSoundEffect : ISoundEffect
	{
		#region Private
		internal SoundEffect parent;

		private WaveInfo waveInfo;

		private TimeSpan duration;

		internal int bufferHandle;
		#endregion

		#region Public
		public TimeSpan Duration
		{
			get
			{
				return duration;
			}
		}
		#endregion

		#region Constructor
		internal OpenALSoundEffect(SoundEffect setParent, Stream stream)
		{
			parent = setParent;

			waveInfo = WaveFile.LoadData(stream);

			float sizeMulBlockAlign = waveInfo.Data.Length / ((int)waveInfo.Channels * 2);
			duration = TimeSpan.FromMilliseconds((double)(sizeMulBlockAlign * 1000f / (float)waveInfo.SampleRate));

			bufferHandle = AL.GenBuffer();
			AL.BufferData(bufferHandle, waveInfo.OpenALFormat, waveInfo.Data, waveInfo.Data.Length, waveInfo.SampleRate);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			waveInfo = null;

			AL.DeleteBuffer(bufferHandle);
		}
		#endregion
	}
}
