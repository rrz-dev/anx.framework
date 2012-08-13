using System;
using ANX.Framework;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Display;
using Windows.UI.Core;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
	public class WindowsGameWindow : GameWindow, IFrameworkView
	{
		#region Private
		private WindowsGameHost gameHost;
		private CoreWindow gameWindow;
		private float dpi;
		private Rectangle clientBounds;
		#endregion

		#region Public
		public CoreWindow Form
		{
			get
			{
				return gameWindow;
			}
		}

		public override IntPtr Handle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		public override bool IsMinimized
		{
			get
			{
				//TODO: return gameWindow.WindowState == FormWindowState.Minimized;
				return false;
			}
		}

		public override string ScreenDeviceName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override DisplayOrientation CurrentOrientation
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool AllowUserResizing
		{
			get
			{
				//return gameWindow.FormBorderStyle == FormBorderStyle.Sizable;
				return false;
			}
			set
			{
				throw new NotSupportedException("AllowUserResizing can not be changed in RenderSystem Metro");
			}
		}

		public override Rectangle ClientBounds
		{
			get
			{
				return clientBounds;
			}
		}
		#endregion

		#region Constructor
		internal WindowsGameWindow(WindowsGameHost setGameHost)
		{
			gameHost = setGameHost;
		}
		#endregion

		#region Close
		public void Close()
		{
			if (gameWindow != null)
			{
				gameWindow.Close();
			}
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

		#region SetTitle
		protected override void SetTitle(string title)
		{
			//TODO: this.gameWindow.Text = title;
		}
		#endregion

		#region SetDpiIfNeeded
		private void SetDpiIfNeeded(float setDpi)
		{
			if (dpi != setDpi)
			{
				dpi = setDpi;
				SizeChanged();
			}
		}
		#endregion

		#region SetWindow
		public void SetWindow(CoreWindow window)
		{
			gameWindow = window;

			window.SizeChanged += delegate
			{
				SizeChanged();
			};

			SetDpiIfNeeded(DisplayProperties.LogicalDpi);
		}
		#endregion

		private void SizeChanged()
		{
			clientBounds = new Rectangle(
				(int)gameWindow.Bounds.Left, (int)gameWindow.Bounds.Top,
				(int)gameWindow.Bounds.Width, (int)gameWindow.Bounds.Height);

			OnClientSizeChanged();
		}

		#region Run
		public void Run()
		{
			DisplayProperties.LogicalDpiChanged += delegate
			{
				SetDpiIfNeeded(DisplayProperties.LogicalDpi);
			};
			gameWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 0);
			gameWindow.Activate();

			while (gameHost.ExitRequested == false)
			{
				gameWindow.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessAllIfPresent);
				gameHost.InvokeOnIdle();
			}
		}
		#endregion
		
		#region IFrameworkView Methods
		public void Uninitialize()
		{
		}

		public void Initialize(CoreApplicationView applicationView)
		{
		}

		public void Load(string entryPoint)
		{
		}
		#endregion
	}
}
