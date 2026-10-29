using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class RasterizerState_Metro : BaseStateObject, INativeRasterizerState
	{
		#region Constants
		private const int intMaxOver16 = int.MaxValue / 16;
		#endregion

		#region Private
		private Dx11.RasterizerStateDescription description;
		private Dx11.RasterizerState nativeRasterizerState;
		#endregion

		#region Public
		public CullMode CullMode
		{
			set
			{
				Dx11.CullMode cullMode = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.CullMode, ref cullMode);
			}
		}

		public float DepthBias
		{
			set
			{
				// XNA uses a float value in the range of 0f..16f as value
				// DirectX 11 uses an INT value

				int depthBiasValue = (int)(value * intMaxOver16);
				SetValueIfDifferentAndMarkDirty(ref description.DepthBias, ref depthBiasValue);
			}
		}

		public FillMode FillMode
		{
			set
			{
				Dx11.FillMode fillMode = FormatConverter.Translate(value);
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

		#region Constructor
		public RasterizerState_Metro()
		{
			description = new Dx11.RasterizerStateDescription();
			description.IsAntialiasedLineEnabled = false;

			isDirty = true;
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
		{
			UpdateNativeRasterizerState();
			bound = true;

			NativeDxDevice.Current.NativeContext.Rasterizer.State =
				nativeRasterizerState;
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (nativeRasterizerState != null)
			{
				nativeRasterizerState.Dispose();
				nativeRasterizerState = null;
			}
		}
		#endregion

		#region UpdateNativeRasterizerState
		private void UpdateNativeRasterizerState()
		{
			if (isDirty == true ||
				nativeRasterizerState == null)
			{
				Dispose();

				try
				{
					nativeRasterizerState = new Dx11.RasterizerState(
						NativeDxDevice.Current.NativeDevice, description);
					isDirty = false;
				}
				catch
				{
				}
			}
		}
		#endregion
	}
}
