using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10.Helpers;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class DepthStencilState_DX10 : INativeDepthStencilState
    {
        #region Private
		private Dx10.DepthStencilStateDescription description;
		private Dx10.DepthStencilState nativeDepthStencilState;
        private bool isDirty;
        private int referenceStencil;
		#endregion

		#region Public (TODO)
		public bool IsBound
		{
			get;
			private set;
		}

		public StencilOperation CounterClockwiseStencilDepthBufferFail
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.BackFace.DepthFailOperation, ref operation);
			}
		}

		public StencilOperation CounterClockwiseStencilFail
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.BackFace.FailOperation, ref operation);
			}
		}

		public CompareFunction CounterClockwiseStencilFunction
		{
			set
			{
				Dx10.Comparison comparison = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.BackFace.Comparison, ref comparison);
			}
		}

		public StencilOperation CounterClockwiseStencilPass
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.BackFace.PassOperation, ref operation);
			}
		}

		public bool DepthBufferEnable
		{
			set
			{
				if (description.IsDepthEnabled != value)
				{
					description.IsDepthEnabled = value;
					isDirty = true;
				}
			}
		}

		public CompareFunction DepthBufferFunction
		{
			set
			{
				Dx10.Comparison comparison = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.DepthComparison, ref comparison);
			}
		}

		public bool DepthBufferWriteEnable
		{
			set
			{
				Dx10.DepthWriteMask writeMask = value ? Dx10.DepthWriteMask.All : Dx10.DepthWriteMask.Zero;
				UpdateValueAndMarkDirtyIfNeeded(ref description.DepthWriteMask, ref writeMask);
			}
		}

		public int ReferenceStencil
		{
			set
			{
				UpdateValueAndMarkDirtyIfNeeded(ref referenceStencil, ref value);
			}
		}

		public StencilOperation StencilDepthBufferFail
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.FrontFace.DepthFailOperation, ref operation);
			}
		}

		public bool StencilEnable
		{
			set
			{
				if (description.IsStencilEnabled != value)
				{
					description.IsStencilEnabled = value;
					isDirty = true;
				}
			}
		}

		public StencilOperation StencilFail
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.FrontFace.FailOperation, ref operation);
			}
		}

		public CompareFunction StencilFunction
		{
			set
			{
				Dx10.Comparison comparison = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.FrontFace.Comparison, ref comparison);
			}
		}

		public int StencilMask
		{
			set
			{
				byte stencilMask = (byte)value;         //TODO: check range
				UpdateValueAndMarkDirtyIfNeeded(ref description.StencilReadMask, ref stencilMask);
			}
		}

		public StencilOperation StencilPass
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				UpdateValueAndMarkDirtyIfNeeded(ref description.FrontFace.PassOperation, ref operation);
			}
		}

		public int StencilWriteMask
		{
			set
			{
				byte stencilWriteMask = (byte)value;        //TODO: check range
				UpdateValueAndMarkDirtyIfNeeded(ref description.StencilWriteMask, ref stencilWriteMask);
			}
		}

		public bool TwoSidedStencilMode
		{
			set
			{
				//TODO: check if we really need  in xna this enables only counter clockwise stencil operations
			}
		}
		#endregion

		#region Constructor
		public DepthStencilState_DX10()
        {
            isDirty = true;
        }
		#endregion
		
		#region Apply
        public void Apply(GraphicsDevice graphicsDevice)
        {
			Dx10.Device device = (graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10).NativeDevice;
            UpdateNativeDepthStencilState(device);
			IsBound = true;

            device.OutputMerger.SetDepthStencilState(nativeDepthStencilState, referenceStencil);
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
            if (nativeDepthStencilState != null)
            {
                nativeDepthStencilState.Dispose();
                nativeDepthStencilState = null;
            }
        }
		#endregion

		#region UpdateNativeDepthStencilState
		private void UpdateNativeDepthStencilState(Dx10.Device device)
        {
            if (isDirty == true || nativeDepthStencilState == null)
            {
                Dispose();
                nativeDepthStencilState = new Dx10.DepthStencilState(device, ref description);
                isDirty = false;
            }
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
