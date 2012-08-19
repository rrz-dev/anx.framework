using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.Metro.Shader;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class EffectPass_Metro : INativeEffectPass
	{
		#region Private
		private ExtendedShaderPass nativePass;
		private Effect parentEffect;
		private Dx11.VertexShader vertexShader;
		private Dx11.PixelShader pixelShader;
		#endregion

		#region Public
		public string Name
		{
			get;
			private set;
		}

		public EffectPass Pass
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		public EffectPass_Metro(Effect setParentEffect, ExtendedShaderPass setNativePass)
		{
			Name = setNativePass.Name;
			nativePass = setNativePass;
			parentEffect = setParentEffect;

			Pass = new EffectPass(parentEffect);
		}
		#endregion

        #region BuildLayout
        public Dx11.InputLayout BuildLayout(Dx11.Device d3dDevice, Dx11.InputElement[] elements)
		{
			return new Dx11.InputLayout(d3dDevice, nativePass.VertexCode, elements);
        }
        #endregion

        #region Apply
        public void Apply(NativeDxDevice device)
        {
            if (vertexShader == null)
                vertexShader = new Dx11.VertexShader(device.NativeDevice, nativePass.VertexCode);

            if (pixelShader == null)
                pixelShader = new Dx11.PixelShader(device.NativeDevice, nativePass.PixelCode);

            device.NativeContext.VertexShader.Set(vertexShader);
            device.NativeContext.PixelShader.Set(pixelShader);
        }
		#endregion
	}
}
