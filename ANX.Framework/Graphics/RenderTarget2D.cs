using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.RenderSystem;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
	public class RenderTarget2D : Texture2D, IDynamicGraphicsResource
	{
		public event EventHandler<EventArgs> ContentLost;

        internal INativeRenderTarget2D NativeRenderTarget { get; private set; }
        public DepthFormat DepthStencilFormat { get; private set; }
        public bool IsContentLost { get; private set; }
        public int MultiSampleCount { get; private set; }
        public RenderTargetUsage RenderTargetUsage { get; private set; }

        #region Constructor
		public RenderTarget2D(GraphicsDevice graphicsDevice, int width, int height)
			: base(graphicsDevice)
		{
			this.width = width;
			this.height = height;
			OneOverWidth = 1f / width;
			OneOverHeight = 1f / height;

			base.LevelCount = 1;
			base.Format = SurfaceFormat.Color;

			this.DepthStencilFormat = DepthFormat.None;
			this.MultiSampleCount = 0;
			this.RenderTargetUsage = RenderTargetUsage.DiscardContents;

			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			this.NativeRenderTarget = creator.CreateRenderTarget(graphicsDevice, width, height, false, SurfaceFormat.Color,
				this.DepthStencilFormat, this.MultiSampleCount, this.RenderTargetUsage);
			base.nativeTexture = this.NativeRenderTarget as INativeTexture2D;
		}

		public RenderTarget2D(GraphicsDevice graphicsDevice, int width, int height,
			[MarshalAsAttribute(UnmanagedType.U1)] bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat)
			: base(graphicsDevice)
		{
            this.width = width;
            this.height = height;
            OneOverWidth = 1f / width;
            OneOverHeight = 1f / height;

            base.LevelCount = 1;
            base.Format = preferredFormat;

			this.DepthStencilFormat = preferredDepthFormat;
			this.MultiSampleCount = 0;
			this.RenderTargetUsage = RenderTargetUsage.DiscardContents;

			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			this.NativeRenderTarget = creator.CreateRenderTarget(graphicsDevice, width, height, false, SurfaceFormat.Color,
				this.DepthStencilFormat, this.MultiSampleCount, this.RenderTargetUsage);
			base.nativeTexture = this.NativeRenderTarget as INativeTexture2D;
		}

		public RenderTarget2D(GraphicsDevice graphicsDevice, int width, int height,
			[MarshalAsAttribute(UnmanagedType.U1)]  bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat,
			int preferredMultiSampleCount, RenderTargetUsage usage)
			: base(graphicsDevice)
		{
            this.width = width;
            this.height = height;
            OneOverWidth = 1f / width;
            OneOverHeight = 1f / height;

            base.LevelCount = 1;
            base.Format = preferredFormat;

			this.DepthStencilFormat = preferredDepthFormat;
			this.MultiSampleCount = preferredMultiSampleCount;
			this.RenderTargetUsage = usage;

			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			this.NativeRenderTarget = creator.CreateRenderTarget(graphicsDevice, width, height, false, SurfaceFormat.Color,
				this.DepthStencilFormat, this.MultiSampleCount, this.RenderTargetUsage);
			base.nativeTexture = this.NativeRenderTarget as INativeTexture2D;
		}
		#endregion

        protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            base.Dispose(false);
        }

        void IDynamicGraphicsResource.SetContentLost(bool isContentLost)
		{
			this.IsContentLost = isContentLost;
			if (isContentLost)
				raise_ContentLost(this, EventArgs.Empty);
		}

		protected void raise_ContentLost(object sender, EventArgs args)
		{
			if (ContentLost != null)
				ContentLost(sender, args);
		}
	}
}
