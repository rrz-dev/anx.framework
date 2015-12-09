#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using ANX.Framework.Content.Pipeline.Helpers;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    public class VertexBufferContent : ContentItem
    {
        public VertexBufferContent()
            : this(0)
        {

        }

        public VertexBufferContent(int size)
        {
            VertexData = new byte[size];
        }

        public byte[] VertexData
        {
            get;
            private set;
        }

        public VertexDeclarationContent VertexDeclaration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the size of the specified type, in bytes. 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        public static int SizeOf(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (!type.IsValueType)
                throw new NotSupportedException("The type of a value that should be used for a VertexBuffer should be a struct.");

            return Marshal.SizeOf(type);
        }

        /// <summary>
        /// Writes additional data into the vertex buffer. Writing begins at the specified byte offset, and each value is spaced according to the specified stride value (in bytes). 
        /// </summary>
        /// <remarks>
        /// This method automatically grows the vertex buffer if an attempt is made to write past the buffer end. Write throws NotSupportedException 
        /// if the specified data type cannot be packed into a vertex buffer. For example, if data is not a valid value type.
        ///  
        /// Use this method to interleave vertex data channels into a single buffer. This can be done by passing the total vertex size as the stride and suitable smaller offsets for each channel. 
        /// You can also concatenate entire vertex buffers by passing the length of the vertex as the offset, 1 as the stride, and the vertex data as the data parameter. 
        /// </remarks>
        /// <typeparam name="T">Type being written.</typeparam>
        /// <param name="offset">Offset to begin writing at.</param>
        /// <param name="stride">Stride of the data being written (in bytes).</param>
        /// <param name="data">Enumerated collection of data.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        public void Write<T>(int offset, int stride, IEnumerable<T> data) where T : struct
        {
            this.Write(offset, stride, typeof(T), data);
        }

        /// <summary>
        /// Writes additional data into the vertex buffer. Writing begins at the specified byte offset, and each value is spaced according to the specified stride value (in bytes). 
        /// </summary>
        /// <remarks>
        /// This method automatically grows the vertex buffer if an attempt is made to write past the buffer end. Write throws NotSupportedException 
        /// if the specified data type cannot be packed into a vertex buffer. For example, if data is not a valid value type.
        ///  
        /// Use this method to interleave vertex data channels into a single buffer. This can be done by passing the total vertex size as the stride and suitable smaller offsets for each channel. 
        /// You can also concatenate entire vertex buffers by passing the length of the vertex as the offset, 1 as the stride, and the vertex data as the data parameter. 
        /// </remarks>
        /// <param name="offset"></param>
        /// <param name="stride"></param>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        public void Write(int offset, int stride, Type dataType, IEnumerable data)
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset");

            if (dataType == null)
                throw new ArgumentNullException("dataType");

            //TODO: support the second documented use case.
            int elementSize = SizeOf(dataType);
            //Make sure we don't intercopy the data if we write it. Having a bigger stride is no problem, in that case we write what we can and then skip all unused bytes to get to the next stride.
            if (stride < elementSize)
                throw new ArgumentOutOfRangeException("stride");

            if (data == null)
                throw new ArgumentNullException("data");

            List<object> dataList = new List<object>();
            foreach (var obj in data)
            {
                //obj should not be able to be null, because we tested with SizeOf if it is a ValueType.
                if (!dataType.IsAssignableFrom(obj.GetType()))
                    throw new ArgumentException(string.Format("data contains elements that are not compatible with \"{0}\"", dataType.Name));

                dataList.Add(obj);
            }

            //regrow the array if the data would write over the boundaries.
            int dataLength = dataList.Count * stride;
            int vertexOffset = offset / stride * stride;
            if (vertexOffset + dataLength > VertexData.Length)
            {
                byte[] newVertexData = new byte[vertexOffset + dataLength];
                Array.Copy(VertexData, newVertexData, VertexData.Length);

                this.VertexData = newVertexData;
            }

            //Copy the data over, the content of dataList is guaranteed to be a value type by the SizeOf check earlier.
            for (int i = 0; i < dataList.Count; i++)
            {
                //We have to copy every element separately to support the first documented use case where someone passed a stride that is much bigger than the real data to write only data into the vertex buffer for
                //a single vertex channel, even though the data in the vertex buffer must be arranged per vertex and not per vertex channel.
                var dataHandle = GCHandle.Alloc(dataList[i], GCHandleType.Pinned);
                try
                {
                    Marshal.Copy(dataHandle.AddrOfPinnedObject(), VertexData, offset + i * stride, elementSize);
                }
                finally
                {
                    dataHandle.Free();
                }
            }
        }


    }
}
