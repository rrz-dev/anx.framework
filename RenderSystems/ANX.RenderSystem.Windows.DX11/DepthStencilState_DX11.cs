using Dx11 = SharpDX.Direct3D11;

#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
    [Developer("Glatzemann")]
    public class DepthStencilState_DX11 : BaseStateObject<Dx11.DepthStencilState>, INativeDepthStencilState
    {
		private Dx11.DepthStencilStateDescription description;
        private int referenceStencil;

		#region Public
		public StencilOperation CounterClockwiseStencilDepthBufferFail
		{
			set
			{
				Dx11.StencilOperation operation = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.BackFace.DepthFailOperation, ref operation);
			}
		}

		public StencilOperation CounterClockwiseStencilFail
		{
			set
			{
				Dx11.StencilOperation operation = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.BackFace.FailOperation, ref operation);
			}
		}

		public CompareFunction CounterClockwiseStencilFunction
		{
			set
			{
				Dx11.Comparison comparison = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.BackFace.Comparison, ref comparison);
			}
		}

		public StencilOperation CounterClockwiseStencilPass
		{
			set
			{
				Dx11.StencilOperation operation = FormatConverter.Translate(value);
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
				Dx11.Comparison comparison = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.DepthComparison, ref comparison);
			}
		}

		public bool DepthBufferWriteEnable
		{
			set
			{
				Dx11.DepthWriteMask writeMask = value ? Dx11.DepthWriteMask.All : Dx11.DepthWriteMask.Zero;
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
				Dx11.StencilOperation operation = FormatConverter.Translate(value);
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
				Dx11.StencilOperation operation = FormatConverter.Translate(value);
				SetValueIfDifferentAndMarkDirty(ref description.FrontFace.FailOperation, ref operation);
			}
		}

		public CompareFunction StencilFunction
		{
			set
			{
				Dx11.Comparison comparison = FormatConverter.Translate(value);
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
				Dx11.StencilOperation operation = FormatConverter.Translate(value);
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
				//TODO: check if we really need this. in xna this enables only counter clockwise stencil operations
			}
		}
		#endregion

		protected override Dx11.DepthStencilState CreateNativeState(GraphicsDevice graphics)
		{
			Dx11.DeviceContext context = (graphics.NativeDevice as GraphicsDeviceDX).NativeDevice;
			return new Dx11.DepthStencilState(context.Device, description);
		}

		protected override void ApplyNativeState(GraphicsDevice graphics)
		{
			Dx11.DeviceContext context = (graphics.NativeDevice as GraphicsDeviceDX).NativeDevice;
			context.OutputMerger.SetDepthStencilState(nativeState, referenceStencil);
		}
	}
}
