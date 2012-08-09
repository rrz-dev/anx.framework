#region Using Statements
using System;
using System.Runtime.InteropServices;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public class TextureCube : Texture, IGraphicsResource
    {
			public TextureCube(GraphicsDevice graphics, int size, [MarshalAsAttribute(UnmanagedType.U1)]  bool mipMap, SurfaceFormat format)
            : base(graphics)
        {
            this.Size = size;
        }

        public void GetData<T>(CubeMapFace cubeMapFace, int level, Nullable<Rectangle> rect, T[] data, int startIndex, int elementCount) where T : struct
        {
            throw new NotImplementedException();
        }

        public void GetData<T>(CubeMapFace cubeMapFace, T[] data) where T : struct
        {
            throw new NotImplementedException();
        }

        public void GetData<T>(CubeMapFace cubeMapFace, T[] data, int startIndex, int elementCount) where T : struct
        {
            throw new NotImplementedException();
        }

        public void SetData<T>(CubeMapFace cubeMapFace, int level, Nullable<Rectangle> rect, T[] data, int startIndex, int elementCount) where T : struct
        {
            throw new NotImplementedException();
        }

        public void SetData<T>(CubeMapFace cubeMapFace, T[] data) where T : struct
        {
            throw new NotImplementedException();
        }

        public void SetData<T>(CubeMapFace cubeMapFace, T[] data, int startIndex, int elementCount) where T : struct
        {
            throw new NotImplementedException();
        }
        
        public override void Dispose()
        {
            throw new NotImplementedException();
        }

				protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            throw new NotImplementedException();
        }

        public int Size
        {
            get;
            private set;
        }

        internal override void ReCreateNativeTextureSurface()
        {
            throw new NotImplementedException();
        }
    }
}
