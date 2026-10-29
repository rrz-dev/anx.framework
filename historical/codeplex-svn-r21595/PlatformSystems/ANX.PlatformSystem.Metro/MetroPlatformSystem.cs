using System;
using ANX.Framework;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.Storage;
using System.IO;
using ANX.Framework.Media;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
	public class MetroPlatformSystem : IPlatformSystem
	{
		#region Constructor
		public MetroPlatformSystem()
		{
		}
		#endregion

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
			return new MetroStorageDevice();
		}
		#endregion

		#region CreateStorageContainer
		public INativeStorageContainer CreateStorageContainer(StorageContainer container)
		{
			return new MetroStorageContainer();
		}
		#endregion

		#region CreateTitleContainer
		public INativeTitleContainer CreateTitleContainer()
		{
			return new MetroTitleContainer();
		}
		#endregion

		#region CreateGameTimer
		public INativeGameTimer CreateGameTimer()
		{
			return new MetroGameTimer();
		}
		#endregion

		#region CreateContentManager
		public INativeContentManager CreateContentManager()
		{
			return new MetroContentManager();
		}
		#endregion

		#region IPlatformSystemCreator Member
		public Stream OpenReadFilestream(string filepath)
		{
			throw new NotImplementedException();
		}

        public INativeMediaLibrary CreateMediaLibrary()
		{
			throw new NotImplementedException();
		}

		public IList<MediaSource> GetAvailableMediaSources()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
