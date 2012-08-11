using System;
using System.IO;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using VitaTexture2D = Sce.PlayStation.Core.Graphics.Texture2D;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaTexture2D : INativeTexture2D
	{
		#region Private
		private VitaTexture2D nativeTexture;
		#endregion

		#region Constructor
		public PsVitaTexture2D(SurfaceFormat surfaceFormat, int width, int height,
			int mipCount)
		{
			nativeTexture = new VitaTexture2D(width, height, mipCount > 0,
				DatatypesMapping.SurfaceFormatToVitaPixelFormat(surfaceFormat));
		}
		#endregion

		#region SaveAsJpeg
		public void SaveAsJpeg(Stream stream, int width, int height)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SaveAsPng
		public void SaveAsPng(Stream stream, int width, int height)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetData
		public void GetData<T>(int level, Rectangle? rect, T[] data, int startIndex,
			int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SetData
		public void SetData<T>(int level, Framework.Rectangle? rect, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			T[] subArray = new T[elementCount];
			Array.Copy(data, startIndex, subArray, 0, elementCount);

			if(rect.HasValue)
			{
				nativeTexture.SetPixels(level, subArray, rect.Value.X, rect.Value.Y,
					rect.Value.Width, rect.Value.Height);
			}
			else
			{
				nativeTexture.SetPixels(level, subArray);
			}
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
		{
			nativeTexture.SetPixels(0, data);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			T[] subArray = new T[elementCount];
			Array.Copy(data, startIndex, subArray, 0, elementCount);
			nativeTexture.SetPixels(0, subArray);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
			T[] data, int startIndex, int elementCount) where T : struct
		{
			T[] subArray = new T[elementCount];
			Array.Copy(data, startIndex, subArray, 0, elementCount);
			// TODO: is 0 okay for pitch?
			nativeTexture.SetPixels(0, subArray, offsetInBytes, 0);
		}
		#endregion

		#region Dispose
		public virtual void Dispose()
		{
			if (nativeTexture != null)
			{
				nativeTexture.Dispose();
				nativeTexture = null;
			}
		}
		#endregion
	}
}
