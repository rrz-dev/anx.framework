#region Using Statements
using System;
using ANX.Framework.Graphics;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.DXGI;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if DX10
namespace ANX.RenderSystem.Windows.DX10
#endif
#if DX11
namespace ANX.RenderSystem.Windows.DX11
#endif
{
	public static class DxFormatConverter
	{
		#region FormatSize (SurfaceFormat)
		public static int FormatSize(SurfaceFormat format)
		{
            // description of texture formats of DX10: http://msdn.microsoft.com/en-us/library/bb694531(v=VS.85).aspx
            // more helpfull information on DX10 textures: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205131(v=vs.85).aspx
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
				case SurfaceFormat.Alpha8:
					return 1;
			}

			throw new ArgumentException("Invalid format '" + format + "'.");
		}
		#endregion

        public static int FormatSize(VertexElementFormat format)
        {
            switch (format)
            {
                case VertexElementFormat.Byte4:
                    return 4;
                case VertexElementFormat.Color:
                    return 4;
                case VertexElementFormat.HalfVector2:
                    return 4;
                case VertexElementFormat.HalfVector4:
                    return 8;
                case VertexElementFormat.NormalizedShort2:
                    return 4;
                case VertexElementFormat.NormalizedShort4:
                    return 8;
                case VertexElementFormat.Short2:
                    return 4;
                case VertexElementFormat.Short4:
                    return 8;
                case VertexElementFormat.Single:
                    return 4;
                case VertexElementFormat.Vector2:
                    return 8;
                case VertexElementFormat.Vector3:
                    return 12;
                case VertexElementFormat.Vector4:
                    return 16;
            }

            throw new ArgumentException("Invalid format '" + format + "'.");
        }

		#region Translate (SurfaceFormat)
		public static SharpDX.DXGI.Format Translate(SurfaceFormat surfaceFormat)
		{
			switch (surfaceFormat)
			{
				case SurfaceFormat.Color:
					return SharpDX.DXGI.Format.R8G8B8A8_UNorm;
                case SurfaceFormat.Dxt1:
                    return SharpDX.DXGI.Format.BC1_UNorm;
				case SurfaceFormat.Dxt3:
					return SharpDX.DXGI.Format.BC2_UNorm;
				case SurfaceFormat.Dxt5:
					return SharpDX.DXGI.Format.BC3_UNorm;
			}

			throw new Exception("Can't translate SurfaceFormat: " + surfaceFormat);
		}
		#endregion

		#region Translate (DepthFormat)
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

		#region Translate (Format)
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
		
		#region Translate (PrimitiveType)
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
			}

			throw new InvalidOperationException("unknown PrimitiveType: " + primitiveType);
		}
		#endregion

		#region Translate (IndexElementSize)
		public static SharpDX.DXGI.Format Translate(IndexElementSize indexElementSize)
		{
			switch (indexElementSize)
			{
				case IndexElementSize.SixteenBits:
					return Format.R16_UInt;
				case IndexElementSize.ThirtyTwoBits:
					return Format.R32_UInt;
			}

			throw new InvalidOperationException("unknown IndexElementSize: " + indexElementSize);
		}
		#endregion

		#region Translate (VertexElement)
		public static string Translate(ref VertexElement element)
		{
		    //TODO: map the other Usages
			if (element.VertexElementUsage == VertexElementUsage.TextureCoordinate)
				return "TEXCOORD";

		    return element.VertexElementUsage.ToString().ToUpperInvariant();
		}
	    #endregion
		
		#region CalculateVertexCount
		public static int CalculateVertexCount(PrimitiveType type, int primitiveCount)
		{
		    switch (type)
		    {
		        case PrimitiveType.TriangleList:
		            return primitiveCount * 3;
		        case PrimitiveType.LineList:
		            return primitiveCount * 2;
		        case PrimitiveType.LineStrip:
		            return primitiveCount + 1;
		        case PrimitiveType.TriangleStrip:
		            return primitiveCount + 2;
		    }

		    throw new NotImplementedException("Couldn't calculate vertex count for PrimitiveType '" + type + "'.");
		}
	    #endregion

		#region ConvertVertexElementFormat
		public static Format ConvertVertexElementFormat(VertexElementFormat format)
		{
			switch (format)
			{
				case VertexElementFormat.Vector2:
					return Format.R32G32_Float;
				case VertexElementFormat.Vector3:
					return Format.R32G32B32_Float;
				case VertexElementFormat.Vector4:
					return Format.R32G32B32A32_Float;
				case VertexElementFormat.Color:
					return Format.R8G8B8A8_UNorm;
				case VertexElementFormat.Single:
					return Format.R32_Float;
				// TODO: validate
				case VertexElementFormat.Short2:
					return Format.R16G16_SInt;
				case VertexElementFormat.Short4:
					return Format.R16G16B16A16_SInt;
			}

			throw new Exception("Can't map '" + format + "' to DXGI.Format in Dx10 CreateInputElementFromVertexElement.");
		}
        #endregion

        public static ANX.Framework.Graphics.EffectParameterClass Translate(this ShaderVariableClass variableClass)
        {
            switch (variableClass)
            {
                case ShaderVariableClass.Vector:
                    return Framework.Graphics.EffectParameterClass.Vector;
                case ShaderVariableClass.Struct:
                    return Framework.Graphics.EffectParameterClass.Struct;
                case ShaderVariableClass.Scalar:
                    return Framework.Graphics.EffectParameterClass.Scalar;
                case ShaderVariableClass.Object:
                    return Framework.Graphics.EffectParameterClass.Object;
                case ShaderVariableClass.MatrixColumns:
                case ShaderVariableClass.MatrixRows:
                    return Framework.Graphics.EffectParameterClass.Matrix;
                default:
                    return (Framework.Graphics.EffectParameterClass)(-1);
            }
        }

        public static ANX.Framework.Graphics.EffectParameterType Translate(this ShaderVariableType variableType)
        {
            switch (variableType)
            {
                case ShaderVariableType.Bool:
                    return Framework.Graphics.EffectParameterType.Bool;
                case ShaderVariableType.Int:
                    return Framework.Graphics.EffectParameterType.Int32;
                case ShaderVariableType.Float:
                    return Framework.Graphics.EffectParameterType.Single;
                case ShaderVariableType.String:
                    return Framework.Graphics.EffectParameterType.String;
                case ShaderVariableType.Texture:
                    return Framework.Graphics.EffectParameterType.Texture;
                case ShaderVariableType.Texture1D:
                    return Framework.Graphics.EffectParameterType.Texture1D;
                case ShaderVariableType.Texture2D:
                    return Framework.Graphics.EffectParameterType.Texture2D;
                case ShaderVariableType.Texture3D:
                    return Framework.Graphics.EffectParameterType.Texture3D;
                case ShaderVariableType.TextureCube:
                    return Framework.Graphics.EffectParameterType.TextureCube;
                case ShaderVariableType.Void:
                    return Framework.Graphics.EffectParameterType.Void;
                default:
                    return (Framework.Graphics.EffectParameterType)(-1);
            }
        }
	}
}
