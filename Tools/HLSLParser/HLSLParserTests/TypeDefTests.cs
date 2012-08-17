using System;
using NUnit.Framework;
using HLSLParser;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParserTests
{
	public static class TypeDefTests
	{
		#region TestParseIfTypeDef
		[Test]
		public static void TestParseIfTypeDef()
		{
			var text = new ParseTextWalker("typedef matrix <bool, 1, 1> bool1x1;");
			var result = TypeDef.TryParse(text);

			Assert.NotNull(result);
			Assert.AreEqual("matrix <bool, 1, 1> bool1x1", result.Value);
		}
		#endregion

		#region TestParseIfTypeDefWithoutCode
		[Test]
		public static void TestParseIfTypeDefWithoutCode()
		{
			var text = new ParseTextWalker("testtest");
			var result = TypeDef.TryParse(text);

			Assert.Null(result);
		}
		#endregion

		#region TestToString
		[Test]
		public static void TestToString()
		{
			string text = "typedef matrix <bool, 1, 1> bool1x1;";
			var walker = new ParseTextWalker(text);
			var result = TypeDef.TryParse(walker);

			Assert.AreEqual(text, result.ToString());
		}
		#endregion
	}
}
