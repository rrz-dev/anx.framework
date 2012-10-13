using System;
using System.IO;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Storage
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public sealed class StorageDevice
	{
        #region Private
        private readonly PlayerIndex playerIndex;
		private readonly INativeStorageDevice nativeImplementation;

		private static Func<PlayerIndex, int, int, StorageDevice> openDeviceDelegate;
		private Func<string, StorageContainer> openContainerDelegate;
		#endregion

		#region Public
#pragma warning disable 0067 //This event is never used
		public static event EventHandler<EventArgs> DeviceChanged;
#pragma warning restore 0067

        public long FreeSpace
        {
            get { return nativeImplementation.FreeSpace; }
        }

        public bool IsConnected
        {
            get { return nativeImplementation.IsConnected; }
        }

        public long TotalSpace
        {
            get { return nativeImplementation.TotalSpace; }
        }

        internal string StoragePath
        {
            get { return nativeImplementation.StoragePath; }
        }
        #endregion

		#region Constructor
		internal StorageDevice(PlayerIndex player, int sizeInBytes, int directoryCount)
		{
		    playerIndex = player;
			nativeImplementation = PlatformSystem.Instance.CreateStorageDevice(this, player, sizeInBytes, directoryCount);
		}
		#endregion

		#region BeginOpenContainer
		public IAsyncResult BeginOpenContainer(string displayName, AsyncCallback callback, Object state)
		{
			if (openContainerDelegate != null)
				throw new InvalidOperationException("There is currently a StorageContainer request pending. " +
					"Please let this request finish.");

			openContainerDelegate = OpenStorageContainer;
			return openContainerDelegate.BeginInvoke(displayName, callback, state);
		}
		#endregion

		#region BeginShowSelector
		public static IAsyncResult BeginShowSelector(AsyncCallback callback, Object state)
		{
			return BeginShowSelector(PlayerIndex.One, 0, 0, callback, state);
		}

		public static IAsyncResult BeginShowSelector(int sizeInBytes, int directoryCount, AsyncCallback callback, Object state)
		{
			return BeginShowSelector(PlayerIndex.One, sizeInBytes, directoryCount, callback, state);
		}

		public static IAsyncResult BeginShowSelector(PlayerIndex player, AsyncCallback callback, Object state)
		{
			return BeginShowSelector(player, 0, 0, callback, state);
		}

		public static IAsyncResult BeginShowSelector(PlayerIndex player, int sizeInBytes, int directoryCount,
            AsyncCallback callback, Object state)
		{
			if (openDeviceDelegate != null)
				throw new InvalidOperationException(
                    "There is currently a StorageDevice request pending. Please let this request finish.");

			openDeviceDelegate = OpenStorageDevice;
			return openDeviceDelegate.BeginInvoke(player, sizeInBytes, directoryCount, callback, state);
		}
		#endregion

		#region DeleteContainer
		public void DeleteContainer(string titleName)
		{
			if (String.IsNullOrEmpty(titleName))
				throw new ArgumentNullException("titleName");

			try
			{
				nativeImplementation.DeleteContainer(titleName);
			}
			catch (IOException e)
			{
				throw new InvalidOperationException("A IOException occured while deleting the container. See inner Exception.", e);
			}
		}
		#endregion

		#region EndOpenContainer
		public StorageContainer EndOpenContainer(IAsyncResult result)
		{
			if (openContainerDelegate == null)
				throw new InvalidOperationException("There is operation pending that could be ended.");

			StorageContainer container = openContainerDelegate.EndInvoke(result);
			openContainerDelegate = null;
			return container;
		}
		#endregion

		#region EndShowSelector
		public static StorageDevice EndShowSelector(IAsyncResult result)
		{
			if (openDeviceDelegate == null)
				throw new InvalidOperationException("There is operation pending that could be ended.");

			StorageDevice device = openDeviceDelegate.EndInvoke(result);
			openDeviceDelegate = null;
			return device;
		}
		#endregion

		#region OpenStorageDevice
		private static StorageDevice OpenStorageDevice(PlayerIndex player, int sizeInBytes, int directoryCount)
		{
			return new StorageDevice(player, sizeInBytes, directoryCount);
		}
		#endregion

		#region OpenStorageContainer
		private StorageContainer OpenStorageContainer(string displayName)
		{
			return new StorageContainer(this, playerIndex, displayName);
		}
		#endregion
	}
}
