#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx10 = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
    public partial class EffectDX : INativeEffect
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
                    var parDx10 = new EffectParameter_DX10
                    {
                        NativeParameter = NativeEffect.GetVariableByIndex(i)
                    };
                    yield return new EffectParameter(parDx10);
                }
			}
		}
		#endregion

		#region Constructor
		public EffectDX(GraphicsDevice graphicsDevice, Effect managedEffect, Stream vertexShaderStream, Stream pixelShaderStream)
			: this(managedEffect)
		{
			var device = ((GraphicsDeviceDX)graphicsDevice.NativeDevice).NativeDevice;
			vertexShader = new Dx10.VertexShader(device, GetByteCode(vertexShaderStream));
			pixelShader = new Dx10.PixelShader(device, GetByteCode(pixelShaderStream));
        }

		public EffectDX(GraphicsDevice graphicsDevice, Effect managedEffect, Stream effectStream)
			: this(managedEffect)
		{
            var device = ((GraphicsDeviceDX)graphicsDevice.NativeDevice).NativeDevice;

            try
            {
                NativeEffect = new Dx10.Effect(device, GetByteCode(effectStream));
            }
            catch (SharpDX.SharpDXException ex)
            {
                System.Diagnostics.Debugger.Break();
            }
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
            ((GraphicsDeviceDX)graphicsDevice.NativeDevice).currentEffect = this;
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
