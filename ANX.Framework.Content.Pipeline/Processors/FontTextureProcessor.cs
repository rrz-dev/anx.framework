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
    public class FontTextureProcessor : ContentProcessor<Texture2DContent, SpriteFontContent>
    {
        [DefaultValue(' ')]
        public virtual char FirstCharacter
        {
            get;
            set;
        }

        [DefaultValue(true)]
        public virtual bool PremultiplyAlpha
        {
            get;
            set;
        }

        [DefaultValue(TextureProcessorOutputFormat.Color)]
        public virtual TextureProcessorOutputFormat TextureFormat
        {
            get;
            set;
        }

        public FontTextureProcessor()
        {
            FirstCharacter = ' ';
            PremultiplyAlpha = true;
            TextureFormat = TextureProcessorOutputFormat.Color;
        }

        public override SpriteFontContent Process(Texture2DContent input, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual char GetCharacterForIndex(int index)
        {
            throw new NotImplementedException();
        }
    }
}
