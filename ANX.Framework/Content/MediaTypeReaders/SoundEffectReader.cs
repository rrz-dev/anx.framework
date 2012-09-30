using System;
using System.IO;
using System.Runtime.InteropServices;
using ANX.Framework.Audio;

namespace ANX.Framework.Content
{
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

			byte[] soundData;
			using (var mStream = new MemoryStream(20 + header.Length + 8 + data.Length))
			{
				var writer = new BinaryWriter(mStream);
				writer.Write("RIFF".ToCharArray());
				writer.Write(20 + header.Length + data.Length);
				writer.Write("WAVE".ToCharArray());

				writer.Write("fmt ".ToCharArray());
				writer.Write(header.Length);
				writer.Write(header);

				writer.Write("data".ToCharArray());
				writer.Write(data.Length);
				writer.Write(data);

				soundData = mStream.ToArray();
			}

			return new SoundEffect(soundData, 0, soundData.Length, headerStruct.nSamplesPerSec,
				(AudioChannels)headerStruct.nChannels, loopStart, loopLength);
		}
	}
}
