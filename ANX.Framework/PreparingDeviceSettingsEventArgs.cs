using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	[PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("???")]
	public class PreparingDeviceSettingsEventArgs : EventArgs
	{
		public GraphicsDeviceInformation GraphicsDeviceInformation
		{
			get;
			private set;
		}

		public PreparingDeviceSettingsEventArgs(GraphicsDeviceInformation graphicsDeviceInformation)
		{
			GraphicsDeviceInformation = graphicsDeviceInformation;
		}
	}
}
