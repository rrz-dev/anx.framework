using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    internal class TargetFrameworkMoniker
    {
        private string moniker;
        private string displayName;

        public string Moniker
        {
            get
            {
                return this.moniker;
            }
        }

        public TargetFrameworkMoniker(string moniker, string displayName)
        {
            this.moniker = moniker;
            this.displayName = displayName;
        }

        public override string ToString()
        {
            return this.displayName;
        }

        public static IEnumerable<TargetFrameworkMoniker> GetSupportedTargetFrameworkMonikers(IVsFrameworkMultiTargeting vsFrameworkMultiTargeting, Project currentProject)
        {
            Array supportedFrameworks;
            ErrorHandler.ThrowOnFailure(vsFrameworkMultiTargeting.GetSupportedFrameworks(out supportedFrameworks));

            Property property = currentProject.Properties.Item("TargetFrameworkMoniker");

            FrameworkName currentFramework = new FrameworkName(property.Value.ToString());

            List<TargetFrameworkMoniker> result = new List<TargetFrameworkMoniker>();

            HashSet<string> hashSet = new HashSet<string>();
            bool supportsWebApplications = false;

            for (int i = 1, count = currentProject.Properties.Count; i <= count; i++)
            {
                if (currentProject.Properties.Item(i).Name.StartsWith("WebApplication."))
                {
                    supportsWebApplications = true;
                    break;
                }
            }

            foreach (string framework in supportedFrameworks)
            {
                if (hashSet.Add(framework))
                {
                    FrameworkName frameworkName = new FrameworkName(framework);
                    if (string.Compare(frameworkName.Identifier, currentFramework.Identifier, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        if (supportsWebApplications)
                        {
                            if (frameworkName.Version.Major < 4 && !string.IsNullOrEmpty(frameworkName.Profile))
                            {
                                continue;
                            }

                            string resolvedAssemblyPath;
                            if (ErrorHandler.Failed(vsFrameworkMultiTargeting.ResolveAssemblyPath("System.Web.dll", framework, out resolvedAssemblyPath)))
                            {
                                continue;
                            }
                            if (string.IsNullOrEmpty(resolvedAssemblyPath))
                            {
                                continue;
                            }
                        }

                        string displayName;
                        ErrorHandler.ThrowOnFailure(vsFrameworkMultiTargeting.GetDisplayNameForTargetFx(framework, out displayName));
                        result.Add(new TargetFrameworkMoniker(framework, displayName));
                    }
                }
            }

            return result;
        }
    }
}
