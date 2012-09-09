using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using ANX.RenderSystem.Windows.DX10;

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
            return System.Text.Encoding.ASCII.GetBytes(str);
        }

        private string ByteArrayToString(byte[] arr)
        {
			return System.Text.Encoding.ASCII.GetString(arr);
        }
    }
}