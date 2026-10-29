using System;
using NUnit.Framework;
using ANX.Framework.Content.Pipeline.Helpers.HLSLParser;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.HLSLParser
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
            var result = Technique.TryParse(text);

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
            var result = Technique.TryParse(text);

            Assert.Null(result);
        }
        #endregion

        #region ParseTechniqueWithMultiplePasses
        [Test]
        public static void ParseTechniqueWithMultiplePasses()
        {
            var text = new ParseTextWalker(TestTechniqueMultipass);
            var result = Technique.TryParse(text);

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
            var result = Technique.TryParse(text);

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

