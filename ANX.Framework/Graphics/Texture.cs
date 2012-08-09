#region Using Statements
using System;
using ANX.Framework.NonXNA.RenderSystem;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public abstract class Texture : GraphicsResource
    {
        protected internal int levelCount;
        protected internal SurfaceFormat format;
        protected internal INativeTexture nativeTexture;

        public Texture(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            base.GraphicsDevice.ResourceCreated += GraphicsDevice_ResourceCreated;
            base.GraphicsDevice.ResourceDestroyed += GraphicsDevice_ResourceDestroyed;
        }

        ~Texture()
        {
            base.GraphicsDevice.ResourceCreated -= GraphicsDevice_ResourceCreated;
            base.GraphicsDevice.ResourceDestroyed -= GraphicsDevice_ResourceDestroyed;
        }

        public int LevelCount
        {
            get
            {
                return this.levelCount;
            }
        }

        public SurfaceFormat Format
        {
            get
            {
                return this.format;
            }
        }

        internal INativeTexture NativeTexture
        {
            get
            {
                if (this.nativeTexture == null)
                {
                    ReCreateNativeTextureSurface();
                }

                return this.nativeTexture;
            }
        }

        public override void Dispose()
        {
            Dispose(true);
        }

        protected override void Dispose(bool disposeManaged)
        {
            if (disposeManaged && nativeTexture != null)
            {
                nativeTexture.Dispose();
                nativeTexture = null;
            }
        }

        internal abstract void ReCreateNativeTextureSurface();

        private void GraphicsDevice_ResourceDestroyed(object sender, ResourceDestroyedEventArgs e)
        {
            if (nativeTexture != null)
            {
                nativeTexture.Dispose();
                nativeTexture = null;
            }
        }

        private void GraphicsDevice_ResourceCreated(object sender, ResourceCreatedEventArgs e)
        {
            if (nativeTexture != null)
            {
                nativeTexture.Dispose();
                nativeTexture = null;
            }

            ReCreateNativeTextureSurface();
        }
    }
}
