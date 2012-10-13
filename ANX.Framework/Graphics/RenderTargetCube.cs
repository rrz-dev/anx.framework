using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(0)]
    [TestState(TestStateAttribute.TestState.Untested)]
	public class RenderTargetCube : TextureCube, IDynamicGraphicsResource
	{
		public event EventHandler<EventArgs> ContentLost;

		public DepthFormat DepthStencilFormat
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsContentLost
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int MultiSampleCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public RenderTargetUsage RenderTargetUsage
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public RenderTargetCube(GraphicsDevice graphicsDevice, int size, [MarshalAsAttribute(UnmanagedType.U1)] bool mipMap,
			SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat)
			: base(graphicsDevice, size, mipMap, preferredFormat)
		{
			throw new NotImplementedException();
		}

		public RenderTargetCube(GraphicsDevice graphicsDevice, int size, [MarshalAsAttribute(UnmanagedType.U1)] bool mipMap,
			SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount,
			RenderTargetUsage usage)
			: base(graphicsDevice, size, mipMap, preferredFormat)
		{
			throw new NotImplementedException();
		}

		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
		{
			throw new NotImplementedException();
		}

		void IDynamicGraphicsResource.SetContentLost(bool isContentLost)
		{
			throw new NotImplementedException();
		}

		protected void raise_ContentLost(object sender, EventArgs args)
		{
			if (ContentLost != null)
				ContentLost(sender, args);
		}
	}
}
