#region Using Statements
using System;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx10 = SharpDX.Direct3D10;
using System.Runtime.InteropServices;

namespace ANX.RenderSystem.Windows.DX10
{
	public partial class DxTexture2D : INativeTexture2D
	{
        private Dx10.Texture2D _nativeTexture;

		public Dx10.ShaderResourceView NativeShaderResourceView { get; private set; }

        public Dx10.Texture2D NativeTexture
        {
            get { return _nativeTexture; }
            set
            {
                if (NativeShaderResourceView != null)
                {
                    NativeShaderResourceView.Dispose();
                    NativeShaderResourceView = null;
                }

                _nativeTexture = value;

                if (_nativeTexture != null)
                {
                    NativeShaderResourceView = new Dx10.ShaderResourceView(this.GraphicsDevice.NativeDevice, NativeTexture);
#if DEBUG
                    NativeShaderResourceView.DebugName = _nativeTexture.DebugName + "_ShaderView";
#endif
                }
            }
        }

        protected DxTexture2D(GraphicsDeviceDX graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;
        }

        public DxTexture2D(GraphicsDeviceDX graphicsDevice, Dx10.Texture2D nativeTexture)
            : this(graphicsDevice)
        {
            if (nativeTexture == null)
                throw new ArgumentNullException("nativeTexture");

            this.Width = nativeTexture.Description.Width;
            this.Height = nativeTexture.Description.Height;
#if DEBUG
            if (string.IsNullOrEmpty(nativeTexture.DebugName))
                nativeTexture.DebugName = "Texture_" + textureCount++;
#endif
            this.NativeTexture = nativeTexture;

            this.surfaceFormat = DxFormatConverter.Translate(nativeTexture.Description.Format);
        }

        public DxTexture2D(GraphicsDeviceDX graphicsDevice, int width, int height, SurfaceFormat surfaceFormat, int mipCount)
            : this(graphicsDevice)
		{
            this.surfaceFormat = surfaceFormat;

			Dx10.Device device = graphicsDevice.NativeDevice;

			var description = new Dx10.Texture2DDescription()
			{
				Width = width,
				Height = height,
				MipLevels = mipCount,
				ArraySize = mipCount,
				Format = DxFormatConverter.Translate(surfaceFormat),
				SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
				Usage = Dx10.ResourceUsage.Default,
				BindFlags = Dx10.BindFlags.ShaderResource,
				CpuAccessFlags = Dx10.CpuAccessFlags.None,
				OptionFlags = Dx10.ResourceOptionFlags.None,
			};

            this.Width = width;
            this.Height = height;
			var texture = new Dx10.Texture2D(device, description);
#if DEBUG
            texture.DebugName = "Texture_" + textureCount++;
#endif

            NativeTexture = texture;
		}

		public override int GetHashCode()
		{
			return NativeTexture.NativePointer.ToInt32();
		}

		public void SaveAsJpeg(Stream stream, int width, int height)
		{
			// TODO: handle width and height?
			Dx10.Texture2D.ToStream(NativeTexture, Dx10.ImageFileFormat.Jpg, stream);
		}

		public void SaveAsPng(Stream stream, int width, int height)
		{
			// TODO: handle width and height?
			Dx10.Texture2D.ToStream(NativeTexture, Dx10.ImageFileFormat.Png, stream);
		}

        protected DataStream Map(Dx10.Texture2D texture, int subresource, int level, ResourceMapping mapping, out int pitch)
		{
            var dataRect = texture.Map(subresource, mapping.ToMapMode(), Dx10.MapFlags.None);

            pitch = dataRect.Pitch;

            bool read = (mapping & ResourceMapping.Read) != 0;
            bool write = (mapping & ResourceMapping.Write) != 0;

            return new DataStream(dataRect.DataPointer, SizeInBytes(level), read, write);
		}

        protected int GetSubresource(int level)
        {
            return Dx10.Texture2D.CalculateSubResourceIndex(level, 0, NativeTexture.Description.MipLevels);
        }

        protected void UpdateSubresource<T>(T[] data, Dx10.Texture2D texture, int level, Framework.Rectangle rect) where T : struct
        {
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                GraphicsDevice.NativeDevice.UpdateSubresource(texture, this.GetSubresource(level), rect.ToResourceRegion(), handle.AddrOfPinnedObject(), GetPitch(level), 0);
            }
            finally
            {
                handle.Free();
            }
        }

        protected Dx10.Texture2D CreateStagingTexture(ResourceMapping mapping)
        {
            var description = new Dx10.Texture2DDescription()
            {
                CpuAccessFlags = mapping.ToCpuAccessFlags(),
                Usage = Dx10.ResourceUsage.Staging,
                Format = NativeTexture.Description.Format,
                Width = this.Width,
                Height = this.Height,
                MipLevels = this.NativeTexture.Description.MipLevels,
                SampleDescription = NativeTexture.Description.SampleDescription,
                ArraySize = NativeTexture.Description.ArraySize,
                OptionFlags = Dx10.ResourceOptionFlags.None,
                
            };

            var texture = new Dx10.Texture2D(GraphicsDevice.NativeDevice, description);
#if DEBUG
            texture.DebugName = NativeTexture.DebugName + "_Staging";
#endif
            return texture;
        }

		protected void Unmap(Dx10.Texture2D texture, int subresource)
		{
            texture.Unmap(subresource);
		}
	}
}
