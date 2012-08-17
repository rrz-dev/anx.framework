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
    public class AudioContent : ContentItem, IDisposable
    {
        public ReadOnlyCollection<byte> Data { get; internal set; }
        public TimeSpan Duration { get; internal set; }
        [ContentSerializer]
        public string FileName { get; internal set; }
        public AudioFileType FileType { get; internal set; }
        public AudioFormat Format { get; internal set; }
        public int LoopLength { get; internal set; }
        public int LoopStart { get; internal set; }

        public void ConvertFormat(ConversionFormat formatType, ConversionQuality quality, string targetFileName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
