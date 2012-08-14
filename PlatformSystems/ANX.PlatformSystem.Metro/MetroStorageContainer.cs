using System;
using System.IO;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
	public class MetroStorageContainer : INativeStorageContainer
	{
		public void CreateDirectory(string directory)
		{
			throw new NotImplementedException();
		}

		public System.IO.Stream CreateFile(string file)
		{
			throw new NotImplementedException();
		}

		public void DeleteDirectory(string directory)
		{
			throw new NotImplementedException();
		}

		public void DeleteFile(string file)
		{
			throw new NotImplementedException();
		}

		public bool DirectoryExists(string directory)
		{
			throw new NotImplementedException();
		}

		public bool FileExists(string file)
		{
			throw new NotImplementedException();
		}

		public string[] GetDirectoryNames(string searchPattern)
		{
			throw new NotImplementedException();
		}

		public string[] GetFileNames(string searchPattern)
		{
			throw new NotImplementedException();
		}

		public Stream OpenFile(string file, FileMode fileMode, FileAccess fileAccess,
			FileShare fileShare)
		{
			throw new NotImplementedException();
		}
	}
}
