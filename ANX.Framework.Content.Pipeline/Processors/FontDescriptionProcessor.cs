#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor]
    public class FontDescriptionProcessor : ContentProcessor<FontDescription, SpriteFontContent>
    {

        public override SpriteFontContent Process(FontDescription input, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
