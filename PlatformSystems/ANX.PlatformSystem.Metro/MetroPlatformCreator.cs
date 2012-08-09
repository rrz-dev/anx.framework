using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.Framework;
using NLog;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
	public class MetroPlatformCreator : IPlatformSystemCreator
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

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

		#region RegisterCreator
		public void RegisterCreator(AddInSystemFactory factory)
		{
			logger.Debug("adding Windows PlatformSystem creator to collection of AddInSystemFactory");
			factory.AddCreator(this);
		}
		#endregion

		public MetroPlatformCreator()
		{
		}

		#region CreateGameHost
		public GameHost CreateGameHost(Game game)
		{
			logger.Info("creating Windows GameHost");
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.PlatformSystem);
			return new WindowsGameHost(game);
		}
		#endregion

		#region IPlatformSystemCreator Member


		public INativeStorageDevice CreateStorageDevice(Framework.Storage.StorageDevice device, PlayerIndex player, int sizeInBytes, int directoryCount)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IPlatformSystemCreator Member


		public INativeStorageContainer CreateStorageContainer(Framework.Storage.StorageContainer container)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IPlatformSystemCreator Member


		public INativeTitleContainer CreateTitleContainer()
		{
			throw new NotImplementedException();
		}

		public INativeGameTimer CreateGameTimer()
		{
			throw new NotImplementedException();
		}

		public INativeContentManager CreateContentManager()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
