using System;
using ANX.Framework.Graphics;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	internal class FormatConverter
	{
		#region FormatSize
		public static int FormatSize(SurfaceFormat format)
		{
			switch (format)
			{
				case SurfaceFormat.Vector4:
					return 16;
				//case SurfaceFormat.Vector3:
				//    return 12;
				case SurfaceFormat.Vector2:
					return 8;
				case SurfaceFormat.Single:
				case SurfaceFormat.Color:
					//case SurfaceFormat.RGBA1010102:
					//case SurfaceFormat.RG32:
					return 4;
				//case SurfaceFormat.BGR565:
				//case SurfaceFormat.BGRA5551:
				//    return 2;
				case SurfaceFormat.Dxt1:
				case SurfaceFormat.Dxt3:
				case SurfaceFormat.Dxt5:
				case SurfaceFormat.Alpha8:
					return 1;
				default:
					throw new ArgumentException("Invalid format");
			}
		}
		#endregion

		#region Translate
		public static SharpDX.DXGI.Format Translate(SurfaceFormat surfaceFormat)
		{
			switch (surfaceFormat)
			{
				case SurfaceFormat.Color:
					return SharpDX.DXGI.Format.R8G8B8A8_UNorm;
				case SurfaceFormat.Dxt3:
					return SharpDX.DXGI.Format.BC2_UNorm;
				case SurfaceFormat.Dxt5:
					return SharpDX.DXGI.Format.BC3_UNorm;
			}

			throw new Exception("can't translate SurfaceFormat: " + surfaceFormat.ToString());
		}
		#endregion

		#region Translate
		public static Format Translate(DepthFormat depthFormat)
		{
			switch (depthFormat)
			{
				case DepthFormat.Depth16:
					return Format.D16_UNorm;
				case DepthFormat.Depth24:
				//TODO: no DirectX10 24Bit depth format???                    
				case DepthFormat.Depth24Stencil8:
					return Format.D24_UNorm_S8_UInt;
				case DepthFormat.None:
					return Format.Unknown;
			}

			throw new Exception("can't translate DepthFormat: " + depthFormat.ToString());
		}
		#endregion

		#region Translate
		public static SurfaceFormat Translate(SharpDX.DXGI.Format format)
		{
			switch (format)
			{
				case SharpDX.DXGI.Format.R8G8B8A8_UNorm:
					return SurfaceFormat.Color;
				case SharpDX.DXGI.Format.BC2_UNorm:
					return SurfaceFormat.Dxt3;
				case SharpDX.DXGI.Format.BC3_UNorm:
					return SurfaceFormat.Dxt5;
			}

			throw new Exception("can't translate Format: " + format.ToString());
		}
		#endregion

		#region Translate
		public static Dx11.Filter Translate(TextureFilter filter)
		{
			switch (filter)
			{
				case TextureFilter.Anisotropic:
					return Dx11.Filter.Anisotropic;
				case TextureFilter.Linear:
					return Dx11.Filter.MinMagMipLinear;
				case TextureFilter.LinearMipPoint:
					return Dx11.Filter.MinMagMipPoint;
				case TextureFilter.MinLinearMagPointMipLinear:
					return Dx11.Filter.MinLinearMagPointMipLinear;
				case TextureFilter.MinLinearMagPointMipPoint:
					return Dx11.Filter.MinLinearMagMipPoint;
				case TextureFilter.MinPointMagLinearMipLinear:
					return Dx11.Filter.MinPointMagMipLinear;
				case TextureFilter.MinPointMagLinearMipPoint:
					return Dx11.Filter.MinPointMagLinearMipPoint;
				case TextureFilter.Point:
					return Dx11.Filter.MinMagMipPoint;
				case TextureFilter.PointMipLinear:
					return Dx11.Filter.MinMagPointMipLinear;
			}

			throw new NotImplementedException();
		}
		#endregion
		
		#region Translate
		public static Dx11.TextureAddressMode Translate(TextureAddressMode addressMode)
		{
			switch (addressMode)
			{
				case TextureAddressMode.Clamp:
					return Dx11.TextureAddressMode.Clamp;
				case TextureAddressMode.Mirror:
					return Dx11.TextureAddressMode.Mirror;
				case TextureAddressMode.Wrap:
					return Dx11.TextureAddressMode.Wrap;
			}

			return Dx11.TextureAddressMode.Clamp;
		}
		#endregion
		
		#region Translate
		public static PrimitiveTopology Translate(PrimitiveType primitiveType)
		{
			switch (primitiveType)
			{
				case PrimitiveType.LineList:
					return PrimitiveTopology.LineList;
				case PrimitiveType.LineStrip:
					return PrimitiveTopology.LineStrip;
				case PrimitiveType.TriangleList:
					return PrimitiveTopology.TriangleList;
				case PrimitiveType.TriangleStrip:
					return PrimitiveTopology.TriangleStrip;
				default:
					throw new InvalidOperationException("unknown PrimitiveType: " + primitiveType.ToString());
			}
		}
		#endregion
		
		#region Translate
		public static SharpDX.DXGI.Format Translate(IndexElementSize indexElementSize)
		{
			switch (indexElementSize)
			{
				case IndexElementSize.SixteenBits:
					return Format.R16_UInt;
				case IndexElementSize.ThirtyTwoBits:
					return Format.R32_UInt;
				default:
					throw new InvalidOperationException("unknown IndexElementSize: " + indexElementSize.ToString());
			}
		}
		#endregion
		
		#region Translate
		public static string Translate(VertexElementUsage usage)
		{
			//TODO: map the other Usages
			if (usage == VertexElementUsage.TextureCoordinate)
			{
				return "TEXCOORD";
			}
			else
			{
				return usage.ToString().ToUpperInvariant();
			}
		}
		#endregion
		
		#region Translate
		public static Dx11.BlendOperation Translate(BlendFunction blendFunction)
		{
			switch (blendFunction)
			{
				case BlendFunction.Add:
					return Dx11.BlendOperation.Add;
				case BlendFunction.Max:
					return Dx11.BlendOperation.Maximum;
				case BlendFunction.Min:
					return Dx11.BlendOperation.Minimum;
				case BlendFunction.ReverseSubtract:
					return Dx11.BlendOperation.ReverseSubtract;
				case BlendFunction.Subtract:
					return Dx11.BlendOperation.Subtract;
			}

			throw new NotImplementedException();
		}
		#endregion
		
		#region Translate
		public static Dx11.BlendOption Translate(Blend blend)
		{
			switch (blend)
			{
				case Blend.BlendFactor:
					return Dx11.BlendOption.BlendFactor;
				case Blend.DestinationAlpha:
					return Dx11.BlendOption.DestinationAlpha;
				case Blend.DestinationColor:
					return Dx11.BlendOption.DestinationColor;
				case Blend.InverseBlendFactor:
					return Dx11.BlendOption.InverseBlendFactor;
				case Blend.InverseDestinationAlpha:
					return Dx11.BlendOption.InverseDestinationAlpha;
				case Blend.InverseDestinationColor:
					return Dx11.BlendOption.InverseDestinationColor;
				case Blend.InverseSourceAlpha:
					return Dx11.BlendOption.InverseSourceAlpha;
				case Blend.InverseSourceColor:
					return Dx11.BlendOption.InverseSourceColor;
				case Blend.One:
					return Dx11.BlendOption.One;
				case Blend.SourceAlpha:
					return Dx11.BlendOption.SourceAlpha;
				case Blend.SourceAlphaSaturation:
					return Dx11.BlendOption.SourceAlphaSaturate;
				case Blend.SourceColor:
					return Dx11.BlendOption.SourceColor;
				case Blend.Zero:
					return Dx11.BlendOption.Zero;
			}

			throw new NotImplementedException();
		}
		#endregion
		
		#region Translate
		public static Dx11.ColorWriteMaskFlags Translate(ColorWriteChannels colorWriteChannels)
		{
			Dx11.ColorWriteMaskFlags mask = 0;

			if ((colorWriteChannels & ColorWriteChannels.All) > 0)
			{
				mask |= Dx11.ColorWriteMaskFlags.All;
			}

			if ((colorWriteChannels & ColorWriteChannels.Alpha) > 0)
			{
				mask |= Dx11.ColorWriteMaskFlags.Alpha;
			}

			if ((colorWriteChannels & ColorWriteChannels.Blue) > 0)
			{
				mask |= Dx11.ColorWriteMaskFlags.Blue;
			}

			if ((colorWriteChannels & ColorWriteChannels.Green) > 0)
			{
				mask |= Dx11.ColorWriteMaskFlags.Green;
			}

			if ((colorWriteChannels & ColorWriteChannels.Red) > 0)
			{
				mask |= Dx11.ColorWriteMaskFlags.Red;
			}

			return mask;
		}
		#endregion
		
		#region Translate
		public static Dx11.StencilOperation Translate(StencilOperation stencilOperation)
		{
			switch (stencilOperation)
			{
				case StencilOperation.Decrement:
					return Dx11.StencilOperation.Decrement;
				case StencilOperation.DecrementSaturation:
					return Dx11.StencilOperation.DecrementAndClamp;
				case StencilOperation.Increment:
					return Dx11.StencilOperation.Increment;
				case StencilOperation.IncrementSaturation:
					return Dx11.StencilOperation.IncrementAndClamp;
				case StencilOperation.Invert:
					return Dx11.StencilOperation.Invert;
				case StencilOperation.Keep:
					return Dx11.StencilOperation.Keep;
				case StencilOperation.Replace:
					return Dx11.StencilOperation.Replace;
				case StencilOperation.Zero:
					return Dx11.StencilOperation.Zero;
			}

			throw new NotImplementedException("unknown StencilOperation");
		}
		#endregion
		
		#region Translate
		public static Dx11.Comparison Translate(CompareFunction compareFunction)
		{
			switch (compareFunction)
			{
				case CompareFunction.Always:
					return Dx11.Comparison.Always;
				case CompareFunction.Equal:
					return Dx11.Comparison.Equal;
				case CompareFunction.Greater:
					return Dx11.Comparison.Greater;
				case CompareFunction.GreaterEqual:
					return Dx11.Comparison.GreaterEqual;
				case CompareFunction.Less:
					return Dx11.Comparison.Less;
				case CompareFunction.LessEqual:
					return Dx11.Comparison.LessEqual;
				case CompareFunction.Never:
					return Dx11.Comparison.Never;
				case CompareFunction.NotEqual:
					return Dx11.Comparison.NotEqual;
			}

			throw new NotImplementedException("unknown CompareFunction");
		}
		#endregion
		
		#region Translate
		public static Dx11.CullMode Translate(CullMode cullMode)
		{
			if (cullMode == CullMode.CullClockwiseFace)
			{
				return Dx11.CullMode.Front;
			}
			else if (cullMode == CullMode.CullCounterClockwiseFace)
			{
				return Dx11.CullMode.Back;
			}
			else
			{
				return Dx11.CullMode.None;
			}
		}
		#endregion

		#region Translate
		public static Dx11.FillMode Translate(FillMode fillMode)
		{
			if (fillMode == FillMode.WireFrame)
			{
				return Dx11.FillMode.Wireframe;
			}
			else
			{
				return Dx11.FillMode.Solid;
			}
		}
		#endregion
	}
}
