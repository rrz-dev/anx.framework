using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class SamplerState_Metro : INativeSamplerState
	{
		#region Private Members
		private Dx11.SamplerStateDescription description;
		private Dx11.SamplerState nativeSamplerState;
		private bool nativeSamplerStateDirty;
		private bool bound;

		#endregion // Private Members

		public SamplerState_Metro()
		{
			this.description = new Dx11.SamplerStateDescription();

			this.nativeSamplerStateDirty = true;
		}

		public void Apply(GraphicsDevice graphicsDevice, int index)
		{
			throw new NotImplementedException();

			//GraphicsDeviceWindowsMetro gdm = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
			//Device device = gdm.NativeDevice;

			//UpdateNativeSamplerState(device);
			//this.bound = true;

			//device.PixelShader.SetSampler(index, this.nativeSamplerState);
		}

		public void Release()
		{
			this.bound = false;
		}

		public bool IsBound
		{
			get
			{
				return this.bound;
			}
		}

		public TextureAddressMode AddressU
		{
			set
			{
				Dx11.TextureAddressMode mode = FormatConverter.Translate(value);

				if (description.AddressU != mode)
				{
					description.AddressU = mode;
					nativeSamplerStateDirty = true;
				}
			}
		}

		public TextureAddressMode AddressV
		{
			set
			{
				Dx11.TextureAddressMode mode = FormatConverter.Translate(value);

				if (description.AddressV != mode)
				{
					description.AddressV = mode;
					nativeSamplerStateDirty = true;
				}
			}
		}

		public TextureAddressMode AddressW
		{
			set
			{
				Dx11.TextureAddressMode mode = FormatConverter.Translate(value);

				if (description.AddressW != mode)
				{
					description.AddressW = mode;
					nativeSamplerStateDirty = true;
				}
			}
		}

		public TextureFilter Filter
		{
			set
			{
				Dx11.Filter filter = FormatConverter.Translate(value);

				if (description.Filter != filter)
				{
					description.Filter = filter;
					nativeSamplerStateDirty = true;
				}
			}
		}

		public int MaxAnisotropy
		{
			set
			{
				if (description.MaximumAnisotropy != value)
				{
					description.MaximumAnisotropy = value;
					nativeSamplerStateDirty = true;
				}
			}
		}

		public int MaxMipLevel
		{
			set
			{
				if (description.MaximumLod != value)
				{
					description.MaximumLod = value;
					nativeSamplerStateDirty = true;
				}
			}
		}

		public float MipMapLevelOfDetailBias
		{
			set
			{
				if (description.MipLodBias != value)
				{
					description.MipLodBias = value;
					nativeSamplerStateDirty = true;
				}
			}
		}

		public void Dispose()
		{
			if (this.nativeSamplerState != null)
			{
				this.nativeSamplerState.Dispose();
				this.nativeSamplerState = null;
			}
		}

		private void UpdateNativeSamplerState(Dx11.Device1 device)
		{
			if (this.nativeSamplerStateDirty == true || this.nativeSamplerState == null)
			{
				if (this.nativeSamplerState != null)
				{
					this.nativeSamplerState.Dispose();
					this.nativeSamplerState = null;
				}

				this.nativeSamplerState = new Dx11.SamplerState(device, ref this.description);

				this.nativeSamplerStateDirty = false;
			}
		}
	}
}
