#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Processors;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class EffectMaterialContent : MaterialContent
    {
        public const string CompiledEffectKey = "";
        public const string EffectKey = "";

        public EffectMaterialContent()
        {
        }

        [ContentSerializerIgnore]
        public ExternalReference<CompiledEffectContent> CompiledEffect
        {
            get;
            set;
        }

        public ExternalReference<EffectContent> Effect
        {
            get;
            set;
        }
    }
}
