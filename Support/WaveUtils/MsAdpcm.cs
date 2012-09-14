using System;
using System.IO;

// This file is part of the ANX.Framework and originally taken from
// the AC.AL OpenAL library, released under the MIT License.
// For details see: http://acal.codeplex.com/license

namespace WaveUtils
{
	/// <summary>
	/// http://wiki.multimedia.cx/index.php?title=Microsoft_ADPCM
	/// <para />
	/// http://dslinux.gits.kiev.ua/trunk/lib/audiofile/src/libaudiofile/modules/msadpcm.c
	/// <para />
	/// http://netghost.narod.ru/gff/vendspec/micriff/ms_riff.txt
	/// </summary>
	internal static class MsAdpcm
	{
		#region Decoding Tables
		private static readonly int[] AdaptationTable =
		{
			230, 230, 230, 230, 307, 409, 512, 614,
			768, 614, 512, 409, 307, 230, 230, 230
		};

		private static readonly int[] AdaptCoeff1 =
		{
			256, 512, 0, 192, 240, 460, 392
		};

		private static readonly int[] AdaptCoeff2 =
		{
			0, -256, 0, 64, 0, -208, -232
		};
		#endregion

		#region StateObject (helper class)
		private class StateObject
		{
			/// <summary>
			/// Indices in the aCoef array to define the predictor
			/// used to encode this block.
			/// </summary>
			public byte predicator;
			/// <summary>
			/// Initial Delta value to use.
			/// </summary>
			public short delta;
			/// <summary>
			/// The second sample value of the block. When decoding this will be
			/// used as the previous sample to start decoding with.
			/// </summary>
			public short sample1;
			/// <summary>
			/// The first sample value of the block. When decoding this will be
			/// used as the previous' previous sample to start decoding with.
			/// </summary>
			public short sample2;
		}
		#endregion
		
		#region ConvertToPcm
		public static void ConvertToPcm(WaveInfo info)
		{
			BinaryReader reader = new BinaryReader(new MemoryStream(info.Data));
			MemoryStream result = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(result);

			StateObject first = new StateObject();
			StateObject second = info.Channels > 1 ? new StateObject() : first;
			StateObject[] states = { first, second };

			int numSamples = (info.ExtSamplesPerBlock - 2) * info.Channels;
			int numberOfBlocks = info.Data.Length / info.BlockAlign;
			for (int blockIndex = 0; blockIndex < numberOfBlocks; blockIndex++)
			{
				for (int index = 0; index < info.Channels; index++)
				{
					states[index].predicator = reader.ReadByte();
				}
				for (int index = 0; index < info.Channels; index++)
				{
					states[index].delta = reader.ReadInt16();
				}
				for (int index = 0; index < info.Channels; index++)
				{
					states[index].sample1 = reader.ReadInt16();
				}
				for (int index = 0; index < info.Channels; index++)
				{
					states[index].sample2 = reader.ReadInt16();
					// Write first samples directly from preamble
					writer.Write(states[index].sample2);
				}

				// Write first samples directly from preamble
				for (int index = 0; index < info.Channels; index++)
				{
					writer.Write(states[index].sample1);
				}

				// We decode the samples two at a time
				for (int index = 0; index < numSamples; index += 2)
				{
					byte code = reader.ReadByte();
					DecodeSample(first, code >> 4, writer);
					DecodeSample(second, code & 0x0f, writer);
				}
			}

			reader.Close();
			writer.Close();
			info.Data = result.ToArray();
			result.Dispose();
		}
		#endregion

		#region DecodeSample
		private static void DecodeSample(StateObject state, int code,
			BinaryWriter writer)
		{
			int linearSample =
				((state.sample1 * AdaptCoeff1[state.predicator]) +
				(state.sample2 * AdaptCoeff2[state.predicator])) / 256;

			linearSample = linearSample + (state.delta *
				((code & 0x08) == 0x08 ? (code - 0x10) : code));

			state.sample2 = state.sample1;
			// clamp predictor within signed 16-bit range
			state.sample1 = (short)Math.Min(short.MaxValue,
				Math.Max(short.MinValue, linearSample));

			state.delta = (short)Math.Max(
				(state.delta * AdaptationTable[code]) / 256, 16);

			writer.Write(state.sample1);
		}
		#endregion
	}
}
