using System;

namespace ANX.Framework.Net
{
	public sealed class WriteLeaderboardsEventArgs : EventArgs
	{
		public NetworkGamer Gamer
		{
			get;
			internal set;
		}

		public bool IsLeaving
		{
			get;
			internal set;
		}
	}
}
