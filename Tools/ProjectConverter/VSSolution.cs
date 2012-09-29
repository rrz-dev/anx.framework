using System.Collections.Generic;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter
{
	public class VsSolution
	{
        private List<VsSolutionProject> allProjects;

        public VsSolutionProject[] Projects
        {
            get { return allProjects.ToArray(); }
        }

        private VsSolution(string filepath)
        {
            allProjects = new List<VsSolutionProject>();
            ParseFile(File.ReadAllLines(filepath));
        }

        private void ParseFile(string[] lines)
        {
            foreach (string currentLine in lines)
            {
                string line = currentLine;
                // Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ANX.Framework", "ANX.Framework\ANX.Framework.csproj", "{6899F0C9-70B9-4EB0-9DD3-E598D4BE3E35}"
                if (line.TrimStart().StartsWith("Project("))
                {
                    int typeGuidStart = line.IndexOf('{');
                    int typeGuidEnd = line.IndexOf('}', typeGuidStart + 1);
                    string typeGuid = line.Substring(typeGuidStart + 1, typeGuidEnd - typeGuidStart - 1);
                    line = line.Remove(0, typeGuidEnd + 2);

                    int nameStart = line.IndexOf('"');
                    int nameEnd = line.IndexOf('"', nameStart + 1);
                    string name = line.Substring(nameStart + 1, nameEnd - nameStart - 1);
                    line = line.Remove(0, nameEnd + 2);

                    int pathStart = line.IndexOf('"');
                    int pathEnd = line.IndexOf('"', pathStart + 1);
                    string relativePath = line.Substring(pathStart + 1, pathEnd - pathStart - 1);
                    line = line.Remove(0, pathEnd);

                    int guidStart = line.IndexOf('{');
                    int guidEnd = line.IndexOf('}', guidStart + 1);
                    string guid = line.Substring(guidStart + 1, guidEnd - guidStart - 1);

                    if (relativePath.ToLower().Contains(".csproj"))
                    {
                        allProjects.Add(new VsSolutionProject
                                        {
                                            TypeGuid = typeGuid,
                                            ProjectName = name,
                                            RelativePath = relativePath,
                                            ProjectGuid = guid
                                        });
                    }
                }
            }
        }

	    public static VsSolution Load(string filepath)
        {
            return new VsSolution(filepath);
        }
	}
}
