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

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

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
