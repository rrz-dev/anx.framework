using System;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.SoundSystem;
using SharpDX;
using SharpDX.Multimedia;
using SharpDX.XAudio2;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Windows.XAudio
{
    [Developer("AstrorEnales")]
	public class XAudioSoundEffect : ISoundEffect
	{
		#region Private
		private TimeSpan duration;
		internal WaveFormat WaveFormat;
		internal AudioBuffer AudioBuffer;
		internal uint[] DecodedPacketsInfo;
		#endregion

		#region Public
	    public TimeSpan Duration
	    {
	        get { return duration; }
	    }
	    #endregion

		#region Constructor
		internal XAudioSoundEffect(Stream stream)
		{
			CreateFromStream(stream);
		}

		internal XAudioSoundEffect(byte[] buffer, int offset, int count, int sampleRate, AudioChannels channels,
            int loopStart, int loopLength)
        {
            WaveFormat = new WaveFormat(sampleRate, (int)channels);
		    AudioBuffer = new AudioBuffer
		    {
                LoopBegin = loopStart,
                LoopLength = loopLength,
                AudioBytes = count,
		        Flags = BufferFlags.EndOfStream
		    };
		    
            IntPtr handle;
		    unsafe
		    {
                fixed (byte* ptr = &buffer[0])
                    handle = (IntPtr)(ptr + offset);
		    }
            AudioBuffer.Stream = new DataStream(handle, count, false, false);

            float sizeMulBlockAlign = (float)count / (WaveFormat.Channels * 2);
            duration = TimeSpan.FromMilliseconds(sizeMulBlockAlign * 1000f / WaveFormat.SampleRate);
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
			WaveFormat = soundStream.Format;
			AudioBuffer = new AudioBuffer
			{
				Stream = soundStream.ToDataStream(),
				AudioBytes = (int)stream.Length,
				Flags = BufferFlags.EndOfStream
			};

			float sizeMulBlockAlign = (float)soundStream.Length / (WaveFormat.Channels * 2);
			duration = TimeSpan.FromMilliseconds(sizeMulBlockAlign * 1000f / WaveFormat.SampleRate);

			DecodedPacketsInfo = soundStream.DecodedPacketsInfo;

			soundStream.Dispose();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			WaveFormat = null;
			AudioBuffer = null;
		}
		#endregion
	}
}
