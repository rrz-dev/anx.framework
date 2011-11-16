using System;

namespace ANX.Framework.Net
{
	public class NetworkSessionEndedEventArgs : EventArgs
	{
		public NetworkSessionEndReason EndReason
		{
			get;
			private set;
		}

		public NetworkSessionEndedEventArgs(NetworkSessionEndReason endReason)
		{
			EndReason = endReason;
		}
	}
}
