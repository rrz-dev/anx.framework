#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public abstract class TextureContent : ContentItem
    {
        protected TextureContent(MipmapChainCollection faces)
        {
            Faces = faces;
        }

        public MipmapChainCollection Faces
        {
            get;
            private set;
        }

        public void ConvertBitmapType(Type newBitmapType)
        {
            if (newBitmapType == null)
                throw new ArgumentNullException("newBitmapType");

            foreach (MipmapChain face in this.Faces)
            {
                for (int i = 0; i < face.Count; i++)
                {
                    face[i] = BitmapContent.Convert(face[i], newBitmapType);
                }
            }
        }

        public virtual void GenerateMipmaps(bool overwriteExistingMipmaps)
        {
            foreach (MipmapChain face in this.Faces)
            {
                if (face.Count == 0)
                    continue;

                //If we are overwriting, remove all bitmaps in the MipmapChain except the first one which we will use as base for all following Mipmaps.
                if (overwriteExistingMipmaps)
                {
                    while (face.Count > 1)
                    {
                        face.RemoveAt(face.Count - 1);
                    }
                }
            
                BitmapContent bitmap = face[0];
                int width = bitmap.Width;
                int height = bitmap.Height;

                while (width > 1 || height > 1)
                {
                    width /= 2;
                    height /= 2;
                    bitmap = BitmapContent.Convert(bitmap, bitmap.GetType(), width, height);

                    face.Add(bitmap);
                }
            }
        }

        public abstract void Validate(Nullable<GraphicsProfile> targetProfile);
    }
}
