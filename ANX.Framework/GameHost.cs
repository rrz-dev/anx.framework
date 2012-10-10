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
	public abstract class GameHost
	{
		internal event EventHandler<EventArgs> Activated;
		internal event EventHandler<EventArgs> Deactivated;
		internal event EventHandler<EventArgs> Exiting;
		internal event EventHandler<EventArgs> Idle;
		internal event EventHandler<EventArgs> Resume;
		internal event EventHandler<EventArgs> Suspend;

		public abstract GameWindow Window { get; }

		public GameHost(Game game)
		{
		}

		public abstract void Run();
		public abstract void Exit();

		protected void OnActivated()
		{
			InvokeIfNotNull(this.Activated);
		}

		protected void OnDeactivated()
		{
			InvokeIfNotNull(this.Deactivated);
		}

		protected void OnIdle()
		{
			InvokeIfNotNull(this.Idle);
		}

		protected void OnExiting()
		{
			InvokeIfNotNull(this.Exiting);
		}

		private void InvokeIfNotNull(EventHandler<EventArgs> eventHandler)
		{
			if (eventHandler != null)
				eventHandler(this, EventArgs.Empty);
		}
	}
}
