#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D10;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using StencilOperation = ANX.Framework.Graphics.StencilOperation;

namespace ANX.RenderSystem.Windows.DX10
{
    public class DepthStencilState_DX10 : INativeDepthStencilState
    {
        #region Private Members
        private DepthStencilStateDescription description;
        private SharpDX.Direct3D10.DepthStencilState nativeDepthStencilState;
        private bool nativeDepthStencilStateDirty;
        private bool bound;

        private int referenceStencil;

        #endregion // Private Members

        public DepthStencilState_DX10()
        {
            this.description = new DepthStencilStateDescription();

            this.nativeDepthStencilStateDirty = true;
        }

        public void Apply(GraphicsDevice graphicsDevice)
        {
            GraphicsDeviceWindowsDX10 gdx10 = graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10;
            Device device = gdx10.NativeDevice;

            UpdateNativeDepthStencilState(device);
            this.bound = true;

            device.OutputMerger.SetDepthStencilState(nativeDepthStencilState, this.referenceStencil);
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

        public StencilOperation CounterClockwiseStencilDepthBufferFail
        {
            set 
            {
                SharpDX.Direct3D10.StencilOperation operation = FormatConverter.Translate(value);

                if (description.BackFace.DepthFailOperation != operation)
                {
                    description.BackFace.DepthFailOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public StencilOperation CounterClockwiseStencilFail
        {
            set 
            {
                SharpDX.Direct3D10.StencilOperation operation = FormatConverter.Translate(value);

                if (description.BackFace.FailOperation != operation)
                {
                    description.BackFace.FailOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public CompareFunction CounterClockwiseStencilFunction
        {
            set 
            {
                SharpDX.Direct3D10.Comparison comparison = FormatConverter.Translate(value);

                if (description.BackFace.Comparison != comparison)
                {
                    description.BackFace.Comparison = comparison;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public StencilOperation CounterClockwiseStencilPass
        {
            set 
            {
                SharpDX.Direct3D10.StencilOperation operation = FormatConverter.Translate(value);

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

        public CompareFunction DepthBufferFunction
        {
            set 
            {
                SharpDX.Direct3D10.Comparison comparison = FormatConverter.Translate(value);

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

        public StencilOperation StencilDepthBufferFail
        {
            set 
            {
                SharpDX.Direct3D10.StencilOperation operation = FormatConverter.Translate(value);

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

        public StencilOperation StencilFail
        {
            set
            {
                SharpDX.Direct3D10.StencilOperation operation = FormatConverter.Translate(value);

                if (description.FrontFace.FailOperation != operation)
                {
                    description.FrontFace.FailOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public CompareFunction StencilFunction
        {
            set 
            {
                SharpDX.Direct3D10.Comparison comparison = FormatConverter.Translate(value);

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

        public StencilOperation StencilPass
        {
            set 
            {
                SharpDX.Direct3D10.StencilOperation operation = FormatConverter.Translate(value);

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

                this.nativeDepthStencilState = new SharpDX.Direct3D10.DepthStencilState(device, ref this.description);

                this.nativeDepthStencilStateDirty = false;
            }
        }
    }
}
