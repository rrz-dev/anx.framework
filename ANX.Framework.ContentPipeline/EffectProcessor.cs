#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using TInput = Microsoft.Xna.Framework.Content.Pipeline.Graphics.EffectContent;
using TOutput = Microsoft.Xna.Framework.Content.Pipeline.Processors.CompiledEffectContent;

namespace ANX.Framework.ContentPipeline
{
    [ContentProcessor(DisplayName = "Effect - ANX Framework")]
    public class AnxEffectProcessor : ContentProcessor<TInput, TOutput>
    {
        Microsoft.Xna.Framework.Content.Pipeline.Processors.EffectProcessor processor;
        private EffectProcessorOutputFormat outputFormat;

        public AnxEffectProcessor()
        {
            processor = new Microsoft.Xna.Framework.Content.Pipeline.Processors.EffectProcessor();
            processor.DebugMode = EffectProcessorDebugMode.Auto;
        }

        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            switch (this.outputFormat)
            {
                case EffectProcessorOutputFormat.XNA_BYTE_CODE:
                    return processor.Process(input, context);
                case EffectProcessorOutputFormat.DX10_HLSL:
                    DX10_EffectProcessor dx10EffectProcessor = new DX10_EffectProcessor();
                    return dx10EffectProcessor.Process(input, context);
                case EffectProcessorOutputFormat.OPEN_GL3_GLSL:
                    GL3_EffectProcessor gl3EffectProcessor = new GL3_EffectProcessor();
                    return gl3EffectProcessor.Process(input, context);
                default:
                    throw new NotSupportedException("Currently it is not possible to create effect with format '" + outputFormat.ToString() + "'");
            }
        }

        [DefaultValue(EffectProcessorOutputFormat.XNA_BYTE_CODE)]
        public EffectProcessorOutputFormat OutputFormat
        {
            get
            {
                return this.outputFormat;
            }
            set
            {
                this.outputFormat = value;
            }
        }

        [DefaultValue(EffectProcessorDebugMode.Auto)]
        public EffectProcessorDebugMode DebugMode
        {
            get
            {
                return processor.DebugMode;
            }
            set
            {
                processor.DebugMode = value;
            }
        }

        public string Defines
        {
            get
            {
                return processor.Defines;
            }
            set
            {
                processor.Defines = value;
            }
        }
    }
}