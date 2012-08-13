#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using System.IO;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	public class GraphicsDeviceManager : IGraphicsDeviceManager, IDisposable, IGraphicsDeviceService
	{
		#region Private Members
		private Game game;
		private GraphicsDevice graphicsDevice;
		private int backBufferWidth = DefaultBackBufferWidth;
		private int backBufferHeight = DefaultBackBufferHeight;
		private SurfaceFormat backBufferFormat = SurfaceFormat.Color;
		private DepthFormat depthStencilFormat = DepthFormat.Depth24;
		private GraphicsProfile graphicsProfile;
		private bool isFullScreen;
		private bool multiSampling;
		private GraphicsDeviceInformation currentGraphicsDeviceInformation;

		#endregion // Private Members

		public static readonly int DefaultBackBufferWidth = 800;
		public static readonly int DefaultBackBufferHeight = 480;

		public event EventHandler<EventArgs> Disposed;
		public event EventHandler<EventArgs> DeviceCreated;
		public event EventHandler<EventArgs> DeviceDisposing;
		public event EventHandler<EventArgs> DeviceReset;
		public event EventHandler<EventArgs> DeviceResetting;
		public event EventHandler<PreparingDeviceSettingsEventArgs> PreparingDeviceSettings;

		public GraphicsDeviceManager(Game game)
		{
			if (game == null)
			{
				throw new ArgumentNullException("game");
			}

			this.game = game;

			if (game.Services.GetService(typeof(IGraphicsDeviceManager)) != null)
			{
				throw new ArgumentException("The GraphicsDeviceManager was already registered to the game class");
			}
			game.Services.AddService(typeof(IGraphicsDeviceManager), this);

			if (game.Services.GetService(typeof(IGraphicsDeviceService)) != null)
			{
				throw new ArgumentException("The GraphicsDeviceService was already registered to the game class");
			}
			game.Services.AddService(typeof(IGraphicsDeviceService), this);

			game.Window.ClientSizeChanged += Window_ClientSizeChanged;
			game.Window.ScreenDeviceNameChanged += Window_ScreenDeviceNameChanged;
			game.Window.OrientationChanged += Window_OrientationChanged;

			this.graphicsProfile = FetchGraphicsProfile();
		}

		void Window_OrientationChanged(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		void Window_ScreenDeviceNameChanged(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		void Window_ClientSizeChanged(object sender, EventArgs e)
		{
			// TODO: validate
			var parameters = graphicsDevice.PresentationParameters;
			parameters.BackBufferWidth = game.Window.ClientBounds.Width;
			parameters.BackBufferHeight = game.Window.ClientBounds.Height;
			graphicsDevice.Reset(parameters);
		}

		public bool BeginDraw()
		{
			return true;
		}

		public void CreateDevice()
		{
			ApplyChanges();
		}

		private void CreateDevice(GraphicsDeviceInformation deviceInformation)
		{
			if (this.graphicsDevice != null)
			{
				System.Diagnostics.Debugger.Break(); // Test this!!!
				this.graphicsDevice.Dispose();
				this.graphicsDevice = null;
			}

			//TODO: validate graphics device

			this.graphicsDevice = new GraphicsDevice(deviceInformation.Adapter, deviceInformation.GraphicsProfile, deviceInformation.PresentationParameters);
			GraphicsResourceTracker.Instance.UpdateGraphicsDeviceReference(this.graphicsDevice);

			//TODO: hookup events
			this.graphicsDevice.DeviceResetting += graphicsDevice_DeviceResetting;
			this.graphicsDevice.DeviceReset += graphicsDevice_DeviceReset;

			OnDeviceCreated(this, EventArgs.Empty);
		}

		void graphicsDevice_DeviceReset(object sender, EventArgs e)
		{
			OnDeviceReset(this, EventArgs.Empty);
		}

		void graphicsDevice_DeviceResetting(object sender, EventArgs e)
		{
			OnDeviceResetting(this, EventArgs.Empty);
		}

		public void EndDraw()
		{
			this.graphicsDevice.Present();
		}

		public void ApplyChanges()
		{
			GraphicsDeviceInformation graphicsDeviceInformation = FindBestDevice(true);
			OnPreparingDeviceSettings(this, new PreparingDeviceSettingsEventArgs(graphicsDeviceInformation));

			if (graphicsDevice != null)
			{
				if (this.CanResetDevice(graphicsDeviceInformation))
				{
					OnDeviceResetting(this, EventArgs.Empty);

					this.graphicsDevice.Reset(graphicsDeviceInformation.PresentationParameters, graphicsDeviceInformation.Adapter);

					OnDeviceReset(this, EventArgs.Empty);
				}
				else
				{
					//graphicsDevice.Dispose();
					//graphicsDevice = null;
					// Dispose could not be used here because all references to graphicsDevice get dirty!

					graphicsDevice.Recreate(graphicsDeviceInformation.PresentationParameters);
				}
			}

			if (graphicsDevice == null)
			{
				CreateDevice(graphicsDeviceInformation);
			}

			this.currentGraphicsDeviceInformation = graphicsDeviceInformation;
		}

		public void ToggleFullScreen()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		protected virtual void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		protected GraphicsDeviceInformation FindBestDevice(bool anySuitableDevice)
		{
			//TODO: implement FindBestDevice

			GraphicsDeviceInformation deviceInformation = new GraphicsDeviceInformation();

			deviceInformation.PresentationParameters.DeviceWindowHandle = game.Window.Handle;
			deviceInformation.PresentationParameters.BackBufferFormat = backBufferFormat;
			deviceInformation.PresentationParameters.BackBufferWidth = backBufferWidth;
			deviceInformation.PresentationParameters.BackBufferHeight = backBufferHeight;
			deviceInformation.PresentationParameters.DepthStencilFormat = depthStencilFormat;

			return deviceInformation;
		}

		protected virtual bool CanResetDevice(GraphicsDeviceInformation newDeviceInfo)
		{
			if (newDeviceInfo.Adapter == currentGraphicsDeviceInformation.Adapter &&
					newDeviceInfo.GraphicsProfile == currentGraphicsDeviceInformation.GraphicsProfile &&
				//                newDeviceInfo.PresentationParameters.DepthStencilFormat == currentGraphicsDeviceInformation.PresentationParameters.DepthStencilFormat &&
					newDeviceInfo.PresentationParameters.BackBufferFormat == currentGraphicsDeviceInformation.PresentationParameters.BackBufferFormat)
			{
				return true;
			}

			//TODO: implement CanResetDevice completly

			return false;
		}

		protected virtual void RankDevices(List<GraphicsDeviceInformation> foundDevices)
		{
			throw new NotImplementedException();
		}

		protected virtual void OnDeviceCreated(Object sender, EventArgs args)
		{
			if (DeviceCreated != null)
			{
				DeviceCreated(sender, args);
			}
		}

		protected virtual void OnDeviceDisposing(Object sender, EventArgs args)
		{
			if (DeviceDisposing != null)
			{
				DeviceDisposing(sender, args);
			}
		}

		protected virtual void OnDeviceReset(Object sender, EventArgs args)
		{
			if (DeviceReset != null)
			{
				DeviceReset(sender, args);
			}
		}

		protected virtual void OnDeviceResetting(Object sender, EventArgs args)
		{
			if (DeviceResetting != null)
			{
				DeviceResetting(sender, args);
			}
		}

		protected virtual void OnPreparingDeviceSettings(Object sender, PreparingDeviceSettingsEventArgs args)
		{
			if (PreparingDeviceSettings != null)
			{
				PreparingDeviceSettings(sender, args);
			}
		}

		public GraphicsDevice GraphicsDevice
		{
			get { return this.graphicsDevice; }
		}

		public GraphicsProfile GraphicsProfile
		{
			get
			{
				return this.currentGraphicsDeviceInformation.GraphicsProfile;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public DepthFormat PreferredDepthStencilFormat
		{
			get
			{
				return this.currentGraphicsDeviceInformation.PresentationParameters.DepthStencilFormat;
			}
			set
			{
				this.depthStencilFormat = value;
			}
		}

		public SurfaceFormat PreferredBackBufferFormat
		{
			get
			{
				return this.currentGraphicsDeviceInformation.PresentationParameters.BackBufferFormat;
			}
			set
			{
				this.backBufferFormat = value;
			}
		}

		public int PreferredBackBufferWidth
		{
			get
			{
				return this.currentGraphicsDeviceInformation.PresentationParameters.BackBufferWidth;
			}
			set
			{
				this.backBufferWidth = value;
			}
		}

		public int PreferredBackBufferHeight
		{
			get
			{
				return this.currentGraphicsDeviceInformation.PresentationParameters.BackBufferHeight;
			}
			set
			{
				this.backBufferHeight = value;
			}
		}

		public bool IsFullScreen
		{
			get
			{
				return this.currentGraphicsDeviceInformation.PresentationParameters.IsFullScreen;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public bool SynchronizeWithVerticalRetrace
		{
			get
			{
				if (graphicsDevice != null && graphicsDevice.NativeDevice != null)
				{
					return graphicsDevice.NativeDevice.VSync;
				}

				return true;
			}
			set
			{
				graphicsDevice.NativeDevice.VSync = value;
			}
		}

		public bool PreferMultiSampling
		{
			get
			{
				return this.currentGraphicsDeviceInformation.PresentationParameters.MultiSampleCount > 0;
			}
			set { throw new NotImplementedException(); }
		}

		public DisplayOrientation SupportedOrientations
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		private GraphicsProfile FetchGraphicsProfile()
		{
			Stream manifestResourceStream = ManifestHelper.GetManifestResourceStream(
				game, "Microsoft.Xna.Framework.RuntimeProfile");

			if (manifestResourceStream != null)
			{
				using (StreamReader reader = new StreamReader(manifestResourceStream))
				{
					if (reader.ReadToEnd().Contains("HiDef"))
					{
						return GraphicsProfile.HiDef;
					}
				}
			}

			return GraphicsProfile.Reach;
		}
	}
}
