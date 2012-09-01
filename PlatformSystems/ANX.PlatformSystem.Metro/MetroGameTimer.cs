#region Using Statements
using System;
using System.Diagnostics;
using ANX.Framework.NonXNA.PlatformSystem;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
	public class MetroGameTimer : INativeGameTimer
	{
        #region Using Statements
        private Stopwatch stopwatch;
        private TimeSpan lastElapsed;

        #endregion

        public MetroGameTimer()
        {
            if (!Stopwatch.IsHighResolution)
            {
                Logger.Warning("Created " + this.GetType().FullName + ", but it is not high resolution. Maybe the underlying platform doesn't support high resolution timers?");
            }
            stopwatch = Stopwatch.StartNew();
            Reset();
        }

        public void Update()
        {
            TimeSpan elapsed = stopwatch.Elapsed;
            ElapsedTime = elapsed - lastElapsed;
            lastElapsed = elapsed;
        }

        public void Reset()
        {
            stopwatch.Restart();
            lastElapsed = stopwatch.Elapsed;
        }

        public void Suspend()
        {
            stopwatch.Stop();
        }

        public void Resume()
        {
            stopwatch.Start();
        }

        public TimeSpan ElapsedTime
        {
            get;
            internal set;
        }

        public TimeSpan CurrentTime
        {
            get;
            internal set;
        }

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
