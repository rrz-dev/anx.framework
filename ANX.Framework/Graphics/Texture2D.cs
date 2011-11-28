#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA.RenderSystem;
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
    public class Texture2D : Texture, IGraphicsResource
    {
        #region Private Members
        private int width;
        private int height;

        #endregion // Private Members

        internal Texture2D(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {

        }

        public Texture2D(GraphicsDevice graphicsDevice, int width, int height)
            : base(graphicsDevice)
        {
            this.width = width;
            this.height = height;

            base.levelCount = 1;
            base.format = SurfaceFormat.Color;

            CreateNativeTextureSurface(graphicsDevice, SurfaceFormat.Color, width, height, levelCount);
        }

        public Texture2D(GraphicsDevice graphicsDevice, int width, int height, bool mipMap, SurfaceFormat format)
            : base(graphicsDevice)
        {
            this.width = width;
            this.height = height;

            base.levelCount = 1;    //TODO: mipmap paramter?!?!?
            base.format = format;

            CreateNativeTextureSurface(graphicsDevice, format, width, height, levelCount);
        }

        public static Texture2D FromStream(GraphicsDevice graphicsDevice, Stream stream)
        {
            throw new NotImplementedException();
        }

        public static Texture2D FromStream(GraphicsDevice graphicsDevice, Stream stream, int width, int height, bool zoom)
        {
            throw new NotImplementedException();
        }

        public void GetData<T>(int level, Nullable<Rectangle> rect, T[] data, int startIndex, int elementCount) where T : struct
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

        public void SaveAsJpeg(Stream stream, int width, int height)
        {
            throw new NotImplementedException();
        }

        public void SaveAsPng(Stream stream, int width, int height)
        {
            throw new NotImplementedException();
        }

        public void SetData<T>(int level, Nullable<Rectangle> rect, T[] data, int startIndex, int elementCount) where T : struct
        {
            throw new NotImplementedException();
        }

        public void SetData<T>(T[] data) where T : struct
        {
            this.nativeTexture.SetData<T>(GraphicsDevice, data);
        }

        public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            this.nativeTexture.SetData<T>(GraphicsDevice, data, startIndex, elementCount);
        }

        public override void Dispose()
        {
            base.Dispose(true);
        }

        protected override void Dispose(Boolean disposeManaged)
        {
            base.Dispose(disposeManaged);
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(0, 0, this.width, this.height);
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
        }

        internal void CreateNativeTextureSurface(GraphicsDevice device, SurfaceFormat format, int width, int height, int levelCount)
        {
            base.nativeTexture = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().CreateTexture(device, format, width, height, levelCount);
        }
    }
}
