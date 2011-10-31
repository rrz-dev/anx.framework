#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ANX.Framework.Content;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

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

        //TODO: The constructor should be overloaded unlike the original XNA. There will be one argument which is used to
        //      try to load a specified environment (DX9, DX10, DX11, OpenGL). The default constructor will decide which
        //      is the best (or only) environment to load. The default environment should behave mostly like XNA.

        public Game()
            : this("DirectX10", "XInput", "XAudio")
        {
        }

        public Game(String renderSystemName = "DirectX10", String inputSystemName = "XInput", String soundSystemName = "XAudio")
        {
            this.gameServices = new GameServiceContainer();
            this.gameTime = new GameTime();

            AddInSystemFactory.Instance.Initialize();

            AddInSystemFactory.Instance.SetCurrentCreator(inputSystemName);
            IInputSystemCreator inputSystemCreator = AddInSystemFactory.Instance.GetCurrentCreator<IInputSystemCreator>();
            if (inputSystemCreator != null)
            {
                this.gameServices.AddService(typeof(IInputSystemCreator), inputSystemCreator);
            }

            AddInSystemFactory.Instance.SetCurrentCreator(soundSystemName);
            ISoundSystemCreator soundSystemCreator = AddInSystemFactory.Instance.GetCurrentCreator<ISoundSystemCreator>();
            if (soundSystemCreator != null)
            {
                this.gameServices.AddService(typeof(ISoundSystemCreator), soundSystemCreator);
            }

            AddInSystemFactory.Instance.SetCurrentCreator(renderSystemName);
            IRenderSystemCreator renderSystemCreator = AddInSystemFactory.Instance.GetCurrentCreator<IRenderSystemCreator>();
            if (renderSystemCreator != null)
            {
                this.gameServices.AddService(typeof(IRenderSystemCreator), renderSystemCreator);
            }

            //TODO: error handling if creator is null
            this.host = AddInSystemFactory.Instance.GetCurrentCreator<IRenderSystemCreator>().CreateGameHost(this);

            this.host.Activated += new EventHandler<EventArgs>(this.HostActivated);
            this.host.Deactivated += new EventHandler<EventArgs>(this.HostDeactivated);
            this.host.Suspend += new EventHandler<EventArgs>(this.HostSuspend);
            this.host.Resume += new EventHandler<EventArgs>(this.HostResume);
            this.host.Idle += new EventHandler<EventArgs>(this.HostIdle);
            this.host.Exiting += new EventHandler<EventArgs>(this.HostExiting);

            this.content = new ContentManager(this.gameServices);

            this.clock = new GameTimer();
            this.isFixedTimeStep = true;
            this.gameUpdateTime = new GameTime();
            this.inactiveSleepTime = TimeSpan.Zero;
            this.targetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60L);  // default is 1/60s 
        }

        ~Game()
        {
            //TODO: implement
        }

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
            TimeSpan elapsedUpdate = TimeSpan.FromTicks(GameTimer.Timestamp - lastUpdate);
            if (isFixedTimeStep)
            {
                while (elapsedUpdate < targetElapsedTime)
                {
                    Thread.Sleep(TargetElapsedTime.Milliseconds - elapsedUpdate.Milliseconds);
                    elapsedUpdate = TimeSpan.FromTicks(GameTimer.Timestamp - elapsedUpdate.Ticks);
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
            lastUpdate = GameTimer.Timestamp;
            Update(gameUpdateTime);

            elapsedUpdate = TimeSpan.FromTicks(GameTimer.Timestamp - lastUpdate);
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
            this.lastUpdate = GameTimer.Timestamp;
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
