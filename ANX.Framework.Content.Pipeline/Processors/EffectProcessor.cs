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
    public class EffectProcessor : ContentProcessor<EffectContent, CompiledEffectContent>
    {
        public virtual EffectProcessorDebugMode DebugMode
        {
            get;
            set;
        }

        public virtual string Defines
        {
            get;
            set;
        }

        public override CompiledEffectContent Process(EffectContent input, ContentProcessorContext context)
        {
            byte[] effectCompiledCode = new byte[1];    //TODO: compile effect!!!

            return new CompiledEffectContent(effectCompiledCode)
            {
                Identity = input.Identity,
                Name = input.Name,
                OpaqueData = input.OpaqueData
            };
        }
    }
}
