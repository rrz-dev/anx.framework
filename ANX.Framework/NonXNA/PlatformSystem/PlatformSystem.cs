#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Media;
using System.IO;
using ANX.Framework.Storage;
using System.Reflection;
using ANX.Framework.NonXNA.Reflection;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.PlatformSystem
{
    internal class PlatformSystem : IPlatformSystem
    {
        private static PlatformSystem singletonInstance;
        private static IPlatformSystem runtimeInstance;

        private const string prefix = "ANX.PlatformSystem.";
        private const string suffix = ".dll";

        private PlatformSystem()
        {
            runtimeInstance = CreateRuntimeInstance(OSInformation.GetName());
        }

        private IPlatformSystem CreateRuntimeInstance(PlatformName platform)
        {
            string platformAssemblyName = string.Empty;

            try
            {
                platformAssemblyName = GetPlatformAssemblyName(platform);
            }
            catch (PlatformSystemInstanceException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }

            Assembly assembly = Assembly.LoadFrom(platformAssemblyName);

            foreach (Type type in assembly.GetExportedTypes())
            {
                if (TypeHelper.IsTypeAssignableFrom(typeof(IPlatformSystem), type))
                {
                    return TypeHelper.Create<IPlatformSystem>(type);
                }
            }

            return null;
        }

        private string GetPlatformAssemblyName(PlatformName platform)
        {
            switch (platform)
            {
                case PlatformName.Windows7:
                case PlatformName.WindowsVista:
                case PlatformName.WindowsXP:
                    return prefix + "Windows" + suffix;
                case PlatformName.Linux:
                case PlatformName.PSVita:
                    return prefix + platform.ToString() + suffix;
                case PlatformName.Windows8:
                    return prefix + "Metro" + suffix;
                default:
                    throw new PlatformSystemInstanceException("couldn't solve assembly name for platform '" + platform.ToString() + "'");
            }
        }

        public static PlatformSystem Instance
        {
            get
            {
                if (singletonInstance == null)
                {
                    singletonInstance = new PlatformSystem();
                }

                return singletonInstance;
            }
        }

        public GameHost CreateGameHost(Game game)
        {
            if (runtimeInstance == null)
            {
                throw new PlatformSystemInstanceException();
            }

            return runtimeInstance.CreateGameHost(game);
        }

        public INativeStorageDevice CreateStorageDevice(StorageDevice device, PlayerIndex player, int sizeInBytes, int directoryCount)
        {
            if (runtimeInstance == null)
            {
                throw new PlatformSystemInstanceException();
            }

            return runtimeInstance.CreateStorageDevice(device, player, sizeInBytes, directoryCount);
        }

        public INativeStorageContainer CreateStorageContainer(StorageContainer container)
        {
            if (runtimeInstance == null)
            {
                throw new PlatformSystemInstanceException();
            }

            return runtimeInstance.CreateStorageContainer(container);
        }

        public INativeTitleContainer CreateTitleContainer()
        {
            if (runtimeInstance == null)
            {
                throw new PlatformSystemInstanceException();
            }

            return runtimeInstance.CreateTitleContainer();
        }

        public INativeGameTimer CreateGameTimer()
        {
            if (runtimeInstance == null)
            {
                throw new PlatformSystemInstanceException();
            }

            return runtimeInstance.CreateGameTimer();
        }

        public INativeContentManager CreateContentManager()
        {
            if (runtimeInstance == null)
            {
                throw new PlatformSystemInstanceException();
            }

            return runtimeInstance.CreateContentManager();
        }

        public Stream OpenReadFilestream(string filepath)
        {
            if (runtimeInstance == null)
            {
                throw new PlatformSystemInstanceException();
            }

            return runtimeInstance.OpenReadFilestream(filepath);
        }

        public INativeMediaLibrary CreateMediaPlayer()
        {
            if (runtimeInstance == null)
            {
                throw new PlatformSystemInstanceException();
            }

            return runtimeInstance.CreateMediaPlayer();
        }

        public IList<MediaSource> GetAvailableMediaSources()
        {
            if (runtimeInstance == null)
            {
                throw new PlatformSystemInstanceException();
            }

            return runtimeInstance.GetAvailableMediaSources();
        }
    }
}
