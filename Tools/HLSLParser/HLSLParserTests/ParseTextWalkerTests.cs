using System;
using NUnit.Framework;
using HLSLParser;

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
