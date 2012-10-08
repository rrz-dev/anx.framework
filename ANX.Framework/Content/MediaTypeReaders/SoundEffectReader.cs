using System;
using System.Runtime.InteropServices;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.InProgress)]
	internal class SoundEffectReader : ContentTypeReader<SoundEffect>
	{
		private struct WaveFormatEx
		{
			public ushort wFormatTag;
			public ushort nChannels;
			public int nSamplesPerSec;
			public int nAvgBytesPerSec;
			public ushort nBlockAlign;
			public ushort wBitsPerSample;
			public ushort cbSize;
		}

		protected internal override SoundEffect Read(ContentReader input, SoundEffect existingInstance)
		{
			int formatCount = input.ReadInt32();
			byte[] header = input.ReadBytes(formatCount);

			WaveFormatEx headerStruct;
			unsafe
			{
				fixed(byte* ptr = &header[0])
					headerStruct = (WaveFormatEx)Marshal.PtrToStructure((IntPtr)ptr, typeof(WaveFormatEx));
			}

			int dataCount = input.ReadInt32();
			byte[] data = input.ReadBytes(dataCount);
			int loopStart = input.ReadInt32();
			int loopLength = input.ReadInt32();
			int duration = input.ReadInt32();

            return new SoundEffect(data, 0, data.Length, headerStruct.nSamplesPerSec, (AudioChannels)headerStruct.nChannels,
                loopStart, loopLength);
		}
	}
}
