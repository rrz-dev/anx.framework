#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D10;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class BlendState_DX10 : INativeBlendState
    {
        #region Private Members
        private BlendStateDescription description;
        private SharpDX.Direct3D10.BlendState nativeBlendState;
        private bool nativeBlendStateDirty;
        private SharpDX.Color4 blendFactor;
        private int multiSampleMask;
        private bool bound;

        #endregion // Private Members

        public BlendState_DX10()
        {
            this.description = new BlendStateDescription();

            for (int i = 0; i < description.IsBlendEnabled.Length; i++)
            {
                description.IsBlendEnabled[i] = (i < 4);
            }

            nativeBlendStateDirty = true;
        }

        public void Apply(GraphicsDevice graphics)
        {
            GraphicsDeviceWindowsDX10 gdx10 = graphics.NativeDevice as GraphicsDeviceWindowsDX10;
            SharpDX.Direct3D10.Device device = gdx10.NativeDevice;

            UpdateNativeBlendState(device);
            this.bound = true;

            device.OutputMerger.SetBlendState(nativeBlendState, this.blendFactor, this.multiSampleMask);
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
                BlendOperation alphaBlendOperation = FormatConverter.Translate(value);

                if (description.AlphaBlendOperation != alphaBlendOperation)
                {
                    nativeBlendStateDirty = true;
                    description.AlphaBlendOperation = alphaBlendOperation;
                }
            }
        }

        public BlendFunction ColorBlendFunction
        {
            set
            {
                BlendOperation blendOperation = FormatConverter.Translate(value);

                if (description.BlendOperation != blendOperation)
                {
                    nativeBlendStateDirty = true;
                    description.BlendOperation = blendOperation;
                }
            }
        }

        public Blend AlphaDestinationBlend
        {
            set
            {
                BlendOption destinationAlphaBlend = FormatConverter.Translate(value);

                if (description.DestinationAlphaBlend != destinationAlphaBlend)
                {
                    nativeBlendStateDirty = true;
                    description.DestinationAlphaBlend = destinationAlphaBlend;
                }
            }
        }

        public Blend ColorDestinationBlend
        {
            set
            {
                BlendOption destinationBlend = FormatConverter.Translate(value);

                if (description.DestinationBlend != destinationBlend)
                {
                    nativeBlendStateDirty = true;
                    description.DestinationBlend = destinationBlend;
                }
            }
        }

        public ColorWriteChannels ColorWriteChannels
        {
            set
            {
                ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

                if (description.RenderTargetWriteMask[0] != renderTargetWriteMask)
                {
                    nativeBlendStateDirty = true;
                    description.RenderTargetWriteMask[0] = renderTargetWriteMask;
                }
            }
        }

        public ColorWriteChannels ColorWriteChannels1
        {
            set
            {
                ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

                if (description.RenderTargetWriteMask[1] != renderTargetWriteMask)
                {
                    nativeBlendStateDirty = true;
                    description.RenderTargetWriteMask[1] = renderTargetWriteMask;
                }
            }
        }

        public ColorWriteChannels ColorWriteChannels2
        {
            set
            {
                ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

                if (description.RenderTargetWriteMask[2] != renderTargetWriteMask)
                {
                    nativeBlendStateDirty = true;
                    description.RenderTargetWriteMask[2] = renderTargetWriteMask;
                }
            }
        }

        public ColorWriteChannels ColorWriteChannels3
        {
            set
            {
                ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

                if (description.RenderTargetWriteMask[3] != renderTargetWriteMask)
                {
                    nativeBlendStateDirty = true;
                    description.RenderTargetWriteMask[3] = renderTargetWriteMask;
                }
            }
        }

        public Blend AlphaSourceBlend
        {
            set
            {
                BlendOption sourceAlphaBlend = FormatConverter.Translate(value);

                if (description.SourceAlphaBlend != sourceAlphaBlend)
                {
                    nativeBlendStateDirty = true;
                    description.SourceAlphaBlend = sourceAlphaBlend;
                }
            }
        }

        public Blend ColorSourceBlend
        {
            set
            {
                BlendOption sourceBlend = FormatConverter.Translate(value);

                if (description.SourceBlend != sourceBlend)
                {
                    nativeBlendStateDirty = true;
                    description.SourceBlend = sourceBlend;
                }
            }
        }

        private void UpdateNativeBlendState(SharpDX.Direct3D10.Device device)
        {
            if (this.nativeBlendStateDirty == true || this.nativeBlendState == null)
            {
                if (this.nativeBlendState != null)
                {
                    this.nativeBlendState.Dispose();
                    this.nativeBlendState = null;
                }

                this.nativeBlendState = new SharpDX.Direct3D10.BlendState(device, ref this.description);

                this.nativeBlendStateDirty = false;
            }
        }
    }
}
