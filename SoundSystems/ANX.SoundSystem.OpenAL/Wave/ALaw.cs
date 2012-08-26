using System;
using System.IO;
using OpenTK.Audio;

// This file is part of the ANX.Framework and originally taken from
// the AC.AL OpenAL library, released under the MIT License.
// For details see: http://acal.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
	/// <summary>
	/// http://www.threejacks.com/?q=node/176
	/// </summary>
	internal static class ALaw
	{
		#region Decode Table
		private static readonly short[] DecodeTable =
		{
			-5504, -5248, -6016, -5760, -4480, -4224, -4992, -4736,
			-7552, -7296, -8064, -7808, -6528, -6272, -7040, -6784,
			-2752, -2624, -3008, -2880, -2240, -2112, -2496, -2368,
			-3776, -3648, -4032, -3904, -3264, -3136, -3520, -3392,
			-22016,-20992,-24064,-23040,-17920,-16896,-19968,-18944,
			-30208,-29184,-32256,-31232,-26112,-25088,-28160,-27136,
			-11008,-10496,-12032,-11520,-8960, -8448, -9984, -9472,
			-15104,-14592,-16128,-15616,-13056,-12544,-14080,-13568,
			-344,  -328,  -376,  -360,  -280,  -264,  -312,  -296,
			-472,  -456,  -504,  -488,  -408,  -392,  -440,  -424,
			-88,   -72,   -120,  -104,  -24,   -8,    -56,   -40,
			-216,  -200,  -248,  -232,  -152,  -136,  -184,  -168,
			-1376, -1312, -1504, -1440, -1120, -1056, -1248, -1184,
			-1888, -1824, -2016, -1952, -1632, -1568, -1760, -1696,
			-688,  -656,  -752,  -720,  -560,  -528,  -624,  -592,
			-944,  -912,  -1008, -976,  -816,  -784,  -880,  -848,
			 5504,  5248,  6016,  5760,  4480,  4224,  4992,  4736,
			 7552,  7296,  8064,  7808,  6528,  6272,  7040,  6784,
			 2752,  2624,  3008,  2880,  2240,  2112,  2496,  2368,
			 3776,  3648,  4032,  3904,  3264,  3136,  3520,  3392,
			 22016, 20992, 24064, 23040, 17920, 16896, 19968, 18944,
			 30208, 29184, 32256, 31232, 26112, 25088, 28160, 27136,
			 11008, 10496, 12032, 11520, 8960,  8448,  9984,  9472,
			 15104, 14592, 16128, 15616, 13056, 12544, 14080, 13568,
			 344,   328,   376,   360,   280,   264,   312,   296,
			 472,   456,   504,   488,   408,   392,   440,   424,
			 88,    72,   120,   104,    24,     8,    56,    40,
			 216,   200,   248,   232,   152,   136,   184,   168,
			 1376,  1312,  1504,  1440,  1120,  1056,  1248,  1184,
			 1888,  1824,  2016,  1952,  1632,  1568,  1760,  1696,
			 688,   656,   752,   720,   560,   528,   624,   592,
			 944,   912,  1008,   976,   816,   784,   880,   848
		};
		#endregion

		#region Encode Table
		private static readonly byte[] EncodeTable =
		{
			1, 1, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 4, 4, 4, 4,
			5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5,
			6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6,
			6, 6, 6, 6, 6, 6, 6, 6,
			7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7,
			7, 7, 7, 7, 7, 7, 7, 7
		};
		#endregion

		#region ConvertToALaw (TODO)
		private static void ConvertToALaw(WaveInfo info)
		{
			//int sign;
			//int exponent;
			//int mantissa;
			//unsigned char compressedByte;

			//sign = ((~sample) >> 8) & 0x80;
			//if (!sign)
			//     sample = (short)-sample;
			//if (sample > cClip)
			//     sample = cClip;
			//if (sample >= 256)
			//{
			//     exponent = (int)ALawCompressTable[(sample >> 8) & 0x7F];
			//     mantissa = (sample >> (exponent + 3) ) & 0x0F;
			//     compressedByte = ((exponent << 4) | mantissa);
			//}
			//else
			//{
			//     compressedByte = (unsigned char)(sample >> 4);
			//}
			//compressedByte ^= (sign ^ 0x55);
			//return compressedByte;
		}
		#endregion

		#region ConvertToPcm
		public static void ConvertToPcm(WaveInfo info)
		{
			info.OpenALFormat = info.Channels == 1 ?
				ALFormat.Mono16 :
				ALFormat.Stereo16;
			MemoryStream destStream = new MemoryStream();
			BinaryWriter destWriter = new BinaryWriter(destStream);
			for (int index = 0; index < info.Data.Length; index++)
			{
				destWriter.Write(DecodeTable[info.Data[index]]);
			}
			destWriter.Close();
			info.Data = destStream.ToArray();
			destStream.Dispose();
		}
		#endregion
	}
}
