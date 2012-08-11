using System;
using ANX.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.PsVita
{
	internal class PsVitaGameWindow : GameWindow
	{
		#region Public
		#region Handle
		public override IntPtr Handle
		{
			get
			{
				// TODO
				return IntPtr.Zero;
			}
		}
		#endregion

		#region IsMinimized
		public override bool IsMinimized
		{
			get
			{
				return false;
			}
		}
		#endregion

		#region ScreenDeviceName
		public override string ScreenDeviceName
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region AllowUserResizing
		public override bool AllowUserResizing
		{
			get
			{
				return false;
			}
			set
			{
			}
		}
		#endregion

		#region ClientBounds
		public override Rectangle ClientBounds
		{
			get
			{
				// TODO
				return Rectangle.Empty;
				//System.Drawing.Rectangle rect = Form.ClientRectangle;
				//return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
			}
		}
		#endregion

		#region CurrentOrientation
		public override DisplayOrientation CurrentOrientation
		{
			get
			{
				return DisplayOrientation.Default;
			}
		}
		#endregion
		#endregion

		#region Constructor
		internal PsVitaGameWindow()
		{
		}
		#endregion

		#region Close
		public void Close()
		{
			// TODO
		}
		#endregion

		#region SetTitle
		protected override void SetTitle(string title)
		{
			// TODO
		}
		#endregion

		#region BeginScreenDeviceChange
		public override void BeginScreenDeviceChange(bool willBeFullScreen)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region EndScreenDeviceChange
		public override void EndScreenDeviceChange(string screenDeviceName,
			int clientWidth, int clientHeight)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
