using System;
using System.IO;
using System.Runtime.InteropServices;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using OpenTK.Graphics.OpenGL;
using ANX.Framework;
using ANX.RenderSystem.Windows.GL3.Helpers;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.GL3
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

		public void SetData<T>(int level, Rectangle? rect, T[] data, int startIndex,
			int elementCount) where T : struct
		{
			throw new NotImplementedException();
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

		// TODO: compressed texture, what about elementCount?
		#region GetData (TODO)
		public void GetData<T>(int level, Rectangle? rect, T[] data, int startIndex,
			int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		// TODO: compressed texture (see: http://www.bearisgaming.com/texture2d-getdata-and-dxt-compression/)
		#region GetData
		public void GetData<T>(T[] data) where T : struct
		{
			if (isCompressed)
			{
				throw new NotImplementedException(
					"GetData is currently not implemented for compressed texture " +
					"formats! Format is " + nativeFormat + ".");
			}

			if (data == null)
			{
				throw new ArgumentNullException("data");
			}

			int size = Marshal.SizeOf(typeof(T)) * data.Length;

			if (size != uncompressedDataSize)
			{
				throw new InvalidDataException(
					"The size of the data passed in is too large or too small " +
					"for this resource.");
			}

			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);

			GL.GetTexImage(TextureTarget.Texture2D, 0, (PixelFormat)nativeFormat,
				PixelType.UnsignedByte, handle.AddrOfPinnedObject());

			handle.Free();
		}
		#endregion

		// TODO: compressed texture, what about elementCount?
		#region GetData
		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			if (isCompressed)
			{
				throw new NotImplementedException(
					"GetData is currently not implemented for compressed texture " +
					"formats! Format is " + nativeFormat + ".");
			}

			if (data == null)
			{
				throw new ArgumentNullException("data");
			}

			int size = Marshal.SizeOf(typeof(T)) * data.Length;

			if (size < uncompressedDataSize)
			{
				throw new InvalidDataException(
					"The size of the data passed in is too large or too small " +
					"for this resource.");
			}

			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
			IntPtr ptr = handle.AddrOfPinnedObject();
			ptr += startIndex;

			GL.GetTexImage(TextureTarget.Texture2D, 0, (PixelFormat)nativeFormat,
				PixelType.UnsignedByte, ptr);

			handle.Free();
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

		#region SaveAsJpeg (TODO)
		public void SaveAsJpeg(Stream stream, int width, int height)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SaveAsPng (TODO)
		public void SaveAsPng(Stream stream, int width, int height)
		{
			throw new NotImplementedException();
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
