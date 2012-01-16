#region Using Statements
using System;
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
    public class DepthStencilState : GraphicsResource
    {
        #region Private Members
        private INativeDepthStencilState nativeDepthStencilState;

        private StencilOperation counterClockwiseStencilDepthBufferFail;
        private StencilOperation counterClockwiseStencilFail;
        private CompareFunction counterClockwiseStencilFunction;
        private StencilOperation counterClockwiseStencilPass;
        private bool depthBufferEnable;
        private CompareFunction depthBufferFunction;
        private bool depthBufferWriteEnable;
        private int referenceStencil;
        private StencilOperation stencilDepthBufferFail;
        private bool stencilEnable;
        private StencilOperation stencilFail;
        private CompareFunction stencilFunction;
        private int stencilMask;
        private StencilOperation stencilPass;
        private int stencilWriteMask;
        private bool twoSidedStencilMode;

        #endregion

        public static readonly DepthStencilState Default;
        public static readonly DepthStencilState DepthRead;
        public static readonly DepthStencilState None;

        public DepthStencilState()
        {
            nativeDepthStencilState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateDepthStencilState();

            // BackFace
            CounterClockwiseStencilDepthBufferFail = StencilOperation.Keep;
            CounterClockwiseStencilFail = StencilOperation.Keep;
            CounterClockwiseStencilFunction = CompareFunction.Always;
            CounterClockwiseStencilPass = StencilOperation.Keep;

            // FrontFace
            StencilDepthBufferFail = StencilOperation.Keep;
            StencilFail = StencilOperation.Keep;
            StencilFunction = CompareFunction.Always;
            StencilPass = StencilOperation.Keep;

            DepthBufferEnable = true;
            DepthBufferFunction = CompareFunction.LessEqual;
            DepthBufferWriteEnable = true;
            ReferenceStencil = 0;
            StencilEnable = false;
            StencilMask = int.MaxValue;
            StencilWriteMask = int.MaxValue;
            TwoSidedStencilMode = false;
        }

        private DepthStencilState(bool depthBufferEnabled, bool depthBufferWriteEnabled, string name)
        {
            nativeDepthStencilState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateDepthStencilState();

            // BackFace
            CounterClockwiseStencilDepthBufferFail = StencilOperation.Keep;
            CounterClockwiseStencilFail = StencilOperation.Keep;
            CounterClockwiseStencilFunction = CompareFunction.Always;
            CounterClockwiseStencilPass = StencilOperation.Keep;
            
            // FrontFace
            StencilDepthBufferFail = StencilOperation.Keep;
            StencilFail = StencilOperation.Keep;
            StencilFunction = CompareFunction.Always;
            StencilPass = StencilOperation.Keep;

            DepthBufferEnable = depthBufferEnabled;
            DepthBufferFunction = CompareFunction.LessEqual;
            DepthBufferWriteEnable = depthBufferWriteEnabled;
            ReferenceStencil = 0;
            StencilEnable = false;
            StencilMask = int.MaxValue;
            StencilWriteMask = int.MaxValue;
            TwoSidedStencilMode = false;

            Name = name;
        }

        static DepthStencilState()
        {
            None = new DepthStencilState(false, false, "DepthStencilState.None");
            Default = new DepthStencilState(true, true, "DepthStencilState.Default");
            DepthRead = new DepthStencilState(true, false, "DepthStencilState.DepthRead");
        }

        internal INativeDepthStencilState NativeDepthStencilState
        {
            get
            {
                return this.nativeDepthStencilState;
            }
        }

        public StencilOperation CounterClockwiseStencilDepthBufferFail
        {
            get
            {
                return this.counterClockwiseStencilDepthBufferFail;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.counterClockwiseStencilDepthBufferFail = value;
                this.nativeDepthStencilState.CounterClockwiseStencilDepthBufferFail = value;
            }
        }

        public StencilOperation CounterClockwiseStencilFail
        {
            get
            {
                return this.counterClockwiseStencilFail;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.counterClockwiseStencilFail = value;
                this.nativeDepthStencilState.CounterClockwiseStencilFail = value;
            }
        }

        public CompareFunction CounterClockwiseStencilFunction
        {
            get
            {
                return this.counterClockwiseStencilFunction;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.counterClockwiseStencilFunction = value;
                this.nativeDepthStencilState.CounterClockwiseStencilFunction = value;
            }
        }

        public StencilOperation CounterClockwiseStencilPass
        {
            get
            {
                return this.counterClockwiseStencilPass;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.counterClockwiseStencilPass = value;
                this.nativeDepthStencilState.CounterClockwiseStencilPass = value;
            }
        }

        public bool DepthBufferEnable
        {
            get
            {
                return this.depthBufferEnable;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.depthBufferEnable = value;
                this.nativeDepthStencilState.DepthBufferEnable = value;
            }
        }

        public CompareFunction DepthBufferFunction
        {
            get
            {
                return this.depthBufferFunction;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.depthBufferFunction = value;
                this.nativeDepthStencilState.DepthBufferFunction = value;
            }
        }

        public bool DepthBufferWriteEnable
        {
            get
            {
                return this.depthBufferWriteEnable;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.depthBufferWriteEnable = value;
                this.nativeDepthStencilState.DepthBufferWriteEnable = value;
            }
        }

        public int ReferenceStencil
        {
            get
            {
                return this.referenceStencil;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.referenceStencil = value;
                this.nativeDepthStencilState.ReferenceStencil = value;
            }
        }

        public StencilOperation StencilDepthBufferFail
        {
            get
            {
                return this.stencilDepthBufferFail;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.stencilDepthBufferFail = value;
                this.nativeDepthStencilState.StencilDepthBufferFail = value;
            }
        }

        public bool StencilEnable
        {
            get
            {
                return this.stencilEnable;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.stencilEnable = value;
                this.nativeDepthStencilState.StencilEnable = value;
            }
        }

        public StencilOperation StencilFail
        {
            get
            {
                return this.stencilFail;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.stencilFail = value;
                this.nativeDepthStencilState.StencilFail = value;
            }
        }

        public CompareFunction StencilFunction
        {
            get
            {
                return this.stencilFunction;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.stencilFunction = value;
                this.nativeDepthStencilState.StencilFunction = value;
            }
        }

        public int StencilMask
        {
            get
            {
                return this.stencilMask;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.stencilMask = value;
                this.nativeDepthStencilState.StencilMask = value;
            }
        }

        public StencilOperation StencilPass
        {
            get
            {
                return this.stencilPass;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.stencilPass = value;
                this.nativeDepthStencilState.StencilPass = value;
            }
        }

        public int StencilWriteMask
        {
            get
            {
                return this.stencilWriteMask;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.stencilWriteMask = value;
                this.nativeDepthStencilState.StencilWriteMask = value;
            }
        }

        public bool TwoSidedStencilMode
        {
            get
            {
                return this.twoSidedStencilMode;
            }
            set
            {
                if (this.nativeDepthStencilState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change DepthStencilState properties while it is bound to the GraphicsDevice.");
                }

                this.twoSidedStencilMode = value;
                this.nativeDepthStencilState.TwoSidedStencilMode = value;
            }
        }

        public override void Dispose()
        {
            if (this.nativeDepthStencilState != null)
            {
                this.nativeDepthStencilState.Dispose();
                this.nativeDepthStencilState = null;
            }
        }

				protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            base.Dispose(disposeManaged);
        }
    }
}
