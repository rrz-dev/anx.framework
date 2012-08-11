using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using SceGraphics = Sce.PlayStation.Core.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaDepthStencilState : INativeDepthStencilState
	{
		#region Public
		public bool IsBound
		{
			get;
			private set;
		}

		public bool DepthBufferEnable
		{
			set;
			private get;
		}

		public CompareFunction DepthBufferFunction
		{
			set;
			private get;
		}

		public bool DepthBufferWriteEnable
		{
			set;
			private get;
		}

		public bool StencilEnable
		{
			set;
			private get;
		}

		public CompareFunction StencilFunction
		{
			set;
			private get;
		}

		public int StencilMask
		{
			set;
			private get;
		}

		public StencilOperation StencilDepthBufferFail
		{
			set;
			private get;
		}

		public StencilOperation StencilFail
		{
			set;
			private get;
		}

		public StencilOperation StencilPass
		{
			set;
			private get;
		}

		public StencilOperation CounterClockwiseStencilDepthBufferFail
		{
			set;
			private get;
		}

		public StencilOperation CounterClockwiseStencilFail
		{
			set;
			private get;
		}

		public CompareFunction CounterClockwiseStencilFunction
		{
			set;
			private get;
		}

		public StencilOperation CounterClockwiseStencilPass
		{
			set;
			private get;
		}

		public bool TwoSidedStencilMode
		{
			set;
			private get;
		}

		public int ReferenceStencil
		{
			set;
			private get;
		}

		public int StencilWriteMask
		{
			set;
			private get;
		}
		#endregion

		#region Constructor
		internal PsVitaDepthStencilState()
		{
			IsBound = false;
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
		{
			IsBound = true;
			var context = PsVitaGraphicsDevice.Current.NativeContext;

			#region Depth
			if (DepthBufferEnable)
			{
				context.Enable(SceGraphics.EnableMode.DepthTest);
			}
			else
			{
				context.Disable(SceGraphics.EnableMode.DepthTest);
			}

			context.SetDepthFunc(TranslateDepthFunction(DepthBufferFunction),
				DepthBufferWriteEnable);
			#endregion

			#region Stencil
			if (StencilEnable)
			{
				context.Enable(SceGraphics.EnableMode.StencilTest);
			}
			else
			{
				context.Disable(SceGraphics.EnableMode.StencilTest);
			}

			if (TwoSidedStencilMode)
			{
				context.SetStencilOpFront(TranslateStencilOp(StencilFail),
					TranslateStencilOp(StencilDepthBufferFail),
					TranslateStencilOp(StencilPass));

				context.SetStencilOpBack(TranslateStencilOp(CounterClockwiseStencilFail),
					TranslateStencilOp(CounterClockwiseStencilDepthBufferFail),
					TranslateStencilOp(CounterClockwiseStencilPass));

				context.SetStencilFuncFront(
					TranslateStencilFunction(StencilFunction),
					ReferenceStencil, StencilMask, StencilWriteMask);

				context.SetStencilFuncBack(
					TranslateStencilFunction(CounterClockwiseStencilFunction),
					ReferenceStencil, StencilMask, StencilWriteMask);
			}
			else
			{
				context.SetStencilOp(TranslateStencilOp(StencilFail),
					TranslateStencilOp(StencilDepthBufferFail),
					TranslateStencilOp(StencilPass));

				context.SetStencilFunc(TranslateStencilFunction(StencilFunction),
					ReferenceStencil, StencilMask, StencilWriteMask);
			}
			#endregion
		}
		#endregion

		#region TranslateStencilOp
		private SceGraphics.StencilOpMode TranslateStencilOp(StencilOperation operation)
		{
			switch (operation)
			{
				default:
				case StencilOperation.Decrement:
					return SceGraphics.StencilOpMode.Decr;

				case StencilOperation.DecrementSaturation:
					return SceGraphics.StencilOpMode.DecrWrap;

				case StencilOperation.Increment:
					return SceGraphics.StencilOpMode.Incr;

				case StencilOperation.IncrementSaturation:
					return SceGraphics.StencilOpMode.IncrWrap;

				case StencilOperation.Invert:
					return SceGraphics.StencilOpMode.Invert;

				case StencilOperation.Keep:
					return SceGraphics.StencilOpMode.Keep;

				case StencilOperation.Replace:
					return SceGraphics.StencilOpMode.Replace;

				case StencilOperation.Zero:
					return SceGraphics.StencilOpMode.Zero;
			}
		}
		#endregion

		#region TranslateDepthFunction
		private SceGraphics.DepthFuncMode TranslateDepthFunction(CompareFunction func)
		{
			switch (func)
			{
				default:
				case CompareFunction.Always:
					return SceGraphics.DepthFuncMode.Always;

				case CompareFunction.Equal:
					return SceGraphics.DepthFuncMode.Equal;

				case CompareFunction.Greater:
					return SceGraphics.DepthFuncMode.Greater;

				case CompareFunction.GreaterEqual:
					return SceGraphics.DepthFuncMode.GEqual;

				case CompareFunction.Less:
					return SceGraphics.DepthFuncMode.Less;

				case CompareFunction.LessEqual:
					return SceGraphics.DepthFuncMode.LEqual;

				case CompareFunction.Never:
					return SceGraphics.DepthFuncMode.Never;

				case CompareFunction.NotEqual:
					return SceGraphics.DepthFuncMode.NotEequal;
			}
		}
		#endregion

		#region TranslateStencilFunction
		private SceGraphics.StencilFuncMode TranslateStencilFunction(CompareFunction func)
		{
			switch (func)
			{
				default:
				case CompareFunction.Always:
					return SceGraphics.StencilFuncMode.Always;

				case CompareFunction.Equal:
					return SceGraphics.StencilFuncMode.Equal;

				case CompareFunction.Greater:
					return SceGraphics.StencilFuncMode.Greater;

				case CompareFunction.GreaterEqual:
					return SceGraphics.StencilFuncMode.GEqual;

				case CompareFunction.Less:
					return SceGraphics.StencilFuncMode.Less;

				case CompareFunction.LessEqual:
					return SceGraphics.StencilFuncMode.LEqual;

				case CompareFunction.Never:
					return SceGraphics.StencilFuncMode.Never;

				case CompareFunction.NotEqual:
					return SceGraphics.StencilFuncMode.NotEequal;
			}
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
		}
		#endregion
	}
}
