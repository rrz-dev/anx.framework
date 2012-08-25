using ANX.Framework;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.Storage;


// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Linux
{
	public class LinuxPlatformCreator : IPlatformSystemCreator
	{
		#region Public
		public string Name
		{
			get
			{
				return "Linux";
			}
		}

		public int Priority
		{
			get
			{
				return 100;
			}
		}

		public bool IsSupported
		{
			get
			{
				return OSInformation.GetName() == PlatformName.Linux;
			}
		}
		#endregion

		public LinuxPlatformCreator()
		{
		}

		#region CreateGameHost
		public GameHost CreateGameHost(Game game)
		{
			Logger.Info("creating Linux GameHost");
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.PlatformSystem);
			return new LinuxGameHost(game);
		}
		#endregion

		#region CreateStorageDevice
		public INativeStorageDevice CreateStorageDevice(StorageDevice device,
			PlayerIndex player, int sizeInBytes, int directoryCount)
		{
			return new LinuxStorageDevice(device, player, sizeInBytes, directoryCount);
		}
		#endregion

		#region CreateStorageContainer
		public INativeStorageContainer CreateStorageContainer(StorageContainer container)
		{
			return new LinuxStorageContainer(container);
		}
		#endregion

		#region CreateTitleContainer
		public INativeTitleContainer CreateTitleContainer()
		{
			return new LinuxTitleContainer();
		}
		#endregion

		#region CreateGameTimer
		public INativeGameTimer CreateGameTimer()
		{
			return new LinuxGameTimer();
		}
		#endregion

		#region CreateContentManager
		public INativeContentManager CreateContentManager()
		{
			return new LinuxContentManager();
		}
		#endregion

		#region IPlatformSystemCreator Member


		public INativeMediaLibrary CreateMediaPlayer()
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
