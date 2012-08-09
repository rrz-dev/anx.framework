using System;
using ANX.Framework.NonXNA.PlatformSystem;
using System.Diagnostics;

namespace ANX.PlatformSystem.Windows
{
	public class WindowsGameTimer : INativeGameTimer
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
