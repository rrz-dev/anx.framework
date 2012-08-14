using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class Effect_Metro : INativeEffect
	{
		#region Private
		private Dx11.VertexShader vertexShader;
		private Dx11.PixelShader pixelShader;
		private Effect managedEffect;
		#endregion

		#region Public
		//internal Dx11.Effect NativeEffect
		//{
		//    get
		//    {
		//        return this.nativeEffect;
		//    }
		//}

		internal Dx11.VertexShader NativeVertexShader
		{
			get
			{
				return this.vertexShader;
			}
		}

		internal Dx11.PixelShader NativePixelShader
		{
			get
			{
				return this.pixelShader;
			}
		}

		internal Dx11.VertexShader VertexShader
		{
			get
			{
				return this.vertexShader;
			}
		}

		internal Dx11.PixelShader PixelShader
		{
			get
			{
				return this.pixelShader;
			}
		}

		public IEnumerable<EffectTechnique> Techniques
		{
			get
			{
				throw new NotImplementedException();

				//for (int i = 0; i < nativeEffect.Description.TechniqueCount; i++)
				//{
				//    EffectTechnique_DX10 teqDx10 = new EffectTechnique_DX10(this.managedEffect);
				//    teqDx10.NativeTechnique = nativeEffect.GetTechniqueByIndex(i);

				//    Graphics.EffectTechnique teq = new Graphics.EffectTechnique(this.managedEffect, teqDx10);

				//    yield return teq;
				//}
			}
		}

		public IEnumerable<EffectParameter> Parameters
		{
			get
			{
				//TODO: implement

				System.Diagnostics.Debugger.Break();

				return null;

				//for (int i = 0; i < nativeEffect.Description.GlobalVariableCount; i++)
				//{
				//    EffectParameter_Metro parDx10 = new EffectParameter_Metro();
				//    parDx10.NativeParameter = nativeEffect.GetVariableByIndex(i);

				//    Graphics.EffectParameter par = new Graphics.EffectParameter();
				//    par.NativeParameter = parDx10;

				//    yield return par;
				//}
			}
		}
		#endregion

		#region Constructor
		public Effect_Metro(GraphicsDevice device, Effect managedEffect,
			Stream vertexShaderByteCode, Stream pixelShaderByteCode)
		{
			this.managedEffect = managedEffect;

			byte[] vertexData = SeekIfPossibleAndReadBytes(vertexShaderByteCode);
			vertexShader = new Dx11.VertexShader((Dx11.Device)device.NativeDevice, vertexData);

			byte[] pixelData = SeekIfPossibleAndReadBytes(pixelShaderByteCode);
			pixelShader = new Dx11.PixelShader((Dx11.Device)device.NativeDevice, pixelData);
		}

		public Effect_Metro(GraphicsDevice device, Effect managedEffect, Stream effectByteCode)
		{
			this.managedEffect = managedEffect;

			if (effectByteCode.CanSeek)
			{
				effectByteCode.Seek(0, SeekOrigin.Begin);
			}
			// TODO
			/*
			byte[] vertexData = SeekIfPossibleAndReadBytes(vertexShaderByteCode);
			vertexShader = new Dx11.VertexShader((Dx11.Device)device.NativeDevice, vertexData);

			byte[] pixelData = SeekIfPossibleAndReadBytes(pixelShaderByteCode);
			pixelShader = new Dx11.PixelShader((Dx11.Device)device.NativeDevice, pixelData);
			*/
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
			//TODO: dummy
			((GraphicsDeviceWindowsMetro)graphicsDevice.NativeDevice).currentEffect = this;
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (pixelShader != null)
			{
				pixelShader.Dispose();
				pixelShader = null;
			}

			if (vertexShader != null)
			{
				vertexShader.Dispose();
				vertexShader = null;
			}
		}
		#endregion
	}
}
