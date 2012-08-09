using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
	public class GamerLeftEventArgs : EventArgs
	{
		public NetworkGamer Gamer
		{
			get;
			private set;
		}

		public GamerLeftEventArgs(NetworkGamer gamer)
		{
			Gamer = gamer;
		}
	}
}
