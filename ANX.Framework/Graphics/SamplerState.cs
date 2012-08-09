#region Using Statements
using System;
using ANX.Framework.NonXNA;
using System.Runtime.InteropServices;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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
            this.nativeSamplerState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateSamplerState();

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
            this.nativeSamplerState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateSamplerState();

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

				protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            base.Dispose(disposeManaged);
        }
    }
}
