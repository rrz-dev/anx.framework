using System;
using System.IO;

// This file is part of the ANX.Framework and originally taken from
// the AC.AL OpenAL library, released under the MIT License.
// For details see: http://acal.codeplex.com/license

namespace WaveUtils
{
	public static class IEEEFloat
	{
		public static void ConvertToPcm(WaveInfo data, int resultChannelCount)
		{
			bool is64BitFloat = data.BitsPerSample == 64;

			using (BinaryReader sourceReader = new BinaryReader(new MemoryStream(data.Data)))
			{
				MemoryStream destStream = new MemoryStream();
				BinaryWriter destWriter = new BinaryWriter(destStream);

				int length = data.Data.Length / (is64BitFloat ? 8 : 4);

				int increment = 1;
				if (data.Channels == 2 && resultChannelCount == 1)
					increment = 2;

				data.Channels = resultChannelCount;
				data.ALFormat = data.Channels == 1 ? ALFormat.Mono16 : ALFormat.Stereo16;

				for (int index = 0; index < length; index += increment)
				{
					double value = is64BitFloat ? sourceReader.ReadDouble() : sourceReader.ReadSingle();
					destWriter.Write((short)(value * 32767));
				}

				data.Data = destStream.ToArray();
			}
		}
	}
}
