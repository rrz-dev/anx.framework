using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using ANX.Framework.Content.Pipeline.Audio;
using WaveUtils;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor(DisplayName = "Sound Effect - ANX Framework")]
    public class SoundEffectProcessor : ContentProcessor<AudioContent, SoundEffectContent>
    {
        private struct WAVEFORMATEX
        {
            public ushort wFormatTag;
            public ushort nChannels;
            public int nSamplesPerSec;
            public int nAvgBytesPerSec;
            public ushort nBlockAlign;
            public ushort wBitsPerSample;
            public ushort cbSize;
        }

        [DefaultValue(ConversionQuality.Best)]
        public ConversionQuality Quality { get; set; }

        public SoundEffectProcessor()
        {
            Quality = ConversionQuality.Best;
        }

        public override SoundEffectContent Process(AudioContent input, ContentProcessorContext context)
        {
            input.ConvertFormat(ConversionFormat.Pcm, Quality, null);

            var waveHeader = new WAVEFORMATEX()
            {
                wFormatTag = (ushort)WaveFormat.PCM,
                nChannels = (ushort)input.LoadedWaveData.Channels,
                nSamplesPerSec = input.LoadedWaveData.SampleRate,
                nAvgBytesPerSec = 0, // TODO
                nBlockAlign = (ushort)input.LoadedWaveData.BlockAlign,
                wBitsPerSample = (ushort)input.LoadedWaveData.BitsPerSample,
                cbSize = 0, // TODO
            };

            int length = Marshal.SizeOf(waveHeader.GetType());
            byte[] array = new byte[length];
            
            IntPtr ptr = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(waveHeader, ptr, true);
            Marshal.Copy(ptr, array, 0, length);
            Marshal.FreeHGlobal(ptr);

            return new SoundEffectContent(new List<byte>(array), new List<byte>(input.Data), input.LoopStart, input.LoopLength,
                (int)input.Duration.TotalMilliseconds);
        }
    }
}
