#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Audio
{
    public sealed class AudioFormat
    {
        public int AverageBytesPerSecond { get; internal set; }
        public int BitsPerSample { get; internal set; }
        public int BlockAlign { get; internal set; }
        public int ChannelCount { get; internal set; }
        public int Format { get; internal set; }
        public ReadOnlyCollection<byte> NativeWaveFormat { get; internal set; }
        public int SampleRate { get; internal set; }

    }
}
