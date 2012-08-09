#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D11;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
    public class BlendState_DX11 : INativeBlendState
    {
        #region Private Members
        private BlendStateDescription blendStateDescription;
        private SharpDX.Direct3D11.BlendState nativeBlendState;
        private bool nativeBlendStateDirty;
        private SharpDX.Color4 blendFactor;
        private int multiSampleMask;
        private bool bound;

        #endregion // Private Members

        public BlendState_DX11()
        {
            for (int i = 0; i < blendStateDescription.RenderTarget.Length; i++)
            {
                blendStateDescription.RenderTarget[i] = new RenderTargetBlendDescription();
                blendStateDescription.RenderTarget[i].IsBlendEnabled = (i < 4);
                blendStateDescription.IndependentBlendEnable = true;
            }

            nativeBlendStateDirty = true;
        }

        public void Apply(GraphicsDevice graphics)
        {
            GraphicsDeviceWindowsDX11 gdx11 = graphics.NativeDevice as GraphicsDeviceWindowsDX11;
            SharpDX.Direct3D11.DeviceContext context = gdx11.NativeDevice;

            UpdateNativeBlendState(context.Device);
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
                BlendOperation alphaBlendOperation = FormatConverter.Translate(value);

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
                BlendOperation blendOperation = FormatConverter.Translate(value);

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
                BlendOption destinationAlphaBlend = FormatConverter.Translate(value);

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
                BlendOption destinationBlend = FormatConverter.Translate(value);

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
                ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

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
                ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

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
                ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

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
                ColorWriteMaskFlags renderTargetWriteMask = FormatConverter.Translate(value);

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
                BlendOption sourceAlphaBlend = FormatConverter.Translate(value);

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
                BlendOption sourceBlend = FormatConverter.Translate(value);

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

        private void UpdateNativeBlendState(SharpDX.Direct3D11.Device device)
        {
            if (this.nativeBlendStateDirty == true || this.nativeBlendState == null)
            {
                if (this.nativeBlendState != null)
                {
                    this.nativeBlendState.Dispose();
                    this.nativeBlendState = null;
                }

                this.nativeBlendState = new SharpDX.Direct3D11.BlendState(device, ref this.blendStateDescription);

                this.nativeBlendStateDirty = false;
            }
        }
    }
}
