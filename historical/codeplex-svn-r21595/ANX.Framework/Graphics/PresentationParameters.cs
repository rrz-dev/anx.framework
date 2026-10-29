using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann")]
	public class PresentationParameters
	{
		#region Public
		#region BackBufferFormat
		public SurfaceFormat BackBufferFormat
		{
			get;
			set;
		}
		#endregion

		#region BackBufferHeight
		public int BackBufferHeight
		{
			get;
			set;
		}
		#endregion

		#region BackBufferWidth
		public int BackBufferWidth
		{
			get;
			set;
		}
		#endregion

		#region Bounds
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(0, 0, BackBufferWidth, BackBufferHeight);
			}
		}
		#endregion

		#region DepthStencilFormat
		public DepthFormat DepthStencilFormat
		{
			get;
			set;
		}
		#endregion

		#region DeviceWindowHandle
		public IntPtr DeviceWindowHandle
		{
			get;
			set;
		}
		#endregion

		#region DisplayOrientation
		public DisplayOrientation DisplayOrientation
		{
			get;
			set;
		}
		#endregion

		#region IsFullScreen
		public bool IsFullScreen
		{
			get;
			set;
		}
		#endregion

		#region MultiSampleCount
		public int MultiSampleCount
		{
			get;
			set;
		}
		#endregion

		#region PresentationInterval
		public PresentInterval PresentationInterval
		{
			get;
			set;
		}
		#endregion

		#region RenderTargetUsage
		public RenderTargetUsage RenderTargetUsage
		{
			get;
			set;
		}
		#endregion
		#endregion

		#region Constructor
		public PresentationParameters()
		{
			IsFullScreen = true;
		}
		#endregion

		#region Clone
		public PresentationParameters Clone()
		{
			return new PresentationParameters()
			{
				BackBufferFormat = this.BackBufferFormat,
				BackBufferHeight = this.BackBufferHeight,
				BackBufferWidth = this.BackBufferWidth,
				DepthStencilFormat = this.DepthStencilFormat,
				DeviceWindowHandle = this.DeviceWindowHandle,
				DisplayOrientation = this.DisplayOrientation,
				IsFullScreen = this.IsFullScreen,
				MultiSampleCount = this.MultiSampleCount,
				PresentationInterval = this.PresentationInterval,
				RenderTargetUsage = this.RenderTargetUsage,
			};
		}
		#endregion
	}
}
