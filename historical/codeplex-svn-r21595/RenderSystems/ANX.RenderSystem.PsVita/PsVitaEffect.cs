using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Sce.PlayStation.Core.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaEffect : INativeEffect
	{
		#region Private
		private ShaderProgram nativeShader;
		#endregion

		#region Public
		public IEnumerable<EffectTechnique> Techniques
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IEnumerable<EffectParameter> Parameters
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region Constructor
		public PsVitaEffect(Effect managedEffect, Stream shaderByteStream)
		{
			int length = (int)(shaderByteStream.Length - shaderByteStream.Position);
			byte[] shaderByteData = new byte[length];
			shaderByteStream.Read(shaderByteData, 0, length);
			nativeShader = new ShaderProgram(shaderByteData);
		}

		public PsVitaEffect(Effect managedEffect, Stream vertexShaderByteStream,
			Stream pixelShaderByteStream)
		{
			int vertexBytesLength = (int)(vertexShaderByteStream.Length -
				vertexShaderByteStream.Position);
			byte[] vertexShaderByteData = new byte[vertexBytesLength];
			vertexShaderByteStream.Read(vertexShaderByteData, 0, vertexBytesLength);

			int fragmentBytesLength = (int)(pixelShaderByteStream.Length -
				pixelShaderByteStream.Position);
			byte[] fragmentShaderByteData = new byte[fragmentBytesLength];
			pixelShaderByteStream.Read(fragmentShaderByteData, 0, fragmentBytesLength);

			nativeShader = new ShaderProgram(vertexShaderByteData, fragmentShaderByteData);
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (nativeShader != null)
			{
				nativeShader.Dispose();
				nativeShader = null;
			}
		}
		#endregion
	}
}
