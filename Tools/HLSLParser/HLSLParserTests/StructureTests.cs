using System;
using NUnit.Framework;
using HLSLParser;

namespace HLSLParserTests
{
	public static class StructureTests
	{
		private const string TestStructure =
			@"struct PixelShaderInput
{
	float4 pos : SV_POSITION;
	float4 col : COLOR;
	float2 tex : TEXCOORD0;
};";

		#region TestParseIfStructure
		[Test]
		public static void TestParseIfStructure()
		{
			var text = new ParseTextWalker(TestStructure);
			var result = Structure.ParseIfStructure(text);

			Assert.NotNull(result);
			Assert.AreEqual("PixelShaderInput", result.Name);
			Assert.AreEqual(3, result.Variables.Count);
			Assert.AreEqual("", text.Text);
		}
		#endregion

		#region TestParseIfStructureWithoutCode
		[Test]
		public static void TestParseIfStructureWithoutCode()
		{
			var text = new ParseTextWalker("testtest");
			var result = Structure.ParseIfStructure(text);
			
			Assert.Null(result);
		}
		#endregion
	}
}
