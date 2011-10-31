using System;
using ANX.Framework.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

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
	/// BIG TODO
	/// </summary>
	public class Texture2DGL3 : Texture2D
	{
		#region Public
		/// <summary>
		/// The OpenGL texture handle.
		/// </summary>
		internal int NativeHandle
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		internal Texture2DGL3(GraphicsDevice graphics,
			SurfaceFormat surfaceFormat, int width, int height, int mipCount,
			byte[] mipMaps)
			: base(graphics, width, height, mipCount > 0, surfaceFormat)
		{
			int dataLengthPerTexture = 0;

			NativeHandle = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, NativeHandle);

			int wrapMode = (int)All.ClampToEdge;
			int filter = (int)(mipCount > 0 ? All.LinearMipmapLinear : All.Linear);

			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureWrapS, wrapMode);
			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureWrapT, wrapMode);
			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureMagFilter, filter);
			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureMinFilter, filter);

			PixelInternalFormat formatUsed = PixelInternalFormat.Rgba;

			GCHandle handle = GCHandle.Alloc(mipMaps, GCHandleType.Pinned);
			try
			{
				IntPtr dataPointer = handle.AddrOfPinnedObject();

				if (surfaceFormat == SurfaceFormat.Dxt1 ||
						surfaceFormat == SurfaceFormat.Dxt3 ||
						surfaceFormat == SurfaceFormat.Dxt5)
				{
					// TODO: read dataLengthPerTexture from dds file.
					#region Dds
					formatUsed =
						surfaceFormat == SurfaceFormat.Dxt1 ?
						PixelInternalFormat.CompressedRgbS3tcDxt1Ext :
						surfaceFormat == SurfaceFormat.Dxt3 ?
						PixelInternalFormat.CompressedRgbaS3tcDxt3Ext :
						PixelInternalFormat.CompressedRgbaS3tcDxt5Ext;

					GL.CompressedTexImage2D(TextureTarget.Texture2D, 0, formatUsed,
						width, height, 0, dataLengthPerTexture, dataPointer);

					int mipmapByteSize = dataLengthPerTexture;
					int mipmapWidth = width;
					int mipmapHeight = height;
					for (int i = 1; i < mipCount; i++)
					{
						// Move our data pointer along.
						dataPointer += mipmapByteSize;
						mipmapByteSize /= 4;
						mipmapWidth /= 2;
						mipmapHeight /= 2;
						if (mipmapByteSize < 32)
						{
							mipmapByteSize = 32;
						}
						if (mipmapWidth < 1)
						{
							mipmapWidth = 1;
						}
						if (mipmapHeight < 1)
						{
							mipmapHeight = 1;
						}
						GL.CompressedTexImage2D(TextureTarget.Texture2D, i, formatUsed,
							mipmapWidth, mipmapHeight, 0, mipmapByteSize, dataPointer);
					}
					#endregion
				}
				else
				{
					#region Other
					PixelType pixelType = PixelType.UnsignedByte;

					GL.TexImage2D(TextureTarget.Texture2D, 0,
						// Use the same for internal format (Rgba or Rgb)
						formatUsed, width, height, 0,
						// And the same for the pixel format of the incoming data
						(PixelFormat)formatUsed, pixelType, dataPointer);

					int mipmapByteSize = dataLengthPerTexture;
					int mipmapWidth = width;
					int mipmapHeight = height;
					if (mipmapByteSize > 0 &&
							mipCount > 0)
					{
						for (int i = 1; i < mipCount; i++)
						{
							// Move our data pointer along.
							dataPointer += mipmapByteSize;
							mipmapByteSize /= 4;
							mipmapWidth /= 2;
							mipmapHeight /= 2;
							if (mipmapWidth < 1)
							{
								mipmapWidth = 1;
							}
							if (mipmapHeight < 1)
							{
								mipmapHeight = 1;
							}

							GL.TexImage2D(TextureTarget.Texture2D, i,
								// Use the same for internal format (Rgba or Rgb)
								formatUsed, mipmapWidth, mipmapHeight, 0,
								// And the same for the pixel format of the incoming data
								(PixelFormat)formatUsed, pixelType, dataPointer);
						}
					}
					#endregion
				}
			}
			finally
			{
				handle.Free();
			}
		}
		#endregion
	}
}
