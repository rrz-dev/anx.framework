using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    class PackageServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return Package.GetGlobalService(serviceType);
        }
    }
}
