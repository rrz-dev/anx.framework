using System;

// This file is part of the ANX.Framework and originally taken from
// the AC.AL OpenAL library, released under the MIT License.
// For details see: http://acal.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
	public enum WaveFormat
	{
		PCM = 1,
		/// <summary>
		/// http://wiki.multimedia.cx/index.php?title=Microsoft_ADPCM
		/// </summary>
		MS_ADPCM = 2,
		IEEE_FLOAT = 3,
		/// <summary>
		/// 8-bit ITU-T G.711 A-law
		/// http://hazelware.luggle.com/tutorials/mulawcompression.html
		/// </summary>
		ALAW = 6,
		/// <summary>
		/// 8-bit ITU-T G.711 µ-law
		/// http://hazelware.luggle.com/tutorials/mulawcompression.html
		/// </summary>
		MULAW = 7,

		/// <summary>
		/// Determined by SubFormat
		/// </summary>
		WAVE_FORMAT_EXTENSIBLE = 0xFFFE,
	}
}
