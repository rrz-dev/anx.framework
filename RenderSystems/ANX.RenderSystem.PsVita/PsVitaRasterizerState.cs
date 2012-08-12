using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using SceGraphics = Sce.PlayStation.Core.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaRasterizerState : INativeRasterizerState
	{
		#region Public
		public bool IsBound
		{
			get;
			private set;
		}
		
		public CullMode CullMode
		{
			set;
			private get;
		}

		public bool ScissorTestEnable
		{
			set;
			private get;
		}

		public FillMode FillMode
		{
			set;
			private get;
		}

		public float SlopeScaleDepthBias
		{
			set;
			private get;
		}

		public float DepthBias
		{
			set;
			private get;
		}

		public bool MultiSampleAntiAlias
		{
			set;
			private get;
		}
		#endregion

		#region Constructor
		internal PsVitaRasterizerState()
		{
			IsBound = false;
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
		{
			IsBound = true;
			var context = PsVitaGraphicsDevice.Current.NativeContext;

			#region Cull Mode
			if (CullMode == CullMode.None)
			{
				context.Disable(SceGraphics.EnableMode.CullFace);
				context.SetCullFace(SceGraphics.CullFaceMode.FrontAndBack,
					SceGraphics.CullFaceDirection.Cw);
			}
			else
			{
				context.Enable(SceGraphics.EnableMode.CullFace);

				var cullMode = CullMode == CullMode.None ?
					SceGraphics.CullFaceMode.FrontAndBack :
					CullMode == CullMode.CullClockwiseFace ?
					SceGraphics.CullFaceMode.Front :
					SceGraphics.CullFaceMode.Back;

				context.SetCullFace(cullMode, SceGraphics.CullFaceDirection.Cw);
			}
			#endregion

			// TODO
			//GL.PolygonMode(MaterialFace.FrontAndBack,
			//  FillMode == FillMode.WireFrame ? PolygonMode.Line : PolygonMode.Fill);

			#region ScissorTestEnable
			if (ScissorTestEnable)
			{
				context.Enable(SceGraphics.EnableMode.ScissorTest);
			}
			else
			{
				context.Disable(SceGraphics.EnableMode.ScissorTest);
			}
			#endregion

			#region DepthBias / SlopeScaleDepthBias (TODO: test!)
			// NOTE: http://www.opengl.org/sdk/docs/man/xhtml/glPolygonOffset.xml

			// Good article about difference between OpenGL and DirectX concerning
			// Depth Bias: http://aras-p.info/blog/2008/06/12/depth-bias-and-the-power-of-deceiving-yourself/

			if (DepthBias != 0f &&
				SlopeScaleDepthBias != 0f)
			{
				context.Enable(SceGraphics.EnableMode.PolygonOffsetFill);
				context.SetPolygonOffset(SlopeScaleDepthBias, DepthBias);
			}
			else
			{
				context.Disable(SceGraphics.EnableMode.PolygonOffsetFill);
			}
			#endregion

			#region MultiSampleAntiAlias (TODO)
			if (MultiSampleAntiAlias)
			{
				//GL.Enable(EnableCap.Multisample);
			}
			else
			{
				//GL.Disable(EnableCap.Multisample);
			}
			#endregion
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
