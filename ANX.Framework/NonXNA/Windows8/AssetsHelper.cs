#if WINDOWSMETRO
using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.Storage;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.Windows8
{
	public static class AssetsHelper
	{
		private static StorageFolder installLocation;

		static AssetsHelper()
		{
			installLocation = Package.Current.InstalledLocation;
		}

		public static Stream LoadStreamFromAssets(string relativeFilepath)
		{
            relativeFilepath = relativeFilepath.Replace('/', '\\');

            var getFile = installLocation.OpenStreamForReadAsync(relativeFilepath);
            return TaskHelper.WaitForAsyncOperation(getFile);
		}
	}
}
#endif
