#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;

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

namespace ANX.Framework.Graphics
{
    public class IndexBuffer : GraphicsResource, IGraphicsResource
    {
        private int indexCount;
        private BufferUsage bufferUsage;
        private IndexElementSize indexElementSize;
        private INativeBuffer nativeIndexBuffer;

        public IndexBuffer(GraphicsDevice graphicsDevice, IndexElementSize indexElementSize, int indexCount, BufferUsage usage)
            : base(graphicsDevice)
        {
            this.indexElementSize = indexElementSize;
            this.indexCount = indexCount;
            this.bufferUsage = usage;

            base.GraphicsDevice.ResourceCreated += new EventHandler<ResourceCreatedEventArgs>(GraphicsDevice_ResourceCreated);
            base.GraphicsDevice.ResourceDestroyed += new EventHandler<ResourceDestroyedEventArgs>(GraphicsDevice_ResourceDestroyed);

            CreateNativeBuffer();
        }

        public IndexBuffer(GraphicsDevice graphicsDevice, Type indexType, int indexCount, BufferUsage usage)
            : base(graphicsDevice)
        {
            if (indexType == typeof(int) || indexType == typeof(uint))
            {
                this.indexElementSize = Graphics.IndexElementSize.ThirtyTwoBits;
            }
            else if (indexType == typeof(short) || indexType == typeof(ushort))
            {
                this.indexElementSize = Graphics.IndexElementSize.SixteenBits;
            }
            else
            {
                throw new Exception("can't use IndexType " + indexType.ToString());
            }

            this.indexCount = indexCount;
            this.bufferUsage = usage;

            CreateNativeBuffer();
        }

        ~IndexBuffer()
        {
            Dispose();
            base.GraphicsDevice.ResourceCreated -= GraphicsDevice_ResourceCreated;
            base.GraphicsDevice.ResourceDestroyed -= GraphicsDevice_ResourceDestroyed;
        }

        private void GraphicsDevice_ResourceDestroyed(object sender, ResourceDestroyedEventArgs e)
        {
            if (this.nativeIndexBuffer != null)
            {
                this.nativeIndexBuffer.Dispose();
                this.nativeIndexBuffer = null;
            }
        }

        private void GraphicsDevice_ResourceCreated(object sender, ResourceCreatedEventArgs e)
        {
            if (nativeIndexBuffer != null)
            {
                nativeIndexBuffer.Dispose();
                nativeIndexBuffer = null;
            }

            CreateNativeBuffer();
        }

        private void CreateNativeBuffer()
        {
            this.nativeIndexBuffer = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateIndexBuffer(GraphicsDevice, indexElementSize, indexCount, bufferUsage);
        }

        public void GetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
        {
            throw new NotImplementedException();
        }

        public void GetData<T>(T[] data) where T : struct
        {
            GetData<T>(0, data, 0, data.Length);
        }

        public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            GetData<T>(0, data, startIndex, elementCount);
        }

        public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
        {
            if (nativeIndexBuffer == null)
            {
                CreateNativeBuffer();
            }

            this.nativeIndexBuffer.SetData<T>(GraphicsDevice, offsetInBytes, data, startIndex, elementCount);
        }

        public void SetData<T>(T[] data) where T : struct
        {
            if (nativeIndexBuffer == null)
            {
                CreateNativeBuffer();
            }

            this.nativeIndexBuffer.SetData<T>(GraphicsDevice, data);
        }

        public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            SetData<T>(0, data, startIndex, elementCount);
        }

        public override void Dispose()
        {
            if (this.nativeIndexBuffer != null)
            {
                this.nativeIndexBuffer.Dispose();
                this.nativeIndexBuffer = null;
            }
        }

		// This is now internal because via befriending the assemblies
		// it's usable in the modules but doesn't confuse the enduser.
        internal INativeBuffer NativeIndexBuffer
        {
            get
            {
                if (nativeIndexBuffer == null)
                {
                    CreateNativeBuffer();
                }

                return this.nativeIndexBuffer;
            }
        }

        public BufferUsage BufferUsage
        {
            get
            {
                return this.bufferUsage;
            }
        }

        public int IndexCount
        {
            get
            {
                return this.indexCount;
            }
        }

        public IndexElementSize IndexElementSize
        {
            get
            {
                return this.indexElementSize;
            }
        }

        protected override void Dispose(Boolean disposeManaged)
        {
            throw new NotImplementedException();
        }
    }
}
