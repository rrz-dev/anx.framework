using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public abstract class GraphicsResource : IDisposable
	{
		public event EventHandler<EventArgs> Disposing;

        internal GraphicsResource()
        {
            GraphicsResourceTracker.Instance.RegisterTrackedObject(this);
        }

		public GraphicsResource(GraphicsDevice device)
            : this()
		{
			this.GraphicsDevice = device;
		}

		public GraphicsDevice GraphicsDevice
		{
			get;
			internal set;
		}

		public bool IsDisposed
		{
			get;
			internal set;
		}

		public string Name
		{
			get;
			set;
		}

		public object Tag
		{
			get;
			set;
		}

		public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

		protected void raise_Disposing(object sender, EventArgs args)
		{
			if (Disposing != null)
			{
				Disposing(sender, args);
			}
		}

		protected virtual void Dispose(bool disposeManaged)
		{
            if (disposeManaged)
            {
                raise_Disposing(this, EventArgs.Empty);

                GraphicsResourceTracker.Instance.UnregisterTrackedObject(this);
            }
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}
