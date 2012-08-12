using System;
using ANX.Framework;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework.Storage;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
	public class MetroPlatformCreator : IPlatformSystemCreator
	{
		#region Public
		public string Name
		{
			get
			{
				return "Platform.Metro";
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
			Logger.Info(
				"adding Metro PlatformSystem creator to collection of AddInSystemFactory");
			factory.AddCreator(this);
		}
		#endregion

		#region Constructor
		public MetroPlatformCreator()
		{
		}
		#endregion

		#region CreateGameHost
		public GameHost CreateGameHost(Game game)
		{
			Logger.Info("creating Windows GameHost");
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.PlatformSystem);
			return new WindowsGameHost(game);
		}
		#endregion

		#region CreateStorageDevice (TODO)
		public INativeStorageDevice CreateStorageDevice(StorageDevice device,
			PlayerIndex player, int sizeInBytes, int directoryCount)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region CreateStorageContainer (TODO)
		public INativeStorageContainer CreateStorageContainer(StorageContainer container)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region CreateTitleContainer (TODO)
		public INativeTitleContainer CreateTitleContainer()
		{
			throw new NotImplementedException();
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
	}
}
