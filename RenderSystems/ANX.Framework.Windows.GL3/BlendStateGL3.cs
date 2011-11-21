using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
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
	/// Native Blend State object for OpenGL.
	/// <para />
	/// Basically this is a wrapper class for setting the different values all
	/// at once, because OpenGL has no State objects like DirectX.
	/// <para />
	/// For Information on OpenGL blending: http://www.opengl.org/wiki/Blending
	/// </summary>
	public class BlendStateGL3 : INativeBlendState
	{
		#region Public
		#region IsBound
		/// <summary>
		/// Flag if the blend state is bound to the device or not.
		/// </summary>
		public bool IsBound
		{
			get;
			private set;
		}
		#endregion

		#region AlphaBlendFunction
		public BlendFunction AlphaBlendFunction
		{
			set;
			private get;
		}
		#endregion

		#region ColorBlendFunction
		public BlendFunction ColorBlendFunction
		{
			set;
			private get;
		}
		#endregion

		#region AlphaSourceBlend
		public Blend AlphaSourceBlend
		{
			set;
			private get;
		}
		#endregion

		#region AlphaDestinationBlend
		public Blend AlphaDestinationBlend
		{
			set;
			private get;
		}
		#endregion

		#region ColorSourceBlend
		public Blend ColorSourceBlend
		{
			set;
			private get;
		}
		#endregion

		#region ColorDestinationBlend
		public Blend ColorDestinationBlend
		{
			set;
			private get;
		}
		#endregion

		#region ColorWriteChannels
		public ColorWriteChannels ColorWriteChannels
		{
			set;
			private get;
		}
		#endregion

		#region ColorWriteChannels1
		public ColorWriteChannels ColorWriteChannels1
		{
			set;
			private get;
		}
		#endregion

		#region ColorWriteChannels2
		public ColorWriteChannels ColorWriteChannels2
		{
			set;
			private get;
		}
		#endregion

		#region ColorWriteChannels3
		public ColorWriteChannels ColorWriteChannels3
		{
			set;
			private get;
		}
		#endregion

		#region BlendFactor
		public Color BlendFactor
		{
			set;
			private get;
		}
		#endregion

		#region MultiSampleMask
		public int MultiSampleMask
		{
			set;
			private get;
		}
		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new OpenGL Blend State wrapper object.
		/// </summary>
		internal BlendStateGL3()
		{
			IsBound = false;
		}
		#endregion

		#region Apply
		/// <summary>
		/// Apply the blend state on the graphics device.
		/// </summary>
		/// <param name="graphicsDevice">The current graphics device.</param>
		public void Apply(GraphicsDevice graphicsDevice)
		{
			IsBound = true;

			GL.BlendEquationSeparate(
				TranslateBlendFunction(ColorBlendFunction),
				TranslateBlendFunction(AlphaBlendFunction));
			ErrorHelper.Check("BlendEquationSeparate");

			GL.BlendFuncSeparate(
				TranslateBlendSrc(ColorSourceBlend),
				TranslateBlendDest(ColorDestinationBlend),
				TranslateBlendSrc(AlphaSourceBlend),
				TranslateBlendDest(AlphaDestinationBlend));
			ErrorHelper.Check("BlendFuncSeparate");

			SetColorWriteChannel(0, ColorWriteChannels);
			SetColorWriteChannel(1, ColorWriteChannels1);
			SetColorWriteChannel(2, ColorWriteChannels2);
			SetColorWriteChannel(3, ColorWriteChannels3);

			GL.BlendColor(BlendFactor.R * DatatypesMapping.ColorMultiplier,
				BlendFactor.G * DatatypesMapping.ColorMultiplier,
				BlendFactor.B * DatatypesMapping.ColorMultiplier,
				BlendFactor.A * DatatypesMapping.ColorMultiplier);
			ErrorHelper.Check("BlendColor");

// TODO: multi sample mask
		}
		#endregion

		#region Release
		/// <summary>
		/// Release the blend state.
		/// </summary>
		public void Release()
		{
			IsBound = false;
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose this blend state object.
		/// </summary>
		public void Dispose()
		{
		}

		#endregion

		#region SetColorWriteChannel
		/// <summary>
		/// Set the color mask for the specified index.
		/// </summary>
		/// <param name="index">Index of the color mask.</param>
		/// <param name="channels">Mask channels to enable.</param>
		private void SetColorWriteChannel(int index, ColorWriteChannels channels)
		{
			bool r = (channels == Graphics.ColorWriteChannels.All ||
				channels == Graphics.ColorWriteChannels.Red);
			bool g = (channels == Graphics.ColorWriteChannels.All ||
				channels == Graphics.ColorWriteChannels.Green);
			bool b = (channels == Graphics.ColorWriteChannels.All ||
				channels == Graphics.ColorWriteChannels.Blue);
			bool a = (channels == Graphics.ColorWriteChannels.All ||
				channels == Graphics.ColorWriteChannels.Alpha);

			GL.ColorMask(index, r, g, b, a);
			ErrorHelper.Check("ColorMask");
		}
		#endregion

		#region TranslateBlendSrc
		/// <summary>
		/// Translate the ANX Blend mode to the OpenGL Blend Factor Source.
		/// </summary>
		/// <param name="func">ANX Blend Function.</param>
		/// <returns>OpenGL Blend Factor Source.</returns>
		private BlendingFactorSrc TranslateBlendSrc(Blend blending)
		{
			switch (blending)
			{
				default:
					throw new NotSupportedException("The blend mode '" + blending +
						"' is not supported for OpenGL BlendingFactorSrc!");

				case Blend.SourceAlpha:
					return BlendingFactorSrc.SrcAlpha;

				case Blend.DestinationAlpha:
					return BlendingFactorSrc.DstAlpha;

				case Blend.DestinationColor:
					return BlendingFactorSrc.DstColor;

				case Blend.InverseDestinationAlpha:
					return BlendingFactorSrc.OneMinusDstAlpha;

				case Blend.InverseDestinationColor:
					return BlendingFactorSrc.OneMinusDstColor;

				case Blend.InverseSourceAlpha:
					return BlendingFactorSrc.OneMinusSrcAlpha;

				case Blend.One:
					return BlendingFactorSrc.One;

				case Blend.SourceAlphaSaturation:
					return BlendingFactorSrc.SrcAlphaSaturate;

				case Blend.Zero:
					return BlendingFactorSrc.Zero;
			}
		}
		#endregion

		#region TranslateBlendDest
		/// <summary>
		/// Translate the ANX Blend mode to the OpenGL Blend Factor Destination.
		/// </summary>
		/// <param name="func">ANX Blend Function.</param>
		/// <returns>OpenGL Blend Factor Destination.</returns>
		private BlendingFactorDest TranslateBlendDest(Blend blending)
		{
			switch (blending)
			{
				case Blend.SourceAlpha:
					return BlendingFactorDest.SrcAlpha;

				default:
					throw new NotSupportedException("The blend mode '" + blending +
						"' is not supported for OpenGL BlendingFactorDest!");

				case Blend.DestinationAlpha:
					return BlendingFactorDest.DstAlpha;

				case Blend.DestinationColor:
					return BlendingFactorDest.DstColor;

				case Blend.InverseDestinationAlpha:
					return BlendingFactorDest.OneMinusDstAlpha;

				case Blend.InverseDestinationColor:
					return BlendingFactorDest.OneMinusDstColor;

				case Blend.InverseSourceAlpha:
					return BlendingFactorDest.OneMinusSrcAlpha;

				case Blend.InverseSourceColor:
					return BlendingFactorDest.OneMinusSrcColor;

				case Blend.One:
					return BlendingFactorDest.One;

				case Blend.SourceColor:
					return BlendingFactorDest.SrcColor;

				case Blend.Zero:
					return BlendingFactorDest.Zero;
			}
		}
		#endregion

		#region TranslateBlendFunction
		/// <summary>
		/// Translate the ANX Blend Function to the OpenGL Blend Equation Mode.
		/// </summary>
		/// <param name="func">ANX Blend Function.</param>
		/// <returns>OpenGL Blend Equation Mode.</returns>
		private BlendEquationMode TranslateBlendFunction(BlendFunction func)
		{
		  switch (func)
		  {
				default:
		    case BlendFunction.Add:
					return BlendEquationMode.FuncAdd;

				case BlendFunction.Subtract:
					return BlendEquationMode.FuncSubtract;

				case BlendFunction.ReverseSubtract:
					return BlendEquationMode.FuncReverseSubtract;

				case BlendFunction.Min:
					return BlendEquationMode.Min;

				case BlendFunction.Max:
					return BlendEquationMode.Max;
		  }
		}
		#endregion
	}
}
