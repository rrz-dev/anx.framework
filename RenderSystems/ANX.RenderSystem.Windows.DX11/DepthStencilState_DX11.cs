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
    public class DepthStencilState_DX11 : INativeDepthStencilState
    {
        #region Private Members
        private DepthStencilStateDescription description;
        private SharpDX.Direct3D11.DepthStencilState nativeDepthStencilState;
        private bool nativeDepthStencilStateDirty;
        private bool bound;

        private int referenceStencil;

        #endregion // Private Members

        public DepthStencilState_DX11()
        {
            this.description = new DepthStencilStateDescription();

            this.nativeDepthStencilStateDirty = true;
        }

        public void Apply(ANX.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            GraphicsDeviceWindowsDX11 gdx11 = graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX11;
            DeviceContext context = gdx11.NativeDevice;

            UpdateNativeDepthStencilState(context.Device);
            this.bound = true;

            context.OutputMerger.SetDepthStencilState(nativeDepthStencilState, this.referenceStencil);
        }

        public void Release()
        {
            this.bound = false;
        }

        public void Dispose()
        {
            if (this.nativeDepthStencilState != null)
            {
                this.nativeDepthStencilState.Dispose();
                this.nativeDepthStencilState = null;
            }
        }

        public bool IsBound
        {
            get
            {
                return this.bound;
            }
        }

        public ANX.Framework.Graphics.StencilOperation CounterClockwiseStencilDepthBufferFail
        {
            set 
            {
                SharpDX.Direct3D11.StencilOperation operation = FormatConverter.Translate(value);

                if (description.BackFace.DepthFailOperation != operation)
                {
                    description.BackFace.DepthFailOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public ANX.Framework.Graphics.StencilOperation CounterClockwiseStencilFail
        {
            set 
            {
                SharpDX.Direct3D11.StencilOperation operation = FormatConverter.Translate(value);

                if (description.BackFace.FailOperation != operation)
                {
                    description.BackFace.FailOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public ANX.Framework.Graphics.CompareFunction CounterClockwiseStencilFunction
        {
            set 
            {
                SharpDX.Direct3D11.Comparison comparison = FormatConverter.Translate(value);

                if (description.BackFace.Comparison != comparison)
                {
                    description.BackFace.Comparison = comparison;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public ANX.Framework.Graphics.StencilOperation CounterClockwiseStencilPass
        {
            set 
            {
                SharpDX.Direct3D11.StencilOperation operation = FormatConverter.Translate(value);

                if (description.BackFace.PassOperation != operation)
                {
                    description.BackFace.PassOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public bool DepthBufferEnable
        {
            set 
            {
                if (description.IsDepthEnabled != value)
                {
                    description.IsDepthEnabled = value;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public ANX.Framework.Graphics.CompareFunction DepthBufferFunction
        {
            set 
            {
                SharpDX.Direct3D11.Comparison comparison = FormatConverter.Translate(value);

                if (description.DepthComparison != comparison)
                {
                    description.DepthComparison = comparison;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public bool DepthBufferWriteEnable
        {
            set 
            { 
                DepthWriteMask writeMask = value ? DepthWriteMask.All : DepthWriteMask.Zero;

                if (description.DepthWriteMask != writeMask)
                {
                    description.DepthWriteMask = writeMask;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public int ReferenceStencil
        {
            set 
            {
                if (this.referenceStencil != value)
                {
                    this.referenceStencil = value;
                    this.nativeDepthStencilStateDirty = true;
                }
            }
        }

        public ANX.Framework.Graphics.StencilOperation StencilDepthBufferFail
        {
            set 
            {
                SharpDX.Direct3D11.StencilOperation operation = FormatConverter.Translate(value);

                if (description.FrontFace.DepthFailOperation != operation)
                {
                    description.FrontFace.DepthFailOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public bool StencilEnable
        {
            set 
            {
                if (description.IsStencilEnabled != value)
                {
                    description.IsStencilEnabled = value;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public ANX.Framework.Graphics.StencilOperation StencilFail
        {
            set
            {
                SharpDX.Direct3D11.StencilOperation operation = FormatConverter.Translate(value);

                if (description.FrontFace.FailOperation != operation)
                {
                    description.FrontFace.FailOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public ANX.Framework.Graphics.CompareFunction StencilFunction
        {
            set 
            {
                SharpDX.Direct3D11.Comparison comparison = FormatConverter.Translate(value);

                if (description.FrontFace.Comparison != comparison)
                {
                    description.FrontFace.Comparison = comparison;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public int StencilMask
        {
            set 
            {
                byte stencilMask = (byte)value;         //TODO: check range

                if (description.StencilReadMask != stencilMask)
                {
                    description.StencilReadMask = stencilMask;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public ANX.Framework.Graphics.StencilOperation StencilPass
        {
            set 
            {
                SharpDX.Direct3D11.StencilOperation operation = FormatConverter.Translate(value);

                if (description.FrontFace.PassOperation != operation)
                {
                    description.FrontFace.PassOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public int StencilWriteMask
        {
            set 
            {
                byte stencilWriteMask = (byte)value;        //TODO: check range

                if (description.StencilWriteMask != stencilWriteMask)
                {
                    description.StencilWriteMask = stencilWriteMask;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public bool TwoSidedStencilMode
        {
            set 
            { 
                //TODO: check if we really need this. in xna this enables only counter clockwise stencil operations
            }
        }

        private void UpdateNativeDepthStencilState(Device device)
        {
            if (this.nativeDepthStencilStateDirty == true || this.nativeDepthStencilState == null)
            {
                if (this.nativeDepthStencilState != null)
                {
                    this.nativeDepthStencilState.Dispose();
                    this.nativeDepthStencilState = null;
                }

                this.nativeDepthStencilState = new SharpDX.Direct3D11.DepthStencilState(device, ref this.description);

                this.nativeDepthStencilStateDirty = false;
            }
        }
    }
}
