#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D10;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

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

namespace ANX.Framework.Windows.DX10
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
                BlendOperation alphaBlendOperation = Translate(value);

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
                BlendOperation blendOperation = Translate(value);

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
                BlendOption destinationAlphaBlend = Translate(value);

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
                BlendOption destinationBlend = Translate(value);

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
                ColorWriteMaskFlags renderTargetWriteMask = Translate(value);

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
                ColorWriteMaskFlags renderTargetWriteMask = Translate(value);

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
                ColorWriteMaskFlags renderTargetWriteMask = Translate(value);

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
                ColorWriteMaskFlags renderTargetWriteMask = Translate(value);

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
                BlendOption sourceAlphaBlend = Translate(value);

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
                BlendOption sourceBlend = Translate(value);

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

        private BlendOperation Translate(BlendFunction blendFunction)
        {
            switch (blendFunction)
            {
                case BlendFunction.Add:
                    return BlendOperation.Add;
                case BlendFunction.Max:
                    return BlendOperation.Maximum;
                case BlendFunction.Min:
                    return BlendOperation.Minimum;
                case BlendFunction.ReverseSubtract:
                    return BlendOperation.ReverseSubtract;
                case BlendFunction.Subtract:
                    return BlendOperation.Subtract;
            }

            throw new NotImplementedException();
        }

        private BlendOption Translate(Blend blend)
        {
            switch (blend)
            {
                case Blend.BlendFactor:
                    return BlendOption.BlendFactor;
                case Blend.DestinationAlpha:
                    return BlendOption.DestinationAlpha;
                case Blend.DestinationColor:
                    return BlendOption.DestinationColor;
                case Blend.InverseBlendFactor:
                    return BlendOption.InverseBlendFactor;
                case Blend.InverseDestinationAlpha:
                    return BlendOption.InverseDestinationAlpha;
                case Blend.InverseDestinationColor:
                    return BlendOption.InverseDestinationColor;
                case Blend.InverseSourceAlpha:
                    return BlendOption.InverseSourceAlpha;
                case Blend.InverseSourceColor:
                    return BlendOption.InverseSourceColor;
                case Blend.One:
                    return BlendOption.One;
                case Blend.SourceAlpha:
                    return BlendOption.SourceAlpha;
                case Blend.SourceAlphaSaturation:
                    return BlendOption.SourceAlphaSaturate;
                case Blend.SourceColor:
                    return BlendOption.SourceColor;
                case Blend.Zero:
                    return BlendOption.Zero;
            }

            throw new NotImplementedException();
        }

        private ColorWriteMaskFlags Translate(ColorWriteChannels colorWriteChannels)
        {
            switch (colorWriteChannels)
            {
                case ColorWriteChannels.All:
                    return ColorWriteMaskFlags.All;
                case ColorWriteChannels.Alpha:
                    return ColorWriteMaskFlags.Alpha;
                case ColorWriteChannels.Blue:
                    return ColorWriteMaskFlags.Blue;
                case ColorWriteChannels.Green:
                    return ColorWriteMaskFlags.Green;
                case ColorWriteChannels.Red:
                    return ColorWriteMaskFlags.Red;
            }

            throw new NotImplementedException();
        }
    }
}
