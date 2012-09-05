using System;
using System.Collections.Generic;
using SharpDX.D3DCompiler;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
	public class Effect_DX11 : INativeEffect
	{
		#region Private
		private Dx11.VertexShader vertexShader;
		private Dx11.PixelShader pixelShader;
		private Effect managedEffect;
		#endregion

		#region Public
		internal Dx11.Effect NativeEffect
		{
			get;
			private set;
		}

		public IEnumerable<EffectTechnique> Techniques
		{
			get
			{
				for (int i = 0; i < NativeEffect.Description.TechniqueCount; i++)
				{
					EffectTechnique_DX11 teqDx11 = new EffectTechnique_DX11(this.managedEffect);
					teqDx11.NativeTechnique = NativeEffect.GetTechniqueByIndex(i);
					yield return new EffectTechnique(this.managedEffect, teqDx11);
				}
			}
		}

		public IEnumerable<EffectParameter> Parameters
		{
			get
			{
				for (int i = 0; i < NativeEffect.Description.GlobalVariableCount; i++)
				{
					EffectParameter_DX11 parDx11 = new EffectParameter_DX11();
					parDx11.NativeParameter = NativeEffect.GetVariableByIndex(i);

					EffectParameter par = new EffectParameter();
					par.NativeParameter = parDx11;
					yield return par;
				}
			}
		}
		#endregion

		#region Constructor
		public Effect_DX11(GraphicsDevice device, Effect setManagedEffect, Stream vertexShaderStream, Stream pixelShaderStream)
		{
			if (setManagedEffect == null)
				throw new ArgumentNullException("managedEffect");
			managedEffect = setManagedEffect;

			if (vertexShaderStream.CanSeek)
				vertexShaderStream.Seek(0, SeekOrigin.Begin);

			var vertexShaderByteCode = ShaderBytecode.FromStream(vertexShaderStream);
			vertexShader = new Dx11.VertexShader((Dx11.Device)device.NativeDevice, vertexShaderByteCode);

			if (pixelShaderStream.CanSeek)
				pixelShaderStream.Seek(0, SeekOrigin.Begin);

			var pixelShaderByteCode = ShaderBytecode.FromStream(pixelShaderStream);
			pixelShader = new Dx11.PixelShader((Dx11.Device)device.NativeDevice, pixelShaderByteCode);
		}

		public Effect_DX11(GraphicsDevice device, Effect setManagedEffect, Stream effectStream)
		{
			if (setManagedEffect == null)
				throw new ArgumentNullException("managedEffect");
			managedEffect = setManagedEffect;

			if (effectStream.CanSeek)
				effectStream.Seek(0, SeekOrigin.Begin);

			var effectByteCode = ShaderBytecode.FromStream(effectStream);
			NativeEffect = new Dx11.Effect(((GraphicsDeviceWindowsDX11)device.NativeDevice).NativeDevice.Device, effectByteCode);
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
		{
			((GraphicsDeviceWindowsDX11)graphicsDevice.NativeDevice).currentEffect = this;
		}
		#endregion

		#region GetCurrentTechnique
		public EffectTechnique_DX11 GetCurrentTechnique()
		{
			return managedEffect.CurrentTechnique.NativeTechnique as EffectTechnique_DX11;
		}
		#endregion

		public static byte[] CompileVertexShader(string effectCode, string directory = "")
		{
			ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(effectCode, "VS", "vs_4_0", ShaderFlags.None,
				EffectFlags.None, null, new IncludeHandler(directory), "unknown");
			byte[] bytecode = new byte[vertexShaderByteCode.BufferSize];
			vertexShaderByteCode.Data.Read(bytecode, 0, bytecode.Length);
			return bytecode;
		}

		public static byte[] CompilePixelShader(string effectCode, string directory = "")
		{
			ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(effectCode, "PS", "ps_4_0", ShaderFlags.None,
				EffectFlags.None, null, new IncludeHandler(directory), "unknown");
			byte[] bytecode = new byte[pixelShaderByteCode.BufferSize];
			pixelShaderByteCode.Data.Read(bytecode, 0, bytecode.Length);
			return bytecode;
		}

		public static byte[] CompileFXShader(string effectCode, string directory = "")
		{
			ShaderBytecode effectByteCode = ShaderBytecode.Compile(effectCode, "fx_5_0", ShaderFlags.None, EffectFlags.None,
				null, new IncludeHandler(directory), "unknown");
			byte[] bytecode = new byte[effectByteCode.BufferSize];
			effectByteCode.Data.Read(bytecode, 0, bytecode.Length);
			return bytecode;
		}

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

			if (NativeEffect != null)
			{
				NativeEffect.Dispose();
				NativeEffect = null;
			}
		}
		#endregion
	}
}
