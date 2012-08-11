using System;
using System.IO;
using ANX.Framework;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.Storage;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.PsVita
{
	public class PsVitaStorageDevice : INativeStorageDevice
	{
		private string storagePath;
		private StorageDevice parent;
		private DriveInfo storageDrive;

		#region Public
		public long FreeSpace
		{
			get
			{
				try
				{
					return storageDrive.AvailableFreeSpace;
				}
				catch (IOException)
				{
					return -1;
				}
			}
		}

		public long TotalSpace
		{
			get
			{
				try
				{
					return storageDrive.TotalSize;
				}
				catch (IOException)
				{
					return -1;
				}
			}
		}

		public bool IsConnected
		{
			get
			{
				return storageDrive.IsReady;
			}
		}
		#endregion

		#region Constructor
		public PsVitaStorageDevice(StorageDevice setParent, PlayerIndex player,
			int sizeInBytes, int directoryCount)
		{
			parent = setParent;

			string playerPath;
			switch (player)
			{
				case PlayerIndex.One:
					playerPath = "Player1";
					break;
				case PlayerIndex.Two:
					playerPath = "Player2";
					break;
				case PlayerIndex.Three:
					playerPath = "Player3";
					break;
				case PlayerIndex.Four:
					playerPath = "Player4";
					break;
				default:
					playerPath = "AllPlayers";
					break;
			}

			// TODO: find the correct PsVita location for this stuff!
			string myDocsPath = Environment.GetFolderPath(
				Environment.SpecialFolder.MyDocuments);
			storagePath = Path.Combine(myDocsPath, "SavedGames", playerPath);
			storagePath = Path.GetFullPath(storagePath);
			storageDrive = new DriveInfo(Path.GetPathRoot(myDocsPath).Substring(0, 1));
		}
		#endregion

		#region DeleteContainer
		public void DeleteContainer(string titleName)
		{
			Directory.Delete(Path.Combine(parent.StoragePath, titleName), true);
		}
		#endregion
	}
}
