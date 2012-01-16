#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using System.Runtime.InteropServices;

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
    public class BlendState : GraphicsResource
    {
        #region Private Members
        private INativeBlendState nativeBlendState;

        private BlendFunction alphaBlendFunction;
        private Blend alphaDestinationBlend;
        private Blend alphaSourceBlend;
        private Color blendFactor;
        private BlendFunction colorBlendFunction;
        private Blend colorDestinationBlend;
        private Blend colorSourceBlend;
        private ColorWriteChannels colorWriteChannels0;
        private ColorWriteChannels colorWriteChannels1;
        private ColorWriteChannels colorWriteChannels2;
        private ColorWriteChannels colorWriteChannels3;
        private int multiSampleMask;

        #endregion // Private Members

        public BlendState()
        {
            this.nativeBlendState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateBlendState();

            this.AlphaBlendFunction = BlendFunction.Add;
            this.AlphaDestinationBlend = Blend.One;
            this.AlphaSourceBlend = Blend.One;
            this.BlendFactor = Color.White;
            this.ColorBlendFunction = BlendFunction.Add;
            this.ColorDestinationBlend = Blend.One;
            this.ColorSourceBlend = Blend.One;
            this.ColorWriteChannels = ColorWriteChannels.All;
            this.ColorWriteChannels1 = ColorWriteChannels.All;
            this.ColorWriteChannels2 = ColorWriteChannels.All;
            this.ColorWriteChannels3 = ColorWriteChannels.All;
            this.MultiSampleMask = -1;
        }

        public static readonly BlendState Opaque;
        public static readonly BlendState AlphaBlend;
        public static readonly BlendState Additive;
        public static readonly BlendState NonPremultiplied;

        private BlendState(Blend sourceBlend, Blend destinationBlend, string name)
        {
            this.nativeBlendState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateBlendState();

            this.AlphaBlendFunction = BlendFunction.Add;
            this.AlphaDestinationBlend = destinationBlend;
            this.AlphaSourceBlend = sourceBlend;
            this.BlendFactor = Color.White;
            this.ColorBlendFunction = BlendFunction.Add;
            this.ColorDestinationBlend = destinationBlend;
            this.ColorSourceBlend = sourceBlend;
            this.ColorWriteChannels = ColorWriteChannels.All;
            this.ColorWriteChannels1 = ColorWriteChannels.All;
            this.ColorWriteChannels2 = ColorWriteChannels.All;
            this.ColorWriteChannels3 = ColorWriteChannels.All;
            this.MultiSampleMask = -1;
            
            Name = name;
        }

        static BlendState()
        {
            Opaque = new BlendState(Blend.One, Blend.Zero, "BlendState.Opaque");
            AlphaBlend = new BlendState(Blend.One, Blend.InverseSourceAlpha, "BlendState.AlphaBlend");
            Additive = new BlendState(Blend.SourceAlpha, Blend.One, "BlendState.Additive");
            NonPremultiplied = new BlendState(Blend.SourceAlpha, Blend.InverseSourceAlpha, "BlendState.NonPremultiplied");
        }

        internal INativeBlendState NativeBlendState
        {
            get
            {
                return this.nativeBlendState;
            }
        }

        public BlendFunction AlphaBlendFunction
        {
            get
            {
                return this.alphaBlendFunction;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.alphaBlendFunction = value;
                this.nativeBlendState.AlphaBlendFunction = value;
            }
        }

        public Blend AlphaDestinationBlend
        {
            get
            {
                return this.alphaDestinationBlend;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.alphaDestinationBlend = value;
                this.nativeBlendState.AlphaDestinationBlend = value;
            }
        }

        public Blend AlphaSourceBlend
        {
            get
            {
                return this.alphaSourceBlend;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.alphaSourceBlend = value;
                this.nativeBlendState.AlphaSourceBlend = value;
            }
        }

        public Color BlendFactor
        {
            get
            {
                return this.blendFactor;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.blendFactor = value;
                this.nativeBlendState.BlendFactor = value;
            }
        }

        public BlendFunction ColorBlendFunction
        {
            get
            {
                return this.colorBlendFunction;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.colorBlendFunction = value;
                this.nativeBlendState.ColorBlendFunction = value;
            }
        }

        public Blend ColorDestinationBlend
        {
            get
            {
                return this.colorDestinationBlend;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.colorDestinationBlend = value;
                this.nativeBlendState.ColorDestinationBlend = value;
            }
        }

        public Blend ColorSourceBlend
        {
            get
            {
                return this.colorSourceBlend;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.colorSourceBlend = value;
                this.nativeBlendState.ColorSourceBlend = value;
            }
        }

        public ColorWriteChannels ColorWriteChannels
        {
            get
            {
                return this.colorWriteChannels0;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.colorWriteChannels0 = value;
                this.nativeBlendState.ColorWriteChannels = value;
            }
        }

        public ColorWriteChannels ColorWriteChannels1
        {
            get
            {
                return this.colorWriteChannels1;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.colorWriteChannels1 = value;
                this.nativeBlendState.ColorWriteChannels1 = value;
            }
        }

        public ColorWriteChannels ColorWriteChannels2
        {
            get
            {
                return this.colorWriteChannels2;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.colorWriteChannels2 = value;
                this.nativeBlendState.ColorWriteChannels2 = value;
            }
        }

        public ColorWriteChannels ColorWriteChannels3
        {
            get
            {
                return this.colorWriteChannels3;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.colorWriteChannels3 = value;
                this.nativeBlendState.ColorWriteChannels3 = value;
            }
        }

        public int MultiSampleMask
        {
            get
            {
                return this.multiSampleMask;
            }
            set
            {
                if (this.nativeBlendState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change BlendState properties while it is bound to the GraphicsDevice.");
                }

                this.multiSampleMask = value;
                this.nativeBlendState.MultiSampleMask = value;
            }
        }

        public override void Dispose()
        {
            if (this.nativeBlendState != null)
            {
                this.nativeBlendState.Dispose();
                this.nativeBlendState = null;
            }
        }

        protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            base.Dispose(disposeManaged);
        }
    }
}
