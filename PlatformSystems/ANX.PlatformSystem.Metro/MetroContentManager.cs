using System.IO;
using ANX.Framework;
using ANX.Framework.NonXNA.PlatformSystem;
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
			filepath = filepath.Replace("/", "\\");
			Stream filestream = LoadStreamFromMetroAssets(filepath);

			// TODO: this copy is really inefficient!!
			// Find out why reading from the asset stream causes
			// the position property to go crazy :/
			MemoryStream stream = new MemoryStream();
			filestream.CopyTo(stream);
			filestream.Dispose();
			filestream = null;

			stream.Position = 0;
			return stream;
		}
		#endregion

		#region LoadStreamFromMetroAssets
		private Stream LoadStreamFromMetroAssets(string filepath)
		{
			try
			{
				var task = installLocation.OpenStreamForReadAsync(filepath);
				return TaskHelper.WaitForAsyncOperation(task);
			}
			catch
			{
			}

			return null;
		}
		#endregion
	}
}
