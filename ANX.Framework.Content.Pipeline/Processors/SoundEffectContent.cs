using System;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    public sealed class SoundEffectContent
	{
		internal List<byte> Format { get; private set; }
		internal List<byte> Data { get; private set; }
		internal int LoopStart { get; private set; }
		internal int LoopLength { get; private set; }
		internal int Duration { get; private set; }

		internal SoundEffectContent(List<byte> setFormat, List<byte> setData, int setLoopStart, int setLoopLength, int setDuration)
		{
			Format = setFormat;
			Data = setData;
			LoopStart = setLoopStart;
			LoopLength = setLoopLength;
			Duration = setDuration;
		}
    }
}
