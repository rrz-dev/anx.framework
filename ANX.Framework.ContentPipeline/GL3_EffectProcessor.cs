#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
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
using ANX.RenderSystem.Windows.GL3;
using System.Text;

namespace ANX.Framework.ContentPipeline
{
	[ContentProcessor(DisplayName = "OpenGL3 Effect - ANX Framework")]
	public class GL3_EffectProcessor : ContentProcessor<TInput, TOutput>
	{
		public override TOutput Process(TInput input, ContentProcessorContext context)
		{
			byte[] effectByteCode = ShaderHelper.SaveShaderCode(input.EffectCode);

			Byte[] byteCode = new Byte[3 + 2 + 1 + 4 + effectByteCode.Length];

			// Magic Number to recognize format
			StringToByteArray("ANX").CopyTo(byteCode, 0);
			// Major Version
			byteCode[3] = 0;
			// Minor Version
			byteCode[4] = 2;
			// Format of byte array
			byteCode[5] = (byte)EffectProcessorOutputFormat.OPEN_GL3_GLSL;

			int dataStart = 6;

			// length of vertexShaderByteCode
			BitConverter.GetBytes(effectByteCode.Length).CopyTo(byteCode, dataStart);
			Array.Copy(effectByteCode, 0, byteCode, dataStart + 4, effectByteCode.Length);

			return new TOutput(byteCode);
		}

		private byte[] StringToByteArray(string str)
		{
			return Encoding.ASCII.GetBytes(str);
		}

		private string ByteArrayToString(byte[] arr)
		{
			return Encoding.ASCII.GetString(arr);
		}

	}
}
