#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Media;
using System.ComponentModel;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor]
    public class VideoProcessor : ContentProcessor<VideoContent, VideoContent>
    {
        [DefaultValue(VideoSoundtrackType.Music)]
        public VideoSoundtrackType VideoSoundtrackType { get; set; }

        public override VideoContent Process(VideoContent input, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
