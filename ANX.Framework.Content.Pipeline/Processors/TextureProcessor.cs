#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using System.ComponentModel;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor]
    public class TextureProcessor : ContentProcessor<TextureContent, TextureContent>
    {
        public virtual Color ColorKeyColor { get; set; }
        public virtual bool ColorKeyEnabled { get; set; }
        public virtual bool GenerateMipmaps { get; set; }
        [DefaultValue(true)]
        public virtual bool PremultiplyAlpha { get; set; }
        public virtual bool ResizeToPowerOfTwo { get; set; }
        public virtual TextureProcessorOutputFormat TextureFormat { get; set; }

        public override TextureContent Process(TextureContent input, ContentProcessorContext context)
        {
            //TODO: implement

            return input;
        }
    }
}
