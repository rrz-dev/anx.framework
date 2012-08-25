#region Using Statements
using System;
using System.Threading;
using ANX.Framework.Content;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	public class Game : IDisposable
	{
		private IGraphicsDeviceManager graphicsDeviceManager;
		private IGraphicsDeviceService graphicsDeviceService;
		private GameServiceContainer gameServices;
		private bool inRun;
		private bool doneFirstUpdate;
		private GameHost host;
		private bool ShouldExit;

		private GameTimer clock;
		private bool isFixedTimeStep;
		private TimeSpan targetElapsedTime;
		private GameTime gameTime;
		private TimeSpan totalGameTime;
		private long lastUpdate;

		private GameTime gameUpdateTime;
		private TimeSpan inactiveSleepTime;

		private ContentManager content;

		// Events
		public event EventHandler<EventArgs> Activated;
		public event EventHandler<EventArgs> Deactivated;
		public event EventHandler<EventArgs> Disposed;
		public event EventHandler<EventArgs> Exiting;

		public Game()
		{
			Logger.Info("created a new Game-Class");

			this.gameServices = new GameServiceContainer();
			this.gameTime = new GameTime();

			try
			{
				Logger.Info("initializing AddInSystemFactory");
				AddInSystemFactory.Instance.Initialize();
			}
			catch (Exception ex)
			{
				Logger.Error("Error while initializing AddInSystem: " + ex);
				throw new AddInLoadingException("Error while initializing AddInSystem.", ex);
			}

			AddSystemCreator<IInputSystemCreator>();
			AddSystemCreator<ISoundSystemCreator>();
			AddSystemCreator<IPlatformSystemCreator>();
			AddSystemCreator<IRenderSystemCreator>();

			CreateGameHost();

			Logger.Info("creating ContentManager");
			this.content = new ContentManager(this.gameServices);

			Logger.Info("creating GameTimer");
			this.clock = new GameTimer();
			this.isFixedTimeStep = true;
			this.gameUpdateTime = new GameTime();
			this.inactiveSleepTime = TimeSpan.Zero;
			this.targetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60L);  // default is 1/60s 

			Logger.Info("finished initializing new Game class");
		}

		~Game()
		{
			//TODO: implement
		}

		#region CreateGameHost
		private void CreateGameHost()
		{
			Logger.Info("creating GameHost");
			var creator =
				AddInSystemFactory.Instance.GetDefaultCreator<IPlatformSystemCreator>();
			if (creator != null)
			{
				host = creator.CreateGameHost(this);

				host.Activated += HostActivated;
				host.Deactivated += HostDeactivated;
				host.Suspend += HostSuspend;
				host.Resume += HostResume;
				host.Idle += HostIdle;
				host.Exiting += HostExiting;
			}
			else
			{
				Logger.Error("could not fetch PlatformSystem creator to create a game host...");
				throw new NullReferenceException("could not fetch PlatformSystem creator");
			}
		}
		#endregion

		#region AddSystemCreator
		private T AddSystemCreator<T>() where T : class, ICreator
		{
			T creator = AddInSystemFactory.Instance.GetDefaultCreator<T>();
			if (creator != null)
				this.gameServices.AddService(typeof(T), creator);

			return creator;
		}
		#endregion

		protected virtual void Initialize()
		{
			//TODO: implement

			this.LoadContent();
		}

		protected virtual void Update(GameTime gameTime)
		{

		}

		protected virtual void Draw(GameTime gameTime)
		{

		}

		protected virtual void LoadContent()
		{

		}

		protected virtual void UnloadContent()
		{

		}

		protected virtual void BeginRun()
		{

		}

		protected virtual void EndRun()
		{

		}

		public void RunOneFrame()
		{
			throw new NotImplementedException();
		}

		public void SuppressDraw()
		{
			throw new NotImplementedException();
		}

		public void ResetElapsedTime()
		{
			throw new NotImplementedException();
		}

		protected virtual bool BeginDraw()
		{
			if ((this.graphicsDeviceManager != null) && !this.graphicsDeviceManager.BeginDraw())
			{
				return false;
			}
			//Logger.BeginLogEvent(LoggingEvent.Draw, "");
			return true;
		}

		protected virtual void EndDraw()
		{
			if (this.graphicsDeviceManager != null)
			{
				this.graphicsDeviceManager.EndDraw();
			}
		}

		public void Exit()
		{
			this.ShouldExit = true;
			this.host.Exit();
		}

		public void Run()
		{
			this.RunGame();
		}

		public void Tick()
		{
			//TODO: calculation of times is wrong
			//TODO: encapsulate timing stuff in GameTimer class
			TimeSpan elapsedUpdate = TimeSpan.FromTicks(clock.Timestamp - lastUpdate);
			if (isFixedTimeStep)
			{
				while (elapsedUpdate < targetElapsedTime)
				{
					// TODO: search replacement
#if !WINDOWSMETRO
					Thread.Sleep(TargetElapsedTime.Milliseconds - elapsedUpdate.Milliseconds);
#endif
					elapsedUpdate = TimeSpan.FromTicks(clock.Timestamp - elapsedUpdate.Ticks);
				}

				gameUpdateTime.ElapsedGameTime = targetElapsedTime;
				gameUpdateTime.TotalGameTime += elapsedUpdate;
			}
			else
			{
				gameUpdateTime.ElapsedGameTime = elapsedUpdate;
				gameUpdateTime.TotalGameTime += elapsedUpdate;
			}

			//TODO: behaviour of update is wrong (I think): update should be called multiple times if we are behind the time
			//TODO: is update called if minimized?
			lastUpdate = clock.Timestamp;
			Update(gameUpdateTime);

			//elapsedUpdate = TimeSpan.FromTicks(GameTimer.Timestamp - lastUpdate);
			gameUpdateTime.IsRunningSlowly = isFixedTimeStep && elapsedUpdate > targetElapsedTime;

			if (!Window.IsMinimized && BeginDraw())
			{
				//TODO: does draw always get the same time as update or is the time updated in between? (this solution is like in Mono.XNA but it is wrong, I think)
				Draw(gameUpdateTime);

				EndDraw();
			}
		}

		private void RunGame()
		{
			this.graphicsDeviceManager = this.Services.GetService(typeof(IGraphicsDeviceManager)) as IGraphicsDeviceManager;
			if (this.graphicsDeviceManager != null)
			{
				this.graphicsDeviceManager.CreateDevice();
			}
			this.Initialize();
			this.inRun = true;
			this.BeginRun();
			this.gameTime.ElapsedGameTime = TimeSpan.Zero;
			this.gameTime.TotalGameTime = this.totalGameTime;
			this.gameTime.IsRunningSlowly = false;
			this.lastUpdate = clock.Timestamp;
			this.Update(this.gameTime);
			this.doneFirstUpdate = true;
			this.host.Run();
			this.EndRun();
		}

		private void DrawFrame()
		{
			try
			{
				if (((!this.ShouldExit && this.doneFirstUpdate) && !this.Window.IsMinimized) && this.BeginDraw())
				{
					this.gameTime.TotalGameTime = this.totalGameTime;
					//this.gameTime.ElapsedGameTime = this.lastFrameElapsedGameTime;
					//this.gameTime.IsRunningSlowly = this.drawRunningSlowly;
					this.Draw(this.gameTime);
					this.EndDraw();
					//this.doneFirstDraw = true;
				}
			}
			finally
			{
				//this.lastFrameElapsedGameTime = TimeSpan.Zero;
			}
		}

		private void HostActivated(object sender, EventArgs e)
		{
			//TODO: implement

			//if (!this.isActive)
			//{
			//    this.isActive = true;
			//    this.OnActivated(this, EventArgs.Empty);
			//}
		}

		private void HostDeactivated(object sender, EventArgs e)
		{
			//TODO: implement

			//if (this.isActive)
			//{
			//    this.isActive = false;
			//    this.OnDeactivated(this, EventArgs.Empty);
			//}
		}

		private void HostExiting(object sender, EventArgs e)
		{
			//TODO: implement
			//this.OnExiting(this, EventArgs.Empty);
		}

		private void HostIdle(object sender, EventArgs e)
		{
			this.Tick();
		}

		private void HostResume(object sender, EventArgs e)
		{
			//TODO: implement
			//this.clock.Resume();
		}

		private void HostSuspend(object sender, EventArgs e)
		{
			//TODO: implement
			//this.clock.Suspend();
		}

		public GameServiceContainer Services
		{
			get
			{
				return this.gameServices;
			}
		}

		public ContentManager Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		public GraphicsDevice GraphicsDevice
		{
			get
			{
				//TODO: GraphicsDevice property is heavily used. Maybe it is better to hook an event to the services container and
				//      cache the reference to the GraphicsDeviceService to prevent accessing the dictionary of the services container

				IGraphicsDeviceService graphicsDeviceService = this.graphicsDeviceService;
				if (graphicsDeviceService == null)
				{
					graphicsDeviceService = this.Services.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;

					//TODO: exception if null
				}
				return graphicsDeviceService.GraphicsDevice;
			}
		}

		public GameWindow Window
		{
			get
			{
				if (this.host != null)
				{
					return this.host.Window;
				}
				return null;
			}
		}

		public bool IsFixedTimeStep
		{
			get
			{
				return isFixedTimeStep;
			}
			set
			{
				isFixedTimeStep = value;
			}
		}

		public TimeSpan TargetElapsedTime
		{
			get
			{
				return targetElapsedTime;
			}
			set
			{
				targetElapsedTime = value;
			}
		}

		public TimeSpan InactiveSleepTime
		{
			get;
			set;
		}

		public bool IsActive
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		internal bool IsActiveIgnoringGuide
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsMouseVisible
		{
			get;
			set;
		}

		public LaunchParameters LaunchParameters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public GameComponentCollection Components
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void Dispose()
		{
			//TODO: dispose everything to dispose :-)
		}

		protected virtual void Dispose(bool disposing)
		{
			//TODO: dispose everything to dispose :-)
		}

		protected virtual void OnActivated(Object sender, EventArgs args)
		{
			throw new NotImplementedException();
		}

		protected virtual void OnDeactivated(Object sender, EventArgs args)
		{
			throw new NotImplementedException();
		}

		protected virtual void OnExiting(Object sender, EventArgs args)
		{
			throw new NotImplementedException();
		}

		protected virtual bool ShowMissingRequirementMessage(Exception exception)
		{
			throw new NotImplementedException();
		}
	}
}
