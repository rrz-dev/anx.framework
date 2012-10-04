using System;
using System.Collections.Generic;

// This file is part of the ANX.Framework and originally taken from
// the AC.AL OpenAL library, released under the MIT License.
// For details see: http://acal.codeplex.com/license

namespace WaveUtils
{
	public class WaveInfo
	{
		public List<KeyValuePair<string, byte[]>> UnloadedChunks { get; internal set; }
		public WaveFormat WaveFormat { get; internal set; }
		public byte[] Data { get; internal set; }
		public ALFormat ALFormat { get; internal set; }
		public int SampleRate { get; internal set; }
		public short BitsPerSample { get; internal set; }
		public short BlockAlign { get; internal set; }
		public int Channels { get; internal set; }
		public short ExtSamplesPerBlock { get; internal set; }

		/// <summary>
		/// NOTE: This only works with standard PCM data!
		/// </summary>
		public TimeSpan CalculateDuration()
		{
			float sizeMulBlockAlign = Data.Length / (Channels * 2f);
			return TimeSpan.FromMilliseconds(sizeMulBlockAlign * 1000f / SampleRate);
		}

		public WaveInfo()
		{
			UnloadedChunks = new List<KeyValuePair<string, byte[]>>();
		}
	}
}
