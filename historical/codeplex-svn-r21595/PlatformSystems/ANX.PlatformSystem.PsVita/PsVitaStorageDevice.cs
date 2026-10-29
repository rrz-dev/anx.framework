using System;
using System.IO;
using System.Reflection;
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
		private readonly StorageDevice parent;
		private readonly DriveInfo storageDrive;

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
	        get { return storageDrive.IsReady; }
	    }

        public string StoragePath { get; private set; }
	    #endregion

		public PsVitaStorageDevice(StorageDevice setParent, PlayerIndex player, int sizeInBytes, int directoryCount)
		{
            parent = setParent;
            StoragePath = GetDirectoryForContainer(player);
            storageDrive = new DriveInfo(Path.GetPathRoot(StoragePath).Substring(0, 1));
        }

        private string GetDirectoryForContainer(PlayerIndex player)
        {
            string result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "SavedGames");
            result = Path.Combine(result, GetGameTitle());
            //result = Path.Combine(result, StorageContainer.DisplayName);
            string playerSubDir;
            switch (player)
            {
                case PlayerIndex.One:
                    playerSubDir = "Player1";
                    break;
                case PlayerIndex.Two:
                    playerSubDir = "Player2";
                    break;
                case PlayerIndex.Three:
                    playerSubDir = "Player3";
                    break;
                case PlayerIndex.Four:
                    playerSubDir = "Player4";
                    break;
                default:
                    playerSubDir = "AllPlayers";
                    break;
            }

            return Path.Combine(result, playerSubDir);
        }

        private static string GetGameTitle()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
                return Path.GetFileNameWithoutExtension(entryAssembly.Location);

            throw new InvalidOperationException();
        }

		public void DeleteContainer(string titleName)
		{
			Directory.Delete(Path.Combine(parent.StoragePath, titleName), true);
		}
	}
}
