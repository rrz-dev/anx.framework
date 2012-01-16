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
    public class RasterizerState : GraphicsResource
    {
        #region Private Members
        private INativeRasterizerState nativeRasterizerState;

        private CullMode cullMode;
        private float depthBias;
        private FillMode fillMode;
        private bool multiSampleAntiAlias;
        private bool scissorTestEnable;
        private float slopeScaleDepthBias;

        #endregion

        public static readonly RasterizerState CullClockwise;
        public static readonly RasterizerState CullCounterClockwise;
        public static readonly RasterizerState CullNone;

        public RasterizerState()
        {
            this.nativeRasterizerState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateRasterizerState();

            this.CullMode = CullMode.CullCounterClockwiseFace;
            this.DepthBias = 0f;
            this.FillMode = FillMode.Solid;
            this.MultiSampleAntiAlias = true;
            this.ScissorTestEnable = false;
            this.SlopeScaleDepthBias = 0f;
        }

        private RasterizerState(CullMode cullMode, string name)
        {
            this.nativeRasterizerState = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateRasterizerState();

            this.CullMode = cullMode;
            this.DepthBias = 0f;
            this.FillMode = FillMode.Solid;
            this.MultiSampleAntiAlias = true;
            this.ScissorTestEnable = false;
            this.SlopeScaleDepthBias = 0f;

            Name = name;
        }

        static RasterizerState()
        {
            CullClockwise = new RasterizerState(CullMode.CullClockwiseFace, "RasterizerState.CullClockwise");
            CullCounterClockwise = new RasterizerState(CullMode.CullCounterClockwiseFace, "RasterizerState.CullCounterClockwise");
            CullNone = new RasterizerState(CullMode.None, "RasterizerState.CullNone");
        }

        internal INativeRasterizerState NativeRasterizerState
        {
            get
            {
                return this.nativeRasterizerState;
            }
        }

        public CullMode CullMode
        {
            get
            {
                return this.cullMode;
            }
            set
            {
                if (this.nativeRasterizerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change RasterizerState properties while it is bound to the GraphicsDevice.");
                }

                this.cullMode = value;
                this.nativeRasterizerState.CullMode = value;
            }
        }

        public float DepthBias
        {
            get
            {
                return this.depthBias;
            }
            set
            {
                if (this.nativeRasterizerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change RasterizerState properties while it is bound to the GraphicsDevice.");
                }

                this.depthBias = value;
                this.nativeRasterizerState.DepthBias = value;
            }
        }

        public FillMode FillMode
        {
            get
            {
                return this.fillMode;
            }
            set
            {
                if (this.nativeRasterizerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change RasterizerState properties while it is bound to the GraphicsDevice.");
                }

                this.fillMode = value;
                this.nativeRasterizerState.FillMode = value;
            }
        }

        public bool MultiSampleAntiAlias
        {
            get
            {
                return this.multiSampleAntiAlias;
            }
            set
            {
                if (this.nativeRasterizerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change RasterizerState properties while it is bound to the GraphicsDevice.");
                }

                this.multiSampleAntiAlias = value;
                this.nativeRasterizerState.MultiSampleAntiAlias = value;
            }
        }

        public bool ScissorTestEnable
        {
            get
            {
                return this.scissorTestEnable;
            }
            set
            {
                if (this.nativeRasterizerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change RasterizerState properties while it is bound to the GraphicsDevice.");
                }

                this.scissorTestEnable = value;
                this.nativeRasterizerState.ScissorTestEnable = value;
            }
        }

        public float SlopeScaleDepthBias
        {
            get
            {
                return this.slopeScaleDepthBias;
            }
            set
            {
                if (this.nativeRasterizerState.IsBound)
                {
                    throw new InvalidOperationException("You are not allowed to change RasterizerState properties while it is bound to the GraphicsDevice.");
                }

                this.slopeScaleDepthBias = value;
                this.nativeRasterizerState.SlopeScaleDepthBias = value;
            }
        }

        public override void Dispose()
        {
            if (this.nativeRasterizerState != null)
            {
                this.nativeRasterizerState.Dispose();
                this.nativeRasterizerState = null;
            }
        }

				protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            base.Dispose(disposeManaged);
        }
    }
}
