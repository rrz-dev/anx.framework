using System;
using System.IO;
using System.Runtime.InteropServices;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using ANX.RenderSystem.GL3.Helpers;
using OpenTK.Graphics.OpenGL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.GL3
{
	public class Texture2DGL3 : INativeTexture2D
	{
		#region Private
		private PixelInternalFormat nativeFormat;

		/// <summary>
		/// [1-n]
		/// </summary>
		private int numberOfMipMaps;

		private int width;
		private int height;
		private bool isCompressed;
		internal bool IsDisposed;
		private int uncompressedDataSize;
		private byte[] texData;
		private int maxSetDataSize;
		#endregion

		#region Public
		protected internal int NativeHandle { get; protected set; }
		#endregion

		#region Constructor
		internal Texture2DGL3()
		{
			GraphicsResourceManager.UpdateResource(this, true);
		}

		internal Texture2DGL3(SurfaceFormat surfaceFormat, int setWidth, int setHeight, int mipCount)
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
			GL.BindTexture(TextureTarget.Texture2D, NativeHandle);

			int wrapMode = (int)All.ClampToEdge;
			All minFilter = numberOfMipMaps > 1 ? All.LinearMipmapLinear : All.Linear;

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, wrapMode);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, wrapMode);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);
#if DEBUG
			ErrorHelper.Check("TexParameter");
#endif
		}
		#endregion

		// TODO: offsetInBytes
		// TODO: elementCount
		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, 0, data.Length);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data, int startIndex, int elementCount) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, 0, data.Length);
		}

		public void SetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			int size = Marshal.SizeOf(typeof(T)) * data.Length;
			if (size > maxSetDataSize)
				maxSetDataSize = size;

			GL.BindTexture(TextureTarget.Texture2D, NativeHandle);
			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
			
			try
			{
				IntPtr dataPointer = handle.AddrOfPinnedObject();
				// Go to the starting point.
				dataPointer += startIndex;

				int mipmapWidth = Math.Max(width >> level, 1);
				int mipmapHeight = Math.Max(height >> level, 1);

				if (isCompressed)
				{
					GL.CompressedTexImage2D(TextureTarget.Texture2D, level, nativeFormat, width, height, 0, data.Length,
						dataPointer);
#if DEBUG
					ErrorHelper.Check("CompressedTexImage2D Format=" + nativeFormat);
#endif
				}
				else
				{
					GL.TexImage2D(TextureTarget.Texture2D, level, nativeFormat, mipmapWidth, mipmapHeight, 0,
						(PixelFormat)nativeFormat, PixelType.UnsignedByte, dataPointer);
#if DEBUG
					ErrorHelper.Check("TexImage2D Format=" + nativeFormat);
#endif
				}
			}
			finally
			{
				handle.Free();
			}
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data, int startIndex, int elementCount)
			where T : struct
		{
			int size = Marshal.SizeOf(typeof(T)) * data.Length;
			if (size > maxSetDataSize)
				maxSetDataSize = size;

			GL.BindTexture(TextureTarget.Texture2D, NativeHandle);
			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);

			try
			{
				IntPtr dataPointer = handle.AddrOfPinnedObject();
				dataPointer += startIndex;

				if (isCompressed)
				{
					GL.CompressedTexImage2D(TextureTarget.Texture2D, 0, nativeFormat, width, height, 0, data.Length, dataPointer);
#if DEBUG
					ErrorHelper.Check("CompressedTexImage2D Format=" + nativeFormat);
#endif
				}
				else
				{
					GL.TexImage2D(TextureTarget.Texture2D, 0, nativeFormat, width, height, 0, (PixelFormat)nativeFormat,
						PixelType.UnsignedByte, dataPointer);
#if DEBUG
					ErrorHelper.Check("TexImage2D Format=" + nativeFormat);
#endif
				}
			}
			finally
			{
				handle.Free();
			}
		}
		#endregion

		#region GetData (TODO)
		public void GetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetData
		public void GetData<T>(T[] data) where T : struct
		{
			GetData(data, 0, data.Length);
		}
		#endregion

		// TODO: compressed texture (see: http://www.bearisgaming.com/texture2d-getdata-and-dxt-compression/)
		// TODO: elementCount
		#region GetData
		public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			if (isCompressed)
				throw new NotImplementedException("GetData is currently not implemented for compressed texture format " +
					nativeFormat + ".");

			if (data == null)
				throw new ArgumentNullException("data");

			int size = Marshal.SizeOf(typeof(T)) * data.Length;

			if (size < uncompressedDataSize)
				throw new InvalidDataException("The size of the data passed in is too large or too small for this resource.");

			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
			IntPtr ptr = handle.AddrOfPinnedObject();
			ptr += startIndex;

			GL.GetTexImage(TextureTarget.Texture2D, 0, (PixelFormat)nativeFormat, PixelType.UnsignedByte, ptr);

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

				GL.GetCompressedTexImage(TextureTarget.Texture2D, 0, handle.AddrOfPinnedObject());

				handle.Free();
			}
			else
			{
				texData = new byte[uncompressedDataSize];
				GCHandle handle = GCHandle.Alloc(texData, GCHandleType.Pinned);

				GL.GetTexImage(TextureTarget.Texture2D, 0, (PixelFormat)nativeFormat, PixelType.UnsignedByte,
					handle.AddrOfPinnedObject());

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
