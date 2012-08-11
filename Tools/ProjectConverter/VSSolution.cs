using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter
{
	public class VSSolution
	{
		//internal class SolutionParser
		//Name: Microsoft.Build.Construction.SolutionParser
		//Assembly: Microsoft.Build, Version=4.0.0.0

		public const BindingFlags NonPublicInstanceFlag =
			BindingFlags.NonPublic | BindingFlags.Instance;

		private static Type solutionParserType;
		private static PropertyInfo readerProperty;
		private static MethodInfo parseSolutionMethod;
		private static PropertyInfo projectsProperty;

		public List<VSSolutionProject> Projects
		{
			get;
			private set;
		}

		static VSSolution()
		{
			solutionParserType = Type.GetType(
				"Microsoft.Build.Construction.SolutionParser, " +
				"Microsoft.Build, Version=4.0.0.0, Culture=neutral, " +
				"PublicKeyToken=b03f5f7f11d50a3a", false, false);
			if (solutionParserType != null)
			{
				readerProperty = solutionParserType.GetProperty(
					"SolutionReader", NonPublicInstanceFlag);
				projectsProperty = solutionParserType.GetProperty(
					"Projects", NonPublicInstanceFlag);
				parseSolutionMethod = solutionParserType.GetMethod(
					"ParseSolution", NonPublicInstanceFlag);
			}
		}

		public VSSolution(string solutionFileName)
		{
			if (solutionParserType == null)
			{
				throw new InvalidOperationException(
					"Can not find type 'Microsoft.Build.Construction.SolutionParser' " +
					"are you missing a assembly reference to 'Microsoft.Build.dll'?");
			}

			var constructors = solutionParserType.GetConstructors(NonPublicInstanceFlag);
			var solutionParser = constructors[0].Invoke(null);

			using (var streamReader = new StreamReader(solutionFileName))
			{
				readerProperty.SetValue(solutionParser, streamReader, null);
				parseSolutionMethod.Invoke(solutionParser, null);
			}
			var projects = new List<VSSolutionProject>();
			var array = (Array)projectsProperty.GetValue(solutionParser, null);
			for (int i = 0; i < array.Length; i++)
			{
				projects.Add(new VSSolutionProject(array.GetValue(i)));
			}
			this.Projects = projects;
		}

		private class VSSolutionTests
		{
			[Test]
			public static void TestSolutionParser()
			{
				const string filepath = @"D:\code\csharp\ANX.Framework\ANX.Framework.sln";
				VSSolution solution = new VSSolution(filepath);

				Assert.Greater(solution.Projects.Count, 0);
			}

			[Test]
			public static void TestParserNotFound()
			{
				Assert.Throws(typeof(InvalidOperationException), delegate
				{
					VSSolution.solutionParserType = null;
					new VSSolution("");
				});
			}
		}
	}
}
