using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10.Helpers;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class SamplerState_DX10 : INativeSamplerState
    {
        #region Private
		private Dx10.SamplerStateDescription description;
		private Dx10.SamplerState nativeSamplerState;
        private bool isDirty;
		#endregion

		#region Public
		public bool IsBound { get; private set; }

		public ANX.Framework.Graphics.TextureAddressMode AddressU
		{
			set
			{
				Dx10.TextureAddressMode mode = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.AddressU, ref mode);
			}
		}

		public ANX.Framework.Graphics.TextureAddressMode AddressV
		{
			set
			{
				Dx10.TextureAddressMode mode = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.AddressV, ref mode);
			}
		}

		public ANX.Framework.Graphics.TextureAddressMode AddressW
		{
			set
			{
				Dx10.TextureAddressMode mode = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.AddressW, ref mode);
			}
		}

		public TextureFilter Filter
		{
			set
			{
				Dx10.Filter filter = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.Filter, ref filter);
			}
		}

		public int MaxAnisotropy
		{
			set
			{
				UpdateValueAndMarkDirtyIfNeeded(ref description.MaximumAnisotropy, ref value);
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
				UpdateValueAndMarkDirtyIfNeeded(ref description.MipLodBias, ref value);
			}
		}
		#endregion

		#region Constructor
		public SamplerState_DX10()
        {
            isDirty = true;
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice, int index)
        {
			Dx10.Device device = (graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10).NativeDevice;
            UpdateNativeSamplerState(device);
			IsBound = true;

            device.PixelShader.SetSampler(index, nativeSamplerState);
		}
		#endregion

		#region Release
		public void Release()
        {
			IsBound = false;
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
		private void UpdateNativeSamplerState(Dx10.Device device)
        {
            if (isDirty == true || nativeSamplerState == null)
            {
				Dispose();
				nativeSamplerState = new Dx10.SamplerState(device, ref description);
                isDirty = false;
            }
		}
		#endregion

		#region UpdateValueAndMarkDirtyIfNeeded
		private void UpdateValueAndMarkDirtyIfNeeded<T>(ref T currentValue, ref T value)
		{
			if (value.Equals(currentValue) == false)
			{
				isDirty = true;
				currentValue = value;
			}
		}
		#endregion
	}
}
