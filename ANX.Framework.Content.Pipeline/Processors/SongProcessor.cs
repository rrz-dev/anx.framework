#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Audio;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor]
    public class SongProcessor : ContentProcessor<AudioContent, SongContent>
    {
        public ConversionQuality Quality
        {
            get;
            set;
        }

        public override SongContent Process(AudioContent input, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
