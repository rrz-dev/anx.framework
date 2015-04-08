using ANX.Framework.Content.Pipeline;
using ANX.Framework.VisualStudio.Nodes;
using Microsoft.VisualStudio.Project.Automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;

namespace ANX.Framework.VisualStudio
{
    class Utilities
    {
        private static EnvDTE.DTE _dte;

        public static void Initialize(EnvDTE.DTE dte)
        {
            if (dte == null)
                throw new ArgumentNullException("dte");

            _dte = dte;
        }

        public static ContentProjectNode GetCurrentProject()
        {
            if (AppDomain.CurrentDomain.IsBuildAppDomain())
                throw new InvalidOperationException("GetCurrentProject called from the wrong appDomain.");

            if (_dte == null)
                throw new InvalidOperationException("The converters have not been initialized.");

            Array projects = (Array)_dte.ActiveSolutionProjects;
            if (projects.Length == 0)
                throw new InvalidOperationException("There's currently no ContentProject selected.");

            if (projects.Length > 1)
                throw new Exception("Test, too many Projects.");

            EnvDTE.Project project = (EnvDTE.Project)projects.GetValue(0);
            if (project is OAProject)
            {
                var oaProject = (OAProject)project;

                if (oaProject.ProjectNode is ContentProjectNode)
                {
                    return (ContentProjectNode)oaProject.ProjectNode;
                }
            }

            return null;
        }

        public static TargetPlatform ParseTargetPlatform(string displayName)
        {
            string[] names = Enum.GetNames(typeof(TargetPlatform));
            for (int i = 0; i < names.Length; i++)
            {
                if (displayName == names[i])
                    return (TargetPlatform)Enum.Parse(typeof(TargetPlatform), names[i]);

                var attribute = typeof(TargetPlatform).GetMember(names[i]).FirstOrDefault().GetCustomAttribute<DescriptionAttribute>(false);
                if (attribute != null && displayName == attribute.Description)
                    return (TargetPlatform)Enum.Parse(typeof(TargetPlatform), names[i]);
            }
            return default(TargetPlatform);
        }

        public static string[] GetTargetPlatformDisplayNames()
        {
            string[] names = Enum.GetNames(typeof(TargetPlatform));
            string[] displayNames = new string[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                var attribute = typeof(TargetPlatform).GetMember(names[i]).FirstOrDefault().GetCustomAttribute<DescriptionAttribute>(false);
                if (attribute != null)
                    displayNames[i] = attribute.Description;
                else
                    displayNames[i] = names[i];
            }

            return displayNames;
        }

        public static string GetDisplayName(TargetPlatform platform)
        {
            string name = Enum.GetName(typeof(TargetPlatform), platform);
            if (string.IsNullOrEmpty(name))
                return name;

            var attribute = typeof(TargetPlatform).GetMember(name).FirstOrDefault().GetCustomAttribute<DescriptionAttribute>(false);
            if (attribute != null)
                return attribute.Description;
            else
                return name;
        }

        public static string GetTargetPlatformName(string displayName)
        {
            string[] names = Enum.GetNames(typeof(TargetPlatform));
            for (int i = 0; i < names.Length; i++)
            {
                var attribute = typeof(TargetPlatform).GetMember(names[i]).FirstOrDefault().GetCustomAttribute<DescriptionAttribute>(false);
                if (attribute != null && displayName == attribute.Description)
                    return names[i];
            }

            return displayName;
        }
    }
}
