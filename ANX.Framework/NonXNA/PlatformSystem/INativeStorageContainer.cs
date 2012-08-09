using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.PlatformSystem
{
	public interface INativeStorageContainer
	{
		void CreateDirectory(string directory);

		Stream CreateFile(string file);

		void DeleteDirectory(string directory);

		void DeleteFile(string file);

		bool DirectoryExists(string directory);

		bool FileExists(string file);

		string[] GetDirectoryNames(string searchPattern);

		string[] GetFileNames(string searchPattern);

		Stream OpenFile(string file, FileMode fileMode, FileAccess fileAccess,
			FileShare fileShare);
	}
}
