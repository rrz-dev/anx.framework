#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using SharpDX.Direct3D11;
using ANX.Framework.NonXNA.RenderSystem;
using System.IO;
using System.Runtime.InteropServices;

#endregion // Using Statements

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

namespace ANX.Framework.Windows.Metro
{
    public class Texture2D_Metro : INativeTexture2D
    {
        #region Private Members
        protected internal SharpDX.Direct3D11.Texture2D nativeTexture;
        protected internal SharpDX.Direct3D11.ShaderResourceView nativeShaderResourceView;
        protected internal int formatSize;
        protected internal SurfaceFormat surfaceFormat;
        protected internal GraphicsDevice graphicsDevice;

        #endregion // Private Members

        internal Texture2D_Metro(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public Texture2D_Metro(GraphicsDevice graphicsDevice, int width, int height, SurfaceFormat surfaceFormat, int mipCount)
        {
            if (mipCount > 1)
            {
                throw new Exception("creating textures with mip map not yet implemented");
            }

            this.graphicsDevice = graphicsDevice;
            this.surfaceFormat = surfaceFormat;

            GraphicsDeviceWindowsMetro graphicsMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
            SharpDX.Direct3D11.DeviceContext context = graphicsMetro.NativeDevice;

            SharpDX.Direct3D11.Texture2DDescription description = new SharpDX.Direct3D11.Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = mipCount,
                ArraySize = mipCount,
                Format = FormatConverter.Translate(surfaceFormat),
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = SharpDX.Direct3D11.ResourceUsage.Dynamic,
                BindFlags = SharpDX.Direct3D11.BindFlags.ShaderResource,
                CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.Write,
                OptionFlags = SharpDX.Direct3D11.ResourceOptionFlags.None,
            };
            this.nativeTexture = new SharpDX.Direct3D11.Texture2D(context.Device, description);
            this.nativeShaderResourceView = new SharpDX.Direct3D11.ShaderResourceView(context.Device, this.nativeTexture);

            // description of texture formats of DX10: http://msdn.microsoft.com/en-us/library/bb694531(v=VS.85).aspx
            // more helpfull information on DX10 textures: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205131(v=vs.85).aspx

            this.formatSize = FormatConverter.FormatSize(surfaceFormat);
        }

        public override int GetHashCode()
        {
            return NativeTexture.NativePointer.ToInt32();
        }

        internal SharpDX.Direct3D11.Texture2D NativeTexture
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

        internal SharpDX.Direct3D11.ShaderResourceView NativeShaderResourceView
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

            GraphicsDeviceWindowsMetro metroGraphicsDevice = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
            DeviceContext context = metroGraphicsDevice.NativeDevice;

            if (this.surfaceFormat == SurfaceFormat.Color)
            {
                int subresource = SharpDX.Direct3D11.Texture2D.CalculateSubResourceIndex(0, 0, 1);
                SharpDX.DataBox rectangle = context.MapSubresource(this.nativeTexture, subresource, MapMode.WriteDiscard, MapFlags.None);
                int rowPitch = rectangle.RowPitch;

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

                context.UnmapSubresource(this.nativeTexture, subresource);
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

                    int subresource = SharpDX.Direct3D11.Texture2D.CalculateSubResourceIndex(0, 0, 1);
                    SharpDX.DataBox rectangle = context.MapSubresource(this.nativeTexture, subresource, MapMode.WriteDiscard, MapFlags.None);
                    SharpDX.DataStream ds = new SharpDX.DataStream(rectangle.DataPointer, Width * Height * 4 * 2, true, true);
                    int pitch = rectangle.RowPitch;
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
                            break;
                        }

                        for (int idx = index; idx < index + actWidth; idx++)
                        {
                            ds.WriteByte(colorData[idx]);
                        }

                        index += actWidth;
                        count -= actWidth;
                    }

                    handle.Free();

                    context.UnmapSubresource(this.nativeTexture, subresource);
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
