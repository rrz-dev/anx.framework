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

        private static Dictionary<string, TargetPlatform> _displayNames;

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

        private static void InitializeTargetPlatforms()
        {
            if (_displayNames == null)
            {
                _displayNames = new Dictionary<string, TargetPlatform>();
                foreach (var enumValue in typeof(TargetPlatform).GetEnumValues())
                {
                    var enumInstance = (TargetPlatform)Enum.ToObject(typeof(TargetPlatform), enumValue);
                    _displayNames.Add(enumInstance.ToDisplayName(), enumInstance);
                }
            }
        }

        public static TargetPlatform ParseTargetPlatform(string displayName)
        {
            InitializeTargetPlatforms();

            TargetPlatform targetPlatform;
            if (_displayNames.TryGetValue(displayName, out targetPlatform))
                return targetPlatform;

            return default(TargetPlatform);
        }

        public static string[] GetTargetPlatformDisplayNames()
        {
            InitializeTargetPlatforms();

            return _displayNames.Keys.ToArray();
        }

        public static string GetDisplayName(TargetPlatform platform)
        {
            return platform.ToDisplayName();
        }

        public static string GetTargetPlatformName(string displayName)
        {
            InitializeTargetPlatforms();

            TargetPlatform targetPlatform;
            if (_displayNames.TryGetValue(displayName, out targetPlatform))
                return targetPlatform.ToString();

            return displayName;
        }
    }
}
