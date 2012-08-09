using ANX.Framework;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.Storage;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Windows
{
	public class WindowsPlatformCreator : IPlatformSystemCreator
	{
		#region Public
		public string Name
		{
			get
			{
				return "Windows";
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
				return OSInformation.IsWindows;
			}
		}
		#endregion

		#region RegisterCreator
		public void RegisterCreator(AddInSystemFactory factory)
		{
			Logger.Info("adding Windows PlatformSystem creator to collection of AddInSystemFactory");
			factory.AddCreator(this);
		}
		#endregion

		public WindowsPlatformCreator()
		{
		}

		#region CreateGameHost
		public GameHost CreateGameHost(Game game)
		{
			Logger.Info("creating Windows GameHost");
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.PlatformSystem);
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
	}
}
