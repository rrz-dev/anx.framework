using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10.Helpers;
using SharpDX.D3DCompiler;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class Effect_DX10 : INativeEffect
    {
        #region Private
        private Dx10.VertexShader vertexShader;
		private Dx10.PixelShader pixelShader;
        private Effect managedEffect;
		#endregion

		#region Public
		internal Dx10.Effect NativeEffect
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
					var teqDx10 = new EffectTechnique_DX10(managedEffect, NativeEffect.GetTechniqueByIndex(i));
					yield return new EffectTechnique(this.managedEffect, teqDx10);
				}
			}
		}

		public IEnumerable<EffectParameter> Parameters
		{
			get
			{
				for (int i = 0; i < NativeEffect.Description.GlobalVariableCount; i++)
				{
					EffectParameter_DX10 parDx10 = new EffectParameter_DX10();
					parDx10.NativeParameter = NativeEffect.GetVariableByIndex(i);

					EffectParameter par = new EffectParameter();
					par.NativeParameter = parDx10;
					yield return par;
				}
			}
		}
		#endregion

		#region Constructor
		public Effect_DX10(GraphicsDevice device, Effect setManagedEffect, Stream vertexShaderStream, Stream pixelShaderStream)
        {
            if (setManagedEffect == null)
                throw new ArgumentNullException("managedEffect");
            managedEffect = setManagedEffect;

			if (vertexShaderStream.CanSeek)
				vertexShaderStream.Seek(0, SeekOrigin.Begin);

			var vertexShaderByteCode = ShaderBytecode.FromStream(vertexShaderStream);
			vertexShader = new Dx10.VertexShader((SharpDX.Direct3D10.Device)device.NativeDevice, vertexShaderByteCode);

			if (pixelShaderStream.CanSeek)
				pixelShaderStream.Seek(0, SeekOrigin.Begin);

			var pixelShaderByteCode = ShaderBytecode.FromStream(pixelShaderStream);
			pixelShader = new Dx10.PixelShader((SharpDX.Direct3D10.Device)device.NativeDevice, pixelShaderByteCode);
        }

		public Effect_DX10(GraphicsDevice device, Effect setManagedEffect, Stream effectStream)
        {
			if (setManagedEffect == null)
                throw new ArgumentNullException("managedEffect");
			managedEffect = setManagedEffect;

			if (effectStream.CanSeek)
				effectStream.Seek(0, SeekOrigin.Begin);

			var effectByteCode = ShaderBytecode.FromStream(effectStream);
			NativeEffect = new Dx10.Effect(((GraphicsDeviceWindowsDX10)device.NativeDevice).NativeDevice,
				effectByteCode, EffectFlags.None);
        }
		#endregion

		#region GetCurrentTechnique
		public EffectTechnique_DX10 GetCurrentTechnique()
		{
			return managedEffect.CurrentTechnique.NativeTechnique as EffectTechnique_DX10;
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
        {
            ((GraphicsDeviceWindowsDX10)graphicsDevice.NativeDevice).currentEffect = this;
        }
		#endregion

		#region CompileVertexShader (TODO)
		public static byte[] CompileVertexShader(string effectCode, string directory = "")
        {
			// TODO: not all entry points are named VS!
            ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(effectCode, "VS", "vs_4_0", ShaderFlags.None,
				EffectFlags.None, null, new IncludeHandler(directory), "unknown");
            byte[] bytecode = new byte[vertexShaderByteCode.BufferSize];
            vertexShaderByteCode.Data.Read(bytecode, 0, bytecode.Length);
            return bytecode;
		}
		#endregion

		#region CompilePixelShader (TODO)
		public static byte[] CompilePixelShader(string effectCode, string directory = "")
		{
			// TODO: not all entry points are named PS!
            ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(effectCode, "PS", "ps_4_0", ShaderFlags.None,
				EffectFlags.None, null, new IncludeHandler(directory), "unknown");
            byte[] bytecode = new byte[pixelShaderByteCode.BufferSize];
            pixelShaderByteCode.Data.Read(bytecode, 0, bytecode.Length);
            return bytecode;
		}
		#endregion

		#region CompileFXShader
		public static byte[] CompileFXShader(string effectCode, string directory = "")
        {
            ShaderBytecode effectByteCode = ShaderBytecode.Compile(effectCode, "fx_4_0", ShaderFlags.None, EffectFlags.None,
				null, new IncludeHandler(directory), "unknown");
            byte[] bytecode = new byte[effectByteCode.BufferSize];
            effectByteCode.Data.Read(bytecode, 0, bytecode.Length);
            return bytecode;
        }
		#endregion

		#region Dispose
		public void Dispose()
		{
			SafeDispose(pixelShader);
			pixelShader = null;

			SafeDispose(vertexShader);
			vertexShader = null;

			SafeDispose(NativeEffect);
			NativeEffect = null;
		}

		private void SafeDispose(IDisposable disposable)
		{
			if (disposable != null)
				disposable.Dispose();
		}
		#endregion
	}
}
