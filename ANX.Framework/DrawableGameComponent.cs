#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    public class DrawableGameComponent : GameComponent, IDrawable
    {
        private bool isInitialized;
        private IGraphicsDeviceService device;

        public GraphicsDevice GraphicsDevice
        {
            get
            {
                if (this.device == null)
                {
                    throw new InvalidOperationException("Component is not initialized");
                }
                return this.device.GraphicsDevice;
            }
        }

        private int drawOrder;

        public int DrawOrder
        {
            get { return drawOrder; }
            set
            {
                if (drawOrder != value)
                {
                    drawOrder = value;
                    OnDrawOrderChanged(this, EventArgs.Empty);
                }
            }
        }

        private bool visible = true;

        public bool Visible
        {
            get { return visible; }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    OnVisibleChanged(this, EventArgs.Empty);
                }
            }
        }
        
        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        public DrawableGameComponent(Game game)
            : base(game)
        {
        }

        protected virtual void OnDrawOrderChanged(object sender, EventArgs arg)
        {
            if (DrawOrderChanged != null)
            {
                DrawOrderChanged(sender, arg);
            }
        }

        protected virtual void OnVisibleChanged(object sender, EventArgs arg)
        {
            if (VisibleChanged != null)
            {
                VisibleChanged(sender, arg);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            if (!isInitialized)
            {
                this.device = base.Game.Services.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
                if (this.device == null)
                {
                    throw new InvalidOperationException("Service not found: IGraphicsDeviceService");
                }
                this.device.DeviceCreated += OnDeviceCreated;
                this.device.DeviceDisposing += OnDeviceDisposing;
            }
            isInitialized = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.UnloadContent();
                if (this.device != null)
                {
                    this.device.DeviceCreated -= OnDeviceCreated;
                    this.device.DeviceDisposing -= OnDeviceDisposing;
                }
            }
            base.Dispose(disposing);
        }

        protected virtual void LoadContent()
        {
        }

        protected virtual void UnloadContent()
        {
        }

        public void Draw(GameTime gameTime)
        {
            
        }

        private void OnDeviceCreated(object sender, EventArgs arg)
        {
            this.LoadContent();
        }

        private void OnDeviceDisposing(object sender, EventArgs arg)
        {
            this.UnloadContent();
        }
    }
}
