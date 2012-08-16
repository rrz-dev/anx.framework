using System;
using NUnit.Framework;
using HLSLParser;

namespace HLSLParserTests
{
	public static class TechniqueTests
	{
		#region Constants
		private const string TestTechnique =
			@"technique10 Render
{
    pass P0
    {
        SetVertexShader( CompileShader( vs_4_0, VS() ) );
        SetGeometryShader( NULL );
        SetPixelShader( CompileShader( ps_4_0, PS() ) );
    }
}";

		private const string TestTechniqueMultipass =
			@"technique Render
{
    pass P0
    {
        SetVertexShader( CompileShader( vs_4_0, VS() ) );
        SetGeometryShader( NULL );
        SetPixelShader( CompileShader( ps_4_0, PS() ) );
    }

    pass P1
    {
        SetVertexShader( CompileShader( vs_4_0, VS() ) );
        SetGeometryShader( NULL );
        SetPixelShader( CompileShader( ps_4_0, PS() ) );
    }
}";
		#endregion

		#region ParseIfTechnique
		[Test]
		public static void ParseIfTechnique()
		{
			var text = new ParseTextWalker(TestTechnique);
			var result = Technique.ParseIfTechnique(text);

			Assert.NotNull(result);
			Assert.AreEqual("Render", result.Name);
			Assert.AreEqual(1, result.Passes.Count);
		}
		#endregion

		#region TestParseIfTypeDefWithoutCode
		[Test]
		public static void TestParseIfTypeDefWithoutCode()
		{
			var text = new ParseTextWalker("testtest");
			var result = Technique.ParseIfTechnique(text);

			Assert.Null(result);
		}
		#endregion

		#region ParseTechniqueWithMultiplePasses
		[Test]
		public static void ParseTechniqueWithMultiplePasses()
		{
			var text = new ParseTextWalker(TestTechniqueMultipass);
			var result = Technique.ParseIfTechnique(text);

			Assert.NotNull(result);
			Assert.AreEqual("Render", result.Name);
			Assert.AreEqual(2, result.Passes.Count);
		}
		#endregion

		#region TestToString
		[Test]
		public static void TestToString()
		{
			var text = new ParseTextWalker(TestTechnique);
			var result = Technique.ParseIfTechnique(text);

			string expected =
				@"technique10 Render
{
	pass P0
	{
		SetVertexShader(CompileShader(vs_4_0, VS()));
		SetGeometryShader(NULL);
		SetPixelShader(CompileShader(ps_4_0, PS()));
	}
}";

			Assert.AreEqual(expected.Replace("\r", ""), result.ToString());
		}
		#endregion
	}
}
