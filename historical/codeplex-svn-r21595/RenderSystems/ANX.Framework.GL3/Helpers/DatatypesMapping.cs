using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.GL3.Helpers
{
	internal static class DatatypesMapping
	{
		#region Constants
		public const float ColorMultiplier = 1f / 255f;
		#endregion

		#region Convert ANX.Color -> OpenTK.Color4
		public static void Convert(ref Color anxColor, out Color4 otkColor)
		{
			otkColor.R = anxColor.R * ColorMultiplier;
			otkColor.G = anxColor.G * ColorMultiplier;
			otkColor.B = anxColor.B * ColorMultiplier;
			otkColor.A = anxColor.A * ColorMultiplier;
		}
		#endregion

		#region Convert OpenTK.Color4 -> ANX.Color
		public static void Convert(ref Color4 otkColor, out Color anxColor)
		{
			byte r = (byte)(otkColor.R * 255);
			byte g = (byte)(otkColor.G * 255);
			byte b = (byte)(otkColor.B * 255);
			byte a = (byte)(otkColor.A * 255);
			anxColor.packedValue = (uint)(r + (g << 8) + (b << 16) + (a << 24));
		}
		#endregion

		#region Convert ANX.Vector4 -> ANX.Color
		public static void Convert(ref Vector4 anxVector, out Color anxColor)
		{
			byte r = (byte)(anxVector.X * 255);
			byte g = (byte)(anxVector.Y * 255);
			byte b = (byte)(anxVector.Z * 255);
			byte a = (byte)(anxVector.W * 255);
			anxColor.packedValue = (uint)(r + (g << 8) + (b << 16) + (a << 24));
		}
		#endregion

		#region SurfaceToColorFormat (TODO)
		/// <summary>
		/// Translate the XNA surface format to an OpenGL ColorFormat.
		/// </summary>
		/// <param name="format">XNA surface format.</param>
		/// <returns>Translated color format for OpenGL.</returns>
		public static ColorFormat SurfaceToColorFormat(SurfaceFormat format)
		{
			switch (format)
			{
				// TODO
				case SurfaceFormat.Dxt1:
				case SurfaceFormat.Dxt3:
				case SurfaceFormat.Dxt5:
				case SurfaceFormat.HdrBlendable:
					throw new NotImplementedException("Surface Format '" + format +
						"' isn't implemented yet!");

				// TODO: CHECK!
				case SurfaceFormat.NormalizedByte2:
					return new ColorFormat(8, 8, 0, 0);

				//DONE
				default:
				case SurfaceFormat.Color:
				case SurfaceFormat.NormalizedByte4:
					return new ColorFormat(8, 8, 8, 8);

				case SurfaceFormat.HalfVector2:
					return new ColorFormat(16, 16, 0, 0);

				case SurfaceFormat.HalfVector4:
					return new ColorFormat(16, 16, 16, 16);

				case SurfaceFormat.Bgra4444:
					return new ColorFormat(4, 4, 4, 4);

				case SurfaceFormat.Bgra5551:
					return new ColorFormat(5, 5, 5, 1);

				case SurfaceFormat.Alpha8:
					return new ColorFormat(0, 0, 0, 8);

				case SurfaceFormat.Bgr565:
					return new ColorFormat(5, 6, 5, 0);

				case SurfaceFormat.Rg32:
					return new ColorFormat(16, 16, 0, 0);

				case SurfaceFormat.Rgba1010102:
					return new ColorFormat(10, 10, 10, 2);

				case SurfaceFormat.Rgba64:
					return new ColorFormat(16, 16, 16, 16);

				case SurfaceFormat.HalfSingle:
					return new ColorFormat(16, 0, 0, 0);

				case SurfaceFormat.Single:
					return new ColorFormat(32, 0, 0, 0);

				case SurfaceFormat.Vector2:
					return new ColorFormat(32, 32, 0, 0);

				case SurfaceFormat.Vector4:
					return new ColorFormat(32, 32, 32, 32);
			}
		}
		#endregion

		#region SurfaceToPixelInternalFormat (TODO)
		/// <summary>
		/// Translate the XNA surface format to an OpenGL PixelInternalFormat.
		/// </summary>
		/// <param name="format">XNA surface format.</param>
		/// <returns>Translated format for OpenGL.</returns>
		public static PixelInternalFormat SurfaceToPixelInternalFormat(
			SurfaceFormat format)
		{
			switch (format)
			{
				// TODO
				case SurfaceFormat.HdrBlendable:
				case SurfaceFormat.Bgr565:
					throw new NotImplementedException("Surface Format '" + format +
						"' isn't implemented yet!");

				// TODO: CHECK!
				case SurfaceFormat.NormalizedByte2:
					return PixelInternalFormat.Rg8;

				default:
				case SurfaceFormat.Color:
				case SurfaceFormat.NormalizedByte4:
					return PixelInternalFormat.Rgba;

				case SurfaceFormat.Dxt1:
					return PixelInternalFormat.CompressedRgbaS3tcDxt1Ext;

				case SurfaceFormat.Dxt3:
					return PixelInternalFormat.CompressedRgbaS3tcDxt3Ext;

				case SurfaceFormat.Dxt5:
					return PixelInternalFormat.CompressedRgbaS3tcDxt5Ext;

				case SurfaceFormat.HalfVector2:
					return PixelInternalFormat.Rg16;

				case SurfaceFormat.Rgba64:
				case SurfaceFormat.HalfVector4:
					return PixelInternalFormat.Rgba16f;

				case SurfaceFormat.Bgra4444:
					return PixelInternalFormat.Rgba4;

				case SurfaceFormat.Bgra5551:
					return PixelInternalFormat.Rgb5A1;

				case SurfaceFormat.Alpha8:
					return PixelInternalFormat.Alpha8;

				case SurfaceFormat.Vector2:
				case SurfaceFormat.Rg32:
					return PixelInternalFormat.Rg32f;

				case SurfaceFormat.Rgba1010102:
					return PixelInternalFormat.Rgb10A2;

				case SurfaceFormat.HalfSingle:
					return PixelInternalFormat.R16f;

				case SurfaceFormat.Single:
					return PixelInternalFormat.R32f;

				case SurfaceFormat.Vector4:
					return PixelInternalFormat.Rgba32f;
			}
		}
		#endregion

		#region PrimitiveTypeToBeginMode
		/// <summary>
		/// Translate the XNA PrimitiveType to an OpenGL BeginMode.
		/// </summary>
		/// <param name="type">XNA PrimitiveType.</param>
		/// <returns>Translated BeginMode for OpenGL.</returns>
		public static BeginMode PrimitiveTypeToBeginMode(PrimitiveType type, int primitiveCount, out int count)
		{
			switch (type)
			{
				case PrimitiveType.LineList:
					count = primitiveCount * 2;
					return BeginMode.Lines;

				case PrimitiveType.LineStrip:
					count = primitiveCount + 1;
					return BeginMode.LineStrip;

				default:
				case PrimitiveType.TriangleList:
					count = primitiveCount * 3;
					return BeginMode.Triangles;

				case PrimitiveType.TriangleStrip:
					count = primitiveCount + 2;
					return BeginMode.TriangleStrip;
			}
		}
		#endregion

		#region Tests
		private class Tests
		{
			#region TestConvertColorToOtkColor
			public static void TestConvertColorToOtkColor()
			{
				Color anxColor = new Color(1f, 0.5f, 0.75f, 0f);
				Color4 color;
				DatatypesMapping.Convert(ref anxColor, out color);
				Console.WriteLine(color.ToString());
			}
			#endregion

			#region TestConvertOtkColorToColor
			public static void TestConvertOtkColorToColor()
			{
				Color4 color = new Color4(1f, 0.5f, 0.75f, 0f);
				Color anxColor;
				DatatypesMapping.Convert(ref color, out anxColor);
				Console.WriteLine(anxColor.ToString());
			}
			#endregion

			#region TestConvertVector4ToColor
			public static void TestConvertVector4ToColor()
			{
				Vector4 vector = new Vector4(1f, 0.5f, 0.75f, 0f);
				Color color;
				DatatypesMapping.Convert(ref vector, out color);
				Console.WriteLine(color.ToString());
			}
			#endregion

			#region TestPrimitiveTypeToBeginMode
			public static void TestPrimitiveTypeToBeginMode()
			{
				PrimitiveType type = PrimitiveType.LineList;
				int primitiveCount = 10;
				int count = 0;

				BeginMode result = DatatypesMapping.PrimitiveTypeToBeginMode(type,
					primitiveCount, out count);
				AssetValues(result, BeginMode.Lines);
				AssetValues(count, primitiveCount * 2);

				type = PrimitiveType.LineStrip;
				result = DatatypesMapping.PrimitiveTypeToBeginMode(type, primitiveCount,
					out count);
				AssetValues(result, BeginMode.LineStrip);
				AssetValues(count, primitiveCount + 1);

				type = PrimitiveType.TriangleList;
				result = DatatypesMapping.PrimitiveTypeToBeginMode(type, primitiveCount,
					out count);
				AssetValues(result, BeginMode.Triangles);
				AssetValues(count, primitiveCount * 3);

				type = PrimitiveType.TriangleStrip;
				result = DatatypesMapping.PrimitiveTypeToBeginMode(type, primitiveCount,
					out count);
				AssetValues(result, BeginMode.TriangleStrip);
				AssetValues(count, primitiveCount + 2);
			}
			#endregion

			#region AssetValues
			private static void AssetValues<T>(T first, T second)
			{
				if (first.Equals(second) == false)
				{
					throw new Exception("The two values are not equal:\n\t1: " + first +
						"\n\t2: " + second);
				}
			}
			#endregion
		}
		#endregion
	}
}
