using System;
using NUnit.Framework;
using HLSLParser;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParserTests
{
	public static class ParseTextWalkerTests
	{
		[Test]
		public static void TestCreateTextWalker()
		{
			string text = "Testtext";
			ParseTextWalker walker = new ParseTextWalker(text);

			Assert.AreEqual(walker.Text, text);
		}

		[Test]
		public static void TestSeeking()
		{
			string text = "Testtext";
			ParseTextWalker walker = new ParseTextWalker(text);

			walker.Seek(4);
			Assert.AreEqual(walker.Text, "text");
		}
	}
}
