using System;
using System.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter
{
	public class VSSolutionProject
	{
		private static readonly Type msBuildProjectType;
		private static readonly PropertyInfo nameProperty;
		private static readonly PropertyInfo relativePathProperty;
		private static readonly PropertyInfo projectGuidProperty;

		public string ProjectName
		{
			get;
			private set;
		}

		public string RelativePath
		{
			get;
			private set;
		}

		public string ProjectGuid
		{
			get;
			private set;
		}

		public bool IsCsProject
		{
			get
			{
				return RelativePath.EndsWith(".csproj");
			}
		}

		static VSSolutionProject()
		{
			msBuildProjectType = Type.GetType(
				"Microsoft.Build.Construction.ProjectInSolution, " +
				"Microsoft.Build, Version=4.0.0.0, Culture=neutral, " +
				"PublicKeyToken=b03f5f7f11d50a3a", false, false);

			if (msBuildProjectType != null)
			{
				nameProperty = msBuildProjectType.GetProperty(
					"ProjectName", VSSolution.NonPublicInstanceFlag);
				relativePathProperty = msBuildProjectType.GetProperty(
					"RelativePath", VSSolution.NonPublicInstanceFlag);
				projectGuidProperty = msBuildProjectType.GetProperty(
					"ProjectGuid", VSSolution.NonPublicInstanceFlag);
			}
		}

		public VSSolutionProject(object solutionProject)
		{
			ProjectName = nameProperty.GetValue(solutionProject, null) as string;
			RelativePath = relativePathProperty.GetValue(solutionProject, null) as string;
			ProjectGuid = projectGuidProperty.GetValue(solutionProject, null) as string;
		}
	}
}
