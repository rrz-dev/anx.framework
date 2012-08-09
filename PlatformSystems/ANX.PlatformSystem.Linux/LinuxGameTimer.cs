using System;
using ANX.Framework.NonXNA.PlatformSystem;
using System.Diagnostics;

namespace ANX.PlatformSystem.Linux
{
	public class LinuxGameTimer : INativeGameTimer
	{
		#region Public
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

		#endregion
	}
}
