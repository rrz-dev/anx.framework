using System;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.SoundSystem;
using SharpDX.XAudio2;
using SharpDX.Multimedia;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Windows.XAudio
{
	public class XAudioSoundEffect : ISoundEffect
	{
		#region Private
		internal SoundEffect parent;
		private TimeSpan duration;
		internal WaveFormat waveFormat;
		internal AudioBuffer audioBuffer;
		internal uint[] DecodedPacketsInfo;
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
		internal XAudioSoundEffect(SoundEffect setParent, Stream stream)
		{
			parent = setParent;
			CreateFromStream(stream);
		}

		internal XAudioSoundEffect(SoundEffect setParent, byte[] buffer, int offset, int count, int sampleRate,
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

		~XAudioSoundEffect()
		{
			Dispose();
		}
		#endregion

		#region CreateFromStream
		private void CreateFromStream(Stream stream)
		{
			var soundStream = new SoundStream(stream);
			waveFormat = soundStream.Format;
			audioBuffer = new AudioBuffer
			{
				Stream = soundStream.ToDataStream(),
				AudioBytes = (int)stream.Length,
				Flags = BufferFlags.EndOfStream
			};

			float sizeMulBlockAlign = soundStream.Length / (waveFormat.Channels * 2);
			duration = TimeSpan.FromMilliseconds((double)(sizeMulBlockAlign * 1000f / (float)waveFormat.SampleRate));

			DecodedPacketsInfo = soundStream.DecodedPacketsInfo;

			soundStream.Dispose();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			waveFormat = null;
			audioBuffer = null;
		}
		#endregion
	}
}
