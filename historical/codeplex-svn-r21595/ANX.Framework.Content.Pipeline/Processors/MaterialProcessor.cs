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
    public class MaterialProcessor : ContentProcessor<MaterialContent, MaterialContent>
    {
        public virtual Color ColorKeyColor { get; set; }
        public virtual bool ColorKeyEnabled { get; set; }
        public virtual MaterialProcessorDefaultEffect DefaultEffect { get; set; }
        public virtual bool GenerateMipmaps { get; set; }
        [DefaultValue(true)]
        public virtual bool PremultiplyTextureAlpha { get; set; }
        public virtual bool ResizeTexturesToPowerOfTwo { get; set; }
        public virtual TextureProcessorOutputFormat TextureFormat { get; set; }

        public override MaterialContent Process(MaterialContent input, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual ExternalReference<CompiledEffectContent> BuildEffect(ExternalReference<EffectContent> effect, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual ExternalReference<TextureContent> BuildTexture(string textureName, ExternalReference<TextureContent> texture, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
