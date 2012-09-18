using System;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Storage
{
	public class StorageContainer : IDisposable
	{
		private INativeStorageContainer nativeImplementation;

		#region Public
		public event EventHandler<EventArgs> Disposing;

		public string DisplayName
		{
			get;
			protected set;
		}

		public StorageDevice StorageDevice
		{
			get;
			protected set;
		}

		public bool IsDisposed
		{
			get;
			protected set;
		}
		#endregion

		internal StorageContainer(StorageDevice device, PlayerIndex player,
			string displayName)
		{
			StorageDevice = device;
			DisplayName = displayName;

            nativeImplementation = PlatformSystem.Instance.CreateStorageContainer(this);
		}

		~StorageContainer()
		{
			Dispose();
		}

		public void CreateDirectory(string directory)
		{
			nativeImplementation.CreateDirectory(directory);
		}

		public Stream CreateFile(string file)
		{
			return nativeImplementation.CreateFile(file);
		}

		public void DeleteDirectory(string directory)
		{
			nativeImplementation.DeleteDirectory(directory);
		}

		public void DeleteFile(string file)
		{
			nativeImplementation.DeleteFile(file);
		}

		public bool DirectoryExists(string directory)
		{
			return nativeImplementation.DirectoryExists(directory);
		}

		public bool FileExists(string file)
		{
			return nativeImplementation.FileExists(file);
		}

		public string[] GetDirectoryNames()
		{
			return GetDirectoryNames("*");
		}

		public string[] GetDirectoryNames(string searchPattern)
		{
			return nativeImplementation.GetDirectoryNames(searchPattern);
		}

		public string[] GetFileNames()
		{
			return GetFileNames("*");
		}

		public string[] GetFileNames(string searchPattern)
		{
			return nativeImplementation.GetFileNames(searchPattern);
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
			return nativeImplementation.OpenFile(file, fileMode, fileAccess, fileShare);
		}

		public void Dispose()
		{
			Disposing(this, EventArgs.Empty);
			IsDisposed = true;
		}
	}
}
