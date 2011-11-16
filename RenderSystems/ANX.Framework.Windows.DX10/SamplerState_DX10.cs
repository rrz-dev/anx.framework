#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using SharpDX.Direct3D10;

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
    public class SamplerState_DX10 : INativeSamplerState
    {
        #region Private Members
        private SamplerStateDescription description;
        private SharpDX.Direct3D10.SamplerState nativeSamplerState;
        private bool nativeSamplerStateDirty;
        private bool bound;

        #endregion // Private Members

        public SamplerState_DX10()
        {
            this.description = new SamplerStateDescription();

            this.nativeSamplerStateDirty = true;
        }

        public void Apply(GraphicsDevice graphicsDevice, int index)
        {
            GraphicsDeviceWindowsDX10 gdx10 = graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10;
            Device device = gdx10.NativeDevice;

            UpdateNativeSamplerState(device);
            this.bound = true;

            device.PixelShader.SetSampler(index, this.nativeSamplerState);
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

        public ANX.Framework.Graphics.TextureAddressMode AddressU
        {
            set 
            {
                SharpDX.Direct3D10.TextureAddressMode mode = FormatConverter.Translate(value);

                if (description.AddressU != mode)
                {
                    description.AddressU = mode;
                    nativeSamplerStateDirty = true;
                }
            }
        }

        public ANX.Framework.Graphics.TextureAddressMode AddressV
        {
            set
            {
                SharpDX.Direct3D10.TextureAddressMode mode = FormatConverter.Translate(value);

                if (description.AddressV != mode)
                {
                    description.AddressV = mode;
                    nativeSamplerStateDirty = true;
                }
            }
        }

        public ANX.Framework.Graphics.TextureAddressMode AddressW
        {
            set
            {
                SharpDX.Direct3D10.TextureAddressMode mode = FormatConverter.Translate(value);

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
                SharpDX.Direct3D10.Filter filter = FormatConverter.Translate(value);

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

        private void UpdateNativeSamplerState(Device device)
        {
            if (this.nativeSamplerStateDirty == true || this.nativeSamplerState == null)
            {
                if (this.nativeSamplerState != null)
                {
                    this.nativeSamplerState.Dispose();
                    this.nativeSamplerState = null;
                }

                this.nativeSamplerState = new SharpDX.Direct3D10.SamplerState(device, ref this.description);

                this.nativeSamplerStateDirty = false;
            }
        }
    }
}
