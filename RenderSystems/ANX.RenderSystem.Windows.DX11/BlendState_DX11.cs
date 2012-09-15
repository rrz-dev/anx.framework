#region Using Statements
using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx11 = SharpDX.Direct3D11;

namespace ANX.RenderSystem.Windows.DX11
{
	public class BlendState_DX11 : BaseStateObject<Dx11.BlendState>, INativeBlendState
    {
		private Dx11.BlendStateDescription blendStateDescription;
        private SharpDX.Color4 blendFactor;
        private int multiSampleMask;

		#region Public
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
						isDirty = true;
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
						isDirty = true;
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
						isDirty = true;
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
						isDirty = true;
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
					isDirty = true;
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
					isDirty = true;
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
					isDirty = true;
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
					isDirty = true;
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
						isDirty = true;
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
						isDirty = true;
						blendStateDescription.RenderTarget[i].SourceBlend = sourceBlend;
					}

				}
			}
		}
		#endregion
		
		protected override void Init()
		{
			for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
			{
				blendStateDescription.RenderTarget[i].IsBlendEnabled = (i < 4);
				blendStateDescription.IndependentBlendEnable = true;
			}
		}

		protected override Dx11.BlendState CreateNativeState(GraphicsDevice graphics)
		{
			Dx11.DeviceContext context = (graphics.NativeDevice as GraphicsDeviceDX).NativeDevice;
			return new Dx11.BlendState(context.Device, ref blendStateDescription);
		}

		protected override void ApplyNativeState(GraphicsDevice graphics)
		{
			Dx11.DeviceContext context = (graphics.NativeDevice as GraphicsDeviceDX).NativeDevice;
			context.OutputMerger.SetBlendState(nativeState, blendFactor, multiSampleMask);
		}
    }
}
