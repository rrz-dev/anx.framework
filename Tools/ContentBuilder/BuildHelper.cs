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
#if WINDOWS
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

#else
            return false;
#endif
        }

        public static string GetOutputFileName(string outputDirectory, BuildItem buildItem)
        {
            return Path.Combine(outputDirectory, Path.GetDirectoryName(buildItem.SourceFilename), buildItem.AssetName + ContentManager.Extension);
        }

        internal static string CreateSafeFileName(string text)
        {
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
                text = text.Replace(invalidChar, '_');

            return text;
        }
    }
}
