#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor]
    public class ModelTextureProcessor : TextureProcessor
    {
        public override Color ColorKeyColor { get; set; }
        public override bool ColorKeyEnabled { get; set; }
        public override bool GenerateMipmaps { get; set; }
        public override bool ResizeToPowerOfTwo { get; set; }
        public override TextureProcessorOutputFormat TextureFormat { get; set; }


    }
}
