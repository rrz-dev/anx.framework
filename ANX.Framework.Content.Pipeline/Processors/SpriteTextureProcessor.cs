#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ANX.Framework.Content.Pipeline.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor]
    public class SpriteTextureProcessor : TextureProcessor
    {
        public override Color ColorKeyColor { get; set; }
        public override bool ColorKeyEnabled { get; set; }
        public override bool GenerateMipmaps { get; set; }
        [Browsable(false)]
        public override bool ResizeToPowerOfTwo { get; set; }
        public override TextureProcessorOutputFormat TextureFormat { get; set; }

        public override TextureContent Process(TextureContent input, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
