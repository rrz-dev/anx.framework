#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Storage
{
#if !WIN8      //TODO: search replacement for Win8

    public class StorageContainer : IDisposable
    {
        private DirectoryInfo baseDirectory;

        public event EventHandler<EventArgs> Disposing;

        internal StorageContainer(StorageDevice device, PlayerIndex player, string displayName)
        {
            StorageDevice = device;
            DisplayName = displayName;

            baseDirectory = new DirectoryInfo(Path.Combine(device.StoragePath, displayName));
            baseDirectory.Create(); //fails silently if directory exists
        }

				~StorageContainer()
				{
					Dispose();
				}

        /// <summary>
        /// Returns the full path for the given relative path, and makes
        /// some sanity checks.
        /// </summary>
        private string GetTestFullPath(string relPath)
        {
            if (string.IsNullOrEmpty(relPath))
                throw new ArgumentNullException("path");

            string fullPath = Path.Combine(baseDirectory.FullName, relPath);

            if (!fullPath.StartsWith(baseDirectory.FullName))
                throw new InvalidOperationException("The given path is not in the selected storage location!");

            return fullPath;
        }

        public void CreateDirectory(string directory)
        {
            baseDirectory.CreateSubdirectory(directory);
        }

        public Stream CreateFile(string file)
        {
            return File.Create(GetTestFullPath(file));
        }

        public void DeleteDirectory(string directory)
        {
            Directory.Delete(GetTestFullPath(directory));
        }

        public void DeleteFile(string file)
        {
            File.Delete(GetTestFullPath(file));
        }

        public bool DirectoryExists(string directory)
        {
            return Directory.Exists(GetTestFullPath(directory));
        }

        public bool FileExists(string file)
        {
            return File.Exists(GetTestFullPath(file));
        }

				public string[] GetDirectoryNames()
				{
					return GetDirectoryNames("*");
				}

        public string[] GetDirectoryNames(string searchPattern)
        {
            List<string> dirs = new List<string>();
            foreach (DirectoryInfo dir in baseDirectory.EnumerateDirectories(searchPattern))
            {
                dirs.Add(dir.FullName.Substring(baseDirectory.FullName.Length));
            }

            return dirs.ToArray();
        }

				public string[] GetFileNames()
				{
					return GetFileNames("*");
				}

        public string[] GetFileNames(string searchPattern)
        {
            List<string> files = new List<string>();
            foreach (FileInfo file in baseDirectory.EnumerateFiles(searchPattern))
            {
                files.Add(file.FullName.Substring(baseDirectory.FullName.Length));
            }

            return files.ToArray();
        }

				public Stream OpenFile(string file, FileMode fileMode)
				{
					return OpenFile(file, fileMode, FileAccess.ReadWrite, FileShare.None);
				}

				public Stream OpenFile(string file, FileMode fileMode, FileAccess fileAccess)
				{
					return OpenFile(file, fileMode, fileAccess, FileShare.None);
				}

        public Stream OpenFile(string file, FileMode fileMode, FileAccess fileAccess,
					FileShare fileShare)
        {
            return File.Open(GetTestFullPath(file), fileMode, fileAccess, fileShare);
        }

        public string DisplayName { get; protected set; }

        public StorageDevice StorageDevice { get; protected set; }

        public bool IsDisposed { get; protected set; }

        public void Dispose()
        {
            Disposing(this, EventArgs.Empty);
            IsDisposed = true;
        }
    }
#endif
}
