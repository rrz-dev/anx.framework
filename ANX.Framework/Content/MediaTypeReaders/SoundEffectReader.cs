using System;
using ANX.Framework.Audio;
using System.IO;
using System.Runtime.InteropServices;

namespace ANX.Framework.Content
{
	internal class SoundEffectReader : ContentTypeReader<SoundEffect>
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

		protected internal override SoundEffect Read(ContentReader input, SoundEffect existingInstance)
		{
			int formatCount = input.ReadInt32();
			byte[] header = input.ReadBytes(formatCount);

			WAVEFORMATEX headerStruct;
			unsafe
			{
				fixed(byte* ptr = &header[0])
					headerStruct = (WAVEFORMATEX)Marshal.PtrToStructure((IntPtr)ptr, typeof(WAVEFORMATEX));
			}

			int dataCount = input.ReadInt32();
			byte[] data = input.ReadBytes(dataCount);

			int loopStart = input.ReadInt32();
			int loopLength = input.ReadInt32();

			int num = input.ReadInt32();

			byte[] soundData = null;
			using (MemoryStream mStream = new MemoryStream(20 + header.Length + 8 + data.Length))
			{
				BinaryWriter writer = new BinaryWriter(mStream);
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
