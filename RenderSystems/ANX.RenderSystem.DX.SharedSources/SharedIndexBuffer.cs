#region Using Statements
using System;
using ANX.Framework.Graphics;
using SharpDX;
using System.IO;
using System.Runtime.InteropServices;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if DX10
namespace ANX.RenderSystem.Windows.DX10
#elif DX11
namespace ANX.RenderSystem.Windows.DX11
#endif
{
    public partial class DxIndexBuffer
    {
        protected IndexElementSize elementSize;

        #region SetData
        public void SetData<S>(S[] data) where S : struct
        {
            SetData<S>(data, 0, data.Length);
        }

        public void SetData<S>(S[] data, int startIndex, int elementCount) where S : struct
        {
            SetData<S>(0, data, startIndex, elementCount);
        }

        public void SetData<S>(int offsetInBytes, S[] data, int startIndex, int elementCount)
            where S : struct
        {
            if (offsetInBytes + elementCount * Marshal.SizeOf(typeof(S)) > SizeInBytes)
                throw new ArgumentOutOfRangeException(string.Format("The offset by \"{0}\" plus the byte length described by \"{1}\" is over the bounds of the buffer.", "offsetInBytes", "elementCount"));

            var buffer = this.NativeBuffer;
            if (this.WriteNeedsStaging)
                buffer = this.CreateStagingBuffer(ResourceMapping.Write);

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

            GetData(0, data, 0, data.Length);
        }

        public void GetData<S>(S[] data, int startIndex, int elementCount) where S : struct
        {
            GetData(0, data, startIndex, elementCount);
        }

        public void GetData<S>(int offsetInBytes, S[] data, int startIndex, int elementCount) where S : struct
        {
            if (data == null)
                throw new ArgumentNullException("data");

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

        protected int GetSizeInBytes(int indexCount)
        {
            return (elementSize == IndexElementSize.SixteenBits ? 2 : 4) * indexCount;
        }
    }
}
