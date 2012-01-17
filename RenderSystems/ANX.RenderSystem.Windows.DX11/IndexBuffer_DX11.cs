#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using SharpDX.Direct3D11;
using ANX.Framework.Graphics;
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

namespace ANX.RenderSystem.Windows.DX11
{
    public class IndexBuffer_DX11 : INativeBuffer, IDisposable
    {
        private SharpDX.Direct3D11.Buffer buffer;
        private IndexElementSize size;

        public IndexBuffer_DX11(GraphicsDevice graphics, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            this.size = size;

            //TODO: translate and use usage

            GraphicsDeviceWindowsDX11 gd11 = graphics.NativeDevice as GraphicsDeviceWindowsDX11;
            SharpDX.Direct3D11.DeviceContext context = gd11 != null ? gd11.NativeDevice as SharpDX.Direct3D11.DeviceContext : null;

            InitializeBuffer(context.Device, size, indexCount, usage);
        }

        internal IndexBuffer_DX11(SharpDX.Direct3D11.Device device, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            this.size = size;

            InitializeBuffer(device, size, indexCount, usage);
        }

        private void InitializeBuffer(SharpDX.Direct3D11.Device device, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            BufferDescription description = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = (size == IndexElementSize.SixteenBits ? 2 : 4) * indexCount,
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None
            };

            this.buffer = new SharpDX.Direct3D11.Buffer(device, description);
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
        {
            SetData<T>(graphicsDevice, data, 0, data.Length);
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
        {
            GraphicsDeviceWindowsDX11 dx11GraphicsDevice = graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX11;
            DeviceContext context = dx11GraphicsDevice.NativeDevice;

            //TODO: check offsetInBytes parameter for bounds etc.

            GCHandle pinnedArray = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr dataPointer = pinnedArray.AddrOfPinnedObject();

            int dataLength = Marshal.SizeOf(typeof(T)) * data.Length;

            unsafe
            {
                using (var vData = new SharpDX.DataStream(dataPointer, dataLength, true, true))
                {
                    if (offsetInBytes > 0)
                    {
                        vData.Seek(offsetInBytes / (size == IndexElementSize.SixteenBits ? 2 : 4), System.IO.SeekOrigin.Begin);
                    }

                    SharpDX.DataStream stream;
                    SharpDX.DataBox box = context.MapSubresource(this.buffer, MapMode.WriteDiscard, MapFlags.None, out stream);
                    if (startIndex > 0 || elementCount < data.Length)
                    {
                        for (int i = startIndex; i < startIndex + elementCount; i++)
                        {
                            vData.Write<T>(data[i]);
                        }
                    }
                    else
                    {
                        vData.CopyTo(stream);
                    } 
                    context.UnmapSubresource(this.buffer, 0);
                }
            }

            pinnedArray.Free();
        }

        public void SetData<T>(GraphicsDevice graphicsDevice, T[] data, int startIndex, int elementCount) where T : struct
        {
            SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
        }

        public SharpDX.Direct3D11.Buffer NativeBuffer
        {
            get
            {
                return this.buffer;
            }
        }

        public void Dispose()
        {
            if (this.buffer != null)
            {
                buffer.Dispose();
                buffer = null;
            }
        }
    }
}
