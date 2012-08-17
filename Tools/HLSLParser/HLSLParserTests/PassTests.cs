using System;
using NUnit.Framework;
using HLSLParser;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParserTests
{
	public static class PassTests
	{
		#region Constants
		private const string TestPass =
			@"pass P0
{
	SetVertexShader( CompileShader( vs_4_0, VS() ) );
	SetGeometryShader( NULL );
	SetPixelShader( CompileShader( ps_4_0, PS() ) );
}";

		private const string TestPassDx9 =
			@"pass P0
{
	VertexShader = compile vs_2_0 RenderSceneVS( 1, true, true );
  PixelShader  = compile ps_2_0 RenderScenePS( true );
}";
		#endregion

		#region ParseIfPass
		[Test]
		public static void ParseIfPass()
		{
			var text = new ParseTextWalker(TestPass);
			var result = Pass.TryParse(text);

			Assert.NotNull(result);
			Assert.AreEqual("P0", result.Name);
			Assert.AreEqual("VS()", result.VertexShader);
			Assert.AreEqual("vs_4_0", result.VertexShaderProfile);
			Assert.AreEqual("PS()", result.PixelShader);
			Assert.AreEqual("ps_4_0", result.PixelShaderProfile);
		}
		#endregion

		#region ParseIfPassDx9
		[Test]
		public static void ParseIfPassDx9()
		{
			var text = new ParseTextWalker(TestPassDx9);
			var result = Pass.TryParse(text);

			Assert.NotNull(result);
			Assert.AreEqual("P0", result.Name);
			Assert.AreEqual("RenderSceneVS(1, true, true)", result.VertexShader);
			Assert.AreEqual("vs_2_0", result.VertexShaderProfile);
			Assert.AreEqual("RenderScenePS(true)", result.PixelShader);
			Assert.AreEqual("ps_2_0", result.PixelShaderProfile);
		}
		#endregion

		#region TestParseIfTypeDefWithoutCode
		[Test]
		public static void TestParseIfTypeDefWithoutCode()
		{
			var text = new ParseTextWalker("testtest");
			var result = Pass.TryParse(text);

			Assert.Null(result);
		}
		#endregion

		#region TestToString
		[Test]
		public static void TestToString()
		{
			var text = new ParseTextWalker(TestPass);
			var result = Pass.TryParse(text);

			string expected =
				@"pass P0
{
	SetVertexShader(CompileShader(vs_4_0, VS()));
	SetGeometryShader(NULL);
	SetPixelShader(CompileShader(ps_4_0, PS()));
}";

			Assert.AreEqual(expected.Replace("\r", ""), result.ToString());
		}
		#endregion
	}
}
