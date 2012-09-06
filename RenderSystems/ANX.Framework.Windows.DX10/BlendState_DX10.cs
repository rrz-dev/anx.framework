using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10.Helpers;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class BlendState_DX10 : INativeBlendState
    {
		private const float ColorByteToFloatFactor = 1f / 255f;

        #region Private
		private Dx10.BlendStateDescription description;
        private Dx10.BlendState nativeBlendState;
        private bool isDirty;
        private SharpDX.Color4 blendFactor;
        private int multiSampleMask;
		#endregion

		#region Public
		public bool IsBound
		{
			get;
			private set;
		}

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
				UpdateValueAndMarkDirtyIfNeeded(ref description.AlphaBlendOperation, ref alphaBlendOperation);
			}
		}

		public BlendFunction ColorBlendFunction
		{
			set
			{
				Dx10.BlendOperation blendOperation = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.BlendOperation, ref blendOperation);
			}
		}

		public Blend AlphaDestinationBlend
		{
			set
			{
				Dx10.BlendOption destinationAlphaBlend = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.DestinationAlphaBlend, ref destinationAlphaBlend);
			}
		}

		public Blend ColorDestinationBlend
		{
			set
			{
				Dx10.BlendOption destinationBlend = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.DestinationBlend, ref destinationBlend);
			}
		}

		public ColorWriteChannels ColorWriteChannels
		{
			set
			{
				Dx10.ColorWriteMaskFlags writeMask = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.RenderTargetWriteMask[0], ref writeMask);
			}
		}

		public ColorWriteChannels ColorWriteChannels1
		{
			set
			{
				Dx10.ColorWriteMaskFlags writeMask = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.RenderTargetWriteMask[1], ref writeMask);
			}
		}

		public ColorWriteChannels ColorWriteChannels2
		{
			set
			{
				Dx10.ColorWriteMaskFlags writeMask = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.RenderTargetWriteMask[2], ref writeMask);
			}
		}

		public ColorWriteChannels ColorWriteChannels3
		{
			set
			{
				Dx10.ColorWriteMaskFlags writeMask = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.RenderTargetWriteMask[3], ref writeMask);
			}
		}

		public Blend AlphaSourceBlend
		{
			set
			{
				Dx10.BlendOption sourceAlphaBlend = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.SourceAlphaBlend, ref sourceAlphaBlend);
			}
		}

		public Blend ColorSourceBlend
		{
			set
			{
				Dx10.BlendOption sourceBlend = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.SourceBlend, ref sourceBlend);
			}
		}
		#endregion

		#region Constructor
		public BlendState_DX10()
		{
			isDirty = true;
            for (int i = 0; i < description.IsBlendEnabled.Length; i++)
                description.IsBlendEnabled[i] = (i < 4);
        }
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphics)
        {
            Dx10.Device device = (graphics.NativeDevice as GraphicsDeviceWindowsDX10).NativeDevice;

            UpdateNativeBlendState(device);
			IsBound = true;

            device.OutputMerger.SetBlendState(nativeBlendState, blendFactor, multiSampleMask);
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
            if (nativeBlendState != null)
            {
                nativeBlendState.Dispose();
                nativeBlendState = null;
            }
		}
		#endregion

		#region UpdateNativeBlendState
		private void UpdateNativeBlendState(Dx10.Device device)
        {
			if (isDirty || nativeBlendState == null)
            {
				Dispose();
                nativeBlendState = new Dx10.BlendState(device, ref description);
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
