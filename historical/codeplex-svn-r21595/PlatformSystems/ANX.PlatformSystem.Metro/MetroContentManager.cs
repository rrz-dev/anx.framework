using System.IO;
using ANX.Framework;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.NonXNA.Windows8;
using Windows.ApplicationModel;
using Windows.Storage;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
	public class MetroContentManager : INativeContentManager
	{
		#region Private
		private StorageFolder installLocation;
		#endregion

		#region Constructor
		public MetroContentManager()
		{
			installLocation = Package.Current.InstalledLocation;
		}
		#endregion

		#region MakeRootDirectoryAbsolute
		public string MakeRootDirectoryAbsolute(string relativePath)
		{
			return Path.Combine("Assets", relativePath);
		}
		#endregion

		#region OpenStream
		public Stream OpenStream(string filepath)
		{
			return AssetsHelper.LoadStreamFromAssets(filepath);
		}
		#endregion
	}
}
