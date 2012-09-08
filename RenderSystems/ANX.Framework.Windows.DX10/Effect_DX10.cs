using System;
using System.Collections.Generic;
using System.IO;
using ANX.BaseDirectX;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class Effect_DX10 : BaseEffect, INativeEffect
    {
        #region Private
        private Dx10.VertexShader vertexShader;
		private Dx10.PixelShader pixelShader;
		#endregion

		#region Public
		internal Dx10.Effect NativeEffect { get; private set; }

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
		public Effect_DX10(GraphicsDevice graphicsDevice, Effect managedEffect, Stream vertexShaderStream,
			Stream pixelShaderStream)
			: base(managedEffect)
		{
			var device = ((GraphicsDeviceWindowsDX10)graphicsDevice.NativeDevice).NativeDevice;
			vertexShader = new Dx10.VertexShader(device, GetByteCode(vertexShaderStream));
			pixelShader = new Dx10.PixelShader(device, GetByteCode(pixelShaderStream));
        }

		public Effect_DX10(GraphicsDevice graphicsDevice, Effect managedEffect, Stream effectStream)
			: base(managedEffect)
		{
			var device = ((GraphicsDeviceWindowsDX10)graphicsDevice.NativeDevice).NativeDevice;
			NativeEffect = new Dx10.Effect(device, GetByteCode(effectStream));
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

		#region CompileFXShader
		public static byte[] CompileFXShader(string effectCode, string directory = "")
		{
			return CompileShader("fx_4_0", effectCode, directory);
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
