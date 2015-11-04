using ANX.Framework.Content;
using ANX.Framework.Content.Pipeline.Tasks;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ContentBuilder
{
    public static class BuildHelper
    {
        public static bool TryGetAnxFrameworkPath(out Uri path)
        {
            path = null;
            var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            var key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework\AssemblyFolders\ANX.Framework", false);
            if (key == null)
                return false;

            var value = key.GetValue(null) as string;
            if (value == null)
                return false;
            else
            {
                path = new Uri(value);
                return true;
            }
        }

        public static string GetOutputFileName(string outputDirectory, string projectDirectory, BuildItem buildItem)
        {
            if (!Path.IsPathRooted(projectDirectory))
                throw new ArgumentException("projectDirectory is not absolute: " + projectDirectory);

            var assetName = buildItem.AssetName;
            if (string.IsNullOrEmpty(assetName))
                assetName = Path.GetFileNameWithoutExtension(buildItem.SourceFilename);

            string relativeSourceFilename = buildItem.SourceFilename;
            if (Path.IsPathRooted(relativeSourceFilename))
            {
                if (buildItem.SourceFilename.StartsWith(projectDirectory))
                    relativeSourceFilename = buildItem.SourceFilename.Substring(projectDirectory.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                else
                    throw new ArgumentException(string.Format("The buildItem is not below the project root.\nProject root: {0}\nBuildItem: {1}", projectDirectory, buildItem.SourceFilename));
            }

            return Path.Combine(outputDirectory, Path.GetDirectoryName(relativeSourceFilename), assetName + ContentManager.Extension);
        }

        internal static string CreateSafeFileName(string text)
        {
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
                text = text.Replace(invalidChar, '_');

            return text;
        }
    }
}
