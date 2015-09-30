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

using Dx11 = SharpDX.Direct3D11;

namespace ANX.RenderSystem.Windows.DX11
{
	public partial class DxTexture2D : INativeTexture2D
	{
        private Dx11.Texture2D _nativeTexture;

        public Dx11.ShaderResourceView NativeShaderResourceView { get; private set; }

        public Dx11.Texture2D NativeTexture
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
                    NativeShaderResourceView = new Dx11.ShaderResourceView(this.GraphicsDevice.NativeDevice.Device, NativeTexture);
#if DEBUG
                    NativeShaderResourceView.DebugName = _nativeTexture.DebugName + "_ShaderView";
#endif
                }
            }
        }

		#region Constructor
        protected DxTexture2D(GraphicsDeviceDX graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;
        }

        public DxTexture2D(GraphicsDeviceDX graphicsDevice, Dx11.Texture2D nativeTexture)
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

            Dx11.Device device = graphicsDevice.NativeDevice.Device;

            var description = new Dx11.Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = mipCount,
                ArraySize = mipCount,
                Format = DxFormatConverter.Translate(surfaceFormat),
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = Dx11.ResourceUsage.Default,
                BindFlags = Dx11.BindFlags.ShaderResource,
                CpuAccessFlags = Dx11.CpuAccessFlags.None,
                OptionFlags = Dx11.ResourceOptionFlags.None,
            };

            this.Width = width;
            this.Height = height;
            var texture = new Dx11.Texture2D(device, description);
#if DEBUG
            texture.DebugName = "Texture_" + textureCount++;
#endif
            this.NativeTexture = texture;
        }
		#endregion

		#region GetHashCode
		public override int GetHashCode()
		{
			return NativeTexture.NativePointer.ToInt32();
		}
		#endregion

		#region SaveAsJpeg (TODO)
		public void SaveAsJpeg(Stream stream, int width, int height)
		{
			// TODO: handle width and height?
			Dx11.Texture2D.ToStream(NativeTexture.Device.ImmediateContext, NativeTexture, Dx11.ImageFileFormat.Jpg, stream);
		}
		#endregion

		#region SaveAsPng (TODO)
		public void SaveAsPng(Stream stream, int width, int height)
		{
			// TODO: handle width and height?
			Dx11.Texture2D.ToStream(NativeTexture.Device.ImmediateContext, NativeTexture, Dx11.ImageFileFormat.Png, stream);
		}
		#endregion

        protected DataStream Map(Dx11.Texture2D texture, int subresource, int level, ResourceMapping mapping, out int pitch)
        {
            DataStream stream;
            var box = GraphicsDevice.NativeDevice.MapSubresource(texture, subresource, mapping.ToMapMode(), Dx11.MapFlags.None, out stream);

            pitch = box.RowPitch;
            return stream;
        }

        protected int GetSubresource(int level)
        {
            return Dx11.Texture2D.CalculateSubResourceIndex(level, 0, NativeTexture.Description.MipLevels);
        }

        protected void UpdateSubresource<T>(T[] data, Dx11.Texture2D texture, int level, Framework.Rectangle rect) where T : struct
        {
            GraphicsDevice.NativeDevice.UpdateSubresource(data, _nativeTexture, this.GetSubresource(level), GetPitch(level), 0, rect.ToResourceRegion());
        }

        protected Dx11.Texture2D CreateStagingTexture(ResourceMapping mapping)
        {
            var description = new Dx11.Texture2DDescription()
            {
                CpuAccessFlags = mapping.ToCpuAccessFlags(),
                Usage = Dx11.ResourceUsage.Staging,
                Format = NativeTexture.Description.Format,
                Width = this.Width,
                Height = this.Height,
                MipLevels = this.NativeTexture.Description.MipLevels,
                SampleDescription = NativeTexture.Description.SampleDescription,
                ArraySize = NativeTexture.Description.ArraySize,
                OptionFlags = Dx11.ResourceOptionFlags.None,

            };

            var texture = new Dx11.Texture2D(GraphicsDevice.NativeDevice.Device, description);
#if DEBUG
            texture.DebugName = NativeTexture.DebugName + "_Staging";
#endif
            return texture;
        }

        protected void Unmap(Dx11.Texture2D texture, int subresource)
        {
            GraphicsDevice.NativeDevice.UnmapSubresource(texture, subresource);
        }
	}
}
