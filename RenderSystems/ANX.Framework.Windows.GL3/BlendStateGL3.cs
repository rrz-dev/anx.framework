using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using OpenTK.Graphics.OpenGL;
using ANX.RenderSystem.Windows.GL3.Helpers;
using ANX.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.GL3
{
	/// <summary>
	/// Native Blend State object for OpenGL.
	/// <para />
	/// Basically this is a wrapper class for setting the different values all
	/// at once, because OpenGL has no State objects like DirectX.
	/// <para />
	/// For Information on OpenGL blending: http://www.opengl.org/wiki/Blending
	/// </summary>
	[PercentageComplete(90)]
	[TestStateAttribute(TestStateAttribute.TestState.Untested)]
	public class BlendStateGL3 : INativeBlendState
	{
		#region Private
		internal static BlendStateGL3 Current
		{
			get;
			private set;
		}
		#endregion

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

		#region Apply (TODO)
		/// <summary>
		/// Apply the blend state on the graphics device.
		/// </summary>
		/// <param name="graphicsDevice">The current graphics device.</param>
		public void Apply(GraphicsDevice graphicsDevice)
		{
			IsBound = true;
			Current = this;

			GL.Enable(EnableCap.Blend);

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
			Current = null;
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
			bool r = (channels | ColorWriteChannels.Red) == channels;
			bool g = (channels | ColorWriteChannels.Green) == channels;
			bool b = (channels | ColorWriteChannels.Blue) == channels;
			bool a = (channels | ColorWriteChannels.Alpha) == channels;

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

				default:
					throw new ArgumentException("Unable to translate SourceBlend '" +
						blending + "' to OpenGL BlendingFactorSrc.");
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

				default:
					throw new ArgumentException("Unable to translate DestinationBlend '" +
						blending + "' to OpenGL BlendingFactorDest.");
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

			throw new ArgumentException("Unable to translate BlendFunction '" +
				func + "' to OpenGL BlendEquationMode.");
		}
		#endregion
	}
}
