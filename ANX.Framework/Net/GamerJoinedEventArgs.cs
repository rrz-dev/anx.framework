using System;

namespace ANX.Framework.Net
{
	public class GamerJoinedEventArgs : EventArgs
	{
		public NetworkGamer Gamer
		{
			get;
			private set;
		}

		public GamerJoinedEventArgs(NetworkGamer gamer)
		{
			Gamer = gamer;
		}
	}
}
