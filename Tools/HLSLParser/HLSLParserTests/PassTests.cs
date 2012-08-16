using System;
using NUnit.Framework;
using HLSLParser;

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
			var result = Pass.ParseIfPass(text);

			Assert.NotNull(result);
			Assert.AreEqual("P0", result.Name);
			Assert.AreEqual("CompileShader(vs_4_0, VS())", result.VertexShader);
			Assert.AreEqual("CompileShader(ps_4_0, PS())", result.PixelShader);
		}
		#endregion

		#region ParseIfPassDx9
		[Test]
		public static void ParseIfPassDx9()
		{
			var text = new ParseTextWalker(TestPassDx9);
			var result = Pass.ParseIfPass(text);

			Assert.NotNull(result);
			Assert.AreEqual("P0", result.Name);
			Assert.AreEqual("compile vs_2_0 RenderSceneVS(1, true, true)",
				result.VertexShader);
			Assert.AreEqual("compile ps_2_0 RenderScenePS(true)", result.PixelShader);
		}
		#endregion

		#region TestParseIfTypeDefWithoutCode
		[Test]
		public static void TestParseIfTypeDefWithoutCode()
		{
			var text = new ParseTextWalker("testtest");
			var result = Pass.ParseIfPass(text);

			Assert.Null(result);
		}
		#endregion

		#region TestToString
		[Test]
		public static void TestToString()
		{
			var text = new ParseTextWalker(TestPass);
			var result = Pass.ParseIfPass(text);

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
