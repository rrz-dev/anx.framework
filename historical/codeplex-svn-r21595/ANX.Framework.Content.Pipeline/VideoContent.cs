#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Media;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    public class VideoContent : ContentItem, IDisposable
    {
        public int BitsPerSecond { get; internal set; }
        public TimeSpan Duration { get; internal set; }
        [ContentSerializer]
        public string Filename { get; set; }
        public float FramesPerSecond { get; internal set; }
        public int Height { get; internal set; }
        [ContentSerializer]
        public VideoSoundtrackType VideoSoundtrackType { get; set; }
        public int Width { get; internal set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
