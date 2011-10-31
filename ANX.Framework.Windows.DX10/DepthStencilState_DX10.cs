using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using SharpDX.Direct3D10;

namespace ANX.Framework.Windows.DX10
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

        public void Apply(Graphics.GraphicsDevice graphicsDevice)
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

        public Graphics.StencilOperation CounterClockwiseStencilDepthBufferFail
        {
            set 
            {
                SharpDX.Direct3D10.StencilOperation operation = Translate(value);

                if (description.BackFace.DepthFailOperation != operation)
                {
                    description.BackFace.DepthFailOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public Graphics.StencilOperation CounterClockwiseStencilFail
        {
            set 
            {
                SharpDX.Direct3D10.StencilOperation operation = Translate(value);

                if (description.BackFace.FailOperation != operation)
                {
                    description.BackFace.FailOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public Graphics.CompareFunction CounterClockwiseStencilFunction
        {
            set 
            {
                SharpDX.Direct3D10.Comparison comparison = Translate(value);

                if (description.BackFace.Comparison != comparison)
                {
                    description.BackFace.Comparison = comparison;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public Graphics.StencilOperation CounterClockwiseStencilPass
        {
            set 
            {
                SharpDX.Direct3D10.StencilOperation operation = Translate(value);

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

        public Graphics.CompareFunction DepthBufferFunction
        {
            set 
            {
                SharpDX.Direct3D10.Comparison comparison = Translate(value);

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

        public Graphics.StencilOperation StencilDepthBufferFail
        {
            set 
            {
                SharpDX.Direct3D10.StencilOperation operation = Translate(value);

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

        public Graphics.StencilOperation StencilFail
        {
            set
            {
                SharpDX.Direct3D10.StencilOperation operation = Translate(value);

                if (description.FrontFace.FailOperation != operation)
                {
                    description.FrontFace.FailOperation = operation;
                    nativeDepthStencilStateDirty = true;
                }
            }
        }

        public Graphics.CompareFunction StencilFunction
        {
            set 
            {
                SharpDX.Direct3D10.Comparison comparison = Translate(value);

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

        public Graphics.StencilOperation StencilPass
        {
            set 
            { 
                SharpDX.Direct3D10.StencilOperation operation = Translate(value);

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

        private SharpDX.Direct3D10.StencilOperation Translate(ANX.Framework.Graphics.StencilOperation stencilOperation)
        {
            switch (stencilOperation)
            {
                case Graphics.StencilOperation.Decrement:
                    return StencilOperation.Decrement;
                case Graphics.StencilOperation.DecrementSaturation:
                    return StencilOperation.DecrementAndClamp;
                case Graphics.StencilOperation.Increment:
                    return StencilOperation.Increment;
                case Graphics.StencilOperation.IncrementSaturation:
                    return StencilOperation.IncrementAndClamp;
                case Graphics.StencilOperation.Invert:
                    return StencilOperation.Invert;
                case Graphics.StencilOperation.Keep:
                    return StencilOperation.Keep;
                case Graphics.StencilOperation.Replace:
                    return StencilOperation.Replace;
                case Graphics.StencilOperation.Zero:
                    return StencilOperation.Zero;
            }

            throw new NotImplementedException("unknown StencilOperation");
        }

        private Comparison Translate(ANX.Framework.Graphics.CompareFunction compareFunction)
        {
            switch (compareFunction)
            {
                case ANX.Framework.Graphics.CompareFunction.Always:
                    return Comparison.Always;
                case ANX.Framework.Graphics.CompareFunction.Equal:
                    return Comparison.Equal;
                case ANX.Framework.Graphics.CompareFunction.Greater:
                    return Comparison.Greater;
                case ANX.Framework.Graphics.CompareFunction.GreaterEqual:
                    return Comparison.GreaterEqual;
                case ANX.Framework.Graphics.CompareFunction.Less:
                    return Comparison.Less;
                case ANX.Framework.Graphics.CompareFunction.LessEqual:
                    return Comparison.LessEqual;
                case ANX.Framework.Graphics.CompareFunction.Never:
                    return Comparison.Never;
                case ANX.Framework.Graphics.CompareFunction.NotEqual:
                    return Comparison.NotEqual;
            }

            throw new NotImplementedException("unknown CompareFunction");
        }
    
    }
}
