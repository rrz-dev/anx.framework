using System;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.SoundSystem;
using OpenTK.Audio.OpenAL;
using WaveUtils;
using ALFormat = OpenTK.Audio.OpenAL.ALFormat;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
    [Developer("AstrorEnales")]
	public class OpenALSoundEffect : ISoundEffect
	{
		#region Private
		private WaveInfo waveInfo;
		private TimeSpan duration;
        internal int BufferHandle { get; private set; }
		#endregion

		#region Public
	    public TimeSpan Duration
	    {
	        get { return duration; }
	    }
	    #endregion

		#region Constructor
		internal OpenALSoundEffect(Stream stream)
		{
			CreateFromStream(stream);
		}

		internal OpenALSoundEffect(byte[] buffer, int offset, int count, int sampleRate, AudioChannels channels,
            int loopStart, int loopLength)
		{
            // TODO: loopStart and loopLength

            byte[] subBuffer = new byte[count];
            Array.Copy(buffer, offset, subBuffer, 0, count);
            BufferHandle = AL.GenBuffer();

            // TODO: evaluate if 8bit or 16bit!!
		    ALFormat format = channels == AudioChannels.Mono ? ALFormat.Mono8 : ALFormat.Stereo8;
            AL.BufferData(BufferHandle, format, subBuffer, count, sampleRate);

            float sizeMulBlockAlign = count / ((int)channels * 2f);
            duration = TimeSpan.FromMilliseconds(sizeMulBlockAlign * 1000f / sampleRate);

            ALError error = AL.GetError();
            if (error != ALError.NoError)
                throw new Exception("OpenAL error " + error + ": " + AL.GetErrorString(error));
		}
		#endregion

		#region CreateFromStream
		private void CreateFromStream(Stream stream)
		{
			waveInfo = WaveFile.LoadData(stream);
			if (waveInfo.WaveFormat != WaveFormat.PCM)
			{
				var converter = new WaveConverter(waveInfo);
				converter.ConvertToPcm();
			}

			duration = waveInfo.CalculateDuration();

			BufferHandle = AL.GenBuffer();
			AL.BufferData(BufferHandle, (ALFormat)waveInfo.ALFormat, waveInfo.Data, waveInfo.Data.Length, waveInfo.SampleRate);

			ALError error = AL.GetError();
			if (error != ALError.NoError)
				throw new Exception("OpenAL error " + error + ": " + AL.GetErrorString(error));
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			waveInfo = null;
			AL.DeleteBuffer(BufferHandle);
		}
		#endregion
	}
}
