using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.Content.Pipeline
{
    static class AssimpDeploy
    {
        private static bool librariesDeployed = false;

        internal static void DeployLibraries()
        {
            if (librariesDeployed)
                return;

            //TODO: Check operation system and offer libraries for them too.
            if (IntPtr.Size == 4)
            {
                DeployLibrary("Assimp32.dll");
            }
            else if (IntPtr.Size == 8)
            {
                DeployLibrary("Assimp64.dll");
            }

            librariesDeployed = true;
        }

        private static void DeployLibrary(string libraryName)
        {
            string executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Stream library = Assembly.GetExecutingAssembly().GetManifestResourceStream("ANX.Framework.Content.Pipeline." + libraryName);

            if (!TryWritingFile(Path.Combine(executingDirectory, libraryName), library))
            {
                string fileName = Path.Combine(Path.GetTempPath(), Assembly.GetEntryAssembly().GetName().Name, libraryName);
                if (!TryWritingFile(fileName, library))
                {
                    Trace.TraceError("Unable to write assimp library to directory of executing assembly or temp directory.");
                }
            }
        }

        private static bool TryWritingFile(string fileName, Stream content)
        {
            FileInfo info = new FileInfo(fileName);
            if (!info.Exists || info.Length != content.Length)
            {
                try
                {
                    FileStream stream = File.Create(fileName);

                    content.CopyTo(stream);

                    stream.Flush();
                    stream.Close();

                    Assimp.Unmanaged.AssimpLibrary.Instance.LoadLibrary(fileName);
                }
                catch
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch { }

                    return false;
                }
            }
            else
            {
                Assimp.Unmanaged.AssimpLibrary.Instance.LoadLibrary(fileName);
            }

            return true;
        }
    }
}
