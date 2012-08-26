using System;
using OpenTK.Audio;

// This file is part of the ANX.Framework and originally taken from
// the AC.AL OpenAL library, released under the MIT License.
// For details see: http://acal.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
	public class WaveInfo
	{
		public WaveFormat WaveFormat
		{
			get;
			internal set;
		}

		public byte[] Data
		{
			get;
			internal set;
		}

		public ALFormat OpenALFormat
		{
			get;
			internal set;
		}

		public int SampleRate
		{
			get;
			internal set;
		}

		public short BitsPerSample
		{
			get;
			internal set;
		}

		public short BlockAlign
		{
			get;
			internal set;
		}

		public int Channels
		{
			get;
			internal set;
		}

		public short ExtSamplesPerBlock
		{
			get;
			internal set;
		}
	}
}
