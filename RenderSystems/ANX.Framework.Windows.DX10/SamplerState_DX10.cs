using System;
using ANX.BaseDirectX;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10.Helpers;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
	public class SamplerState_DX10 : BaseStateObject<Dx10.SamplerState>, INativeSamplerState
    {
        #region Private
		private Dx10.SamplerStateDescription description;
		#endregion

		#region Public
		public TextureAddressMode AddressU
		{
			set
			{
				Dx10.TextureAddressMode mode = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.AddressU, ref mode);
			}
		}

		public TextureAddressMode AddressV
		{
			set
			{
				Dx10.TextureAddressMode mode = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.AddressV, ref mode);
			}
		}

		public TextureAddressMode AddressW
		{
			set
			{
				Dx10.TextureAddressMode mode = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.AddressW, ref mode);
			}
		}

		public TextureFilter Filter
		{
			set
			{
				Dx10.Filter filter = FormatConverter.Translate(value);
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
			Dx10.Device device = (graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10).NativeDevice;
            UpdateNativeSamplerState(device);
			IsBound = true;

			device.PixelShader.SetSampler(index, nativeState);
		}
		#endregion

		#region UpdateNativeSamplerState
		private void UpdateNativeSamplerState(Dx10.Device device)
        {
			if (isDirty == true || nativeState == null)
            {
				Dispose();
				nativeState = new Dx10.SamplerState(device, ref description);
                isDirty = false;
            }
		}
		#endregion

		protected override Dx10.SamplerState CreateNativeState(GraphicsDevice graphics)
		{
			return null;
		}

		protected override void ApplyNativeState(GraphicsDevice graphics)
		{
		}
	}
}
