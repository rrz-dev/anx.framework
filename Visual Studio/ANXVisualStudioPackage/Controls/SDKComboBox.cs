using ANX.Framework.VisualStudio.Nodes;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANX.Framework.VisualStudio.Controls
{
    public class SDKComboBox : ComboBox
    {
        public class Platform
        {
            string folder;
            string displayName;
            string name;
            string version;
            string registryPath;

            public Platform(string folder, string name, string displayName, string version, string registryPath)
            {
                this.folder = folder;
                this.name = name;
                this.displayName = displayName;
                this.version = version;
                this.registryPath = registryPath;
            }

            public string Folder
            {
                get { return this.folder; }
            }

            public string DisplayName
            {
                get { return this.displayName; }
            }

            public string Name
            {
                get { return this.name; }
            }

            public string Version
            {
                get { return version; }
            }

            public string RegistryPath
            {
                get { return registryPath; }
            }

            public override string ToString()
            {
                return displayName;
            }

            public static Platform[] GetSupportedSdks()
            {
                List<Platform> platforms = new List<Platform>();

                var windows8 =Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SDKs\Windows\v8.0", false);
                if (windows8 != null)
                {
                    platforms.Add(GetPlatform(windows8));
                }

                var windows81 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SDKs\Windows\v8.1", false);
                if (windows81 != null)
                {
                    platforms.Add(GetPlatform(windows81));
                }

                /*var windowsPhone8 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SDKs\Windows Phone\v8.0", false);
                if (windowsPhone8 != null)
                {
                    platforms.Add(GetPlatform(windowsPhone8));
                }*/

                return platforms.Where((x) => x != null).ToArray();
            }

            private static Platform GetPlatform(RegistryKey key)
            {
                var folderValue = key.GetValue("InstallationFolder");
                if (folderValue == null || !(folderValue is string) || string.IsNullOrWhiteSpace((string)folderValue))
                    return null;

                var nameValue = key.GetValue("ProductName");
                if (nameValue == null || !(nameValue is string) || string.IsNullOrWhiteSpace((string)nameValue))
                    return null;

                var versionValue = key.GetValue("ProductVersion");
                if (nameValue == null || !(nameValue is string) || string.IsNullOrWhiteSpace((string)nameValue))
                    return null;

                return new Platform((string)folderValue, Path.GetDirectoryName(key.Name), (string)nameValue, (string)versionValue, key.Name);
            }
        }

        ContentProjectNode node;

        public SDKComboBox()
        {
            
        }

        public Platform GetSelectedSdk()
        {
            if (this.SelectedIndex == -1 || this.SelectedItem is String)
                return null;

            Platform platform = this.SelectedItem as Platform;
            if (platform == null)
                return null;

            return platform;
        }

        public void Initialize(ContentProjectNode node)
        {
            this.node = node;

            Items.Clear();

            Items.Add(PackageResources.GetString(PackageResources.None));

            Items.AddRange(Platform.GetSupportedSdks());
        }
    }
}
