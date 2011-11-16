using System;

namespace ANX.Framework.Net
{
	public sealed class QualityOfService
	{
		public bool IsAvailable
		{
			get;
			internal set;
		}

		public int BytesPerSecondUpstream
		{
			get;
			internal set;
		}

		public int BytesPerSecondDownstream
		{
			get;
			internal set;
		}

		public TimeSpan AverageRoundtripTime
		{
			get;
			internal set;
		}

		public TimeSpan MinimumRoundtripTime
		{
			get;
			internal set;
		}
	}
}
