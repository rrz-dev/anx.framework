using System;
using System.Reflection;
using NUnit.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter
{
	public static class ReflectionLearningTests
	{
		[Test]
		public static void TestEmptyAssemblyGetManifestResourceNames()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();

			string[] names = assembly.GetManifestResourceNames();

			Assert.NotNull(names);
			Assert.AreEqual(names.Length, 0);
		}
	}
}
