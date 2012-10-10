#region Using Statements
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;
using System;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    //TODO: this class is public on Windows Phone

    [Developer("Glatzemann")]
	internal class GameTimer
	{
		private INativeGameTimer nativeImplementation;
        private TimeSpan lastUpdate;

		public GameTimer()
		{
			nativeImplementation = PlatformSystem.Instance.CreateGameTimer();

            nativeImplementation.Reset();
            lastUpdate = nativeImplementation.ElapsedTime;
        }

        public void Update()
        {
            nativeImplementation.Update();
            lastUpdate = nativeImplementation.ElapsedTime;
        }

        public TimeSpan Elapsed
        {
            get
            {
                return nativeImplementation.ElapsedTime;
            }
        }

		public long Timestamp
		{
			get
			{
				return nativeImplementation.Timestamp;
			}
		}
	}
}
