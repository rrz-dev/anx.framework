using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using OpenTK.Graphics.OpenGL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.GL3
{
	/// <summary>
	/// Native Rasterizer State object for OpenGL.
	/// <para />
	/// Basically this is a wrapper class for setting the different values all
	/// at once, because OpenGL has no State objects like DirectX.
	/// </summary>
	[PercentageComplete(100)]
	[TestStateAttribute(TestStateAttribute.TestState.Untested)]
	public class RasterizerStateGL3 : INativeRasterizerState
	{
		#region Private
		internal static RasterizerStateGL3 Current
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

		#region CullMode
		/// <summary>
		/// The cull mode of the state object.
		/// </summary>
		public CullMode CullMode
		{
			set;
			private get;
		}
		#endregion

		#region ScissorTestEnable
		/// <summary>
		/// Flag if the state object has scissor test enabled.
		/// </summary>
		public bool ScissorTestEnable
		{
			set;
			private get;
		}
		#endregion

		#region FillMode
		/// <summary>
		/// The fill mode of the state object.
		/// </summary>
		public FillMode FillMode
		{
			set;
			private get;
		}
		#endregion

		#region SlopeScaleDepthBias
		/// <summary>
		/// The SlopeScaleDepthBias of the state object.
		/// </summary>
		public float SlopeScaleDepthBias
		{
			set;
			private get;
		}
		#endregion

		#region DepthBias
		/// <summary>
		/// The depth bias of the state object.
		/// </summary>
		public float DepthBias
		{
			set;
			private get;
		}
		#endregion

		#region MultiSampleAntiAlias
		/// <summary>
		/// Flag if the state object has MSAA enabled.
		/// </summary>
		public bool MultiSampleAntiAlias
		{
			set;
			private get;
		}
		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new rasterizer state object.
		/// </summary>
		internal RasterizerStateGL3()
		{
			IsBound = false;
		}
		#endregion

		#region Apply
		/// <summary>
		/// Apply the rasterizer state to the graphics device.
		/// </summary>
		/// <param name="graphicsDevice">The current graphics device.</param>
		public void Apply(GraphicsDevice graphicsDevice)
		{
			IsBound = true;
			Current = this;

			#region Cull Mode
			GL.FrontFace(FrontFaceDirection.Cw);
			ErrorHelper.Check("FrontFace");
			if (CullMode == CullMode.None)
			{
				GL.Disable(EnableCap.CullFace);
				GL.CullFace(CullFaceMode.FrontAndBack);
			}
			else
			{
				GL.Enable(EnableCap.CullFace);
				GL.CullFace(CullMode == CullMode.None ?
					CullFaceMode.FrontAndBack :
					CullMode == CullMode.CullClockwiseFace ?
					CullFaceMode.Front :
					CullFaceMode.Back);
			}
			ErrorHelper.Check("Set CullMode");
			#endregion

			GL.PolygonMode(MaterialFace.FrontAndBack,
				FillMode == FillMode.WireFrame ? PolygonMode.Line : PolygonMode.Fill);
			ErrorHelper.Check("PolygonMode");

			#region ScissorTestEnable
			if (ScissorTestEnable)
			{
				GL.Enable(EnableCap.ScissorTest);
			}
			else
			{
				GL.Disable(EnableCap.ScissorTest);
			}
			ErrorHelper.Check("Set ScissorTest");
			#endregion

			#region DepthBias / SlopeScaleDepthBias (TODO: test!)
			// NOTE: http://www.opengl.org/sdk/docs/man/xhtml/glPolygonOffset.xml

			// Good article about difference between OpenGL and DirectX concerning
			// Depth Bias: http://aras-p.info/blog/2008/06/12/depth-bias-and-the-power-of-deceiving-yourself/

			if (DepthBias != 0f &&
				SlopeScaleDepthBias != 0f)
			{
				GL.Enable(EnableCap.PolygonOffsetFill);
				GL.PolygonOffset(SlopeScaleDepthBias, DepthBias);
			}
			else
			{
				GL.Disable(EnableCap.PolygonOffsetFill);
			}
			ErrorHelper.Check("Set DepthBias");
			#endregion

			#region MultiSampleAntiAlias
			if (MultiSampleAntiAlias)
			{
				GL.Enable(EnableCap.Multisample);
			}
			else
			{
				GL.Disable(EnableCap.Multisample);
			}
			ErrorHelper.Check("Set Multisample");
			#endregion
		}
		#endregion

		#region Release
		/// <summary>
		/// Release the rasterizer state.
		/// </summary>
		public void Release()
		{
			IsBound = false;
			Current = null;
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose the rasterizer state object.
		/// </summary>
		public void Dispose()
		{
		}
		#endregion
	}
}
