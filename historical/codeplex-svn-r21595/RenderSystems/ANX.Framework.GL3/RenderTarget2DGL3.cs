using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using OpenTK.Graphics.OpenGL;
using ANX.RenderSystem.GL3.Helpers;

namespace ANX.RenderSystem.GL3
{
	public class RenderTarget2DGL3 : Texture2DGL3, INativeRenderTarget2D
	{
		#region Private
		private int framebufferHandle;
		private int renderbufferHandle;

		private bool generateMipmaps;
		#endregion

		// TODO: usage, preferredMultiSampleCount
		#region Constructor
		public RenderTarget2DGL3(int width, int height, bool mipMap,
			SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat,
			int preferredMultiSampleCount, RenderTargetUsage usage)
			: base()
		{
			generateMipmaps = mipMap;
			PixelInternalFormat nativeFormat =
				DatatypesMapping.SurfaceToPixelInternalFormat(preferredFormat);

			#region Image creation
			NativeHandle = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, NativeHandle);
			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureMagFilter, (int)All.Linear);
			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
			GL.TexParameter(TextureTarget.Texture2D,
				TextureParameterName.TextureWrapT, (int)All.ClampToEdge);
			if (generateMipmaps)
			{
				GL.TexParameter(TextureTarget.Texture2D,
					TextureParameterName.GenerateMipmap, 1);
				GL.TexParameter(TextureTarget.Texture2D,
					TextureParameterName.TextureMinFilter, (int)All.LinearMipmapLinear);
			}
			else
			{
				GL.TexParameter(TextureTarget.Texture2D,
					TextureParameterName.TextureMinFilter, (int)All.Linear);
			}

			GL.TexImage2D(TextureTarget.Texture2D, 0, nativeFormat,
				width, height, 0, (PixelFormat)nativeFormat, PixelType.UnsignedByte,
				IntPtr.Zero);
			GL.BindTexture(TextureTarget.Texture2D, 0);
			#endregion
			
			// create a renderbuffer object to store depth info
			GL.GenRenderbuffers(1, out renderbufferHandle);
			GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderbufferHandle);
			GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer,
				DepthFormatConversion(preferredDepthFormat), width, height);
			GL.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, 0);

			// create a framebuffer object
			GL.GenFramebuffers(1, out framebufferHandle);
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferHandle);

			// attach the texture to FBO color attachment point
			GL.FramebufferTexture2D(FramebufferTarget.Framebuffer,
				FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D,
				NativeHandle, 0);

			// attach the renderbuffer to depth attachment point
			GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer,
				FramebufferAttachment.DepthAttachment,
				RenderbufferTarget.Renderbuffer, renderbufferHandle);

			// check FBO status
			FramebufferErrorCode status = GL.CheckFramebufferStatus(
				FramebufferTarget.Framebuffer);
			if (status != FramebufferErrorCode.FramebufferComplete)
			{
				throw new InvalidOperationException(
					"Failed to create the render target! Error=" + status);
			}

			// switch back to window-system-provided framebuffer
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
		}
		#endregion

		#region DepthFormatConversion
		private RenderbufferStorage DepthFormatConversion(DepthFormat depthFormat)
		{
			switch(depthFormat)
			{
				default:
				case DepthFormat.None:
					// TODO
					return RenderbufferStorage.DepthComponent16;
					//return (RenderbufferStorage)All.DepthComponent;

				case DepthFormat.Depth16:
					return RenderbufferStorage.DepthComponent16;

				case DepthFormat.Depth24:
					return RenderbufferStorage.DepthComponent24;

				case DepthFormat.Depth24Stencil8:
					return RenderbufferStorage.DepthComponent32;
			}
		}
		#endregion

		#region Bind
		public void Bind()
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferHandle);
		}
		#endregion

		#region Unbind
		public void Unbind()
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
			if (generateMipmaps)
			{
				GL.BindTexture(TextureTarget.Texture2D, NativeHandle);
				GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
				GL.BindTexture(TextureTarget.Texture2D, 0);
			}
		}
		#endregion

		#region Dispose
		public override void Dispose()
		{
			base.Dispose();
			GL.DeleteFramebuffers(1, ref framebufferHandle);
			GL.DeleteRenderbuffers(1, ref renderbufferHandle);
		}
		#endregion
	}
}
