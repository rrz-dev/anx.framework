using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter
{
	public class VsSolutionProject
    {
        public string ProjectName { get; set; }
        public string RelativePath { get; set; }
        public string ProjectGuid { get; set; }
        public string TypeGuid { get; set; }

	    public bool IsCsProject
	    {
	        get { return RelativePath.EndsWith(".csproj", StringComparison.InvariantCultureIgnoreCase); }
	    }
	}
}
