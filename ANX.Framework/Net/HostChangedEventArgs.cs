using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
	public class HostChangedEventArgs : EventArgs
	{
		public NetworkGamer OldHost
		{
			get;
			private set;
		}

		public NetworkGamer NewHost
		{
			get;
			private set;
		}

		public HostChangedEventArgs(NetworkGamer oldHost, NetworkGamer newHost)
		{
			OldHost = oldHost;
			NewHost = newHost;
		}
	}
}
