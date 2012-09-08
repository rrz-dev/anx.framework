using System.Collections.Generic;
using System.IO;
using ANX.BaseDirectX;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
	public class Effect_DX11 : BaseEffect, INativeEffect
	{
		#region Private
		private Dx11.VertexShader vertexShader;
		private Dx11.PixelShader pixelShader;
		#endregion

		#region Public
		internal Dx11.Effect NativeEffect { get; private set; }

		public IEnumerable<EffectTechnique> Techniques
		{
			get
			{
				for (int i = 0; i < NativeEffect.Description.TechniqueCount; i++)
				{
					var teqDx11 = new EffectTechnique_DX11(managedEffect, NativeEffect.GetTechniqueByIndex(i));
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
		public Effect_DX11(GraphicsDevice graphicsDevice, Effect managedEffect, Stream vertexShaderStream,
			Stream pixelShaderStream)
			: base(managedEffect)
		{
			var device = ((GraphicsDeviceWindowsDX11)graphicsDevice.NativeDevice).NativeDevice.Device;
			vertexShader = new Dx11.VertexShader(device, GetByteCode(vertexShaderStream));
			pixelShader = new Dx11.PixelShader(device, GetByteCode(pixelShaderStream));
		}

		public Effect_DX11(GraphicsDevice graphicsDevice, Effect managedEffect, Stream effectStream)
			: base(managedEffect)
		{
			var device = ((GraphicsDeviceWindowsDX11)graphicsDevice.NativeDevice).NativeDevice.Device;
			NativeEffect = new Dx11.Effect(device, GetByteCode(effectStream));
		}
		#endregion

		#region GetCurrentTechnique
		public EffectTechnique_DX11 GetCurrentTechnique()
		{
			return managedEffect.CurrentTechnique.NativeTechnique as EffectTechnique_DX11;
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
		{
			((GraphicsDeviceWindowsDX11)graphicsDevice.NativeDevice).currentEffect = this;
		}
		#endregion

		#region CompileFXShader
		public static byte[] CompileFXShader(string effectCode, string directory = "")
		{
			return CompileShader("fx_5_0", effectCode, directory);
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

			if (NativeEffect != null)
			{
				NativeEffect.Dispose();
				NativeEffect = null;
			}
		}
		#endregion
	}
}
