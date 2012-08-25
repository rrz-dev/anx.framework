using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	internal class GameTimer
	{
		private INativeGameTimer nativeImplementation;

		//private long lastTicks;
		//private long frequency;

		public GameTimer()
		{
			nativeImplementation = AddInSystemFactory.DefaultPlatformCreator.CreateGameTimer();

			//lastTicks = nativeImplementation.Timestamp;
			//frequency = nativeImplementation.Frequency;
		}

		//internal TimeSpan Update()
		//{
		//  long newTicks = nativeImplementation.Timestamp;
		//  //long elapseTenthsOfMilliseconds =
		//  //  ((newTicks - lastTicks) * 10000) / frequency;
		//  //float frameTime = (float)(elapseTenthsOfMilliseconds / 10000f);
		//  TimeSpan elapsedUpdate = TimeSpan.FromTicks(newTicks - lastTicks);
		//  lastTicks = newTicks;

		//  return elapsedUpdate;
		//}

		public long Timestamp
		{
			get
			{
				return nativeImplementation.Timestamp;
			}
		}
	}
}
