using System.IO;
using System.Reflection;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.PsVita
{
	public class PsVitaContentManager : INativeContentManager
	{
		#region MakeRootDirectoryAbsolute
		public string MakeRootDirectoryAbsolute(string relativePath)
		{
			var location = Assembly.GetEntryAssembly().Location;
			var assemblyFile = new FileInfo(location);
			return Path.Combine(assemblyFile.Directory.FullName, relativePath);
		}
		#endregion

		#region OpenStream
		public Stream OpenStream(string filepath)
		{
			return new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
		}
		#endregion
	}
}
