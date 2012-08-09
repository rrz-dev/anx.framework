using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public static class OSInformation
	{
		#region IsWindows
		public static bool IsWindows
		{
			get
			{
				PlatformName name = GetName();
				return name == PlatformName.Windows8 ||
					name == PlatformName.Windows7 ||
					name == PlatformName.WindowsVista ||
					name == PlatformName.WindowsXP;
			}
		}
		#endregion

		#region GetName
		public static PlatformName GetName()
		{
#if WINDOWSMETRO
			return PlatformName.Windows8;
#elif PSVITA
			return PlatformName.PSVita;
#elif ANDROID
			return PlatformName.Android;
#elif IOS
			return PlatformName.IOS;
#elif LINUX
			return PlatformName.Linux;
#elif MACOSX
			return PlatformName.MacOSX;
#else
			return DetermineEnvironmentPlatformName();
#endif
		}
		#endregion

		#region GetVersion
		public static Version GetVersion()
		{
#if WINDOWSMETRO
			// TODO: RuntimeEnvironment doesn't exist any more??
			//return new Version(RuntimeEnvironment.GetSystemVersion());
			return new Version(-1, 0);
#else
			return Environment.OSVersion.Version;
#endif
		}
		#endregion

		#region GetVersionString
		public static string GetVersionString()
		{
#if WINDOWSMETRO
			return "Win8";
#else
			return Environment.OSVersion.VersionString;
#endif
		}
		#endregion

		#region DetermineEnvironmentPlatformName
#if !WINDOWSMETRO
		private static PlatformName DetermineEnvironmentPlatformName()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					return PlatformName.MacOSX;

				case PlatformID.Unix:
					return PlatformName.Linux;

				case PlatformID.Win32NT:
					Version osVersion = GetVersion();
					if (osVersion.Major >= 6)
					{
						if (osVersion.Minor == 0)
						{
							return PlatformName.WindowsVista;
						}

						return PlatformName.Windows7;
					}

					return PlatformName.WindowsXP;

				default:
					return PlatformName.Windows7;
			}
		}
#endif
		#endregion
	}
}
