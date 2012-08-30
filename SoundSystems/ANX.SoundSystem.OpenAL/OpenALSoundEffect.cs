using System;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.SoundSystem;
using OpenTK.Audio.OpenAL;
using WaveUtils;
using ALFormat = OpenTK.Audio.OpenAL.ALFormat;

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
			CreateFromStream(stream);
		}

		internal OpenALSoundEffect(SoundEffect setParent, byte[] buffer, int offset, int count, int sampleRate,
			AudioChannels channels, int loopStart, int loopLength)
		{
			parent = setParent;

			using (MemoryStream stream = new MemoryStream())
			{
				BinaryWriter writer = new BinaryWriter(stream);
				writer.Write(buffer, offset, count);
				stream.Position = 0;

				CreateFromStream(stream);
			}
		}
		#endregion

		#region CreateFromStream
		private void CreateFromStream(Stream stream)
		{
			waveInfo = WaveFile.LoadData(stream);
			if (waveInfo.WaveFormat != WaveFormat.PCM)
			{
				WaveConverter converter = new WaveConverter(waveInfo);
				converter.ConvertToPcm();
			}

			duration = waveInfo.CalculateDuration();

			bufferHandle = AL.GenBuffer();
			AL.BufferData(bufferHandle, (ALFormat)waveInfo.ALFormat, waveInfo.Data, waveInfo.Data.Length, waveInfo.SampleRate);

			ALError error = AL.GetError();
			if (error != ALError.NoError)
				throw new Exception("OpenAL error " + error + ": " + AL.GetErrorString(error));
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
