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
		private Dx11.VertexShader vertexShader;
		private Dx11.PixelShader pixelShader;
		private Effect managedEffect;

		public Effect_Metro(GraphicsDevice device, Effect managedEffect, Stream vertexShaderByteCode, Stream pixelShaderByteCode)
		{
			if (this.managedEffect == null)
			{
				throw new ArgumentNullException("managedEffect");
			}
			this.managedEffect = managedEffect;

			if (vertexShaderByteCode.CanSeek)
			{
				vertexShaderByteCode.Seek(0, SeekOrigin.Begin);
			}

			int vertexSize = (int)(vertexShaderByteCode.Length - vertexShaderByteCode.Position);
			byte[] vertexData = new byte[vertexSize];
			vertexShaderByteCode.Read(vertexData, 0, vertexSize);
			this.vertexShader = new Dx11.VertexShader((Dx11.Device)device.NativeDevice, vertexData);

			if (pixelShaderByteCode.CanSeek)
			{
				pixelShaderByteCode.Seek(0, SeekOrigin.Begin);
			}

			int pixelSize = (int)(pixelShaderByteCode.Length - pixelShaderByteCode.Position);
			byte[] pixelData = new byte[pixelSize];
			pixelShaderByteCode.Read(pixelData, 0, pixelSize);
			this.pixelShader = new Dx11.PixelShader((Dx11.Device)device.NativeDevice, pixelData);
		}

		public Effect_Metro(GraphicsDevice device, Effect managedEffect, Stream effectByteCode)
		{
			if (managedEffect == null)
			{
				throw new ArgumentNullException("managedEffect");
			}
			this.managedEffect = managedEffect;

			if (effectByteCode.CanSeek)
			{
				effectByteCode.Seek(0, SeekOrigin.Begin);
			}
			// TODO
			//this.nativeEffect = new Dx11.Effect(((GraphicsDeviceWindowsDX10)device.NativeDevice).NativeDevice, this.effectByteCode);
		}

		public void Apply(GraphicsDevice graphicsDevice)
		{
			//TODO: dummy
			((GraphicsDeviceWindowsMetro)graphicsDevice.NativeDevice).currentEffect = this;
		}

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

		public void Dispose()
		{
			if (this.pixelShader != null)
			{
				this.pixelShader.Dispose();
				this.pixelShader = null;
			}

			if (this.vertexShader != null)
			{
				this.vertexShader.Dispose();
				this.vertexShader = null;
			}

			//if (this.nativeEffect != null)
			//{
			//    this.nativeEffect.Dispose();
			//    this.nativeEffect = null;
			//}
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
	}
}
