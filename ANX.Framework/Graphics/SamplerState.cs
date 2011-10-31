#region Using Statements
using System;
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

namespace ANX.Framework.Graphics
{
    public class SamplerState : GraphicsResource
    {
        #region Private Members
        private INativeSamplerState nativeSamplerState;

        private TextureAddressMode addressU;
        private TextureAddressMode addressV;
        private TextureAddressMode addressW;
        private TextureFilter filter;
        private int maxAnisotropy;
        private int maxMipLevel;
        private float mipMapLevelOfDetailBias;

        #endregion // Private Members

        public static readonly SamplerState AnisotropicClamp;
        public static readonly SamplerState AnisotropicWrap;
        public static readonly SamplerState LinearClamp;
        public static readonly SamplerState LinearWrap;
        public static readonly SamplerState PointClamp;
        public static readonly SamplerState PointWrap;

        public SamplerState()
        {
            this.nativeSamplerState = AddInSystemFactory.Instance.GetCurrentCreator<IRenderSystemCreator>().CreateSamplerState();

            this.AddressU = TextureAddressMode.Wrap;
            this.AddressV = TextureAddressMode.Wrap;
            this.AddressW = TextureAddressMode.Wrap;
            this.Filter = TextureFilter.Linear;
            this.MaxAnisotropy = 0;
            this.MaxMipLevel = 0;
            this.MipMapLevelOfDetailBias = 0f;
        }

        private SamplerState(TextureFilter filter, TextureAddressMode addressMode, string name)
        {
            this.nativeSamplerState = AddInSystemFactory.Instance.GetCurrentCreator<IRenderSystemCreator>().CreateSamplerState();

            this.AddressU = addressMode;
            this.AddressV = addressMode;
            this.AddressW = addressMode;
            this.Filter = filter;
            this.MaxAnisotropy = 0;
            this.MaxMipLevel = 0;
            this.MipMapLevelOfDetailBias = 0f;

            Name = name;
        }

        static SamplerState()
        {
            PointWrap = new SamplerState(TextureFilter.Point, TextureAddressMode.Wrap, "SamplerState.PointWrap");
            PointClamp = new SamplerState(TextureFilter.Point, TextureAddressMode.Clamp, "SamplerState.PointClamp");
            LinearWrap = new SamplerState(TextureFilter.Linear, TextureAddressMode.Wrap, "SamplerState.LinearWrap");
            LinearClamp = new SamplerState(TextureFilter.Linear, TextureAddressMode.Clamp, "SamplerState.LinearClamp");
            AnisotropicWrap = new SamplerState(TextureFilter.Anisotropic, TextureAddressMode.Wrap, "SamplerState.AnisotropicWrap");
            AnisotropicClamp = new SamplerState(TextureFilter.Anisotropic, TextureAddressMode.Clamp, "SamplerState.AnisotropicClamp");
        }

        internal INativeSamplerState NativeSamplerState
        {
            get
            {
                return this.nativeSamplerState;
            }
        }

        public TextureAddressMode AddressU
        {
            get
            {
                return this.addressU;
            }
            set
            {
                if (this.nativeSamplerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change SamplerState properties while it is bound to the GraphicsDevice.");
                }

                this.addressU = value;
                this.nativeSamplerState.AddressU = value;
            }
        }

        public TextureAddressMode AddressV
        {
            get
            {
                return this.addressV;
            }
            set
            {
                if (this.nativeSamplerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change SamplerState properties while it is bound to the GraphicsDevice.");
                }

                this.addressV = value;
                this.nativeSamplerState.AddressV = value;
            }
        }

        public TextureAddressMode AddressW
        {
            get
            {
                return this.addressW;
            }
            set
            {
                if (this.nativeSamplerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change SamplerState properties while it is bound to the GraphicsDevice.");
                }

                this.addressW = value;
                this.nativeSamplerState.AddressW = value;
            }
        }

        public TextureFilter Filter
        {
            get
            {
                return this.filter;
            }
            set
            {
                if (this.nativeSamplerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change SamplerState properties while it is bound to the GraphicsDevice.");
                }

                this.filter = value;
                this.nativeSamplerState.Filter = value;
            }
        }

        public int MaxAnisotropy
        {
            get
            {
                return this.maxAnisotropy;
            }
            set
            {
                if (this.nativeSamplerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change SamplerState properties while it is bound to the GraphicsDevice.");
                }

                this.maxAnisotropy = value;
                this.nativeSamplerState.MaxAnisotropy = value;
            }
        }

        public int MaxMipLevel
        {
            get
            {
                return this.maxMipLevel;
            }
            set
            {
                if (this.nativeSamplerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change SamplerState properties while it is bound to the GraphicsDevice.");
                }

                this.maxMipLevel = value;
                this.nativeSamplerState.MaxMipLevel = value;
            }
        }

        public float MipMapLevelOfDetailBias
        {
            get
            {
                return this.mipMapLevelOfDetailBias;
            }
            set
            {
                if (this.nativeSamplerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change SamplerState properties while it is bound to the GraphicsDevice.");
                }

                this.mipMapLevelOfDetailBias = value;
                this.nativeSamplerState.MipMapLevelOfDetailBias = value;
            }
        }

        public override void Dispose()
        {
            if (this.nativeSamplerState != null)
            {
                this.nativeSamplerState.Dispose();
                this.nativeSamplerState = null;
            }
        }

        protected override void Dispose(Boolean disposeManaged)
        {
            base.Dispose(disposeManaged);
        }
    }
}
