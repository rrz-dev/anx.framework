#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [PercentageComplete(60)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public class GraphicsDeviceManager : IGraphicsDeviceManager, IDisposable, IGraphicsDeviceService
	{
		#region Constants
		public static readonly int DefaultBackBufferWidth = 800;
		public static readonly int DefaultBackBufferHeight = 480;
		private const string RuntimeProfileResourceName = "Microsoft.Xna.Framework.RuntimeProfile";
		#endregion

		#region Private
		private Game game;
		private GraphicsDevice graphicsDevice;
		private int backBufferWidth = DefaultBackBufferWidth;
		private int backBufferHeight = DefaultBackBufferHeight;
		private SurfaceFormat backBufferFormat = SurfaceFormat.Color;
		private DepthFormat depthStencilFormat = DepthFormat.Depth24;
		private GraphicsProfile graphicsProfile;
		private GraphicsDeviceInformation currentGraphicsDeviceInformation;
		private bool synchronizeWithVerticalRetrace = true;

		#endregion

		#region Events
		public event EventHandler<EventArgs> Disposed;
		public event EventHandler<EventArgs> DeviceCreated;
		public event EventHandler<EventArgs> DeviceDisposing;
		public event EventHandler<EventArgs> DeviceReset;
		public event EventHandler<EventArgs> DeviceResetting;
		public event EventHandler<PreparingDeviceSettingsEventArgs> PreparingDeviceSettings;
		#endregion

		#region Public
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
				return (graphicsDevice != null && graphicsDevice.NativeDevice != null) ?
					graphicsDevice.NativeDevice.VSync :
					synchronizeWithVerticalRetrace;
			}
			set
			{
				synchronizeWithVerticalRetrace = value;
				if (graphicsDevice != null && graphicsDevice.NativeDevice != null)
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
		#endregion

		#region Constructor
		public GraphicsDeviceManager(Game game)
		{
			if (game == null)
				throw new ArgumentNullException("game");
			this.game = game;

			if (game.Services.GetService(typeof(IGraphicsDeviceManager)) != null)
				throw new ArgumentException("The GraphicsDeviceManager was already registered to the game class");
			game.Services.AddService(typeof(IGraphicsDeviceManager), this);

			if (game.Services.GetService(typeof(IGraphicsDeviceService)) != null)
				throw new ArgumentException("The GraphicsDeviceService was already registered to the game class");
			game.Services.AddService(typeof(IGraphicsDeviceService), this);

			game.Window.ClientSizeChanged += Window_ClientSizeChanged;
			game.Window.ScreenDeviceNameChanged += Window_ScreenDeviceNameChanged;
			game.Window.OrientationChanged += Window_OrientationChanged;

			this.graphicsProfile = FetchGraphicsProfile();
		}
		#endregion

		#region BeginDraw
		public bool BeginDraw()
		{
			return true;
		}
		#endregion

		#region CreateDevice
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

			this.graphicsDevice = new GraphicsDevice(deviceInformation.Adapter, deviceInformation.GraphicsProfile,deviceInformation.PresentationParameters);
			GraphicsResourceTracker.Instance.UpdateGraphicsDeviceReference(this.graphicsDevice);

			//TODO: hookup events
			this.graphicsDevice.DeviceResetting += OnDeviceResetting;
			this.graphicsDevice.DeviceReset += OnDeviceReset;

			// Update the vsync value in case it was set before creation of the device
			SynchronizeWithVerticalRetrace = synchronizeWithVerticalRetrace;

			OnDeviceCreated(this, EventArgs.Empty);
		}
		#endregion

		#region EndDraw
		public void EndDraw()
		{
			this.graphicsDevice.Present();
		}
		#endregion

		#region ApplyChanges
		public void ApplyChanges()
		{
			GraphicsDeviceInformation graphicsDeviceInformation = FindBestDevice(true);
			OnPreparingDeviceSettings(this, new PreparingDeviceSettingsEventArgs(graphicsDeviceInformation));

			if (graphicsDevice != null)
			{
				if (this.CanResetDevice(graphicsDeviceInformation))
				{
					OnDeviceResetting(this, EventArgs.Empty);

					this.graphicsDevice.Reset(graphicsDeviceInformation.PresentationParameters,
						graphicsDeviceInformation.Adapter);

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
				CreateDevice(graphicsDeviceInformation);

			this.currentGraphicsDeviceInformation = graphicsDeviceInformation;
		}
		#endregion

		#region ApplyChanges
		public void ToggleFullScreen()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (game != null && game.Services.GetService(typeof(IGraphicsDeviceService)) == this)
					game.Services.RemoveService(typeof(IGraphicsDeviceService));

				if (graphicsDevice != null)
				{
					graphicsDevice.Dispose();
					graphicsDevice = null;
				}

				if (Disposed != null)
					Disposed(this, EventArgs.Empty);
			}
		}
		#endregion

		#region FindBestDevice
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
		#endregion

		#region CanResetDevice
		protected virtual bool CanResetDevice(GraphicsDeviceInformation newDeviceInfo)
		{
			return GraphicsDevice.GraphicsProfile == newDeviceInfo.GraphicsProfile;
		}
		#endregion

		protected virtual void RankDevices(List<GraphicsDeviceInformation> foundDevices)
		{
			throw new NotImplementedException();
		}

		private void Window_OrientationChanged(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Window_ScreenDeviceNameChanged(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Window_ClientSizeChanged(object sender, EventArgs e)
		{
			// TODO: validate
			var parameters = graphicsDevice.PresentationParameters;
			parameters.BackBufferWidth = game.Window.ClientBounds.Width;
			parameters.BackBufferHeight = game.Window.ClientBounds.Height;
			graphicsDevice.Reset(parameters);
		}

		protected virtual void OnDeviceCreated(object sender, EventArgs args)
		{
			RaiseEventIfNotNull(DeviceCreated, sender, args);
		}

		protected virtual void OnDeviceDisposing(object sender, EventArgs args)
		{
			RaiseEventIfNotNull(DeviceDisposing, sender, args);
		}

		protected virtual void OnDeviceReset(object sender, EventArgs args)
		{
			RaiseEventIfNotNull(DeviceReset, sender, args);
		}

		protected virtual void OnDeviceResetting(object sender, EventArgs args)
		{
			RaiseEventIfNotNull(DeviceResetting, sender, args);
		}

		protected virtual void OnPreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs args)
		{
			RaiseEventIfNotNull(PreparingDeviceSettings, sender, args);
		}

		#region RaiseEventIfNotNull
		private void RaiseEventIfNotNull<T>(EventHandler<T> handler, object sender, T args) where T : EventArgs
		{
			if (handler != null)
				handler(sender, args);
		}
		#endregion

		#region FetchGraphicsProfile
		private GraphicsProfile FetchGraphicsProfile()
		{
			var result = GraphicsProfile.Reach;
			Stream manifestResourceStream = ManifestHelper.GetManifestResourceStream(game, RuntimeProfileResourceName);
			if (manifestResourceStream == null)
				return result;

			using (StreamReader reader = new StreamReader(manifestResourceStream))
				if (reader.ReadToEnd().Contains("HiDef"))
					result = GraphicsProfile.HiDef;

			return result;
		}
		#endregion
	}
}
