#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using SharpDX.Direct3D10;
using ANX.Framework.NonXNA.RenderSystem;
using System.IO;
using System.Runtime.InteropServices;
using ANX.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class Texture2D_DX10 : INativeTexture2D
    {
        #region Private Members
        protected internal SharpDX.Direct3D10.Texture2D nativeTexture;
        protected internal SharpDX.Direct3D10.ShaderResourceView nativeShaderResourceView;
        protected internal int formatSize;
        protected internal SurfaceFormat surfaceFormat;
        protected internal GraphicsDevice graphicsDevice;

        #endregion // Private Members

        internal Texture2D_DX10(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public Texture2D_DX10(GraphicsDevice graphicsDevice, int width, int height, SurfaceFormat surfaceFormat, int mipCount)
        {
            if (mipCount > 1)
            {
                throw new Exception("creating textures with mip map not yet implemented");
            }

            this.graphicsDevice = graphicsDevice;
            this.surfaceFormat = surfaceFormat;

            GraphicsDeviceWindowsDX10 graphicsDX10 = graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10;
            SharpDX.Direct3D10.Device device = graphicsDX10.NativeDevice;

            SharpDX.Direct3D10.Texture2DDescription description = new SharpDX.Direct3D10.Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = mipCount,
                ArraySize = mipCount,
                Format = FormatConverter.Translate(surfaceFormat),
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = SharpDX.Direct3D10.ResourceUsage.Dynamic,
                BindFlags = SharpDX.Direct3D10.BindFlags.ShaderResource,
                CpuAccessFlags = SharpDX.Direct3D10.CpuAccessFlags.Write,
                OptionFlags = SharpDX.Direct3D10.ResourceOptionFlags.None,
            };
            this.nativeTexture = new SharpDX.Direct3D10.Texture2D(graphicsDX10.NativeDevice, description);
            this.nativeShaderResourceView = new SharpDX.Direct3D10.ShaderResourceView(graphicsDX10.NativeDevice, this.nativeTexture);

            // description of texture formats of DX10: http://msdn.microsoft.com/en-us/library/bb694531(v=VS.85).aspx
            // more helpfull information on DX10 textures: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205131(v=vs.85).aspx

            this.formatSize = FormatConverter.FormatSize(surfaceFormat);
        }

        public override int GetHashCode()
        {
            return NativeTexture.NativePointer.ToInt32();
        }

        internal SharpDX.Direct3D10.Texture2D NativeTexture
        {
            get
            {
                return this.nativeTexture;
            }
            set
            {
                if (this.nativeTexture != value)
                {
                    if (this.nativeTexture != null)
                    {
                        this.nativeTexture.Dispose();
                    }

                    this.nativeTexture = value;
                }
            }
        }

        internal SharpDX.Direct3D10.ShaderResourceView NativeShaderResourceView
        {
            get
            {
                return this.nativeShaderResourceView;
            }
            set
            {
                if (this.nativeShaderResourceView != value)
                {
                    if (this.nativeShaderResourceView != null)
                    {
                        this.nativeShaderResourceView.Dispose();
                    }

                    this.nativeShaderResourceView = value;
                }
            }
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
        {
            SetData<T>(graphicsDevice, 0, data, 0, data.Length);
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, T[] data, int startIndex, int elementCount) where T : struct
        {
            SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
        {
            //TODO: handle offsetInBytes parameter
            //TODO: handle startIndex parameter
            //TODO: handle elementCount parameter

            if (this.surfaceFormat == SurfaceFormat.Color)
            {
                int subresource = SharpDX.Direct3D10.Texture2D.CalculateSubresourceIndex(0, 0, 1);
                SharpDX.DataRectangle rectangle = this.nativeTexture.Map(subresource, SharpDX.Direct3D10.MapMode.WriteDiscard, SharpDX.Direct3D10.MapFlags.None);
                int rowPitch = rectangle.Pitch;

                unsafe
                {
                    GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                    byte* colorData = (byte*)handle.AddrOfPinnedObject();

                    byte* pTexels = (byte*)rectangle.DataPointer;
                    int srcIndex = 0;

                    for (int row = 0; row < Height; row++)
                    {
                        int rowStart = row * rowPitch;

                        for (int col = 0; col < Width; col++)
                        {
                            int colStart = col * formatSize;
                            pTexels[rowStart + colStart + 0] = colorData[srcIndex++];
                            pTexels[rowStart + colStart + 1] = colorData[srcIndex++];
                            pTexels[rowStart + colStart + 2] = colorData[srcIndex++];
                            pTexels[rowStart + colStart + 3] = colorData[srcIndex++];
                        }
                    }

                    handle.Free();
                }

                this.nativeTexture.Unmap(subresource);
            }
            else if (surfaceFormat == SurfaceFormat.Dxt5 || surfaceFormat == SurfaceFormat.Dxt3 || surfaceFormat == SurfaceFormat.Dxt1)
            {
                unsafe
                {
                    GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                    byte* colorData = (byte*)handle.AddrOfPinnedObject();

                    int w = (Width + 3) >> 2;
                    int h = (Height + 3) >> 2;
                    formatSize = (surfaceFormat == SurfaceFormat.Dxt1) ? 8 : 16;

                    int subresource = SharpDX.Direct3D10.Texture2D.CalculateSubresourceIndex(0, 0, 1);
                    SharpDX.DataRectangle rectangle = this.nativeTexture.Map(subresource, SharpDX.Direct3D10.MapMode.WriteDiscard, SharpDX.Direct3D10.MapFlags.None);
                    SharpDX.DataStream ds = new SharpDX.DataStream(rectangle.DataPointer, Width * Height * 4 * 2, true, true);
                    int pitch = rectangle.Pitch;
                    int col = 0;
                    int index = 0; // startIndex
                    int count = data.Length; // elementCount
                    int actWidth = w * formatSize;

                    for (int i = 0; i < h; i++)
                    {
                        ds.Position = (i * pitch) + (col * formatSize);
                        if (count <= 0)
                        {
                            break;
                        }
                        else if (count < actWidth)
                        {
                            for (int idx = index; idx < index + count; idx++)
                            {
                                ds.WriteByte(colorData[idx]);
                            }
                            //ds.WriteRange<byte>(colorDataArray, index, count);

                            break;
                        }

                        for (int idx = index; idx < index + actWidth; idx++)
                        {
                            ds.WriteByte(colorData[idx]);
                        }
                        //ds.WriteRange<byte>(colorDataArray, index, actWidth);

                        index += actWidth;
                        count -= actWidth;
                    }

                    handle.Free();

                    this.nativeTexture.Unmap(subresource);
                }
            }
            else
            {
                throw new Exception(string.Format("creating textures of format {0} not yet implemented...", surfaceFormat.ToString()));
            }
        }

        public int Width
        {
            get
            {
                if (this.nativeTexture != null)
                {
                    return this.nativeTexture.Description.Width;
                }

                return 0;
            }
        }

        public int Height
        {
            get
            {
                if (this.nativeTexture != null)
                {
                    return this.nativeTexture.Description.Height;
                }

                return 0;
            }
        }

        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return this.graphicsDevice;
            }
        }

        public void Dispose()
        {
            if (this.nativeShaderResourceView != null)
            {
                this.nativeShaderResourceView.Dispose();
                this.nativeShaderResourceView = null;
            }

            if (this.nativeTexture != null)
            {
                this.nativeTexture.Dispose();
                this.nativeTexture = null;
            }
				}

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

				#region INativeTexture2D Member


				public void GetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
				{
					throw new NotImplementedException();
				}

				public void SetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
				{
					throw new NotImplementedException();
				}

				#endregion

				#region INativeBuffer Member


				public void GetData<T>(T[] data) where T : struct
				{
					throw new NotImplementedException();
				}

				public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
				{
					throw new NotImplementedException();
				}

				#endregion
		}
}
