#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    public class GameComponent : IGameComponent, IUpdateable, IDisposable
    {
        private bool enabled = true;

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    OnEnabledChanged(this, EventArgs.Empty);
                }
            }
        }

        private int updateOrder;

        public int UpdateOrder
        {
            get { return updateOrder; }
            set
            {
                if (updateOrder != value)
                {
                    updateOrder = value;
                    OnUpdateOrderChanged(this, EventArgs.Empty);
                }
            }
        }

        private Game game;

        public Game Game
        {
            get { return game; }
        }

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

        public event EventHandler<EventArgs> Disposed;

        public GameComponent(Game game)
        {
            this.game = game;
        }

        ~GameComponent()
        {
            this.Dispose(false);
        }

        protected virtual void OnEnabledChanged(object sender, EventArgs args)
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(sender, args);
            }
        }

        protected virtual void OnUpdateOrderChanged(object sender, EventArgs args)
        {
            if (UpdateOrderChanged != null)
            {
                UpdateOrderChanged(sender, args);
            }
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Game != null)
                {
                    this.Game.Components.Remove(this);
                }
                if (this.Disposed != null)
                {
                    this.Disposed(this, EventArgs.Empty);
                }
            }
        }
    }
}
