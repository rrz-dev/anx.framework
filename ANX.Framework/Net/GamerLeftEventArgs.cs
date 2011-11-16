using System;

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
