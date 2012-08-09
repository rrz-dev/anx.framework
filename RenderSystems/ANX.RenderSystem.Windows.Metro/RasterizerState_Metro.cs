#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D11;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Windows.Metro
{
    public class RasterizerState_Metro : INativeRasterizerState
    {
        #region Private Members
        private RasterizerStateDescription description;
        private SharpDX.Direct3D11.RasterizerState nativeRasterizerState;
        private bool nativeRasterizerStateDirty;
        private bool bound;

        private const int intMaxOver16 = int.MaxValue / 16;

        #endregion // Private Members

        public RasterizerState_Metro()
        {
            this.description = new RasterizerStateDescription();

            this.description.IsAntialiasedLineEnabled = false;

            this.nativeRasterizerStateDirty = true;
        }

        public void Apply(ANX.Framework.Graphics.GraphicsDevice graphicsDevice)
        {
            GraphicsDeviceWindowsMetro gdMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
            DeviceContext context = gdMetro.NativeDevice;

            UpdateNativeRasterizerState(context.Device);
            this.bound = true;

            context.Rasterizer.State = this.nativeRasterizerState;
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

        public ANX.Framework.Graphics.CullMode CullMode
        {
            set 
            {
                SharpDX.Direct3D11.CullMode cullMode = FormatConverter.Translate(value);

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

        public ANX.Framework.Graphics.FillMode FillMode
        {
            set 
            {
                SharpDX.Direct3D11.FillMode fillMode = FormatConverter.Translate(value);

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

                this.nativeRasterizerState = new SharpDX.Direct3D11.RasterizerState(device, ref this.description);

                this.nativeRasterizerStateDirty = false;
            }
        }
    }
}

