using System;
using NUnit.Framework;
using HLSLParser;

namespace HLSLParserTests
{
	public static class MethodTests
	{
		#region Constants
		private const string TestMethod =
			@"float4 PSPointSprite(PSSceneIn input) : SV_Target
{
	return g_txDiffuse.Sample(g_samLinear, input.tex) * input.color;
}";

		private const string TestMethodWithBodyBraces =
			@"float4 PSPointSprite(PSSceneIn input) : SV_Target
{
	if(true)
	{
    return g_txDiffuse.Sample(g_samLinear, input.tex) * input.color;
	}
	else
	{
    return g_txDiffuse.Sample(g_samLinear, input.tex);
	}
}";

		private const string TestMethodInline =
			@"inline float4 PSPointSprite(PSSceneIn input) : SV_Target
{   
	return g_txDiffuse.Sample(g_samLinear, input.tex) * input.color;
}";
		#endregion

		#region TestParseIfMethod
		[Test]
		public static void TestParseIfMethod()
		{
			var text = new ParseTextWalker(TestMethod);
			var result = Method.ParseIfMethod(text);

			Assert.NotNull(result);
			Assert.AreEqual("PSPointSprite", result.Name);
			Assert.AreEqual("float4", result.ReturnType);
			Assert.AreEqual("PSSceneIn input", result.Arguments);
			Assert.AreEqual("SV_Target", result.Semantic);
			Assert.AreEqual(
				"return g_txDiffuse.Sample(g_samLinear, input.tex) * input.color;",
				result.Body);
		}
		#endregion

		#region TestParseIfMethodWithBodyBraces
		[Test]
		public static void TestParseIfMethodWithBodyBraces()
		{
			var text = new ParseTextWalker(TestMethodWithBodyBraces);
			var result = Method.ParseIfMethod(text);

			Assert.NotNull(result);
			Assert.AreEqual("PSPointSprite", result.Name);
			Assert.AreEqual("float4", result.ReturnType);

			string expected =
				@"if(true)
	{
    return g_txDiffuse.Sample(g_samLinear, input.tex) * input.color;
	}
	else
	{
    return g_txDiffuse.Sample(g_samLinear, input.tex);
	}";

			Assert.AreEqual(expected, result.Body);
		}
		#endregion

		#region TestParseIfMethodInline
		[Test]
		public static void TestParseIfMethodInline()
		{
			var text = new ParseTextWalker(TestMethodInline);
			var result = Method.ParseIfMethod(text);

			Assert.NotNull(result);
			Assert.AreEqual("PSPointSprite", result.Name);
			Assert.AreEqual("float4", result.ReturnType);
			Assert.AreEqual("inline", result.StorageClass);
		}
		#endregion

		#region TestParseIfMethodWithVariable
		[Test]
		public static void TestParseIfMethodWithVariable()
		{
			var text = new ParseTextWalker("float4 value;");
			var result = Method.ParseIfMethod(text);

			Assert.Null(result);
		}
		#endregion

		#region TestParseIfMethodWithoutCode
		[Test]
		public static void TestParseIfMethodWithoutCode()
		{
			var text = new ParseTextWalker("testtest");
			var result = Method.ParseIfMethod(text);

			Assert.Null(result);
		}
		#endregion

		//#region TestToString
		//[Test]
		//public static void TestToString()
		//{
		//  string text = "typedef matrix <bool, 1, 1> bool1x1;";
		//  var walker = new ParseTextWalker(text);
		//  var result = TypeDef.ParseIfTypeDef(walker);

		//  Assert.AreEqual(text, result.ToString());
		//}
		//#endregion
	}
}
