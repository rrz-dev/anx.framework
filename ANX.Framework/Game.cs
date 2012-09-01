#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework.Content;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.PlatformSystem;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [PercentageComplete(60)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann")]
	public class Game : IDisposable
    {
        #region Private Members
        private IGraphicsDeviceManager graphicsDeviceManager;
		private IGraphicsDeviceService graphicsDeviceService;
		private GameServiceContainer gameServices;

        private bool firstUpdateDone;
        private bool firstDrawDone;
        private bool drawingSlow;
        private bool inRun;

		private GameHost host;
		private bool ShouldExit;

		private GameTimer gameTimer;
        private TimeSpan gameTimeAccu;
		private GameTime gameTime;
		private TimeSpan totalGameTime;
        private long updatesSinceRunningSlowly1;
        private long updatesSinceRunningSlowly2;
        private bool suppressDraw;

		private GameTime gameUpdateTime;

		private ContentManager content;

        private GameComponentCollection components;
        private List<IGameComponent> drawableGameComponents;

        #endregion

        #region Events
        public event EventHandler<EventArgs> Activated;
		public event EventHandler<EventArgs> Deactivated;
		public event EventHandler<EventArgs> Disposed;
		public event EventHandler<EventArgs> Exiting;

        #endregion

        public Game()
		{
			Logger.Info("created a new Game-Class");

			this.gameServices = new GameServiceContainer();
			this.gameTime = new GameTime();

			try
			{
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
			this.gameTimer = new GameTimer();
			this.IsFixedTimeStep = true;
			this.gameUpdateTime = new GameTime();
            this.InactiveSleepTime = TimeSpan.FromMilliseconds(20.0); 
            this.TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60L);  // default is 1/60s 

            //TODO: implement draw- and update-order handling of GameComponents
            this.components = new GameComponentCollection();
            this.components.ComponentAdded += components_ComponentAdded;
            this.components.ComponentRemoved += components_ComponentRemoved;
            this.drawableGameComponents = new List<IGameComponent>();

			Logger.Info("finished initializing new Game class");

            this.IsActive = true;
		}

		~Game()
		{
            this.components.ComponentAdded -= components_ComponentAdded;
            this.components.ComponentRemoved -= components_ComponentRemoved;

			Dispose(false);
		}

		#region CreateGameHost
		private void CreateGameHost()
		{
			Logger.Info("creating GameHost");
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IPlatformSystemCreator>();
			if (creator == null)
				Logger.ErrorAndThrow<NullReferenceException>("Could not fetch PlatformSystem creator to create a game host.");

			host = creator.CreateGameHost(this);

			host.Activated += HostActivated;
			host.Deactivated += HostDeactivated;
			host.Suspend += HostSuspend;
			host.Resume += HostResume;
			host.Idle += HostIdle;
			host.Exiting += HostExiting;
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
            foreach (IUpdateable updateable in this.components)
            {
                if (updateable.Enabled)
                {
                    updateable.Update(gameTime);
                }
            }
		}

		protected virtual void Draw(GameTime gameTime)
		{
            foreach (IDrawable drawable in this.drawableGameComponents)
            {
                if (drawable.Visible)
                {
                    drawable.Draw(gameTime);
                }
            }
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
            this.suppressDraw = true;
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
            if (this.ShouldExit)
            {
                return;
            }

            // Throttle speed when the game is not active
            if (!this.IsActive)
            {
                ThreadHelper.Sleep(InactiveSleepTime);
            }

            gameTimer.Update();

            bool skipDraw = IsFixedTimeStep ? DoFixedTimeStep(gameTimer.Elapsed) : DoTimeStep(gameTimer.Elapsed);
            this.suppressDraw = false;
            if (skipDraw == false)
            {
                DrawFrame();
            }

		}

        private bool DoFixedTimeStep(TimeSpan time)
        {
            bool skipDraw = false;

            if (Math.Abs(time.Ticks - this.TargetElapsedTime.Ticks) < this.TargetElapsedTime.Ticks >> 6)
            {
                time = this.TargetElapsedTime;
            }

            this.gameTimeAccu += time;
            long updateCount = this.gameTimeAccu.Ticks / this.TargetElapsedTime.Ticks;

            if (updateCount <= 0)
            {
                return false;
            }

            if (updateCount > 1)
            {
                this.updatesSinceRunningSlowly2 = this.updatesSinceRunningSlowly1;
                this.updatesSinceRunningSlowly1 = 0;
            }
            else
            {
                this.updatesSinceRunningSlowly1++;
                this.updatesSinceRunningSlowly2++;
            }

            this.drawingSlow = (this.updatesSinceRunningSlowly2 < 20);

            while (updateCount > 0)
            {
                if (this.ShouldExit)
                {
                    break;
                }

                updateCount -= 1L;

                this.gameTime.ElapsedGameTime = this.TargetElapsedTime;
                this.gameTime.TotalGameTime = this.totalGameTime;
                this.gameTime.IsRunningSlowly = this.drawingSlow;
                this.Update(this.gameTime);
                skipDraw &= this.suppressDraw;
                this.suppressDraw = false;

                this.gameTimeAccu -= this.TargetElapsedTime;
                this.totalGameTime += this.TargetElapsedTime;
            }

            return skipDraw;
        }

        private bool DoTimeStep(TimeSpan time)
        {
            this.gameTime.ElapsedGameTime = time;
            this.gameTime.TotalGameTime = this.totalGameTime;
            this.gameTime.IsRunningSlowly = false;

            this.Update(this.gameTime);

            this.totalGameTime += time;

            return suppressDraw;
        }

        private void RunGame()
		{
			this.graphicsDeviceManager = this.Services.GetService(typeof(IGraphicsDeviceManager)) as IGraphicsDeviceManager;
			if (this.graphicsDeviceManager != null)
				this.graphicsDeviceManager.CreateDevice();

			this.Initialize();
			this.inRun = true;
			this.BeginRun();
			this.gameTime.ElapsedGameTime = TimeSpan.Zero;
			this.gameTime.TotalGameTime = this.totalGameTime;
			this.gameTime.IsRunningSlowly = false;
			this.Update(this.gameTime);
			this.firstUpdateDone = true;
			this.host.Run();
			this.EndRun();
		}

		private void DrawFrame()
		{
            if (!this.ShouldExit)
            {
                if (this.firstUpdateDone)
                {
                    if (!this.Window.IsMinimized)
                    {
                        if (this.BeginDraw())
                        {
                            this.Draw(this.gameTime);
                            this.EndDraw();

                            if (!this.firstDrawDone)
                            {
                                this.firstDrawDone = true;
                            }
                        }
                    }
                }
            }
		}

        #region Public Properties
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
                return (host != null) ? host.Window : null;
			}
		}

		public bool IsFixedTimeStep
		{
			get;
			set;
		}

		public TimeSpan TargetElapsedTime
		{
			get;
			set;
		}

		public TimeSpan InactiveSleepTime
		{
			get;
			set;
		}

		public bool IsActive
		{
			get;
            internal set;
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
            get;
            private set;
        }

        #endregion

        internal bool IsActiveIgnoringGuide
        {
            get
            {
                throw new NotImplementedException();
            }
        }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IDisposable disposable;
				var array = new IGameComponent[components.Count];
				components.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					disposable = (IDisposable)array[i];
					if (disposable != null)
						disposable.Dispose();
				}

				disposable = (IDisposable)graphicsDeviceManager;
				if (disposable != null)
					disposable.Dispose();

				if (Disposed != null)
					Disposed(this, EventArgs.Empty);
			}
		}

        protected virtual bool ShowMissingRequirementMessage(Exception exception)
        {
            throw new NotImplementedException();
        }

        #region Event Handling
        protected virtual void OnActivated(Object sender, EventArgs args)
		{
            RaiseIfNotNull(this.Activated, sender, args);
		}

		protected virtual void OnDeactivated(Object sender, EventArgs args)
		{
            RaiseIfNotNull(this.Deactivated, sender, args);
		}

		protected virtual void OnExiting(Object sender, EventArgs args)
		{
            RaiseIfNotNull(this.Exiting, sender, args);
		}

        private void RaiseIfNotNull(EventHandler<EventArgs> eventDelegate, Object sender, EventArgs args)
        {
            if (eventDelegate != null)
            {
                eventDelegate(sender, args);
            }
        }

        private void components_ComponentRemoved(object sender, GameComponentCollectionEventArgs e)
        {
            if (e.GameComponent is IDrawable)
            {
                drawableGameComponents.Remove(e.GameComponent);
            }
        }

        private void components_ComponentAdded(object sender, GameComponentCollectionEventArgs e)
        {
            if (e.GameComponent is IDrawable)
            {
                drawableGameComponents.Add(e.GameComponent);
            }

            e.GameComponent.Initialize();
        }

        private void HostActivated(object sender, EventArgs e)
        {
            if (!IsActive)
            {
                this.IsActive = true;
                this.OnActivated(this, EventArgs.Empty);
            }
        }

        private void HostDeactivated(object sender, EventArgs e)
        {
            if (IsActive)
            {
                this.IsActive = false;
                this.OnDeactivated(this, EventArgs.Empty);
            }
        }

        private void HostExiting(object sender, EventArgs e)
        {
            ShouldExit = true;

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

        #endregion
    }
}
