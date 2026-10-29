using System;
using System.Collections.ObjectModel;
using WaveUtils;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Audio
{
    public class AudioContent : ContentItem, IDisposable
    {
        public ReadOnlyCollection<byte> Data { get; internal set; }
        public TimeSpan Duration { get; internal set; }
        [ContentSerializer]
        public string FileName { get; internal set; }
        public AudioFileType FileType { get; internal set; }
        public AudioFormat Format { get; internal set; }
        public int LoopLength { get; internal set; }
        public int LoopStart { get; internal set; }

		internal WaveInfo LoadedWaveData { get; private set; }

		internal AudioContent(WaveInfo setLoadedWaveData)
		{
			LoadedWaveData = setLoadedWaveData;
		}

        public void ConvertFormat(ConversionFormat formatType, ConversionQuality quality, string targetFileName)
        {
			var converter = new WaveConverter(LoadedWaveData);

			int resultChannelCount = LoadedWaveData.Channels;
			// Make mono on mobile devices which is totally enough.
			if (quality == ConversionQuality.Low)
				resultChannelCount = 1;

			if (formatType == ConversionFormat.Pcm)
				converter.ConvertToPcm(resultChannelCount);
			else
				throw new NotImplementedException();

			Data = new ReadOnlyCollection<byte>(LoadedWaveData.Data);
        }

        public void Dispose()
        {
        }
    }
}
