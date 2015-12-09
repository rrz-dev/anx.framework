#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10.Helpers;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx10 = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
	public class DepthStencilState_DX10 : BaseStateObject<Dx10.DepthStencilState>, INativeDepthStencilState
    {
		private Dx10.DepthStencilStateDescription description;
        private int referenceStencil;

#if DEBUG
        private static int depthStencilStateCount = 0;
#endif

		#region Public (TODO)
		public StencilOperation CounterClockwiseStencilDepthBufferFail
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.BackFace.DepthFailOperation, ref operation);
			}
		}

		public StencilOperation CounterClockwiseStencilFail
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.BackFace.FailOperation, ref operation);
			}
		}

		public CompareFunction CounterClockwiseStencilFunction
		{
			set
			{
				Dx10.Comparison comparison = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.BackFace.Comparison, ref comparison);
			}
		}

		public StencilOperation CounterClockwiseStencilPass
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.BackFace.PassOperation, ref operation);
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
				SetValueIfDifferentAndMarkDirty(ref description.DepthComparison, ref comparison);
			}
		}

		public bool DepthBufferWriteEnable
		{
			set
			{
				Dx10.DepthWriteMask writeMask = value ? Dx10.DepthWriteMask.All : Dx10.DepthWriteMask.Zero;
				SetValueIfDifferentAndMarkDirty(ref description.DepthWriteMask, ref writeMask);
			}
		}

		public int ReferenceStencil
		{
			set
			{
				SetValueIfDifferentAndMarkDirty(ref referenceStencil, ref value);
			}
		}

		public StencilOperation StencilDepthBufferFail
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.FrontFace.DepthFailOperation, ref operation);
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
				SetValueIfDifferentAndMarkDirty(ref description.FrontFace.FailOperation, ref operation);
			}
		}

		public CompareFunction StencilFunction
		{
			set
			{
				Dx10.Comparison comparison = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.FrontFace.Comparison, ref comparison);
			}
		}

		public int StencilMask
		{
			set
			{
				byte stencilMask = (byte)value;         //TODO: check range
				SetValueIfDifferentAndMarkDirty(ref description.StencilReadMask, ref stencilMask);
			}
		}

		public StencilOperation StencilPass
		{
			set
			{
				Dx10.StencilOperation operation = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.FrontFace.PassOperation, ref operation);
			}
		}

		public int StencilWriteMask
		{
			set
			{
				byte stencilWriteMask = (byte)value;        //TODO: check range
				SetValueIfDifferentAndMarkDirty(ref description.StencilWriteMask, ref stencilWriteMask);
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

		protected override Dx10.DepthStencilState CreateNativeState(GraphicsDevice graphics)
		{
			Dx10.Device device = (graphics.NativeDevice as GraphicsDeviceDX).NativeDevice;
			var state = new Dx10.DepthStencilState(device, ref description);
#if DEBUG
            state.DebugName = "DepthStencilState_" + depthStencilStateCount++;
#endif
            return state;
		}

		protected override void ApplyNativeState(GraphicsDevice graphics)
		{
			Dx10.Device device = (graphics.NativeDevice as GraphicsDeviceDX).NativeDevice;
			device.OutputMerger.SetDepthStencilState(nativeState, referenceStencil);
		}
    }
}
