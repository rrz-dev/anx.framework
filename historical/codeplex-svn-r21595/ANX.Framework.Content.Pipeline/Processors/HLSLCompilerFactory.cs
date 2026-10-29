#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

#endregion

namespace ANX.Framework.Content.Pipeline.Processors
{
    public class HLSLCompilerFactory
    {
        private List<HLSLCompiler> compilers = new List<HLSLCompiler>();

        public IEnumerable<HLSLCompiler> Compilers
        {
            get
            {
                if (compilers == null || compilers.Count <= 0)
                {
                    HashSet<HLSLCompiler> tempCompilers = new HashSet<HLSLCompiler>();
                    HLSLCompilerExecutables.All(x => tempCompilers.Add(x)); // deduplicate list
                    compilers = new List<HLSLCompiler>(tempCompilers);
                    compilers.Sort();
                }

                foreach (HLSLCompiler compiler in compilers)
                {
                    yield return compiler;
                }
            }
        }

        private IEnumerable<string> ExecutablePaths
        {
            get
            {
                foreach (String subdir in new String[] { "x64", "x86" })
                {
                    string sdkPath = Environment.GetEnvironmentVariable("DXSDK_DIR");
                    if (String.IsNullOrEmpty(sdkPath) == false)
                    {
                        yield return Path.Combine(sdkPath, subdir);
                    }

                    foreach (String programFilesPath in new String[] { Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                                                       Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) })
                    {
                        yield return Path.Combine(Path.Combine(programFilesPath, @"Windows Kits\8.0\bin\"), subdir);
                        yield return Path.Combine(Path.Combine(programFilesPath, @"Microsoft DirectX SDK (June 2010)\Utilities\bin\"), subdir);
                    }
                }
            }
        }

        private IEnumerable<HLSLCompiler> HLSLCompilerExecutables
        {
            get
            {
                string fxcFile;

                foreach (string path in ExecutablePaths)
                {
                    fxcFile = Path.Combine(path, @"fxc.exe");

                    if (File.Exists(fxcFile))
                    {
                        yield return new HLSLCompiler(fxcFile);
                    }
                }
            }
        }


    }
}
