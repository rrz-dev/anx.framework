using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	[PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann")]
	public class GameTime
	{
		#region Public
		public TimeSpan ElapsedGameTime
		{
			get;
			internal set;
		}

		public TimeSpan TotalGameTime
		{
			get;
			internal set;
		}

		public bool IsRunningSlowly
		{
			get;
			internal set;
		}
		#endregion

		#region Constructor
		public GameTime()
		{
		}

		public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime)
		{
			this.TotalGameTime = totalGameTime;
			this.ElapsedGameTime = elapsedGameTime;
            this.IsRunningSlowly = false;
		}

		public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime, bool isRunningSlowly)
		{
			this.TotalGameTime = totalGameTime;
			this.ElapsedGameTime = elapsedGameTime;
			this.IsRunningSlowly = isRunningSlowly;
		}
		#endregion
	}
}
