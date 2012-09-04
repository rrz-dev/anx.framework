using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.RenderSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	public class RenderTarget2D : Texture2D, IDynamicGraphicsResource
	{
		public event EventHandler<EventArgs> ContentLost;

		#region Private
		private DepthFormat depthStencilFormat;
		private int multiSampleCount;
		private RenderTargetUsage usage;
		private bool isContentLost;
		private INativeRenderTarget2D nativeRenderTarget;
		#endregion

		internal INativeRenderTarget2D NativeRenderTarget
		{
			get
			{
				return this.nativeRenderTarget;
			}
		}

		public DepthFormat DepthStencilFormat
		{
			get
			{
				return this.depthStencilFormat;
			}
		}

		public bool IsContentLost
		{
			get
			{
				return this.isContentLost;
			}
		}

		public int MultiSampleCount
		{
			get
			{
				return this.multiSampleCount;
			}
		}

		public RenderTargetUsage RenderTargetUsage
		{
			get
			{
				return this.usage;
			}
		}

		#region Constructor
		public RenderTarget2D(GraphicsDevice graphicsDevice, int width, int height)
			: base(graphicsDevice)
		{
			this.width = width;
			this.height = height;

			base.levelCount = 1;
			base.format = SurfaceFormat.Color;

			this.depthStencilFormat = DepthFormat.None;
			this.multiSampleCount = 0;
			this.usage = RenderTargetUsage.DiscardContents;

			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			this.nativeRenderTarget = creator.CreateRenderTarget(graphicsDevice, width, height, false, SurfaceFormat.Color,
				this.depthStencilFormat, this.multiSampleCount, this.usage);
			base.nativeTexture = this.nativeRenderTarget as INativeTexture2D;
		}

		public RenderTarget2D(GraphicsDevice graphicsDevice, int width, int height,
			[MarshalAsAttribute(UnmanagedType.U1)] bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat)
			: base(graphicsDevice)
		{
			this.depthStencilFormat = DepthFormat.None;
			this.multiSampleCount = 0;
			this.usage = RenderTargetUsage.DiscardContents;

			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			this.nativeRenderTarget = creator.CreateRenderTarget(graphicsDevice, width, height, false, SurfaceFormat.Color,
				this.depthStencilFormat, this.multiSampleCount, this.usage);
			base.nativeTexture = this.nativeRenderTarget as INativeTexture2D;
		}

		public RenderTarget2D(GraphicsDevice graphicsDevice, int width, int height,
			[MarshalAsAttribute(UnmanagedType.U1)]  bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat,
			int preferredMultiSampleCount, RenderTargetUsage usage)
			: base(graphicsDevice)
		{
			this.depthStencilFormat = preferredDepthFormat;
			this.multiSampleCount = preferredMultiSampleCount;
			this.usage = usage;

			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			this.nativeRenderTarget = creator.CreateRenderTarget(graphicsDevice, width, height, false, SurfaceFormat.Color,
				this.depthStencilFormat, this.multiSampleCount, this.usage);
			base.nativeTexture = this.nativeRenderTarget as INativeTexture2D;
		}
		#endregion

		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
		{
			throw new NotImplementedException();
		}

		void IDynamicGraphicsResource.SetContentLost(bool isContentLost)
		{
			this.isContentLost = isContentLost;
			if (isContentLost)
				raise_ContentLost(this, EventArgs.Empty);

			throw new NotImplementedException();
		}

		protected void raise_ContentLost(object sender, EventArgs args)
		{
			if (ContentLost != null)
				ContentLost(sender, args);
		}
	}
}
