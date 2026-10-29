using System;
using NUnit.Framework;
using ANX.Framework.Content.Pipeline.Helpers.HLSLParser;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.HLSLParser
{
    public static class BufferTests
    {
        #region TestParseIfBuffer
        [Test]
        public static void TestParseIfBuffer()
        {
            var text = new ParseTextWalker("Buffer<float4> g_Buffer;");
            var result = EffectBuffer.TryParse(text);

            Assert.NotNull(result);
            Assert.AreEqual("g_Buffer", result.Name);
            Assert.AreEqual("float4", result.Type);
        }
        #endregion

        #region TestParseIfBufferWithoutCode
        [Test]
        public static void TestParseIfBufferWithoutCode()
        {
            var text = new ParseTextWalker("testtest");
            var result = EffectBuffer.TryParse(text);

            Assert.Null(result);
        }
        #endregion

        #region TestToString
        [Test]
        public static void TestToString()
        {
            string text = "Buffer<float4> g_Buffer;";
            var walker = new ParseTextWalker(text);
            var result = EffectBuffer.TryParse(walker);

            Assert.AreEqual(text, result.ToString());
        }
        #endregion
    }
}
