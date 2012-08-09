using System;
using System.Reflection;
using NUnit.Framework;

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
