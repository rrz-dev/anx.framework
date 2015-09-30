#region Using Statements
using System;
using System.Runtime.InteropServices;
using ANX.Framework.Graphics;
using SharpDX;

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
	public partial class DxTexture2D : IDisposable 
	{
#if DEBUG
        static int textureCount = 0;
#endif

		protected SurfaceFormat surfaceFormat;

        public int Width
        {
            get;
            protected set;
        }

        public int Height
        {
            get;
            protected set;
        }

		#region Public
		public GraphicsDeviceDX GraphicsDevice
		{
			get;
			protected set;
		}
		#endregion

		public void SetData<T>(T[] data) where T : struct
		{
			SetData<T>(0, null, data, 0, data.Length);
		}

		public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			SetData<T>(0, null, data, startIndex, elementCount);
		}

        public void SetData<T>(int level, Framework.Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
        {
            int formatSize = DxFormatConverter.FormatSize(this.surfaceFormat);
            int elementSize = Marshal.SizeOf(typeof(T));

            Framework.Rectangle realRect;
            if (rect.HasValue)
                realRect = rect.Value;
            else
            {
                realRect.X = 0;
                realRect.Y = 0;
                realRect.Width = this.Width;
                realRect.Height = this.Height;
            }

            UpdateSubresource(data, _nativeTexture, level, realRect);
        }

        public void GetData<T>(T[] data) where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data");

            this.GetData<T>(0, null, data, 0, data.Length);
        }

        public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            this.GetData<T>(0, null, data, startIndex, elementCount);
        }

        private Framework.Rectangle ValidateRect<T>(int level, Framework.Rectangle? rect, T[] data, int startIndex, int elementCount)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (startIndex + elementCount > data.Length)
                throw new ArgumentOutOfRangeException("startIndex + elementCount is bigger than data.Length");

            int mipmapWidth = Math.Max(Width >> level, 1);
            int mipmapHeight = Math.Max(Height >> level, 1);

            Framework.Rectangle finalRect;
            if (rect != null)
            {
                finalRect = rect.Value;

                if (finalRect.X < 0 || finalRect.Width <= 0 || finalRect.Y < 0 || finalRect.Height <= 0)
                    throw new ArgumentException("The given rectangle is invalid.");

                if (finalRect.Right > mipmapWidth || finalRect.Bottom > mipmapHeight)
                    throw new ArgumentException("The given rectangle is bigger than the texture on the given mip level.");
            }
            else
                finalRect = new Framework.Rectangle(0, 0, mipmapWidth, mipmapHeight);

            int formatSize = DxFormatConverter.FormatSize(this.surfaceFormat);

            int elementSize = Marshal.SizeOf(typeof(T));
            if (elementSize * elementCount != formatSize * mipmapWidth * mipmapHeight)
                throw new ArgumentException("\"data\" has the wrong size.");

            return finalRect;
        }

        public void GetData<T>(int level, Framework.Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
        {
            var finalRect = ValidateRect(level, rect, data, startIndex, elementCount);
            
            using (var texture = CreateStagingTexture(ResourceMapping.Read))
            {
                GraphicsDevice.NativeDevice.CopyResource(NativeTexture, texture);

                int subresource = GetSubresource(level);
                int pitch;
                using (var dataStream = Map(texture, subresource, level, ResourceMapping.Read, out pitch))
                {
                    try
                    {
                        int elementIndex = startIndex;
                        for (int y = finalRect.Top; y < finalRect.Bottom; y++)
                        {
                            int width = Math.Min(finalRect.Width, elementCount);
                            if (width <= 0)
                                break;

                            dataStream.Position = y * pitch;
                            dataStream.ReadRange(data, elementIndex, width);

                            elementIndex += finalRect.Width;
                            elementCount -= width;
                        }
                    }
                    finally
                    {
                        Unmap(texture, subresource);
                    }
                }
            }
        }

        private bool IsDxt
        {
            get
            {
                return this.surfaceFormat == SurfaceFormat.Dxt1 || this.surfaceFormat == SurfaceFormat.Dxt3 || this.surfaceFormat == SurfaceFormat.Dxt5;
            }
        }

        protected int SizeInBytes(int level)
        {
            int mipmapWidth = Math.Max(Width >> level, 1);
            int mipmapHeight = Math.Max(Height >> level, 1);

            if (this.IsDxt)
            {
                //Dxt uses 4x4 blocks and each of this block is 8 bytes in size for dxt1, so on average half a byte per pixel.
                //For Dxt3 and Dxt5, it's 1 byte per pixel on average.
			    int w = (mipmapWidth + 3) / 4;
			    int h = (mipmapHeight + 3) / 4;
			    var formatSize = (surfaceFormat == SurfaceFormat.Dxt1) ? 8 : 16;

                return w * h * formatSize;
            }
            else
            {
                return mipmapWidth * mipmapHeight * DxFormatConverter.FormatSize(this.surfaceFormat);
            }
        }

        protected int GetPitch(int level)
        {
            int mipmapWidth = Math.Max(Width >> level, 1);

            if (this.IsDxt)
            {
                //Dxt uses 4x4 blocks and each of this block is 8 bytes in size for dxt1, so on average half a byte per pixel.
                //For Dxt3 and Dxt5, it's 1 byte per pixel on average.
                int w = (mipmapWidth + 3) / 4;
                var formatSize = (surfaceFormat == SurfaceFormat.Dxt1) ? 8 : 16;

                return w * formatSize;
            }
            else
            {
                return mipmapWidth * DxFormatConverter.FormatSize(this.surfaceFormat);
            }
        }

		#region Dispose
		public void Dispose()
		{
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool managed)
        {
            if (NativeShaderResourceView != null)
            {
                NativeShaderResourceView.Dispose();
                NativeShaderResourceView = null;
            }
        }
        #endregion
	}
}
