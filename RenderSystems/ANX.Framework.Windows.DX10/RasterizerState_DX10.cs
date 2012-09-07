using System;
using ANX.BaseDirectX;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10.Helpers;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class RasterizerState_DX10 : BaseStateObject<Dx10.RasterizerState>, INativeRasterizerState
	{
        private Dx10.RasterizerStateDescription description;

		#region Public
		public CullMode CullMode
		{
			set
			{
				Dx10.CullMode cullMode = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.CullMode, ref cullMode);
			}
		}

		public float DepthBias
		{
			set
			{
				// XNA uses a float value in the range of 0f..16f as value
				// DirectX 10 uses a INT value

				int depthBiasValue = (int)(value * IntMaxOver16);
				SetValueIfDifferentAndMarkDirty(ref description.DepthBias, ref depthBiasValue);
			}
		}

		public FillMode FillMode
		{
			set
			{
				Dx10.FillMode fillMode = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.FillMode, ref fillMode);
			}
		}

		public bool MultiSampleAntiAlias
		{
			set
			{
				if (description.IsMultisampleEnabled != value)
				{
					isDirty = true;
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
					isDirty = true;
					description.IsScissorEnabled = value;
				}
			}
		}

		public float SlopeScaleDepthBias
		{
			set
			{
				SetValueIfDifferentAndMarkDirty(ref description.SlopeScaledDepthBias, ref value);
			}
		}
		#endregion

		protected override void Init()
		{
			description.IsAntialiasedLineEnabled = false;
		}

		protected override Dx10.RasterizerState CreateNativeState(GraphicsDevice graphics)
		{
			Dx10.Device device = (graphics.NativeDevice as GraphicsDeviceWindowsDX10).NativeDevice;
			return new Dx10.RasterizerState(device, ref description);
		}

		protected override void ApplyNativeState(GraphicsDevice graphics)
		{
			Dx10.Device device = (graphics.NativeDevice as GraphicsDeviceWindowsDX10).NativeDevice;
			device.Rasterizer.State = nativeState;
		}
	}
}
