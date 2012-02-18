using System;
using System.Runtime.InteropServices;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using ANX.Framework.Windows.GL3.Helpers;
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
	/// Native OpenGL Texture implementation.
	/// </summary>
	public class Texture2DGL3 : INativeTexture2D
	{
		#region Private
		/// <summary>
		/// The native OpenGL pixel format of the texture.
		/// </summary>
		private PixelInternalFormat nativeFormat;

		/// <summary>
		/// The number of mipmaps used by this texture [1-n].
		/// </summary>
		private int numberOfMipMaps;

		/// <summary>
		/// Width of the texture.
		/// </summary>
		private int width;

		/// <summary>
		/// Height of the texture.
		/// </summary>
		private int height;

		/// <summary>
		/// Flag if the texture is a compressed format or not.
		/// </summary>
		private bool isCompressed;

		internal bool IsDisposed;

		private int uncompressedDataSize;

		private byte[] texData;

		/// <summary>
		/// TODO: find better solution
		/// </summary>
		private int maxSetDataSize;
		#endregion

		#region Public
		/// <summary>
		/// The OpenGL texture handle.
		/// </summary>
		protected internal int NativeHandle
		{
			get;
			protected set;
		}
		#endregion

		#region Constructor
		internal Texture2DGL3()
		{
			GraphicsResourceManager.UpdateResource(this, true);
		}

		/// <summary>
		/// Create a new native OpenGL texture.
		/// </summary>
		/// <param name="surfaceFormat">Surface format of the texture.</param>
		/// <param name="setWidth">Width of the first mip level.</param>
		/// <param name="setHeight">Height of the first mip level.</param>
		/// <param name="mipCount">Number of mip levels [1-n].</param>
		internal Texture2DGL3(SurfaceFormat surfaceFormat, int setWidth,
			int setHeight, int mipCount)
		{
			GraphicsResourceManager.UpdateResource(this, true);

			width = setWidth;
			height = setHeight;
			numberOfMipMaps = mipCount;
			nativeFormat = DatatypesMapping.SurfaceToPixelInternalFormat(surfaceFormat);
			isCompressed = nativeFormat.ToString().StartsWith("Compressed");

			uncompressedDataSize = GetUncompressedDataSize();

			CreateTexture();
		}

		~Texture2DGL3()
		{
			GraphicsResourceManager.UpdateResource(this, false);
		}
		#endregion

		#region CreateTexture
		private void CreateTexture()
		{
			NativeHandle = GL.GenTexture();
#if DEBUG
			ErrorHelper.Check("GenTexture");
#endif
			GL.BindTexture(TextureTarget.Texture2D, NativeHandle);
#if DEBUG
			ErrorHelper.Check("BindTexture");
#endif

			int wrapMode = (int)All.ClampToEdge;
			int filter = (int)(numberOfMipMaps > 1 ?
				All.LinearMipmapLinear :
				All.Linear);

			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureWrapS, wrapMode);
			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureWrapT, wrapMode);
			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureMagFilter, filter);
			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureMinFilter, filter);
#if DEBUG
			ErrorHelper.Check("TexParameter");
#endif
		}
		#endregion

		// TODO: offsetInBytes
		// TODO: elementCount
		// TODO: get size of first mipmap!
		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data)
			where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, 0, data.Length);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, 0, data.Length);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
			T[] data, int startIndex, int elementCount) where T : struct
		{
			if (numberOfMipMaps > 1)
			{
				throw new NotImplementedException(
					"Loading mipmaps is not correctly implemented yet!");
			}

			GL.BindTexture(TextureTarget.Texture2D, NativeHandle);
#if DEBUG
			ErrorHelper.Check("BindTexture");
#endif

			if (data.Length > maxSetDataSize)
			{
				maxSetDataSize = data.Length;
			}

			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);

			// TODO: get size of first mipmap!
			int mipmapByteSize = data.Length;

			try
			{
				IntPtr dataPointer = handle.AddrOfPinnedObject();
				// Go to the starting point.
				dataPointer += startIndex;

				if (isCompressed)
				{
					GL.CompressedTexImage2D(TextureTarget.Texture2D, 0, nativeFormat,
						width, height, 0, mipmapByteSize, dataPointer);
#if DEBUG
					ErrorHelper.Check("CompressedTexImage2D Format=" + nativeFormat);
#endif
				}
				else
				{
					GL.TexImage2D(TextureTarget.Texture2D, 0, nativeFormat,
						width, height, 0, (PixelFormat)nativeFormat,
						PixelType.UnsignedByte, dataPointer);
#if DEBUG
					ErrorHelper.Check("TexImage2D Format=" + nativeFormat);
#endif
				}

				int mipmapWidth = width;
				int mipmapHeight = height;
				for (int index = 1; index < numberOfMipMaps; index++)
				{
					dataPointer += mipmapByteSize;
					mipmapByteSize /= 4;
					mipmapWidth /= 2;
					mipmapHeight /= 2;
					mipmapWidth = Math.Max(mipmapWidth, 1);
					mipmapHeight = Math.Max(mipmapHeight, 1);

					if (isCompressed)
					{
						GL.CompressedTexImage2D(TextureTarget.Texture2D, index,
							nativeFormat, width, height, 0, mipmapByteSize, dataPointer);
#if DEBUG
						ErrorHelper.Check("CompressedTexImage2D Format=" + nativeFormat);
#endif
					}
					else
					{
						GL.TexImage2D(TextureTarget.Texture2D, index, nativeFormat,
							mipmapWidth, mipmapHeight, 0, (PixelFormat)nativeFormat,
							PixelType.UnsignedByte, dataPointer);
#if DEBUG
						ErrorHelper.Check("TexImage2D Format=" + nativeFormat);
#endif
					}
				}
			}
			finally
			{
				handle.Free();
			}
		}
		#endregion

		#region GetTextureData
		private void GetTextureData()
		{
			GL.BindTexture(TextureTarget.Texture2D, NativeHandle);

			if (isCompressed)
			{
				texData = new byte[maxSetDataSize];
				GCHandle handle = GCHandle.Alloc(texData, GCHandleType.Pinned);

				GL.GetCompressedTexImage(TextureTarget.Texture2D, 0,
					handle.AddrOfPinnedObject());

				handle.Free();
			}
			else
			{
				texData = new byte[uncompressedDataSize];
				GCHandle handle = GCHandle.Alloc(texData, GCHandleType.Pinned);

				GL.GetTexImage(TextureTarget.Texture2D, 0, (PixelFormat)nativeFormat,
					PixelType.UnsignedByte, handle.AddrOfPinnedObject());

				handle.Free();
			}
		}
		#endregion

		#region RecreateData
		internal void RecreateData()
		{
			CreateTexture();
			SetData(null, texData);
			texData = null;
		}
		#endregion

		#region GetUncompressedDataSize
		private int GetUncompressedDataSize()
		{
			int size = width * height;

			switch (nativeFormat)
			{
				default:
				case PixelInternalFormat.R32f:
				case PixelInternalFormat.Rgb10A2:
				case PixelInternalFormat.Rg16:
				case PixelInternalFormat.Rgba:
					size *= 4;
					break;

				case PixelInternalFormat.Rg32f:
				case PixelInternalFormat.Rgba16f:
					size *= 8;
					break;

				case PixelInternalFormat.R16f:
				case PixelInternalFormat.Rgb5A1:
				case PixelInternalFormat.Rgba4:
					size *= 2;
					break;

				case PixelInternalFormat.Alpha8:
					//size *= 1;
					break;

				case PixelInternalFormat.Rgba32f:
					size *= 16;
					break;
			}

			return size;
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose the native OpenGL texture handle.
		/// </summary>
		public virtual void Dispose()
		{
			if (IsDisposed == false)
			{
				IsDisposed = true;
				DisposeResource();
			}
		}

		internal void DisposeResource()
		{
			if (IsDisposed == false)
			{
				GetTextureData();
			}

			if (NativeHandle != -1 &&
				GraphicsDeviceWindowsGL3.IsContextCurrent)
			{
				GL.DeleteTexture(NativeHandle);
				NativeHandle = -1;
#if DEBUG
				ErrorHelper.Check("DeleteTexture");
#endif
			}
		}
		#endregion
	}
}
