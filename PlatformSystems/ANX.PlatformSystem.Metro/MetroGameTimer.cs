using System;
using System.Diagnostics;
using ANX.Framework.NonXNA.PlatformSystem;

namespace ANX.PlatformSystem.Metro
{
	public class MetroGameTimer : INativeGameTimer
	{
		public long Frequency
		{
			get
			{
				return Stopwatch.Frequency;
			}
		}

		public long Timestamp
		{
			get
			{
				return Stopwatch.GetTimestamp();
			}
		}
	}
}
