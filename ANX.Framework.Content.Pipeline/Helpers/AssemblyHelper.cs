using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Helpers
{
    public static class AssemblyHelper
    {
        public static bool IsValidForPipeline(AssemblyName assemblyName)
        {
#if LINUX
            //Apparently these Assemblies are bad juju, so lets blacklist them as we don't need to check them anyway
            if (assemblyName.FullName == "MonoDevelop.Core, Version=2.6.0.0, Culture=neutral, PublicKeyToken=null" || assemblyName.FullName == "pango-sharp, Version=2.12.0.0, Culture=neutral, PublicKeyToken=35e10195dab3c99f"
                || assemblyName.FullName == "Mono.TextEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" || assemblyName.FullName == "MonoDevelop.Ide, Version=2.6.0.0, Culture=neutral, PublicKeyToken=null")
                return false;
#endif
            return true;
        }
    }
}
