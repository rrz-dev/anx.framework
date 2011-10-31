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
	/// Native Rasterizer State object for OpenGL.
	/// <para />
	/// Basically this is a wrapper class for setting the different values all
	/// at once, because OpenGL has no State objects like DirectX.
	/// </summary>
	public class RasterizerStateGL3 : INativeRasterizerState
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

			#region Cull Mode
			GL.FrontFace(FrontFaceDirection.Cw);
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
			#endregion

			GL.PolygonMode(MaterialFace.FrontAndBack,
				FillMode == FillMode.WireFrame ? PolygonMode.Line : PolygonMode.Fill);

			#region ScissorTestEnable
			if (ScissorTestEnable)
			{
				GL.Enable(EnableCap.ScissorTest);
			}
			else
			{
				GL.Disable(EnableCap.ScissorTest);
			}
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
