using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.Storage;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Windows
{
	public class WindowsStorageContainer : INativeStorageContainer
	{
		private readonly StorageContainer parent;
		private readonly DirectoryInfo baseDirectory;

		public WindowsStorageContainer(StorageContainer setParent)
		{
			parent = setParent;
            baseDirectory = new DirectoryInfo(parent.StorageDevice.StoragePath);
			// fails silently if directory exists
			baseDirectory.Create();
        }

		#region CreateDirectory
		public void CreateDirectory(string directory)
		{
			baseDirectory.CreateSubdirectory(directory);
		}
		#endregion

		#region CreateFile
		public Stream CreateFile(string file)
		{
			return File.Create(GetTestFullPath(file));
		}
		#endregion
		
		#region DeleteDirectory
		public void DeleteDirectory(string directory)
		{
			Directory.Delete(GetTestFullPath(directory));
		}
		#endregion
		
		#region DeleteFile
		public void DeleteFile(string file)
		{
			File.Delete(GetTestFullPath(file));
		}
		#endregion
		
		#region DirectoryExists
		public bool DirectoryExists(string directory)
		{
			return Directory.Exists(GetTestFullPath(directory));
		}
		#endregion
		
		#region FileExists
		public bool FileExists(string file)
		{
			return File.Exists(GetTestFullPath(file));
		}
		#endregion
		
		#region GetDirectoryNames
		public string[] GetDirectoryNames(string searchPattern)
		{
			List<string> dirs = new List<string>();
			foreach (DirectoryInfo dir in baseDirectory.EnumerateDirectories(searchPattern))
			{
				dirs.Add(dir.FullName.Substring(baseDirectory.FullName.Length));
			}

			return dirs.ToArray();
		}
		#endregion
		
		#region GetFileNames
		public string[] GetFileNames(string searchPattern)
		{
			List<string> files = new List<string>();
			foreach (FileInfo file in baseDirectory.EnumerateFiles(searchPattern))
			{
				files.Add(file.FullName.Substring(baseDirectory.FullName.Length));
			}

			return files.ToArray();
		}
		#endregion
		
		#region OpenFile
		public Stream OpenFile(string file, FileMode fileMode, FileAccess fileAccess,
			FileShare fileShare)
		{
			return File.Open(GetTestFullPath(file), fileMode, fileAccess, fileShare);
		}
		#endregion

		#region GetTestFullPath
		/// <summary>
		/// Returns the full path for the given relative path, and makes
		/// some sanity checks.
		/// </summary>
		private string GetTestFullPath(string relPath)
		{
			if (String.IsNullOrEmpty(relPath))
				throw new ArgumentNullException("path");

			string fullPath = Path.Combine(baseDirectory.FullName, relPath);

			if (fullPath.StartsWith(baseDirectory.FullName) == false)
				throw new InvalidOperationException(
					"The given path is not in the selected storage location!");

			return fullPath;
		}
		#endregion
	}
}
