#region Using Statements
using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10.Helpers;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx10 = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
    public class BlendState_DX10 : BaseStateObject<Dx10.BlendState>, INativeBlendState
    {
		private Dx10.BlendStateDescription description;
        private SharpDX.Color4 blendFactor;
        private int multiSampleMask;

#if DEBUG
        private static int blendStateCount = 0;
#endif

		#region Public
		public Color BlendFactor
		{
			set
			{
				blendFactor.Red = value.R * ColorByteToFloatFactor;
				blendFactor.Green = value.G * ColorByteToFloatFactor;
				blendFactor.Blue = value.B * ColorByteToFloatFactor;
				blendFactor.Alpha = value.A * ColorByteToFloatFactor;
			}
		}

		public int MultiSampleMask
		{
			set
			{
				multiSampleMask = value;
			}
		}

		public BlendFunction AlphaBlendFunction
		{
			set
			{
				Dx10.BlendOperation alphaBlendOperation = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.AlphaBlendOperation, ref alphaBlendOperation);
			}
		}

		public BlendFunction ColorBlendFunction
		{
			set
			{
				Dx10.BlendOperation blendOperation = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.BlendOperation, ref blendOperation);
			}
		}

		public Blend AlphaDestinationBlend
		{
			set
			{
				Dx10.BlendOption destinationAlphaBlend = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.DestinationAlphaBlend, ref destinationAlphaBlend);
			}
		}

		public Blend ColorDestinationBlend
		{
			set
			{
				Dx10.BlendOption destinationBlend = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.DestinationBlend, ref destinationBlend);
			}
		}

		public ColorWriteChannels ColorWriteChannels
		{
			set
			{
				Dx10.ColorWriteMaskFlags writeMask = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.RenderTargetWriteMask[0], ref writeMask);
			}
		}

		public ColorWriteChannels ColorWriteChannels1
		{
			set
			{
				Dx10.ColorWriteMaskFlags writeMask = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.RenderTargetWriteMask[1], ref writeMask);
			}
		}

		public ColorWriteChannels ColorWriteChannels2
		{
			set
			{
				Dx10.ColorWriteMaskFlags writeMask = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.RenderTargetWriteMask[2], ref writeMask);
			}
		}

		public ColorWriteChannels ColorWriteChannels3
		{
			set
			{
				Dx10.ColorWriteMaskFlags writeMask = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.RenderTargetWriteMask[3], ref writeMask);
			}
		}

		public Blend AlphaSourceBlend
		{
			set
			{
				Dx10.BlendOption sourceAlphaBlend = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.SourceAlphaBlend, ref sourceAlphaBlend);
			}
		}

		public Blend ColorSourceBlend
		{
			set
			{
				Dx10.BlendOption sourceBlend = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.SourceBlend, ref sourceBlend);
			}
		}
		#endregion

		protected override void Init()
		{
			for (int i = 0; i < description.IsBlendEnabled.Length; i++)
				description.IsBlendEnabled[i] = (i < 4);
		}

		protected override Dx10.BlendState CreateNativeState(GraphicsDevice graphics)
		{
			Dx10.Device device = (graphics.NativeDevice as GraphicsDeviceDX).NativeDevice;
			var blendState = new Dx10.BlendState(device, ref description);
#if DEBUG
            blendState.DebugName = "BlendState_" + blendStateCount++;
#endif
            return blendState;
		}

		protected override void ApplyNativeState(GraphicsDevice graphics)
		{
			Dx10.Device device = (graphics.NativeDevice as GraphicsDeviceDX).NativeDevice;
			device.OutputMerger.SetBlendState(nativeState, blendFactor, multiSampleMask);
		}
	}
}
