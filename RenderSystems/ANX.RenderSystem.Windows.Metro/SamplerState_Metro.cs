using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class SamplerState_Metro : BaseStateObject, INativeSamplerState
	{
		#region Private
		private Dx11.SamplerStateDescription description;
		private Dx11.SamplerState nativeSamplerState;
		#endregion

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

		#region Constructor
		public SamplerState_Metro()
			: base()
		{
			description = new Dx11.SamplerStateDescription();
		}
		#endregion

		#region Apply (TODO)
		public void Apply(GraphicsDevice graphicsDevice, int index)
		{
			UpdateNativeSamplerState();
			bound = true;

			NativeDxDevice.Current.NativeContext.PixelShader.SetSampler(
				index, this.nativeSamplerState);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (nativeSamplerState != null)
			{
				nativeSamplerState.Dispose();
				nativeSamplerState = null;
			}
		}
		#endregion

		#region UpdateNativeSamplerState
		private void UpdateNativeSamplerState()
		{
			if (isDirty == true || nativeSamplerState == null)
			{
				Dispose();

				// TODO: otherwise crashes for now
				description.MaximumLod = float.MaxValue;

				nativeSamplerState = new Dx11.SamplerState(
					NativeDxDevice.Current.NativeDevice, ref description);

				isDirty = false;
			}
		}
		#endregion
	}
}
