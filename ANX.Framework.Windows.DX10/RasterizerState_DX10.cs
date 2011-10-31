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
    public class RasterizerState_DX10 : INativeRasterizerState
    {
        #region Private Members
        private RasterizerStateDescription description;
        private SharpDX.Direct3D10.RasterizerState nativeRasterizerState;
        private bool nativeRasterizerStateDirty;
        private bool bound;

        private const int intMaxOver16 = int.MaxValue / 16;

        #endregion // Private Members

        public RasterizerState_DX10()
        {
            this.description = new RasterizerStateDescription();

            this.description.IsAntialiasedLineEnabled = false;

            this.nativeRasterizerStateDirty = true;
        }

        public void Apply(Graphics.GraphicsDevice graphicsDevice)
        {
            GraphicsDeviceWindowsDX10 gdx10 = graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10;
            Device device = gdx10.NativeDevice;

            UpdateNativeRasterizerState(device);
            this.bound = true;

            device.Rasterizer.State = this.nativeRasterizerState;
        }

        public void Release()
        {
            this.bound = false;
        }

        public void Dispose()
        {
            if (this.nativeRasterizerState != null)
            {
                this.nativeRasterizerState.Dispose();
                this.nativeRasterizerState = null;
            }
        }

        public bool IsBound
        {
            get 
            {
                return this.bound;    
            }
        }

        public Graphics.CullMode CullMode
        {
            set 
            {
                SharpDX.Direct3D10.CullMode cullMode = Translate(value);

                if (description.CullMode != cullMode)
                {
                    nativeRasterizerStateDirty = true;
                    description.CullMode = cullMode;
                }
            }
        }

        public float DepthBias
        {
            set 
            { 
                // XNA uses a float value in the range of 0f..16f as value
                // DirectX 10 uses a INT value

                int depthBiasValue = (int)(value * intMaxOver16);

                if (description.DepthBias != depthBiasValue)
                {
                    nativeRasterizerStateDirty = true;
                    description.DepthBias = depthBiasValue;
                }
            }
        }

        public Graphics.FillMode FillMode
        {
            set 
            {
                SharpDX.Direct3D10.FillMode fillMode = Translate(value);

                if (description.FillMode != fillMode)
                {
                    nativeRasterizerStateDirty = true;
                    description.FillMode = fillMode;
                }
            }
        }

        public bool MultiSampleAntiAlias
        {
            set 
            {
                if (description.IsMultisampleEnabled != value)
                {
                    nativeRasterizerStateDirty = true;
                    description.IsMultisampleEnabled = value;
                }
            }
        }

        public bool ScissorTestEnable
        {
            set 
            {
                if (description.IsScissorEnabled != value)
                {
                    nativeRasterizerStateDirty = true;
                    description.IsScissorEnabled = value;
                }
            }
        }

        public float SlopeScaleDepthBias
        {
            set
            {
                if (description.SlopeScaledDepthBias != value)
                {
                    nativeRasterizerStateDirty = true;
                    description.SlopeScaledDepthBias = value;
                }
            }
        }

        private void UpdateNativeRasterizerState(Device device)
        {
            if (this.nativeRasterizerStateDirty == true || this.nativeRasterizerState == null)
            {
                if (this.nativeRasterizerState != null)
                {
                    this.nativeRasterizerState.Dispose();
                    this.nativeRasterizerState = null;
                }

                this.nativeRasterizerState = new SharpDX.Direct3D10.RasterizerState(device, ref this.description);

                this.nativeRasterizerStateDirty = false;
            }
        }

        private SharpDX.Direct3D10.CullMode Translate(ANX.Framework.Graphics.CullMode cullMode)
        {
            if (cullMode == Graphics.CullMode.CullClockwiseFace)
            {
                return SharpDX.Direct3D10.CullMode.Front;
            }
            else if (cullMode == Graphics.CullMode.CullCounterClockwiseFace)
            {
                return SharpDX.Direct3D10.CullMode.Back;
            }
            else
            {
                return SharpDX.Direct3D10.CullMode.None;
            }
        }

        private SharpDX.Direct3D10.FillMode Translate(ANX.Framework.Graphics.FillMode fillMode)
        {
            if (fillMode == Graphics.FillMode.WireFrame)
            {
                return SharpDX.Direct3D10.FillMode.Wireframe;
            }
            else
            {
                return SharpDX.Direct3D10.FillMode.Solid;
            }
        }
    }
}
