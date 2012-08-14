using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class BlendState_Metro : BaseStateObject, INativeBlendState
	{
		#region Private
		private Dx11.BlendStateDescription blendStateDescription;
		private Dx11.BlendState nativeBlendState;
		private SharpDX.Color4 blendFactor;
		private int multiSampleMask;
		#endregion

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
					SetValueIfDifferentAndMarkDirty(
						ref blendStateDescription.RenderTarget[i].AlphaBlendOperation,
						ref alphaBlendOperation);
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
					SetValueIfDifferentAndMarkDirty(
						ref blendStateDescription.RenderTarget[i].BlendOperation,
						ref blendOperation);
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
					SetValueIfDifferentAndMarkDirty(
						ref blendStateDescription.RenderTarget[i].DestinationAlphaBlend,
						ref destinationAlphaBlend);
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
					SetValueIfDifferentAndMarkDirty(
						ref blendStateDescription.RenderTarget[i].DestinationBlend,
						ref destinationBlend);
				}
			}
		}

		public ColorWriteChannels ColorWriteChannels
		{
			set
			{
				Dx11.ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);
				//TODO: range check
				SetValueIfDifferentAndMarkDirty(
					ref blendStateDescription.RenderTarget[0].RenderTargetWriteMask,
					ref renderTargetWriteMask);
			}
		}

		public ColorWriteChannels ColorWriteChannels1
		{
			set
			{
				Dx11.ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);
				//TODO: range check
				SetValueIfDifferentAndMarkDirty(
					ref blendStateDescription.RenderTarget[1].RenderTargetWriteMask,
					ref renderTargetWriteMask);
			}
		}

		public ColorWriteChannels ColorWriteChannels2
		{
			set
			{
				Dx11.ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);
				//TODO: range check
				SetValueIfDifferentAndMarkDirty(
					ref blendStateDescription.RenderTarget[2].RenderTargetWriteMask,
					ref renderTargetWriteMask);
			}
		}

		public ColorWriteChannels ColorWriteChannels3
		{
			set
			{
				Dx11.ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);
				//TODO: range check
				SetValueIfDifferentAndMarkDirty(
					ref blendStateDescription.RenderTarget[3].RenderTargetWriteMask,
					ref renderTargetWriteMask);
			}
		}

		public Blend AlphaSourceBlend
		{
			set
			{
				Dx11.BlendOption sourceAlphaBlend = FormatConverter.Translate(value);

				for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
				{
					SetValueIfDifferentAndMarkDirty(
						ref blendStateDescription.RenderTarget[i].SourceAlphaBlend,
						ref sourceAlphaBlend);
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
					SetValueIfDifferentAndMarkDirty(
						ref blendStateDescription.RenderTarget[i].SourceBlend,
						ref sourceBlend);
				}
			}
		}
		#endregion

		#region Constructor
		public BlendState_Metro()
			: base()
		{
			for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
			{
				blendStateDescription.RenderTarget[i] = new Dx11.RenderTargetBlendDescription();
				blendStateDescription.RenderTarget[i].IsBlendEnabled = (i < 4);
				blendStateDescription.IndependentBlendEnable = true;
			}
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
		{
			UpdateNativeBlendState();
			this.bound = true;

			NativeDxDevice.Current.OutputMerger.SetBlendState(
				nativeBlendState, this.blendFactor, this.multiSampleMask);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (this.nativeBlendState != null)
			{
				this.nativeBlendState.Dispose();
				this.nativeBlendState = null;
			}
		}
		#endregion

		#region UpdateNativeBlendState
		private void UpdateNativeBlendState()
		{
			if (isDirty == true || nativeBlendState == null)
			{
				if (nativeBlendState != null)
				{
					nativeBlendState.Dispose();
					nativeBlendState = null;
				}

				nativeBlendState = new Dx11.BlendState(
					NativeDxDevice.Current.NativeDevice,
					ref blendStateDescription);

				isDirty = false;
			}
		}
		#endregion
	}
}
