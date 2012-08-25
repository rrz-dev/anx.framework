using ANX.Framework;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.Storage;


// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.PsVita
{
	public class PsVitaPlatformCreator : IPlatformSystemCreator
	{
		#region Public
		public string Name
		{
			get
			{
				return "Platform.PsVita";
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
				return OSInformation.GetName() == PlatformName.PSVita;
			}
		}
		#endregion

		#region Constructor
		public PsVitaPlatformCreator()
		{
		}
		#endregion

		#region CreateGameHost
		public GameHost CreateGameHost(Game game)
		{
			Logger.Info("creating PsVita GameHost");
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.PlatformSystem);
			return new PsVitaGameHost(game);
		}
		#endregion

		#region CreateStorageDevice
		public INativeStorageDevice CreateStorageDevice(StorageDevice device,
			PlayerIndex player, int sizeInBytes, int directoryCount)
		{
			return new PsVitaStorageDevice(device, player, sizeInBytes, directoryCount);
		}
		#endregion

		#region CreateStorageContainer
		public INativeStorageContainer CreateStorageContainer(StorageContainer container)
		{
			return new PsVitaStorageContainer(container);
		}
		#endregion

		#region CreateTitleContainer
		public INativeTitleContainer CreateTitleContainer()
		{
			return new PsVitaTitleContainer();
		}
		#endregion

		#region CreateGameTimer
		public INativeGameTimer CreateGameTimer()
		{
			return new PsVitaGameTimer();
		}
		#endregion

		#region CreateContentManager
		public INativeContentManager CreateContentManager()
		{
			return new PsVitaContentManager();
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
