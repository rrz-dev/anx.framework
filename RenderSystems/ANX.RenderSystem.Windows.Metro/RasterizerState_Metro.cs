using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class RasterizerState_Metro : INativeRasterizerState
	{
		#region Private Members
		private Dx11.RasterizerStateDescription description;
		private Dx11.RasterizerState nativeRasterizerState;
		private bool nativeRasterizerStateDirty;
		private bool bound;

		private const int intMaxOver16 = int.MaxValue / 16;

		#endregion // Private Members

		public RasterizerState_Metro()
		{
			this.description = new Dx11.RasterizerStateDescription();

			this.description.IsAntialiasedLineEnabled = false;

			this.nativeRasterizerStateDirty = true;
		}

		public void Apply(ANX.Framework.Graphics.GraphicsDevice graphicsDevice)
		{
			GraphicsDeviceWindowsMetro gdMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
			var device = gdMetro.NativeDevice.NativeDevice;
			var context = gdMetro.NativeDevice.NativeContext;

			UpdateNativeRasterizerState(device);
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
				Dx11.CullMode cullMode = FormatConverter.Translate(value);

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
				Dx11.FillMode fillMode = FormatConverter.Translate(value);

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

		private void UpdateNativeRasterizerState(Dx11.Device1 device)
		{
			if (this.nativeRasterizerStateDirty == true || this.nativeRasterizerState == null)
			{
				if (this.nativeRasterizerState != null)
				{
					this.nativeRasterizerState.Dispose();
					this.nativeRasterizerState = null;
				}

				this.nativeRasterizerState = new Dx11.RasterizerState(device, ref this.description);

				this.nativeRasterizerStateDirty = false;
			}
		}
	}
}
