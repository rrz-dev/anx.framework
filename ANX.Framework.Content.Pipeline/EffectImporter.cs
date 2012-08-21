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

namespace ANX.Framework.Content.Pipeline
{
    [ContentImporter(".fx")]
    public class EffectImporter : ContentImporter<EffectContent>
    {
        public EffectImporter()
        {
        }

        public override EffectContent Import(string filename, ContentImporterContext context)
        {
            EffectContent content = new EffectContent()
            {
                EffectCode = System.IO.File.ReadAllText(filename),
                Identity = new ContentIdentity(filename, null, null),
            };

            return content;
        }
    }
}
