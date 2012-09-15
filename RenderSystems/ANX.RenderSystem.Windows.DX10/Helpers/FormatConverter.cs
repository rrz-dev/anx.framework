using System;
using ANX.Framework.Graphics;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10.Helpers
{
	internal class FormatConverter
	{
		#region Translate (TextureFilter)
		public static Dx10.Filter Translate(TextureFilter filter)
		{
			switch (filter)
			{
				case TextureFilter.Anisotropic:
					return Dx10.Filter.Anisotropic;
				case TextureFilter.Linear:
					return Dx10.Filter.MinMagMipLinear;
				case TextureFilter.LinearMipPoint:
					return Dx10.Filter.MinMagMipPoint;
				case TextureFilter.MinLinearMagPointMipLinear:
					return Dx10.Filter.MinLinearMagPointMipLinear;
				case TextureFilter.MinLinearMagPointMipPoint:
					return Dx10.Filter.MinLinearMagMipPoint;
				case TextureFilter.MinPointMagLinearMipLinear:
					return Dx10.Filter.MinPointMagMipLinear;
				case TextureFilter.MinPointMagLinearMipPoint:
					return Dx10.Filter.MinPointMagLinearMipPoint;
				case TextureFilter.Point:
					return Dx10.Filter.MinMagMipPoint;
				case TextureFilter.PointMipLinear:
					return Dx10.Filter.MinMagPointMipLinear;
			}

			throw new NotImplementedException();
		}
		#endregion

		#region Translate (TextureAddressMode)
		public static Dx10.TextureAddressMode Translate(TextureAddressMode addressMode)
		{
			switch (addressMode)
			{
				case TextureAddressMode.Clamp:
					return Dx10.TextureAddressMode.Clamp;
				case TextureAddressMode.Mirror:
					return Dx10.TextureAddressMode.Mirror;
				case TextureAddressMode.Wrap:
					return Dx10.TextureAddressMode.Wrap;
			}

			return Dx10.TextureAddressMode.Clamp;
		}
		#endregion

		#region Translate (BlendFunction)
		public static Dx10.BlendOperation Translate(BlendFunction blendFunction)
		{
			switch (blendFunction)
			{
				case BlendFunction.Add:
					return Dx10.BlendOperation.Add;
				case BlendFunction.Max:
					return Dx10.BlendOperation.Maximum;
				case BlendFunction.Min:
					return Dx10.BlendOperation.Minimum;
				case BlendFunction.ReverseSubtract:
					return Dx10.BlendOperation.ReverseSubtract;
				case BlendFunction.Subtract:
					return Dx10.BlendOperation.Subtract;
			}

			throw new NotImplementedException();
		}
		#endregion

		#region Translate (Blend)
		public static Dx10.BlendOption Translate(Blend blend)
		{
			switch (blend)
			{
				case Blend.BlendFactor:
					return Dx10.BlendOption.BlendFactor;
				case Blend.DestinationAlpha:
					return Dx10.BlendOption.DestinationAlpha;
				case Blend.DestinationColor:
					return Dx10.BlendOption.DestinationColor;
				case Blend.InverseBlendFactor:
					return Dx10.BlendOption.InverseBlendFactor;
				case Blend.InverseDestinationAlpha:
					return Dx10.BlendOption.InverseDestinationAlpha;
				case Blend.InverseDestinationColor:
					return Dx10.BlendOption.InverseDestinationColor;
				case Blend.InverseSourceAlpha:
					return Dx10.BlendOption.InverseSourceAlpha;
				case Blend.InverseSourceColor:
					return Dx10.BlendOption.InverseSourceColor;
				case Blend.One:
					return Dx10.BlendOption.One;
				case Blend.SourceAlpha:
					return Dx10.BlendOption.SourceAlpha;
				case Blend.SourceAlphaSaturation:
					return Dx10.BlendOption.SourceAlphaSaturate;
				case Blend.SourceColor:
					return Dx10.BlendOption.SourceColor;
				case Blend.Zero:
					return Dx10.BlendOption.Zero;
			}

			throw new NotImplementedException();
		}
		#endregion

		#region Translate (ColorWriteChannels)
		public static Dx10.ColorWriteMaskFlags Translate(ColorWriteChannels colorWriteChannels)
		{
			Dx10.ColorWriteMaskFlags mask = 0;

			if ((colorWriteChannels & ColorWriteChannels.All) == ColorWriteChannels.All)
				mask |= Dx10.ColorWriteMaskFlags.All;

			if ((colorWriteChannels & ColorWriteChannels.Alpha) == ColorWriteChannels.Alpha)
				mask |= Dx10.ColorWriteMaskFlags.Alpha;

			if ((colorWriteChannels & ColorWriteChannels.Blue) == ColorWriteChannels.Blue)
				mask |= Dx10.ColorWriteMaskFlags.Blue;

			if ((colorWriteChannels & ColorWriteChannels.Green) == ColorWriteChannels.Green)
				mask |= Dx10.ColorWriteMaskFlags.Green;

			if ((colorWriteChannels & ColorWriteChannels.Red) == ColorWriteChannels.Red)
				mask |= Dx10.ColorWriteMaskFlags.Red;

			return mask;
		}
		#endregion

		#region Translate (StencilOperation)
		public static Dx10.StencilOperation Translate(StencilOperation stencilOperation)
		{
			switch (stencilOperation)
			{
				case StencilOperation.Decrement:
					return Dx10.StencilOperation.Decrement;
				case StencilOperation.DecrementSaturation:
					return Dx10.StencilOperation.DecrementAndClamp;
				case StencilOperation.Increment:
					return Dx10.StencilOperation.Increment;
				case StencilOperation.IncrementSaturation:
					return Dx10.StencilOperation.IncrementAndClamp;
				case StencilOperation.Invert:
					return Dx10.StencilOperation.Invert;
				case StencilOperation.Keep:
					return Dx10.StencilOperation.Keep;
				case StencilOperation.Replace:
					return Dx10.StencilOperation.Replace;
				case StencilOperation.Zero:
					return Dx10.StencilOperation.Zero;
			}

			throw new NotImplementedException("unknown StencilOperation");
		}
		#endregion

		#region Translate (CompareFunction)
		public static Dx10.Comparison Translate(CompareFunction compareFunction)
		{
			switch (compareFunction)
			{
				case CompareFunction.Always:
					return Dx10.Comparison.Always;
				case CompareFunction.Equal:
					return Dx10.Comparison.Equal;
				case CompareFunction.Greater:
					return Dx10.Comparison.Greater;
				case CompareFunction.GreaterEqual:
					return Dx10.Comparison.GreaterEqual;
				case CompareFunction.Less:
					return Dx10.Comparison.Less;
				case CompareFunction.LessEqual:
					return Dx10.Comparison.LessEqual;
				case CompareFunction.Never:
					return Dx10.Comparison.Never;
				case CompareFunction.NotEqual:
					return Dx10.Comparison.NotEqual;
			}

			throw new NotImplementedException("unknown CompareFunction");
		}
		#endregion

		#region Translate (CullMode)
		public static Dx10.CullMode Translate(CullMode cullMode)
		{
			if (cullMode == CullMode.CullClockwiseFace)
				return Dx10.CullMode.Front;
			else if (cullMode == CullMode.CullCounterClockwiseFace)
				return Dx10.CullMode.Back;
			else
				return Dx10.CullMode.None;
		}
		#endregion

		#region Translate (FillMode)
		public static Dx10.FillMode Translate(FillMode fillMode)
		{
			return fillMode == FillMode.WireFrame ? Dx10.FillMode.Wireframe : Dx10.FillMode.Solid;
		}
		#endregion
	}
}
