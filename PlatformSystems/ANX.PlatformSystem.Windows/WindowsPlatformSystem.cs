using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework;
using ANX.Framework.Media;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.Storage;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Windows
{
	public class WindowsPlatformSystem : IPlatformSystem
	{
		public WindowsPlatformSystem()
		{
		}

		#region CreateGameHost
		public GameHost CreateGameHost(Game game)
		{
			Logger.Info("creating Windows GameHost");
			return new WindowsGameHost(game);
		}
		#endregion

		#region CreateStorageDevice
		public INativeStorageDevice CreateStorageDevice(StorageDevice device,
			PlayerIndex player, int sizeInBytes, int directoryCount)
		{
			return new WindowsStorageDevice(device, player, sizeInBytes, directoryCount);
		}
		#endregion

		#region CreateStorageContainer
		public INativeStorageContainer CreateStorageContainer(StorageContainer container)
		{
			return new WindowsStorageContainer(container);
		}
		#endregion

		#region CreateTitleContainer
		public INativeTitleContainer CreateTitleContainer()
		{
			return new WindowsTitleContainer();
		}
		#endregion

		#region CreateGameTimer
		public INativeGameTimer CreateGameTimer()
		{
			return new WindowsGameTimer();
		}
		#endregion

		#region CreateContentManager
		public INativeContentManager CreateContentManager()
		{
			return new WindowsContentManager();
		}
		#endregion

		#region CreateMediaPlayer (TODO)
		public INativeMediaLibrary CreateMediaPlayer()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetAvailableMediaSources (TODO)
		public IList<MediaSource> GetAvailableMediaSources()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region OpenReadFilestream
		public Stream OpenReadFilestream(string filepath)
		{
			return File.OpenRead(filepath);
		}
		#endregion
	}
}
