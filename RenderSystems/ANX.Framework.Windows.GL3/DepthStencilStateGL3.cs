using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using OpenTK.Graphics.OpenGL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.GL3
{
	/// <summary>
	/// Native Depth Stencil State object for OpenGL.
	/// <para />
	/// Basically this is a wrapper class for setting the different values all
	/// at once, because OpenGL has no State objects like DirectX.
	/// </summary>
	[PercentageComplete(100)]
	[TestStateAttribute(TestStateAttribute.TestState.Untested)]
	public class DepthStencilStateGL3 : INativeDepthStencilState
	{
		#region Private
		internal static DepthStencilStateGL3 Current
		{
			get;
			private set;
		}
		#endregion

		#region Public
		#region IsBound
		/// <summary>
		/// Flag if the state object is bound to the device.
		/// </summary>
		public bool IsBound
		{
			get;
			private set;
		}
		#endregion

		#region DepthBufferEnable
		public bool DepthBufferEnable
		{
			set;
			private get;
		}
		#endregion

		#region DepthBufferFunction
		public CompareFunction DepthBufferFunction
		{
			set;
			private get;
		}
		#endregion

		#region DepthBufferWriteEnable
		public bool DepthBufferWriteEnable
		{
			set;
			private get;
		}
		#endregion

		#region StencilEnable
		public bool StencilEnable
		{
			set;
			private get;
		}
		#endregion

		#region StencilFunction
		public CompareFunction StencilFunction
		{
			set;
			private get;
		}
		#endregion

		#region StencilMask
		public int StencilMask
		{
			set;
			private get;
		}
		#endregion

		#region StencilDepthBufferFail
		public StencilOperation StencilDepthBufferFail
		{
			set;
			private get;
		}
		#endregion

		#region StencilFail
		public StencilOperation StencilFail
		{
			set;
			private get;
		}
		#endregion

		#region StencilPass
		public StencilOperation StencilPass
		{
			set;
			private get;
		}
		#endregion

		#region CounterClockwiseStencilDepthBufferFail
		public StencilOperation CounterClockwiseStencilDepthBufferFail
		{
			set;
			private get;
		}
		#endregion

		#region CounterClockwiseStencilFail
		public StencilOperation CounterClockwiseStencilFail
		{
			set;
			private get;
		}
		#endregion

		#region CounterClockwiseStencilFunction
		public CompareFunction CounterClockwiseStencilFunction
		{
			set;
			private get;
		}
		#endregion

		#region CounterClockwiseStencilPass
		public StencilOperation CounterClockwiseStencilPass
		{
			set;
			private get;
		}
		#endregion

		#region TwoSidedStencilMode
		public bool TwoSidedStencilMode
		{
			set;
			private get;
		}
		#endregion

		#region ReferenceStencil
		public int ReferenceStencil
		{
			set;
			private get;
		}
		#endregion

		#region StencilWriteMask
		public int StencilWriteMask
		{
			set;
			private get;
		}
		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new depth stencil state object.
		/// </summary>
		internal DepthStencilStateGL3()
		{
			IsBound = false;
		}
		#endregion

		#region Apply
		/// <summary>
		/// Apply the depth stencil state to the graphics device.
		/// </summary>
		/// <param name="graphicsDevice">The current graphics device.</param>
		public void Apply(GraphicsDevice graphicsDevice)
		{
			IsBound = true;
			Current = this;

			#region Depth
			if (DepthBufferEnable)
			{
				GL.Enable(EnableCap.DepthTest);
			}
			else
			{
				GL.Disable(EnableCap.DepthTest);
			}
			ErrorHelper.Check("DepthTest");

			GL.DepthFunc(TranslateDepthFunction(DepthBufferFunction));
			ErrorHelper.Check("DepthFunc");

			GL.DepthMask(DepthBufferWriteEnable);
			ErrorHelper.Check("DepthMask");
			#endregion

			#region Stencil
			if (StencilEnable)
			{
				GL.Enable(EnableCap.StencilTest);
			}
			else
			{
				GL.Disable(EnableCap.StencilTest);
			}
			ErrorHelper.Check("StencilTest");

			GL.StencilMask(StencilWriteMask);
			ErrorHelper.Check("StencilMask");

			if (TwoSidedStencilMode)
			{
				GL.StencilOpSeparate(StencilFace.Front,
					TranslateStencilOp(StencilFail),
					TranslateStencilOp(StencilDepthBufferFail),
					TranslateStencilOp(StencilPass));
				ErrorHelper.Check("StencilOpSeparate Front");

				GL.StencilOpSeparate(StencilFace.Back,
					TranslateStencilOp(CounterClockwiseStencilFail),
					TranslateStencilOp(CounterClockwiseStencilDepthBufferFail),
					TranslateStencilOp(CounterClockwiseStencilPass));
				ErrorHelper.Check("StencilOpSeparate Back");

				GL.StencilFuncSeparate(StencilFace.Front,
					TranslateStencilFunction(StencilFunction),
					ReferenceStencil, StencilMask);
				ErrorHelper.Check("StencilFuncSeparate Front");

				GL.StencilFuncSeparate(StencilFace.Back,
					TranslateStencilFunction(CounterClockwiseStencilFunction),
					ReferenceStencil, StencilMask);
				ErrorHelper.Check("StencilFuncSeparate Back");
			}
			else
			{
				GL.StencilOp(
					TranslateStencilOp(StencilFail),
					TranslateStencilOp(StencilDepthBufferFail),
					TranslateStencilOp(StencilPass));
				ErrorHelper.Check("StencilOp");

				GL.StencilFunc(TranslateStencilFunction(StencilFunction),
					ReferenceStencil, StencilMask);
				ErrorHelper.Check("StencilFunc");
			}
			#endregion
		}
		#endregion

		#region TranslateStencilOp
		/// <summary>
		/// Translate the ANX stencil operation to OpenGL.
		/// </summary>
		/// <param name="func">ANX stencil operation.</param>
		/// <returns>Translated OpenGL stencil operation.</returns>
		private OpenTK.Graphics.OpenGL.StencilOp TranslateStencilOp(
			StencilOperation operation)
		{
			switch (operation)
			{
				default:
				case StencilOperation.Decrement:
					return StencilOp.Decr;

				case StencilOperation.DecrementSaturation:
					return StencilOp.DecrWrap;

				case StencilOperation.Increment:
					return StencilOp.Incr;

				case StencilOperation.IncrementSaturation:
					return StencilOp.IncrWrap;

				case StencilOperation.Invert:
					return StencilOp.Invert;

				case StencilOperation.Keep:
					return StencilOp.Keep;

				case StencilOperation.Replace:
					return StencilOp.Replace;

				case StencilOperation.Zero:
					return StencilOp.Zero;
			}
		}
		#endregion

		#region TranslateDepthFunction
		/// <summary>
		/// Translate the ANX compare function to the OpenGL depth function.
		/// </summary>
		/// <param name="func">ANX compare function.</param>
		/// <returns>Translated OpenGL depth function.</returns>
		private OpenTK.Graphics.OpenGL.DepthFunction TranslateDepthFunction(
			CompareFunction func)
		{
			switch (func)
			{
				default:
				case CompareFunction.Always:
					return OpenTK.Graphics.OpenGL.DepthFunction.Always;

				case CompareFunction.Equal:
					return OpenTK.Graphics.OpenGL.DepthFunction.Equal;

				case CompareFunction.Greater:
					return OpenTK.Graphics.OpenGL.DepthFunction.Greater;

				case CompareFunction.GreaterEqual:
					return OpenTK.Graphics.OpenGL.DepthFunction.Gequal;

				case CompareFunction.Less:
					return OpenTK.Graphics.OpenGL.DepthFunction.Less;

				case CompareFunction.LessEqual:
					return OpenTK.Graphics.OpenGL.DepthFunction.Lequal;

				case CompareFunction.Never:
					return OpenTK.Graphics.OpenGL.DepthFunction.Never;

				case CompareFunction.NotEqual:
					return OpenTK.Graphics.OpenGL.DepthFunction.Notequal;
			}
		}
		#endregion

		#region TranslateStencilFunction
		/// <summary>
		/// Translate the ANX compare function to the OpenGL stencil function.
		/// </summary>
		/// <param name="func">ANX compare function.</param>
		/// <returns>Translated OpenGL stencil function.</returns>
		private OpenTK.Graphics.OpenGL.StencilFunction TranslateStencilFunction(
			CompareFunction func)
		{
			switch (func)
			{
				default:
				case CompareFunction.Always:
					return OpenTK.Graphics.OpenGL.StencilFunction.Always;

				case CompareFunction.Equal:
					return OpenTK.Graphics.OpenGL.StencilFunction.Equal;

				case CompareFunction.Greater:
					return OpenTK.Graphics.OpenGL.StencilFunction.Greater;

				case CompareFunction.GreaterEqual:
					return OpenTK.Graphics.OpenGL.StencilFunction.Gequal;

				case CompareFunction.Less:
					return OpenTK.Graphics.OpenGL.StencilFunction.Less;

				case CompareFunction.LessEqual:
					return OpenTK.Graphics.OpenGL.StencilFunction.Lequal;

				case CompareFunction.Never:
					return OpenTK.Graphics.OpenGL.StencilFunction.Never;

				case CompareFunction.NotEqual:
					return OpenTK.Graphics.OpenGL.StencilFunction.Notequal;
			}
		}
		#endregion

		#region Release
		/// <summary>
		/// Release the depth stencil state.
		/// </summary>
		public void Release()
		{
			IsBound = false;
			Current = null;
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose the depth stencil state object.
		/// </summary>
		public void Dispose()
		{
		}
		#endregion
	}
}
