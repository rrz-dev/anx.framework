#region Using Statements
using System;
using System.IO;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Storage
{
    public sealed class StorageDevice
    {
        private static Func<PlayerIndex, int, int, StorageDevice> openDeviceDelegate = null;

        private DriveInfo storageDrive;
        private Func<string, StorageContainer> openContainerDelegate = null;

        public static event EventHandler<EventArgs> DeviceChanged;

        internal StorageDevice(string storagePath)
        {
            StoragePath = Path.GetFullPath(storagePath);
            storageDrive = new DriveInfo(Path.GetPathRoot(storagePath).Substring(0, 1));
        }

        public IAsyncResult BeginOpenContainer(string displayName, AsyncCallback callback, Object state)
        {
            //See comments of OpenStorageDevice
            if (openContainerDelegate != null)
                throw new InvalidOperationException("There is currently a StorageContainer request pending. Please let this request finish.");

            openContainerDelegate = new Func<string, StorageContainer>(OpenStorageContainer);
            return openContainerDelegate.BeginInvoke(displayName, callback, state);
        }

        public static IAsyncResult BeginShowSelector(AsyncCallback callback, Object state) //We can't use optional parameters, because they can only be used as last!
        { return BeginShowSelector(PlayerIndex.One, 0, 0, callback, state); }

        public static IAsyncResult BeginShowSelector(int sizeInBytes, int directoryCount, AsyncCallback callback, Object state)
        { return BeginShowSelector(PlayerIndex.One, sizeInBytes, directoryCount, callback, state); }

        public static IAsyncResult BeginShowSelector(PlayerIndex player, AsyncCallback callback, Object state)
        { return BeginShowSelector(player, 0, 0, callback, state); }

        public static IAsyncResult BeginShowSelector(PlayerIndex player, int sizeInBytes, int directoryCount, AsyncCallback callback, Object state)
        {
            //See comments of OpenStorageDevice
            if (openDeviceDelegate != null)
                throw new InvalidOperationException("There is currently a StorageDevice request pending. Please let this request finish.");

            openDeviceDelegate = new Func<PlayerIndex, int, int, StorageDevice>(OpenStorageDevice);
            return openDeviceDelegate.BeginInvoke(player, sizeInBytes, directoryCount, callback, state);
        }

        public void DeleteContainer(string titleName)
        {
            if(string.IsNullOrEmpty(titleName))
                throw new ArgumentNullException("titleName");

            try
            {
                Directory.Delete(Path.Combine(StoragePath, titleName), true);
            }
            catch (IOException e)
            {
                throw new InvalidOperationException("A IOException occured while deleting the container. See inner Exception.", e);
            }
        }

        public StorageContainer EndOpenContainer(IAsyncResult result)
        {
            if (openContainerDelegate == null)
                throw new InvalidOperationException("There is operation pending that could be ended.");

            StorageContainer container = openContainerDelegate.EndInvoke(result);
            openContainerDelegate = null;
            return container;
        }

        public static StorageDevice EndShowSelector(IAsyncResult result)
        {
            if (openDeviceDelegate == null)
                throw new InvalidOperationException("There is operation pending that could be ended.");

            StorageDevice device = openDeviceDelegate.EndInvoke(result);
            openDeviceDelegate = null;
            return device;
        }

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

        public bool IsConnected
        { get { return storageDrive.IsReady; } }

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

        /// <summary>
        /// The path this storage device is currently pointing to.
        /// </summary>
        internal string StoragePath { get; private set; }

        /// <summary>
        /// The player this device is currently assosiated with.
        /// </summary>
        internal PlayerIndex PlayerIndex { get; private set; }

        /// <summary>
        /// Private Helper Method that does the real work of *OpenStorageDevice.
        /// </summary>
        /// <remarks>We invoke this Method async using a delegate to have a IAsyncResult that we can return.
        /// This Method will return nearly instant, but XNA requires to have Begin/End-Methods.
        /// Currently, there is only one "device", the HDD. Saves are placed in /My Documents/SavedGames. We don't care about the size or
        /// directory count, the HDD will should enough space anyway ;)</remarks>
        private static StorageDevice OpenStorageDevice(PlayerIndex player, int sizeInBytes, int directoryCount)
        {
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

            string myDocsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SavedGames", playerPath);
            return new StorageDevice(myDocsPath);
        }

        /// <summary>
        /// See comment for OpenStorageDevice.
        /// </summary>
        private StorageContainer OpenStorageContainer(string displayName)
        {
            return new StorageContainer(this, this.PlayerIndex, displayName);
        }
    }
}
