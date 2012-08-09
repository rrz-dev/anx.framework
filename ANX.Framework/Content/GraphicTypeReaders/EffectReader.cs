#region Using Statements
using System;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.ContentPipeline;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
	internal class EffectReader : ContentTypeReader<Effect>
	{
		protected internal override Effect Read(ContentReader input, Effect existingInstance)
		{
            IServiceProvider service = input.ContentManager.ServiceProvider;

            var rfc = service.GetService(typeof(IRenderSystemCreator)) as IRenderSystemCreator;
            if (rfc == null)
            {
                throw new ContentLoadException("Service not found IRenderFrameworkCreator");
            }

            var gds = service.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
            if (gds == null)
            {
                throw new ContentLoadException("Service not found IGraphicsDeviceService");
            }

            int totalLength = input.ReadInt32();

            byte magicA = input.ReadByte();
            byte magicN = input.ReadByte();
            byte magicX = input.ReadByte();

            // The first three bytes must be the characters ANX
            // to identify the ANX-Effect-Format of the file.
            // Otherwise fallback on the original effect loading code.
            if (magicA != 'A' || magicN != 'N' || magicX != 'X')
            {
                byte[] effectCode = input.ReadBytes(totalLength);
                var memStream = new MemoryStream(effectCode);

                // the XNA Way
                return new Effect(gds.GraphicsDevice, effectCode);

                // the ANX Way
                //return rfc.CreateEffect(gds.GraphicsDevice, memStream);
            }

            byte majorVersion = input.ReadByte();
            byte minorVersion = input.ReadByte();
            EffectProcessorOutputFormat format = (EffectProcessorOutputFormat)input.ReadByte();

            //int vertexShaderByteCount = input.ReadInt32();
            //byte[] vertexShaderByteCode = input.ReadBytes(vertexShaderByteCount);

            //int pixelShaderByteCount = input.ReadInt32();
            //byte[] pixelShaderByteCode = input.ReadBytes(pixelShaderByteCount);

            int effectByteCodeCount = input.ReadInt32();
            byte[] effectByteCode = input.ReadBytes(effectByteCodeCount);

            switch (format)
            {
                case EffectProcessorOutputFormat.DX10_HLSL:
                case EffectProcessorOutputFormat.OPEN_GL3_GLSL:
                    //return rfc.CreateEffect(gds.GraphicsDevice, new MemoryStream(vertexShaderByteCode, false), new MemoryStream(pixelShaderByteCode, false));
                    //return rfc.CreateEffect(gds.GraphicsDevice, new MemoryStream(effectByteCode, false));
                    return new Effect(gds.GraphicsDevice, effectByteCode);
                default:
                    throw new NotImplementedException("loading of ANX-Effect format with type '" + format.ToString() + "' not yet implemented.");
            }
		}
	}
}
