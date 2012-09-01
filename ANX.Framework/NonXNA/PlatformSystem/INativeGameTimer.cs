using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.PlatformSystem
{
	public interface INativeGameTimer
	{
        void Update();
        void Reset();
        void Suspend();
        void Resume();

        TimeSpan CurrentTime
        {
            get;
        }

        TimeSpan ElapsedTime
        {
            get;
        }

		long Frequency
		{
			get;
		}

		long Timestamp
		{
			get;
		}
	}
}
