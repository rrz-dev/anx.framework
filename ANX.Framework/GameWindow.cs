#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [Developer("Glatzemann")]
	public abstract class GameWindow
	{
		#region Private
		private string title;
		#endregion

		#region Events
		public event EventHandler<EventArgs> Activated;
		public event EventHandler<EventArgs> ClientSizeChanged;
		public event EventHandler<EventArgs> Deactivated;
		public event EventHandler<EventArgs> OrientationChanged;
		public event EventHandler<EventArgs> Paint;
		public event EventHandler<EventArgs> ScreenDeviceNameChanged;
		#endregion

		#region Public
		#region Title
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				if (String.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException();
				}

				if (value != this.title)
				{
					this.title = value;
					SetTitle(value);
				}
			}
		}
		#endregion

		public abstract bool AllowUserResizing
		{
			get;
			set;
		}

		public abstract Rectangle ClientBounds
		{
			get;
		}

		public abstract string ScreenDeviceName
		{
			get;
		}

		public abstract DisplayOrientation CurrentOrientation
		{
			get;
		}

		public abstract IntPtr Handle
		{
			get;
		}

		public abstract bool IsMinimized
		{
			get;
		}
		#endregion

		#region BeginScreenDeviceChange (abstract)
		public abstract void BeginScreenDeviceChange(bool willBeFullScreen);
		#endregion

		#region EndScreenDeviceChange
		public void EndScreenDeviceChange(string screenDeviceName)
		{
			EndScreenDeviceChange(screenDeviceName, ClientBounds.Width,
				ClientBounds.Height);
		}
		#endregion

		#region EndScreenDeviceChange (abstract)
		public abstract void EndScreenDeviceChange(string screenDeviceName,
			int clientWidth, int clientHeight);
		#endregion

		#region SetTitle (abstract)
		protected abstract void SetTitle(string title);
		#endregion

		#region OnActivated
		protected void OnActivated()
		{
			if (Activated != null)
			{
				Activated(this, EventArgs.Empty);
			}
		}
		#endregion

		#region OnDeactivated
		protected void OnDeactivated()
		{
			if (Deactivated != null)
			{
				Deactivated(this, EventArgs.Empty);
			}
		}
		#endregion

		#region OnOrientationChanged
		protected void OnOrientationChanged()
		{
			if (OrientationChanged != null)
			{
				OrientationChanged(this, EventArgs.Empty);
			}
		}
		#endregion

		#region OnPaint
		protected void OnPaint()
		{
			if (Paint != null)
			{
				Paint(this, EventArgs.Empty);
			}
		}
		#endregion

		#region OnScreenDeviceNameChanged
		protected void OnScreenDeviceNameChanged()
		{
			if (ScreenDeviceNameChanged != null)
			{
				ScreenDeviceNameChanged(this, EventArgs.Empty);
			}
		}
		#endregion

		#region OnClientSizeChanged
		protected void OnClientSizeChanged()
		{
			if (ClientSizeChanged != null)
				ClientSizeChanged(this, EventArgs.Empty);
		}
		#endregion
	}
}
