using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ANX.ContentCompiler.GUI.Helpers
{
    static class PathHelper
    {
        public static string EnsureTrailingSlash(string path)
        {
            return path.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
        }
    }
}
