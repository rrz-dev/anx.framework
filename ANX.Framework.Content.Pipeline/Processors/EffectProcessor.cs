#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using System.IO;
using System.ComponentModel;
using ANX.RenderSystem.GL3;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor]
    public class EffectProcessor : ContentProcessor<EffectContent, CompiledEffectContent>
    {
        HLSLCompilerFactory hlslCompilerFactory = new HLSLCompilerFactory();
        private string targetProfile = "fx_4_0";

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

        [DefaultValue("fx_4_0")]
        public virtual string TargetProfile
        {
            get
            {
                return targetProfile;
            }
            set
            {
                targetProfile = value;
            }
        }

        public override CompiledEffectContent Process(EffectContent input, ContentProcessorContext context)
        {
            byte[] effectCompiledCode = null;

            if (input.SourceLanguage == NonXNA.EffectSourceLanguage.HLSL_FX)
            {
                HLSLCompiler compiler = hlslCompilerFactory.Compilers.Last<HLSLCompiler>();
                effectCompiledCode = compiler.Compile(input.EffectCode, DebugMode, TargetProfile);
            }
            else if (input.SourceLanguage == NonXNA.EffectSourceLanguage.GLSL_FX)
            {
                //TODO: parse and split the effect code and save two effect globs
                // if we do it this way, we don't need to parse the glsl source at runtime when loading it.

                effectCompiledCode = ShaderHelper.SaveShaderCode(input.EffectCode);
            }
            else
            {
                throw new InvalidContentException("EffectProcessor is unable to process content with format '" + input.SourceLanguage.ToString() + "'");
            }

            return new CompiledEffectContent(effectCompiledCode)
            {
                Identity = input.Identity,
                Name = input.Name,
                OpaqueData = input.OpaqueData,
                SourceLanguage = input.SourceLanguage,
            };
        }


    }
}
