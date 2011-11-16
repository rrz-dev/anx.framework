using System;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;
using OpenTK.Graphics.OpenGL;

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Windows.GL3
{
	/// <summary>
	/// Native Depth Stencil State object for OpenGL.
	/// <para />
	/// Basically this is a wrapper class for setting the different values all
	/// at once, because OpenGL has no State objects like DirectX.
	/// </summary>
	public class DepthStencilStateGL3 : INativeDepthStencilState
	{
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

			#region Depth
			if (DepthBufferEnable)
			{
				GL.Enable(EnableCap.DepthTest);
			}
			else
			{
				GL.Disable(EnableCap.DepthTest);
			}

			GL.DepthFunc(TranslateDepthFunction(DepthBufferFunction));

			GL.DepthMask(DepthBufferWriteEnable);
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

			GL.StencilMask(StencilWriteMask);

			if (TwoSidedStencilMode)
			{
				GL.StencilOpSeparate(StencilFace.Front,
					TranslateStencilOp(StencilFail),
					TranslateStencilOp(StencilDepthBufferFail),
					TranslateStencilOp(StencilPass));

				GL.StencilOpSeparate(StencilFace.Back,
					TranslateStencilOp(CounterClockwiseStencilFail),
					TranslateStencilOp(CounterClockwiseStencilDepthBufferFail),
					TranslateStencilOp(CounterClockwiseStencilPass));

				GL.StencilFuncSeparate(StencilFace.Front,
					TranslateStencilFunction(StencilFunction),
					ReferenceStencil, StencilMask);

				GL.StencilFuncSeparate(StencilFace.Back,
					TranslateStencilFunction(CounterClockwiseStencilFunction),
					ReferenceStencil, StencilMask);
			}
			else
			{
				GL.StencilOp(
					TranslateStencilOp(StencilFail),
					TranslateStencilOp(StencilDepthBufferFail),
					TranslateStencilOp(StencilPass));

				GL.StencilFunc(TranslateStencilFunction(StencilFunction),
					ReferenceStencil, StencilMask);
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
