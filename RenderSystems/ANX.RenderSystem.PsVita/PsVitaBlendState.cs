using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using SceGraphics = Sce.PlayStation.Core.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaBlendState : INativeBlendState
	{
		#region Public
		public bool IsBound
		{
			get;
			private set;
		}
		
		public BlendFunction AlphaBlendFunction
		{
			set;
			private get;
		}

		public BlendFunction ColorBlendFunction
		{
			set;
			private get;
		}

		public Blend AlphaSourceBlend
		{
			set;
			private get;
		}

		public Blend AlphaDestinationBlend
		{
			set;
			private get;
		}

		public Blend ColorSourceBlend
		{
			set;
			private get;
		}

		public Blend ColorDestinationBlend
		{
			set;
			private get;
		}

		public ColorWriteChannels ColorWriteChannels
		{
			set;
			private get;
		}

		public ColorWriteChannels ColorWriteChannels1
		{
			set;
			private get;
		}

		public ColorWriteChannels ColorWriteChannels2
		{
			set;
			private get;
		}

		public ColorWriteChannels ColorWriteChannels3
		{
			set;
			private get;
		}

		public Color BlendFactor
		{
			set;
			private get;
		}

		public int MultiSampleMask
		{
			set;
			private get;
		}
		#endregion

		#region Constructor
		internal PsVitaBlendState()
		{
			IsBound = false;
		}
		#endregion

		#region Apply (TODO)
		public void Apply(GraphicsDevice graphicsDevice)
		{
			IsBound = true;

			var context = PsVitaGraphicsDevice.Current.NativeContext;
			context.Enable(SceGraphics.EnableMode.Blend);

			context.SetBlendFuncRgb(TranslateBlendFunction(ColorBlendFunction),
				TranslateBlend(ColorSourceBlend), TranslateBlend(ColorDestinationBlend));

			context.SetBlendFuncAlpha(TranslateBlendFunction(AlphaBlendFunction),
				TranslateBlend(AlphaSourceBlend), TranslateBlend(AlphaDestinationBlend));
			
			SetColorWriteChannel(context, ColorWriteChannels);

			// TODO
			//SetColorWriteChannel(context, ColorWriteChannels1);
			//SetColorWriteChannel(context, ColorWriteChannels2);
			//SetColorWriteChannel(context, ColorWriteChannels3);
			
			//GL.BlendColor(BlendFactor.R * DatatypesMapping.ColorMultiplier,
			//    BlendFactor.G * DatatypesMapping.ColorMultiplier,
			//    BlendFactor.B * DatatypesMapping.ColorMultiplier,
			//    BlendFactor.A * DatatypesMapping.ColorMultiplier);
			//ErrorHelper.Check("BlendColor");

			// TODO: multi sample mask
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

		#region SetColorWriteChannel
		private void SetColorWriteChannel(SceGraphics.GraphicsContext context,
			ColorWriteChannels channels)
		{
			var mask = SceGraphics.ColorMask.None;
			if((channels & ColorWriteChannels.Red) != 0)
			{
				mask |= SceGraphics.ColorMask.R;
			}
			if((channels & ColorWriteChannels.Green) != 0)
			{
				mask |= SceGraphics.ColorMask.G;
			}
			if((channels & ColorWriteChannels.Blue) != 0)
			{
				mask |= SceGraphics.ColorMask.B;
			}
			if((channels & ColorWriteChannels.Alpha) != 0)
			{
				mask |= SceGraphics.ColorMask.A;
			}

			context.SetColorMask(mask);
		}
		#endregion

		#region TranslateBlend
		private SceGraphics.BlendFuncFactor TranslateBlend(Blend blending)
		{
			switch (blending)
			{
				case Blend.SourceAlpha:
					return SceGraphics.BlendFuncFactor.SrcAlpha;

				case Blend.DestinationAlpha:
					return SceGraphics.BlendFuncFactor.DstAlpha;

				case Blend.DestinationColor:
					return SceGraphics.BlendFuncFactor.DstColor;

				case Blend.InverseDestinationAlpha:
					return SceGraphics.BlendFuncFactor.OneMinusDstAlpha;

				case Blend.InverseDestinationColor:
					return SceGraphics.BlendFuncFactor.OneMinusDstColor;

				case Blend.InverseSourceAlpha:
					return SceGraphics.BlendFuncFactor.OneMinusSrcAlpha;

				case Blend.One:
					return SceGraphics.BlendFuncFactor.One;

				case Blend.SourceAlphaSaturation:
					return SceGraphics.BlendFuncFactor.SrcAlphaSaturate;

				case Blend.Zero:
					return SceGraphics.BlendFuncFactor.Zero;

				default:
					throw new ArgumentException("Unable to translate SourceBlend '" +
						blending + "' to PsVita BlendFuncFactor.");
			}
		}
		#endregion

		#region TranslateBlendFunction
		private SceGraphics.BlendFuncMode TranslateBlendFunction(BlendFunction func)
		{
			switch (func)
			{
				case BlendFunction.Add:
					return SceGraphics.BlendFuncMode.Add;

				case BlendFunction.Subtract:
					return SceGraphics.BlendFuncMode.Subtract;

				case BlendFunction.ReverseSubtract:
					return SceGraphics.BlendFuncMode.ReverseSubtract;
			}

			throw new ArgumentException("Unable to translate BlendFunction '" +
				func + "' to PsVita BlendFuncMode.");
		}
		#endregion
	}
}
