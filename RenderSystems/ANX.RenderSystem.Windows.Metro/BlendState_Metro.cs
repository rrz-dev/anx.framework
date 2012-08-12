using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class BlendState_Metro : INativeBlendState
	{
		#region Private Members
		private Dx11.BlendStateDescription blendStateDescription;
		private Dx11.BlendState nativeBlendState;
		private bool nativeBlendStateDirty;
		private SharpDX.Color4 blendFactor;
		private int multiSampleMask;
		private bool bound;


		#endregion // Private Members

		public BlendState_Metro()
		{
			for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
			{
				blendStateDescription.RenderTarget[i] = new Dx11.RenderTargetBlendDescription();
				blendStateDescription.RenderTarget[i].IsBlendEnabled = (i < 4);
				blendStateDescription.IndependentBlendEnable = true;
			}

			nativeBlendStateDirty = true;
		}

		public void Apply(GraphicsDevice graphicsDevice)
		{
			GraphicsDeviceWindowsMetro gdMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
			var device = gdMetro.NativeDevice.NativeDevice;
			var context = gdMetro.NativeDevice.NativeContext;

			UpdateNativeBlendState(device);
			this.bound = true;

			context.OutputMerger.SetBlendState(nativeBlendState, this.blendFactor, this.multiSampleMask);
		}

		public void Release()
		{
			this.bound = false;
		}

		public void Dispose()
		{
			if (this.nativeBlendState != null)
			{
				this.nativeBlendState.Dispose();
				this.nativeBlendState = null;
			}
		}

		public bool IsBound
		{
			get
			{
				return this.bound;
			}
		}

		public Color BlendFactor
		{
			set
			{
				const float colorConvert = 1f / 255f;

				blendFactor.Red = value.R * colorConvert;
				blendFactor.Green = value.G * colorConvert;
				blendFactor.Blue = value.B * colorConvert;
				blendFactor.Alpha = value.A * colorConvert;
			}
		}

		public int MultiSampleMask
		{
			set
			{
				this.multiSampleMask = value;
			}
		}

		public BlendFunction AlphaBlendFunction
		{
			set
			{
				Dx11.BlendOperation alphaBlendOperation = FormatConverter.Translate(value);

				for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
				{
					if (blendStateDescription.RenderTarget[i].AlphaBlendOperation != alphaBlendOperation)
					{
						nativeBlendStateDirty = true;
						blendStateDescription.RenderTarget[i].AlphaBlendOperation = alphaBlendOperation;
					}

				}
			}
		}

		public BlendFunction ColorBlendFunction
		{
			set
			{
				Dx11.BlendOperation blendOperation = FormatConverter.Translate(value);

				for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
				{
					if (blendStateDescription.RenderTarget[i].BlendOperation != blendOperation)
					{
						nativeBlendStateDirty = true;
						blendStateDescription.RenderTarget[i].BlendOperation = blendOperation;
					}

				}
			}
		}

		public Blend AlphaDestinationBlend
		{
			set
			{
				Dx11.BlendOption destinationAlphaBlend = FormatConverter.Translate(value);

				for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
				{
					if (blendStateDescription.RenderTarget[i].DestinationAlphaBlend != destinationAlphaBlend)
					{
						nativeBlendStateDirty = true;
						blendStateDescription.RenderTarget[i].DestinationAlphaBlend = destinationAlphaBlend;
					}

				}
			}
		}

		public Blend ColorDestinationBlend
		{
			set
			{
				Dx11.BlendOption destinationBlend = FormatConverter.Translate(value);

				for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
				{
					if (blendStateDescription.RenderTarget[i].DestinationBlend != destinationBlend)
					{
						nativeBlendStateDirty = true;
						blendStateDescription.RenderTarget[i].DestinationBlend = destinationBlend;
					}

				}
			}
		}

		public ColorWriteChannels ColorWriteChannels
		{
			set
			{
				Dx11.ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

				//TODO: range check

				if (blendStateDescription.RenderTarget[0].RenderTargetWriteMask != renderTargetWriteMask)
				{
					nativeBlendStateDirty = true;
					blendStateDescription.RenderTarget[0].RenderTargetWriteMask = renderTargetWriteMask;
				}
			}
		}

		public ColorWriteChannels ColorWriteChannels1
		{
			set
			{
				Dx11.ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

				//TODO: range check

				if (blendStateDescription.RenderTarget[1].RenderTargetWriteMask != renderTargetWriteMask)
				{
					nativeBlendStateDirty = true;
					blendStateDescription.RenderTarget[1].RenderTargetWriteMask = renderTargetWriteMask;
				}
			}
		}

		public ColorWriteChannels ColorWriteChannels2
		{
			set
			{
				Dx11.ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

				//TODO: range check

				if (blendStateDescription.RenderTarget[2].RenderTargetWriteMask != renderTargetWriteMask)
				{
					nativeBlendStateDirty = true;
					blendStateDescription.RenderTarget[2].RenderTargetWriteMask = renderTargetWriteMask;
				}
			}
		}

		public ColorWriteChannels ColorWriteChannels3
		{
			set
			{
				Dx11.ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

				//TODO: range check

				if (blendStateDescription.RenderTarget[3].RenderTargetWriteMask != renderTargetWriteMask)
				{
					nativeBlendStateDirty = true;
					blendStateDescription.RenderTarget[3].RenderTargetWriteMask = renderTargetWriteMask;
				}
			}
		}

		public Blend AlphaSourceBlend
		{
			set
			{
				Dx11.BlendOption sourceAlphaBlend = FormatConverter.Translate(value);

				for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
				{
					if (blendStateDescription.RenderTarget[i].SourceAlphaBlend != sourceAlphaBlend)
					{
						nativeBlendStateDirty = true;
						blendStateDescription.RenderTarget[i].SourceAlphaBlend = sourceAlphaBlend;
					}

				}
			}
		}

		public Blend ColorSourceBlend
		{
			set
			{
				Dx11.BlendOption sourceBlend = FormatConverter.Translate(value);

				for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
				{
					if (blendStateDescription.RenderTarget[i].SourceBlend != sourceBlend)
					{
						nativeBlendStateDirty = true;
						blendStateDescription.RenderTarget[i].SourceBlend = sourceBlend;
					}

				}
			}
		}

		private void UpdateNativeBlendState(Dx11.Device device)
		{
			if (this.nativeBlendStateDirty == true || this.nativeBlendState == null)
			{
				if (this.nativeBlendState != null)
				{
					this.nativeBlendState.Dispose();
					this.nativeBlendState = null;
				}

				this.nativeBlendState = new Dx11.BlendState(device, ref this.blendStateDescription);

				this.nativeBlendStateDirty = false;
			}
		}
	}
}
