#region Using Statements
using System;
using System.IO;
using ANX.Framework.Graphics;
using SharpDX;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using System.Runtime.InteropServices;

#if DX10
using Dx = SharpDX.Direct3D10;
using DxDevice = SharpDX.Direct3D10.Device;

namespace ANX.RenderSystem.Windows.DX10
#endif
#if DX11
using Dx = SharpDX.Direct3D11;
using DxDevice = SharpDX.Direct3D11.Device;

namespace ANX.RenderSystem.Windows.DX11
#endif
{
    public partial class DxVertexBuffer
    {
        private int vertexStride;

        #region SetData
        public void SetData<S>(S[] data) where S : struct
        {
            if (data == null)
                throw new ArgumentNullException("data");

            SetData(data, 0, data.Length);
        }

        public void SetData<S>(S[] data, int startIndex, int elementCount) where S : struct
        {
            SetData<S>(0, data, startIndex, elementCount, vertexStride);
        }

        public void SetData<S>(int offsetInBytes, S[] data, int startIndex, int elementCount, int vertexStride)
            where S : struct
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (startIndex + elementCount > data.Length)
                throw new ArgumentOutOfRangeException("startIndex must be smaller than elementCount + data.Length.");

            if (offsetInBytes + elementCount * Marshal.SizeOf(typeof(S)) > this.SizeInBytes)
                throw new ArgumentOutOfRangeException(string.Format("The offset by \"{0}\" plus the byte length described by \"{1}\" is over the bounds of the buffer.", "offsetInBytes", "elementCount"));

            var buffer = this.NativeBuffer;
            if (this.WriteNeedsStaging)
            {
                buffer = CreateStagingBuffer(ResourceMapping.Write);
            }

            try
            {
                using (var stream = MapBuffer(buffer, ResourceMapping.Write))
                {
                    if (offsetInBytes > 0)
                        stream.Seek(offsetInBytes, SeekOrigin.Current);

                    if (startIndex > 0 || elementCount < data.Length)
                        for (int i = startIndex; i < startIndex + elementCount; i++)
                            stream.Write<S>(data[i]);
                    else
                        for (int i = 0; i < data.Length; i++)
                            stream.Write<S>(data[i]);

                    UnmapBuffer(buffer);
                }
            }
            finally
            {
                if (this.WriteNeedsStaging)
                {
                    CopySubresource(buffer, this.NativeBuffer);
                    buffer.Dispose();
                }
            }
        }
        #endregion

        #region GetData
        public void GetData<S>(S[] data) where S : struct
        {
            if (data == null)
                throw new ArgumentNullException("data");

            GetData(data, 0, data.Length);
        }

        public void GetData<S>(S[] data, int startIndex, int elementCount) where S : struct
        {
            GetData(0, data, startIndex, elementCount, vertexStride);
        }

        public void GetData<S>(int offsetInBytes, S[] data, int startIndex, int elementCount, int vertexStride)
            where S : struct
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (startIndex + elementCount > data.Length)
                throw new ArgumentOutOfRangeException("startIndex must be smaller than elementCount + data.Length.");

            //TODO: Create a staging buffer only with the needed size that correctly handles startIndex and offsetInBytes.
            using (var stagingBuffer = CreateStagingBuffer(ResourceMapping.Read))
            {
                CopySubresource(NativeBuffer, stagingBuffer);

                using (var stream = MapBuffer(stagingBuffer, ResourceMapping.Read))
                {
                    if (offsetInBytes > 0)
                        stream.Seek(offsetInBytes, SeekOrigin.Current);

                    stream.ReadRange(data, startIndex, elementCount);
                    UnmapBuffer(stagingBuffer);
                }
            }
        }
        #endregion
    }
}
