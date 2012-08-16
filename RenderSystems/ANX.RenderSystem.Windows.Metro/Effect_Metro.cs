using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;
using ANX.RenderSystem.Windows.Metro.Shader;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class Effect_Metro : INativeEffect
	{
		#region Private
		private List<EffectTechnique> techniques;
		private List<EffectParameter> parameters;

		private ExtendedShader shader;
		#endregion

		#region Public
		public Effect ManagedEffect
		{
			get;
			private set;
		}

		public IEnumerable<EffectTechnique> Techniques
		{
			get
			{
				if (techniques == null)
				{
					ParseTechniques();
				}

				return techniques;
			}
		}

		public IEnumerable<EffectParameter> Parameters
		{
			get
			{
				if (parameters == null)
				{
					ParseParameters();
				}

				return parameters;
			}
		}
		#endregion

		#region Constructor
		public Effect_Metro(GraphicsDevice device, Effect managedEffect,
			Stream vertexShaderByteCode, Stream pixelShaderByteCode)
		{
			ManagedEffect = managedEffect;

			throw new NotImplementedException();

			/*byte[] vertexData = SeekIfPossibleAndReadBytes(vertexShaderByteCode);
			NativeVertexShader = new Dx11.VertexShader((Dx11.Device)device.NativeDevice, vertexData);

			byte[] pixelData = SeekIfPossibleAndReadBytes(pixelShaderByteCode);
			NativePixelShader = new Dx11.PixelShader((Dx11.Device)device.NativeDevice, pixelData);*/
		}

		public Effect_Metro(GraphicsDevice device, Effect managedEffect, Stream effectByteCode)
		{
			ManagedEffect = managedEffect;

			shader = new ExtendedShader(effectByteCode);
		}
		#endregion

		#region ParseTechniques
		private void ParseTechniques()
		{
			techniques = new List<EffectTechnique>();

			foreach (string key in shader.Techniques.Keys)
			{
				var nativeTechnique = new EffectTechnique_Metro(key, ManagedEffect,
					shader.Techniques[key]);
				techniques.Add(new EffectTechnique(ManagedEffect, nativeTechnique));
			}
		}
		#endregion

		#region ParseParameters
		private void ParseParameters()
		{
			parameters = new List<EffectParameter>();

			foreach (ExtendedShaderParameter parameter in shader.Parameters)
			{
				EffectParameter newParam = new EffectParameter();
				newParam.NativeParameter = new EffectParameter_Metro(parameter);
				parameters.Add(newParam);
			}
		}
		#endregion

		#region SeekIfPossibleAndReadBytes
		private byte[] SeekIfPossibleAndReadBytes(Stream stream)
		{
			if (stream.CanSeek)
			{
				stream.Seek(0, SeekOrigin.Begin);
			}

			int pixelSize = (int)(stream.Length - stream.Position);
			byte[] data = new byte[pixelSize];
			stream.Read(data, 0, pixelSize);

			return data;
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
		{
			((GraphicsDeviceWindowsMetro)graphicsDevice.NativeDevice).currentEffect = this;
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			// TODO
		}
		#endregion
	}
}
