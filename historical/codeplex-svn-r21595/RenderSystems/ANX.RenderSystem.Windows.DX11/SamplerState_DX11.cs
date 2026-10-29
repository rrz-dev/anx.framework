#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx11 = SharpDX.Direct3D11;

namespace ANX.RenderSystem.Windows.DX11
{
    public class SamplerState_DX11 : BaseStateObject<Dx11.SamplerState>, INativeSamplerState
    {
		private Dx11.SamplerStateDescription description;

		#region Public
		public TextureAddressMode AddressU
		{
			set
			{
				Dx11.TextureAddressMode mode = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.AddressU, ref mode);
			}
		}

		public TextureAddressMode AddressV
		{
			set
			{
				Dx11.TextureAddressMode mode = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.AddressV, ref mode);
			}
		}

		public TextureAddressMode AddressW
		{
			set
			{
				Dx11.TextureAddressMode mode = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.AddressW, ref mode);
			}
		}

		public TextureFilter Filter
		{
			set
			{
				Dx11.Filter filter = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.Filter, ref filter);
			}
		}

		public int MaxAnisotropy
		{
			set
			{
				SetValueIfDifferentAndMarkDirty(ref description.MaximumAnisotropy, ref value);
			}
		}

		public int MaxMipLevel
		{
			set
			{
				if (description.MaximumLod != value)
				{
					description.MaximumLod = value;
					isDirty = true;
				}
			}
		}

		public float MipMapLevelOfDetailBias
		{
			set
			{
				SetValueIfDifferentAndMarkDirty(ref description.MipLodBias, ref value);
			}
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice, int index)
        {
			Dx11.DeviceContext context = (graphicsDevice.NativeDevice as GraphicsDeviceDX).NativeDevice;

            UpdateNativeSamplerState(context.Device);
            IsBound = true;

			context.PixelShader.SetSampler(index, nativeState);
		}
		#endregion

		#region UpdateNativeSamplerState
		private void UpdateNativeSamplerState(Dx11.Device device)
        {
			if (isDirty || nativeState == null)
            {
				Dispose();
                nativeState = new Dx11.SamplerState(device, ref description);
                isDirty = false;
            }
        }
		#endregion

		protected override Dx11.SamplerState CreateNativeState(GraphicsDevice graphics)
		{
			return null;
		}

		protected override void ApplyNativeState(GraphicsDevice graphics)
		{
		}
    }
}
