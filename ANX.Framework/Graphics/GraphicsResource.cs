using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	public abstract class GraphicsResource : IDisposable
	{
		public event EventHandler<EventArgs> Disposing;

		public GraphicsResource()
		{
			GraphicsResourceTracker.Instance.RegisterTrackedObject(this);
		}

		~GraphicsResource()
		{
			GraphicsResourceTracker.Instance.UnregisterTrackedObject(this);
		}

		public GraphicsResource(GraphicsDevice device)
		{
			this.GraphicsDevice = device;
			GraphicsResourceTracker.Instance.RegisterTrackedObject(this);
		}

		public GraphicsDevice GraphicsDevice
		{
			get;
			set;
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

		public abstract void Dispose();

		protected void raise_Disposing(object sender, EventArgs args)
		{
			if (Disposing != null)
			{
				Disposing(sender, args);
			}
		}

		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
		{
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}
