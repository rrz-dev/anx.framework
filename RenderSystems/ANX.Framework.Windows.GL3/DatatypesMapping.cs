using System;
using ANX.Framework.Graphics;
using OpenTK.Graphics;
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

				case SurfaceFormat.HalfVector4:
					return PixelInternalFormat.Rgba16f;

				case SurfaceFormat.Bgra4444:
					return PixelInternalFormat.Rgba4;

				case SurfaceFormat.Bgra5551:
					return PixelInternalFormat.Rgb5A1;

				case SurfaceFormat.Alpha8:
					return PixelInternalFormat.Alpha8;

				case SurfaceFormat.Rg32:
					return PixelInternalFormat.Rg32f;

				case SurfaceFormat.Rgba1010102:
					return PixelInternalFormat.Rgb10A2;

				case SurfaceFormat.Rgba64:
					return PixelInternalFormat.Rgba16f;

				case SurfaceFormat.HalfSingle:
					return PixelInternalFormat.R16f;

				case SurfaceFormat.Single:
					return PixelInternalFormat.R32f;

				case SurfaceFormat.Vector2:
					return PixelInternalFormat.Rg32f;

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
		public static BeginMode PrimitiveTypeToBeginMode(PrimitiveType type)
		{
			switch (type)
			{
				case PrimitiveType.LineList:
					return BeginMode.Lines;

				case PrimitiveType.LineStrip:
					return BeginMode.LineStrip;

				default:
				case PrimitiveType.TriangleList:
					return BeginMode.Triangles;

				case PrimitiveType.TriangleStrip:
					return BeginMode.TriangleStrip;
			}
		}
		#endregion

		#region Tests
		private class Tests
		{
			#region TestConvertVector4ToColor
			public static void TestConvertVector4ToColor()
			{
				Vector4 vector = new Vector4(1f, 0.5f, 0.75f, 0f);
				Color color;
				DatatypesMapping.Convert(ref vector, out color);
				Console.WriteLine(color.ToString());
			}
			#endregion
		}
		#endregion
	}
}
