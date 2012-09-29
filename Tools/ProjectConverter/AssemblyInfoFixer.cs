using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter
{
    public class AssemblyInfoFixer
    {
        public static void FixCompanyAndCopyright(string filepath, int yearOfLastChange)
        {
            FixAllEntries(filepath, "AssemblyCompany", "ANX.Framework Team");
            FixAllEntries(filepath, "AssemblyCopyright", "Copyright © ANX.Framework Team 2011 - " + yearOfLastChange);
        }

        #region FixAllEntries
        private static void FixAllEntries(string filepath, string entryName, string newValue)
        {
            var projectPaths = CollectProjectPaths(filepath);
            foreach (string project in projectPaths)
            {
                string assemblyInfoPath = Path.Combine(Path.GetDirectoryName(project), "Properties//AssemblyInfo.cs");
                string text = File.ReadAllText(assemblyInfoPath, Encoding.UTF8);
                int index = text.IndexOf("[assembly: " + entryName);
                if (index == -1)
                    continue;

                index = text.IndexOf("\"", index) + 1;
                int endIndex = text.IndexOf("\"", index);
                text = text.Remove(index, endIndex - index).Insert(index, newValue);

                File.WriteAllText(assemblyInfoPath, text, Encoding.UTF8);
            }
        }
        #endregion

        #region CollectProjectPaths
        private static List<string> CollectProjectPaths(string filepath)
        {
            var projectPaths = new List<string>();
            if (filepath.EndsWith(".sln", StringComparison.InvariantCultureIgnoreCase))
            {
                var solution = VsSolution.Load(filepath);
                string basePath = Path.GetDirectoryName(filepath);

                foreach (var project in solution.Projects)
                    if (project.IsCsProject)
                        projectPaths.Add(Path.Combine(basePath, project.RelativePath));
            }
            else
                projectPaths.Add(filepath);

            return projectPaths;
        }
        #endregion
    }
}
