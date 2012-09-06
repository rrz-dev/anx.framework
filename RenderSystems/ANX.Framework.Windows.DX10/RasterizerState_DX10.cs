using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10.Helpers;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class RasterizerState_DX10 : INativeRasterizerState
	{
		private const int intMaxOver16 = int.MaxValue / 16;

        #region Private
        private Dx10.RasterizerStateDescription description;
        private Dx10.RasterizerState nativeRasterizerState;
        private bool isDirty;
        #endregion

		#region Public
		public bool IsBound { get; private set; }

		public CullMode CullMode
		{
			set
			{
				Dx10.CullMode cullMode = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.CullMode, ref cullMode);
			}
		}

		public float DepthBias
		{
			set
			{
				// XNA uses a float value in the range of 0f..16f as value
				// DirectX 10 uses a INT value

				int depthBiasValue = (int)(value * intMaxOver16);
				UpdateValueAndMarkDirtyIfNeeded(ref description.DepthBias, ref depthBiasValue);
			}
		}

		public FillMode FillMode
		{
			set
			{
				Dx10.FillMode fillMode = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.FillMode, ref fillMode);
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
				UpdateValueAndMarkDirtyIfNeeded(ref description.SlopeScaledDepthBias, ref value);
			}
		}
		#endregion

		#region Constructor
		public RasterizerState_DX10()
        {
            description.IsAntialiasedLineEnabled = false;
            isDirty = true;
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
        {
			UpdateNativeRasterizerState(graphicsDevice);
            IsBound = true;
		}
		#endregion

		#region Release
		public void Release()
        {
			IsBound = false;
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
		private void UpdateNativeRasterizerState(GraphicsDevice graphicsDevice)
		{
			Dx10.Device device = (graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10).NativeDevice;

            if (isDirty == true || nativeRasterizerState == null)
            {
				Dispose();
                nativeRasterizerState = new Dx10.RasterizerState(device, ref description);
                isDirty = false;
            }

			device.Rasterizer.State = nativeRasterizerState;
		}
		#endregion

		#region UpdateValueAndMarkDirtyIfNeeded
		private void UpdateValueAndMarkDirtyIfNeeded<T>(ref T currentValue, ref T value)
		{
			if (value.Equals(currentValue) == false)
			{
				isDirty = true;
				currentValue = value;
			}
		}
		#endregion
	}
}
