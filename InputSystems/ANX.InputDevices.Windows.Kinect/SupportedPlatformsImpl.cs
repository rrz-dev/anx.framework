using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.Kinect
{
	public class SupportedPlatformsImpl : ISupportedPlatforms
	{
		public PlatformName[] Names
		{
			get
			{
				return new PlatformName[]
				{
					PlatformName.WindowsXP,
					PlatformName.WindowsVista,
					PlatformName.Windows7,
					PlatformName.Windows8
				};
			}
		}
	}
}
