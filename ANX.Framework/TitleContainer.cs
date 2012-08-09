using System;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	public static class TitleContainer
	{
		private static INativeTitleContainer nativeImplementation;

		static TitleContainer()
		{
			nativeImplementation =
				AddInSystemFactory.DefaultPlatformCreator.CreateTitleContainer();
		}
		
		public static Stream OpenStream(string name)
		{
			return nativeImplementation.OpenStream(name);
		}

		internal static string GetCleanPath(string path)
		{
			return nativeImplementation.GetCleanPath(path);
		}
	}
}
