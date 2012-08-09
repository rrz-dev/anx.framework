#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using ANX.RenderSystem.Windows.DX10;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using TInput = Microsoft.Xna.Framework.Content.Pipeline.Graphics.EffectContent;
using TOutput = Microsoft.Xna.Framework.Content.Pipeline.Processors.CompiledEffectContent;

namespace ANX.Framework.ContentPipeline
{
    [ContentProcessor(DisplayName = "DX10 Effect - ANX Framework")]
    public class DX10_EffectProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            byte[] effectByteCode = Effect_DX10.CompileFXShader(input.EffectCode);

            Byte[] byteCode = new Byte[3 + 2 + 1 + 4 + effectByteCode.Length];

            StringToByteArray("ANX").CopyTo(byteCode, 0);               // Magic Number to recognize format
            byteCode[3] = 0;                                            // Major Version
            byteCode[4] = 2;                                            // Minor Version
            byteCode[5] = (byte)EffectProcessorOutputFormat.DX10_HLSL;  // Format of byte array

            int dataStart = 6;

            BitConverter.GetBytes(effectByteCode.Length).CopyTo(byteCode, dataStart); // length of vertexShaderByteCode
            Array.Copy(effectByteCode, 0, byteCode, dataStart + 4, effectByteCode.Length);

            return new TOutput(byteCode);
        }

        private byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }

        private string ByteArrayToString(byte[] arr)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arr);
        }

    }
}